using System;
using System.Windows;
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

    private static readonly SolidColorBrush HeaderBrush = CreateFrozenBrush(0x1A, 0x1A, 0x1A);
    private static readonly SolidColorBrush CodeBackground = CreateFrozenBrush(0xF5, 0xF5, 0xF5);
    private static readonly SolidColorBrush CodeBorder = CreateFrozenBrush(0xE5, 0xE5, 0xE5);
    private static readonly SolidColorBrush InlineCodeBackground = CreateFrozenBrush(0xF0, 0xF0, 0xF0);
    private static readonly SolidColorBrush BlockquoteBrush = CreateFrozenBrush(0x66, 0x66, 0x66);
    private static readonly SolidColorBrush TableBorderBrush = CreateFrozenBrush(0xE5, 0xE5, 0xE5);
    private static readonly SolidColorBrush TableHeaderBrush = CreateFrozenBrush(0xF5, 0xF5, 0xF5);

    /// <summary>
    /// Converts Markdown into a selectable WPF document.
    /// Returns <see langword="null"/> for empty input or when rendering fails.
    /// </summary>
    public static WpfDocuments.FlowDocument? Render(string markdown)
    {
        if (string.IsNullOrWhiteSpace(markdown))
            return null;

        try
        {
            var parsedDocument = Markdown.Parse(markdown, s_pipeline);
            var flowDocument = CreateDocument("Segoe UI", 13);

            foreach (var block in parsedDocument)
                RenderBlock(flowDocument.Blocks, block);

            return flowDocument;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Creates a selectable plain-text document used when Markdown rendering fails.
    /// </summary>
    public static WpfDocuments.FlowDocument CreatePlainTextFallback(string text)
    {
        var flowDocument = CreateDocument("Consolas", 12);
        flowDocument.Blocks.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(text ?? string.Empty)));
        return flowDocument;
    }

    private static WpfDocuments.FlowDocument CreateDocument(string fontFamily, double fontSize)
    {
        return new WpfDocuments.FlowDocument
        {
            FontFamily = new FontFamily(fontFamily),
            FontSize = fontSize,
            PagePadding = new Thickness(8)
        };
    }

    private static void RenderBlock(WpfDocuments.BlockCollection target, Markdig.Syntax.Block block)
    {
        switch (block)
        {
            case HeadingBlock heading:
                target.Add(CreateHeading(heading));
                break;

            case ParagraphBlock paragraph:
                target.Add(CreateParagraph(paragraph));
                break;

            case ListBlock list:
                RenderList(target, list, depth: 0);
                break;

            case FencedCodeBlock fencedCode:
                AddCodeBlock(target, fencedCode);
                break;

            case CodeBlock codeBlock:
                AddCodeBlock(target, codeBlock);
                break;

            case QuoteBlock quote:
                target.Add(CreateQuoteBlock(quote));
                break;

            case ThematicBreakBlock:
                target.Add(CreateHorizontalRule());
                break;

            case Table table:
                target.Add(CreateTable(table));
                break;

            case ContainerBlock container:
                foreach (var child in container)
                    RenderBlock(target, child);
                break;

            default:
                var text = block.ToString();
                if (!string.IsNullOrWhiteSpace(text))
                    target.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(text)));
                break;
        }
    }

    private static WpfDocuments.Paragraph CreateHeading(HeadingBlock heading)
    {
        var paragraph = new WpfDocuments.Paragraph
        {
            Foreground = HeaderBrush,
            FontWeight = FontWeights.SemiBold,
            FontSize = heading.Level switch
            {
                1 => 20,
                2 => 17,
                3 => 15,
                _ => 14
            },
            Margin = new Thickness(0, heading.Level == 1 ? 12 : 8, 0, 4)
        };

        RenderInlineChildren(paragraph.Inlines, heading.Inline);
        return paragraph;
    }

    private static WpfDocuments.Paragraph CreateParagraph(ParagraphBlock paragraphBlock)
    {
        var paragraph = new WpfDocuments.Paragraph
        {
            Margin = new Thickness(0, 0, 0, 8)
        };

        RenderInlineChildren(paragraph.Inlines, paragraphBlock.Inline);
        return paragraph;
    }

    private static void RenderList(WpfDocuments.BlockCollection target, ListBlock list, int depth)
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
                    {
                        var paragraph = new WpfDocuments.Paragraph
                        {
                            Margin = new Thickness(16 + depth * 16, 0, 0, 4)
                        };

                        paragraph.Inlines.Add(new WpfDocuments.Run(list.IsOrdered ? $"{index}. " : "• ")
                        {
                            Foreground = Brushes.Gray
                        });
                        RenderInlineChildren(paragraph.Inlines, paragraphBlock.Inline);
                        target.Add(paragraph);
                        break;
                    }

                    case ListBlock nestedList:
                        RenderList(target, nestedList, depth + 1);
                        break;

                    default:
                        RenderBlock(target, child);
                        break;
                }
            }

            index++;
        }
    }

    private static void AddCodeBlock(WpfDocuments.BlockCollection target, CodeBlock codeBlock)
    {
        var code = codeBlock.Lines.ToString().TrimEnd();
        if (string.IsNullOrEmpty(code))
            return;

        var section = new WpfDocuments.Section
        {
            Background = CodeBackground,
            BorderBrush = CodeBorder,
            BorderThickness = new Thickness(1),
            Margin = new Thickness(0, 4, 0, 8),
            Padding = new Thickness(8)
        };

        if (codeBlock is FencedCodeBlock fencedCode && !string.IsNullOrWhiteSpace(fencedCode.Info))
        {
            section.Blocks.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(fencedCode.Info.Trim()))
            {
                FontSize = 10,
                Foreground = Brushes.Gray,
                Margin = new Thickness(0, 0, 0, 4)
            });
        }

        section.Blocks.Add(new WpfDocuments.Paragraph(new WpfDocuments.Run(code))
        {
            FontFamily = new FontFamily("Consolas"),
            FontSize = 12,
            Margin = new Thickness(0)
        });

        target.Add(section);
    }

    private static WpfDocuments.Section CreateQuoteBlock(QuoteBlock quote)
    {
        var section = new WpfDocuments.Section
        {
            BorderBrush = BlockquoteBrush,
            BorderThickness = new Thickness(3, 0, 0, 0),
            Padding = new Thickness(8, 0, 0, 0),
            Margin = new Thickness(0, 4, 0, 8)
        };

        foreach (var child in quote)
        {
            if (child is ParagraphBlock paragraphBlock)
            {
                var paragraph = CreateParagraph(paragraphBlock);
                paragraph.Foreground = BlockquoteBrush;
                paragraph.FontStyle = FontStyles.Italic;
                paragraph.Margin = new Thickness(0, 0, 0, 4);
                section.Blocks.Add(paragraph);
            }
            else
            {
                RenderBlock(section.Blocks, child);
            }
        }

        return section;
    }

    private static WpfDocuments.Paragraph CreateHorizontalRule()
    {
        return new WpfDocuments.Paragraph
        {
            Margin = new Thickness(0, 8, 0, 8),
            BorderBrush = Brushes.LightGray,
            BorderThickness = new Thickness(0, 1, 0, 0),
            Padding = new Thickness(0, 8, 0, 0)
        };
    }

    private static WpfDocuments.Table CreateTable(Table table)
    {
        var wpfTable = new WpfDocuments.Table
        {
            BorderBrush = TableBorderBrush,
            BorderThickness = new Thickness(1),
            CellSpacing = 0,
            Margin = new Thickness(0, 4, 0, 8)
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

        foreach (var row in table)
        {
            if (row is not TableRow tableRow)
                continue;

            var wpfRow = new WpfDocuments.TableRow();

            foreach (var cell in tableRow)
            {
                if (cell is not TableCell tableCell)
                    continue;

                var wpfCell = new WpfDocuments.TableCell
                {
                    BorderBrush = TableBorderBrush,
                    BorderThickness = new Thickness(0, 0, 1, 1),
                    Padding = new Thickness(6),
                    Background = tableRow.IsHeader ? TableHeaderBrush : Brushes.Transparent
                };

                foreach (var child in tableCell)
                {
                    if (child is ParagraphBlock paragraphBlock)
                    {
                        var paragraph = CreateParagraph(paragraphBlock);
                        paragraph.Margin = new Thickness(0);
                        paragraph.FontWeight = tableRow.IsHeader ? FontWeights.SemiBold : FontWeights.Normal;
                        wpfCell.Blocks.Add(paragraph);
                    }
                    else
                    {
                        RenderBlock(wpfCell.Blocks, child);
                    }
                }

                wpfRow.Cells.Add(wpfCell);
            }

            rowGroup.Rows.Add(wpfRow);
        }

        wpfTable.RowGroups.Add(rowGroup);
        return wpfTable;
    }

    private static void RenderInlineChildren(WpfDocuments.InlineCollection target, ContainerInline? container)
    {
        if (container == null)
            return;

        foreach (var inline in container)
            target.Add(CreateInline(inline));
    }

    private static WpfDocuments.Inline CreateInline(Markdig.Syntax.Inlines.Inline inline)
    {
        switch (inline)
        {
            case LiteralInline literal:
                return new WpfDocuments.Run(literal.Content.ToString());

            case EmphasisInline emphasis:
            {
                var span = new WpfDocuments.Span();
                RenderInlineChildren(span.Inlines, emphasis);

                if (emphasis.DelimiterCount >= 2)
                    span.FontWeight = FontWeights.Bold;
                else
                    span.FontStyle = FontStyles.Italic;

                return span;
            }

            case CodeInline code:
                return new WpfDocuments.Run(code.Content)
                {
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = 12,
                    Background = InlineCodeBackground,
                    BaselineAlignment = BaselineAlignment.Center
                };

            case LinkInline link:
            {
                var span = new WpfDocuments.Span
                {
                    Foreground = HeaderBrush,
                    TextDecorations = TextDecorations.Underline,
                    ToolTip = link.Url
                };
                RenderInlineChildren(span.Inlines, link);
                return span;
            }

            case LineBreakInline:
                return new WpfDocuments.LineBreak();

            case HtmlInline html:
                return new WpfDocuments.Run(html.Tag);

            case ContainerInline container:
            {
                var span = new WpfDocuments.Span();
                RenderInlineChildren(span.Inlines, container);
                return span;
            }

            default:
                return new WpfDocuments.Run(inline.ToString() ?? string.Empty);
        }
    }

    private static SolidColorBrush CreateFrozenBrush(byte red, byte green, byte blue)
    {
        var brush = new SolidColorBrush(Color.FromRgb(red, green, blue));
        brush.Freeze();
        return brush;
    }
}
