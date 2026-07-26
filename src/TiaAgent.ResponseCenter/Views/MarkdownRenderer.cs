using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Markdig;
using Markdig.Extensions.Tables;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using WpfDocuments = System.Windows.Documents;

namespace TiaAgent.ResponseCenter.Views;

/// <summary>
/// Renders Markdown content into a WPF <see cref="WpfDocuments.FlowDocument"/>.
/// Markdig is used only for parsing; WPF elements are created explicitly so that
/// untrusted agent output cannot inject executable XAML or HTML.
/// </summary>
public static class MarkdownRenderer
{
    private static readonly MarkdownPipeline s_pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .Build();

    public static WpfDocuments.FlowDocument? Render(string markdown) => Render(markdown, null);

    /// <summary>
    /// Converts Markdown using semantic resources resolved from the supplied WPF element.
    /// Missing resources always fall back to safe built-in values.
    /// </summary>
    public static WpfDocuments.FlowDocument? Render(string markdown, FrameworkElement? resourceOwner)
    {
        if (string.IsNullOrWhiteSpace(markdown))
            return null;

        try
        {
            var theme = MarkdownTheme.From(resourceOwner);
            var parsedDocument = Markdown.Parse(markdown, s_pipeline);
            var flowDocument = CreateDocument(theme, useMonospaceFont: false);

            foreach (var block in parsedDocument)
                RenderBlock(flowDocument.Blocks, block, theme);

            return flowDocument;
        }
        catch
        {
            return null;
        }
    }

    public static WpfDocuments.FlowDocument CreatePlainTextFallback(string text) =>
        CreatePlainTextFallback(text, null);

    public static WpfDocuments.FlowDocument CreatePlainTextFallback(
        string text,
        FrameworkElement? resourceOwner)
    {
        var theme = MarkdownTheme.From(resourceOwner);
        var flowDocument = CreateDocument(theme, useMonospaceFont: true);
        flowDocument.Blocks.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(text ?? string.Empty))
        {
            Margin = theme.ParagraphMargin,
            Foreground = theme.BodyBrush
        });
        return flowDocument;
    }

    private static WpfDocuments.FlowDocument CreateDocument(
        MarkdownTheme theme,
        bool useMonospaceFont)
    {
        return new WpfDocuments.FlowDocument
        {
            FontFamily = useMonospaceFont ? theme.MonospaceFontFamily : theme.UiFontFamily,
            FontSize = useMonospaceFont ? theme.CodeFontSize : theme.BodyFontSize,
            Foreground = theme.BodyBrush,
            PagePadding = theme.DocumentPadding,
            LineHeight = theme.LineHeight,
            LineStackingStrategy = LineStackingStrategy.BlockLineHeight
        };
    }

    private static void RenderBlock(
        WpfDocuments.BlockCollection target,
        Markdig.Syntax.Block block,
        MarkdownTheme theme)
    {
        switch (block)
        {
            case HeadingBlock heading:
                target.Add(CreateHeading(heading, theme));
                break;
            case ParagraphBlock paragraph:
                target.Add(CreateParagraph(paragraph, theme));
                break;
            case ListBlock list:
                RenderList(target, list, depth: 0, theme);
                break;
            case FencedCodeBlock fencedCode:
                AddCodeBlock(target, fencedCode, theme);
                break;
            case CodeBlock codeBlock:
                AddCodeBlock(target, codeBlock, theme);
                break;
            case QuoteBlock quote:
                target.Add(CreateQuoteBlock(quote, theme));
                break;
            case ThematicBreakBlock:
                target.Add(CreateHorizontalRule(theme));
                break;
            case Table table:
                target.Add(CreateTable(table, theme));
                break;
            case ContainerBlock container:
                foreach (var child in container)
                    RenderBlock(target, child, theme);
                break;
            default:
                var text = block.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    target.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(text))
                    {
                        Foreground = theme.BodyBrush,
                        Margin = theme.ParagraphMargin
                    });
                }
                break;
        }
    }

    private static WpfDocuments.Paragraph CreateHeading(
        HeadingBlock heading,
        MarkdownTheme theme)
    {
        var paragraph = new WpfDocuments.Paragraph
        {
            Foreground = theme.HeadingBrush,
            FontWeight = FontWeights.SemiBold,
            FontSize = heading.Level switch
            {
                1 => theme.H1FontSize,
                2 => theme.H2FontSize,
                3 => theme.H3FontSize,
                _ => theme.BodyFontSize
            },
            Margin = new Thickness(0, heading.Level == 1 ? 12 : 9, 0, 5),
            KeepWithNext = true
        };

        if (heading.Level == 1)
        {
            paragraph.BorderBrush = theme.SubtleBorderBrush;
            paragraph.BorderThickness = new Thickness(0, 0, 0, 1);
            paragraph.Padding = new Thickness(0, 0, 0, 5);
        }

        RenderInlineChildren(paragraph.Inlines, heading.Inline, theme);
        return paragraph;
    }

    private static WpfDocuments.Paragraph CreateParagraph(
        ParagraphBlock paragraphBlock,
        MarkdownTheme theme)
    {
        var paragraph = new WpfDocuments.Paragraph
        {
            Foreground = theme.BodyBrush,
            Margin = theme.ParagraphMargin
        };
        RenderInlineChildren(paragraph.Inlines, paragraphBlock.Inline, theme);
        return paragraph;
    }

    private static void RenderList(
        WpfDocuments.BlockCollection target,
        ListBlock list,
        int depth,
        MarkdownTheme theme)
    {
        var index = 1;
        foreach (var item in list)
        {
            if (item is not ListItemBlock listItem)
                continue;

            foreach (var child in listItem)
            {
                switch (child)
                {
                    case ParagraphBlock paragraphBlock:
                        var paragraph = new WpfDocuments.Paragraph
                        {
                            Foreground = theme.BodyBrush,
                            Margin = new Thickness(18 + depth * 18, 0, 0, 5)
                        };
                        paragraph.Inlines.Add(new WpfDocuments.Run(list.IsOrdered ? $"{index}. " : "• ")
                        {
                            Foreground = theme.BulletBrush,
                            FontWeight = FontWeights.SemiBold
                        });
                        RenderInlineChildren(paragraph.Inlines, paragraphBlock.Inline, theme);
                        target.Add(paragraph);
                        break;
                    case ListBlock nestedList:
                        RenderList(target, nestedList, depth + 1, theme);
                        break;
                    default:
                        RenderBlock(target, child, theme);
                        break;
                }
            }
            index++;
        }
    }

    private static void AddCodeBlock(
        WpfDocuments.BlockCollection target,
        CodeBlock codeBlock,
        MarkdownTheme theme)
    {
        var code = codeBlock.Lines.ToString().TrimEnd();
        if (string.IsNullOrEmpty(code))
            return;

        var section = new WpfDocuments.Section
        {
            Background = theme.CodeBackgroundBrush,
            BorderBrush = theme.CodeBorderBrush,
            BorderThickness = new Thickness(1),
            Margin = theme.CodeMargin,
            Padding = theme.CodePadding
        };

        if (codeBlock is FencedCodeBlock fencedCode && !string.IsNullOrWhiteSpace(fencedCode.Info))
        {
            section.Blocks.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(fencedCode.Info.Trim()))
            {
                FontFamily = theme.UiFontFamily,
                FontSize = theme.SmallFontSize,
                FontWeight = FontWeights.SemiBold,
                Foreground = theme.SecondaryBrush,
                Margin = new Thickness(0, 0, 0, 5)
            });
        }

        section.Blocks.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(code))
        {
            FontFamily = theme.MonospaceFontFamily,
            FontSize = theme.CodeFontSize,
            Foreground = theme.CodeForegroundBrush,
            LineHeight = Math.Max(theme.CodeFontSize * 1.45, 17),
            Margin = new Thickness(0)
        });
        target.Add(section);
    }

    private static WpfDocuments.Section CreateQuoteBlock(QuoteBlock quote, MarkdownTheme theme)
    {
        var section = new WpfDocuments.Section
        {
            Background = theme.BlockquoteBackgroundBrush,
            BorderBrush = theme.BlockquoteBorderBrush,
            BorderThickness = new Thickness(3, 0, 0, 0),
            Padding = theme.QuotePadding,
            Margin = new Thickness(0, 4, 0, 10)
        };

        foreach (var child in quote)
        {
            if (child is ParagraphBlock paragraphBlock)
            {
                var paragraph = CreateParagraph(paragraphBlock, theme);
                paragraph.Foreground = theme.SecondaryBrush;
                paragraph.Margin = new Thickness(0, 0, 0, 4);
                section.Blocks.Add(paragraph);
            }
            else
            {
                RenderBlock(section.Blocks, child, theme);
            }
        }
        return section;
    }

    private static WpfDocuments.Paragraph CreateHorizontalRule(MarkdownTheme theme)
    {
        return new WpfDocuments.Paragraph
        {
            Margin = new Thickness(0, 8, 0, 10),
            BorderBrush = theme.SubtleBorderBrush,
            BorderThickness = new Thickness(0, 1, 0, 0),
            Padding = new Thickness(0, 6, 0, 0)
        };
    }

    private static WpfDocuments.Table CreateTable(Table table, MarkdownTheme theme)
    {
        var wpfTable = new WpfDocuments.Table
        {
            BorderBrush = theme.TableBorderBrush,
            BorderThickness = new Thickness(1),
            CellSpacing = 0,
            Margin = new Thickness(0, 4, 0, 10)
        };

        var columnCount = table.ColumnDefinitions?.Count ?? 0;
        if (columnCount == 0)
        {
            foreach (var row in table)
            {
                if (row is TableRow tableRow)
                {
                    columnCount = tableRow.Count;
                    break;
                }
            }
        }

        for (var index = 0; index < Math.Max(columnCount, 1); index++)
            wpfTable.Columns.Add(new WpfDocuments.TableColumn());

        var rowGroup = new WpfDocuments.TableRowGroup();
        var rowIndex = 0;
        foreach (var row in table)
        {
            if (row is not TableRow tableRow)
                continue;

            var wpfRow = new WpfDocuments.TableRow();
            var rowBackground = tableRow.IsHeader
                ? theme.TableHeaderBrush
                : rowIndex % 2 == 1
                    ? theme.TableAlternateBrush
                    : Brushes.Transparent;

            foreach (var cell in tableRow)
            {
                if (cell is not TableCell tableCell)
                    continue;

                var wpfCell = new WpfDocuments.TableCell
                {
                    BorderBrush = theme.TableBorderBrush,
                    BorderThickness = new Thickness(0, 0, 1, 1),
                    Padding = theme.TableCellPadding,
                    Background = rowBackground
                };

                foreach (var child in tableCell)
                {
                    if (child is ParagraphBlock paragraphBlock)
                    {
                        var paragraph = CreateParagraph(paragraphBlock, theme);
                        paragraph.Margin = new Thickness(0);
                        paragraph.FontWeight = tableRow.IsHeader ? FontWeights.SemiBold : FontWeights.Normal;
                        wpfCell.Blocks.Add(paragraph);
                    }
                    else
                    {
                        RenderBlock(wpfCell.Blocks, child, theme);
                    }
                }
                wpfRow.Cells.Add(wpfCell);
            }

            rowGroup.Rows.Add(wpfRow);
            rowIndex++;
        }
        wpfTable.RowGroups.Add(rowGroup);
        return wpfTable;
    }

    private static void RenderInlineChildren(
        WpfDocuments.InlineCollection target,
        ContainerInline? container,
        MarkdownTheme theme)
    {
        if (container == null)
            return;
        foreach (var inline in container)
            target.Add(CreateInline(inline, theme));
    }

    private static WpfDocuments.Inline CreateInline(
        Markdig.Syntax.Inlines.Inline inline,
        MarkdownTheme theme)
    {
        switch (inline)
        {
            case LiteralInline literal:
                return new WpfDocuments.Run(literal.Content.ToString());
            case EmphasisInline emphasis:
                var span = new WpfDocuments.Span();
                RenderInlineChildren(span.Inlines, emphasis, theme);
                if (emphasis.DelimiterCount >= 2)
                    span.FontWeight = FontWeights.Bold;
                else
                    span.FontStyle = FontStyles.Italic;
                return span;
            case CodeInline code:
                return new WpfDocuments.Run(code.Content)
                {
                    FontFamily = theme.MonospaceFontFamily,
                    FontSize = theme.CodeFontSize,
                    Foreground = theme.CodeForegroundBrush,
                    Background = theme.CodeBackgroundBrush,
                    BaselineAlignment = BaselineAlignment.Center
                };
            case LinkInline link:
                var hyperlink = new WpfDocuments.Hyperlink
                {
                    Foreground = theme.LinkBrush,
                    TextDecorations = TextDecorations.Underline,
                    Cursor = Cursors.Hand,
                    ToolTip = link.Url
                };
                if (Uri.TryCreate(link.Url, UriKind.Absolute, out var navigateUri))
                    hyperlink.NavigateUri = navigateUri;
                hyperlink.MouseEnter += (_, _) => hyperlink.Foreground = theme.AssistantAccentBrush;
                hyperlink.MouseLeave += (_, _) => hyperlink.Foreground = theme.LinkBrush;
                RenderInlineChildren(hyperlink.Inlines, link, theme);
                return hyperlink;
            case LineBreakInline:
                return new WpfDocuments.LineBreak();
            case HtmlInline html:
                return new WpfDocuments.Run(html.Tag);
            case ContainerInline container:
                var containerSpan = new WpfDocuments.Span();
                RenderInlineChildren(containerSpan.Inlines, container, theme);
                return containerSpan;
            default:
                return new WpfDocuments.Run(inline.ToString() ?? string.Empty);
        }
    }

    private sealed class MarkdownTheme
    {
        private static readonly Brush DefaultPrimary = CreateFrozenBrush(0x20, 0x21, 0x24);
        private static readonly Brush DefaultSecondary = CreateFrozenBrush(0x60, 0x65, 0x6D);
        private static readonly Brush DefaultHeading = CreateFrozenBrush(0x34, 0x44, 0x5A);
        private static readonly Brush DefaultBorder = CreateFrozenBrush(0xD7, 0xDA, 0xDF);
        private static readonly Brush DefaultAccent = CreateFrozenBrush(0xF2, 0xA0, 0x00);
        private static readonly Brush DefaultAssistantAccent = CreateFrozenBrush(0x00, 0x8F, 0x8C);
        private static readonly Brush DefaultCodeBackground = CreateFrozenBrush(0xF5, 0xF6, 0xF7);
        private static readonly Brush DefaultCodeBorder = CreateFrozenBrush(0xD5, 0xD8, 0xDC);
        private static readonly Brush DefaultCodeForeground = CreateFrozenBrush(0x1F, 0x23, 0x28);
        private static readonly Brush DefaultQuoteBackground = CreateFrozenBrush(0xF7, 0xF8, 0xF9);
        private static readonly Brush DefaultTableHeader = CreateFrozenBrush(0xE9, 0xED, 0xF1);
        private static readonly Brush DefaultTableAlternate = CreateFrozenBrush(0xFA, 0xFB, 0xFC);
        private static readonly Brush DefaultLink = CreateFrozenBrush(0x00, 0x6E, 0x78);

        public FontFamily UiFontFamily { get; private init; } = new("Segoe UI");
        public FontFamily MonospaceFontFamily { get; private init; } = new("Consolas");
        public Brush BodyBrush { get; private init; } = DefaultPrimary;
        public Brush SecondaryBrush { get; private init; } = DefaultSecondary;
        public Brush HeadingBrush { get; private init; } = DefaultHeading;
        public Brush BulletBrush { get; private init; } = DefaultAssistantAccent;
        public Brush SubtleBorderBrush { get; private init; } = DefaultBorder;
        public Brush CodeBackgroundBrush { get; private init; } = DefaultCodeBackground;
        public Brush CodeBorderBrush { get; private init; } = DefaultCodeBorder;
        public Brush CodeForegroundBrush { get; private init; } = DefaultCodeForeground;
        public Brush BlockquoteBackgroundBrush { get; private init; } = DefaultQuoteBackground;
        public Brush BlockquoteBorderBrush { get; private init; } = DefaultAccent;
        public Brush TableBorderBrush { get; private init; } = DefaultBorder;
        public Brush TableHeaderBrush { get; private init; } = DefaultTableHeader;
        public Brush TableAlternateBrush { get; private init; } = DefaultTableAlternate;
        public Brush LinkBrush { get; private init; } = DefaultLink;
        public Brush AssistantAccentBrush { get; private init; } = DefaultAssistantAccent;
        public double BodyFontSize { get; private init; } = 13.5;
        public double SmallFontSize { get; private init; } = 11;
        public double CodeFontSize { get; private init; } = 12.5;
        public double H1FontSize { get; private init; } = 18;
        public double H2FontSize { get; private init; } = 16;
        public double H3FontSize { get; private init; } = 14.5;
        public double LineHeight { get; private init; } = 20;
        public Thickness DocumentPadding { get; private init; } = new(20, 18, 20, 24);
        public Thickness ParagraphMargin { get; private init; } = new(0, 0, 0, 9);
        public Thickness CodePadding { get; private init; } = new(10, 8, 10, 8);
        public Thickness CodeMargin { get; private init; } = new(0, 4, 0, 10);
        public Thickness QuotePadding { get; private init; } = new(10, 6, 10, 4);
        public Thickness TableCellPadding { get; private init; } = new(7, 5, 7, 5);

        public static MarkdownTheme From(FrameworkElement? owner)
        {
            return new MarkdownTheme
            {
                UiFontFamily = Find(owner, "FontFamily.Ui", new FontFamily("Segoe UI")),
                MonospaceFontFamily = Find(owner, "FontFamily.Monospace", new FontFamily("Consolas")),
                BodyBrush = Find(owner, "Brush.PrimaryText", DefaultPrimary),
                SecondaryBrush = Find(owner, "Brush.SecondaryText", DefaultSecondary),
                HeadingBrush = Find(owner, "Brush.HeaderBackground", DefaultHeading),
                BulletBrush = Find(owner, "Brush.AssistantAccent", DefaultAssistantAccent),
                SubtleBorderBrush = Find(owner, "Brush.SubtleBorder", DefaultBorder),
                CodeBackgroundBrush = Find(owner, "Brush.CodeBackground", DefaultCodeBackground),
                CodeBorderBrush = Find(owner, "Brush.CodeBorder", DefaultCodeBorder),
                CodeForegroundBrush = Find(owner, "Brush.CodeForeground", DefaultCodeForeground),
                BlockquoteBackgroundBrush = Find(owner, "Brush.BlockquoteBackground", DefaultQuoteBackground),
                BlockquoteBorderBrush = Find(owner, "Brush.TiaAccent", DefaultAccent),
                TableBorderBrush = Find(owner, "Brush.SubtleBorder", DefaultBorder),
                TableHeaderBrush = Find(owner, "Brush.TableHeaderBackground", DefaultTableHeader),
                TableAlternateBrush = Find(owner, "Brush.TableAlternateBackground", DefaultTableAlternate),
                LinkBrush = Find(owner, "Brush.Link", DefaultLink),
                AssistantAccentBrush = Find(owner, "Brush.AssistantAccent", DefaultAssistantAccent),
                BodyFontSize = Find(owner, "Markdown.BodyFontSize", 13.5),
                SmallFontSize = Find(owner, "Markdown.SmallFontSize", 11d),
                CodeFontSize = Find(owner, "Markdown.CodeFontSize", 12.5),
                H1FontSize = Find(owner, "Markdown.H1FontSize", 18d),
                H2FontSize = Find(owner, "Markdown.H2FontSize", 16d),
                H3FontSize = Find(owner, "Markdown.H3FontSize", 14.5),
                LineHeight = Find(owner, "Markdown.LineHeight", 20d),
                DocumentPadding = Find(owner, "Markdown.DocumentPadding", new Thickness(20, 18, 20, 24)),
                ParagraphMargin = Find(owner, "Markdown.ParagraphMargin", new Thickness(0, 0, 0, 9)),
                CodePadding = Find(owner, "Markdown.CodePadding", new Thickness(10, 8, 10, 8)),
                CodeMargin = Find(owner, "Markdown.CodeMargin", new Thickness(0, 4, 0, 10)),
                QuotePadding = Find(owner, "Markdown.QuotePadding", new Thickness(10, 6, 10, 4)),
                TableCellPadding = Find(owner, "Markdown.TableCellPadding", new Thickness(7, 5, 7, 5))
            };
        }

        private static T Find<T>(FrameworkElement? owner, string key, T fallback)
        {
            try
            {
                return owner?.TryFindResource(key) is T value ? value : fallback;
            }
            catch
            {
                return fallback;
            }
        }
    }

    private static SolidColorBrush CreateFrozenBrush(byte red, byte green, byte blue)
    {
        var brush = new SolidColorBrush(Color.FromRgb(red, green, blue));
        brush.Freeze();
        return brush;
    }
}
