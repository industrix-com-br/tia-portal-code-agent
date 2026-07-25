using System.Linq;
using System.Windows;
using FluentAssertions;
using TiaAgent.AddIn.Ui;
using Xunit;

namespace TiaAgent.AddIn.Tests;

public class SimpleMarkdownFlowDocumentRendererTests
{
    private readonly SimpleMarkdownFlowDocumentRenderer _renderer = new();
    private static readonly string[] s_emojis = new[] { "🔴", "🟡", "🟢" };

    [Fact]
    public void Render_ReturnsNullForEmptyInput()
    {
        _renderer.Render("").Should().BeNull();
        _renderer.Render(null!).Should().BeNull();
        _renderer.Render("   ").Should().BeNull();
    }

    [Fact]
    public void Render_ProducesFlowDocumentForValidMarkdown()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var doc = _renderer.Render("# Hello\n\nSome text.");
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThan(0);
        });
    }

    [Fact]
    public void Render_HandlesHeadings()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var doc = _renderer.Render("# H1\n## H2\n### H3");
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(3);
        });
    }

    [Fact]
    public void Render_HandlesBoldAndItalic()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var doc = _renderer.Render("This is **bold** and *italic* text.");
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().Be(1);
        });
    }

    [Fact]
    public void Render_HandlesInlineCode()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var doc = _renderer.Render("Use `var x = 1;` in your code.");
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().Be(1);
        });
    }

    [Fact]
    public void Render_HandlesFencedCodeBlocks()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "```csharp\nvar x = 1;\nvar y = 2;\n```";
            var doc = _renderer.Render(md);
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(1);

            // Verify structured code block representation
            var textBox = FindCodeTextBox(doc!);
            textBox.Should().NotBeNull("fenced code blocks should render as BlockUIContainer with TextBox");

            textBox!.Text.Should().Be("var x = 1;\nvar y = 2;");
            textBox.TextWrapping.Should().Be(TextWrapping.NoWrap, "code blocks must not wrap");
            textBox.HorizontalScrollBarVisibility.Should().Be(System.Windows.Controls.ScrollBarVisibility.Auto,
                "code blocks must support horizontal scrolling");
            textBox.IsReadOnly.Should().BeTrue("code blocks must be read-only");
        });
    }

    [Fact]
    public void Render_HandlesUnorderedLists()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "- Item 1\n- Item 2\n- Item 3";
            var doc = _renderer.Render(md);
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(3);
        });
    }

    [Fact]
    public void Render_HandlesOrderedList()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "1. First\n2. Second\n3. Third";
            var doc = _renderer.Render(md);
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(3);
        });
    }

    [Fact]
    public void Render_HandlesHorizontalRule()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "Before\n\n---\n\nAfter";
            var doc = _renderer.Render(md);
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(3);
        });
    }

    [Fact]
    public void Render_HandlesTable()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "| Name | Value |\n|------|-------|\n| Foo  | 1     |\n| Bar  | 2     |";
            var doc = _renderer.Render(md);
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(1);
        });
    }

    [Fact]
    public void Render_HandlesMixedContent()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = @"# Title

Some paragraph with **bold** and *italic*.

- List item 1
- List item 2

```python
print('hello')
```

---

End.";
            var doc = _renderer.Render(md);
            doc.Should().NotBeNull();
#if SIEMENS
            doc!.Blocks.Count.Should().BeGreaterThan(5);
#else
            // Without SIEMENS, renderer falls back to plain text (1 block)
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(1);
#endif
        });
    }

    [Fact]
    public void Render_DoesNotThrowOnMalformedMarkdown()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var cases = new[]
            {
                "```\nunclosed code block",
                "**unclosed bold",
                "| incomplete | table",
                new string('x', 10000),
            };

            foreach (var md in cases)
            {
                var act = () => _renderer.Render(md);
                act.Should().NotThrow();
            }
        });
    }

    [Fact]
    public void Render_LargeResponse_DoesNotThrow()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine($"## Section {i}");
                sb.AppendLine();
                sb.AppendLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
                sb.AppendLine();
            }

            var act = () => _renderer.Render(sb.ToString());
            act.Should().NotThrow();
        });
    }

    [Fact]
    public void PlainTextFlowDocumentHelper_Create_ReturnsDocumentForTextInput()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var doc = PlainTextFlowDocumentHelper.Create("Hello World");
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().Be(1);
        });
    }

    [Fact]
    public void PlainTextFlowDocumentHelper_Create_HandlesEmptyString()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var doc = PlainTextFlowDocumentHelper.Create("");
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().Be(1);
        });
    }

    [Fact]
    public void PlainTextFlowDocumentHelper_CreateEmpty_ReturnsEmptyStateDocument()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var doc = PlainTextFlowDocumentHelper.CreateEmpty();
            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().Be(1);
        });
    }

    // ── Deterministic runtime test ──

    [Fact]
    public void Render_DeterministicRuntimeTest()
    {
        StaTestHelper.RunOnSta(() =>
        {
            // Fixed content from the requirements — verifies all major syntax elements
            // render correctly in a single document.
            var md = @"# Test title

This is **bold** and this is `inline code`.

- First item
- Second item";

            var doc = _renderer.Render(md);

            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(3,
                "heading + paragraph + 2 list items should produce at least 3 blocks");

            // Verify the heading block exists and has the right font size
            var heading = doc.Blocks.First() as System.Windows.Documents.Paragraph;
            heading.Should().NotBeNull();
            heading!.FontSize.Should().Be(20, "H1 headings should be 20pt");

            // Verify the paragraph contains bold and inline code runs
            var para = doc.Blocks.ElementAt(1) as System.Windows.Documents.Paragraph;
            para.Should().NotBeNull();
            var inlines = para!.Inlines.ToList();
            inlines.Count.Should().BeGreaterThanOrEqualTo(3,
                "paragraph should contain: literal 'This is ', bold 'bold', literal ' and this is ', code 'inline code', literal '.'");

            // Verify list items exist
            doc.Blocks.Count.Should().BeGreaterThanOrEqualTo(4,
                "document should have heading + paragraph + 2 list items");
        });
    }

    [Fact]
    public void Render_Emojis_InParagraphs_ArePreserved()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "Status: 🔴 Critical\nWarning: 🟡 Attention\nOK: 🟢 Normal";
            var doc = _renderer.Render(md);

            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().Be(3);

            // Verify each paragraph contains the emoji text
            foreach (var block in doc.Blocks)
            {
                var para = block as System.Windows.Documents.Paragraph;
                para.Should().NotBeNull();
                var text = string.Concat(para!.Inlines.Select(i => GetInlineText(i)));
                s_emojis.Should().Contain(e => text.Contains(e),
                    "paragraph should contain at least one of the expected emojis");
            }
        });
    }

    [Fact]
    public void Render_PortugueseAccents_ArePreserved()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "Ação — révisão\nMáquina: válvula, ç, ã, é, ê";
            var doc = _renderer.Render(md);

            doc.Should().NotBeNull();
            var fullText = string.Concat(doc!.Blocks.SelectMany(b =>
                b is System.Windows.Documents.Paragraph p
                    ? p.Inlines.Select(i => GetInlineText(i))
                    : System.Linq.Enumerable.Empty<string>()));
            fullText.Should().Contain("Ação");
            fullText.Should().Contain("révisão");
            fullText.Should().Contain("válvula");
        });
    }

#if SIEMENS
    [Fact]
    public void Render_FencedCodeBlock_UsesBlockUIContainer()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "```scl\nIF #ação THEN\n    #saída := TRUE;\nEND_IF;\n```";
            var doc = _renderer.Render(md);

            doc.Should().NotBeNull();
            var textBox = FindCodeTextBox(doc!);
            textBox.Should().NotBeNull("fenced code blocks should render as BlockUIContainer with TextBox");

            textBox!.Text.Should().Be("IF #ação THEN\n    #saída := TRUE;\nEND_IF;");
            textBox.TextWrapping.Should().Be(TextWrapping.NoWrap, "code blocks must not wrap");
            textBox.HorizontalScrollBarVisibility.Should().Be(System.Windows.Controls.ScrollBarVisibility.Auto,
                "code blocks must support horizontal scrolling");
            textBox.IsReadOnly.Should().BeTrue("code blocks must be read-only");
        });
    }

    [Fact]
    public void Render_FencedCodeBlock_PreservesIndentation()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "```\n    level1\n        level2\n            level3\n```";
            var doc = _renderer.Render(md);

            var textBox = FindCodeTextBox(doc!);
            textBox.Should().NotBeNull();

            const string expectedIndent = "    level1\n        level2\n            level3";
            textBox!.Text.Should().Be(expectedIndent,
                "indentation in code blocks must be preserved exactly");
        });
    }

    [Fact]
    public void Render_FencedCodeBlock_PreservesTabs()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "```\ncol1\tcol2\tcol3\n```";
            var doc = _renderer.Render(md);

            var textBox = FindCodeTextBox(doc!);
            textBox.Should().NotBeNull();

            const string expectedTabs = "col1\tcol2\tcol3";
            textBox!.Text.Should().Be(expectedTabs, "tabs in code blocks must be preserved");
        });
    }

    [Fact]
    public void Render_LongCodeLine_UsesNoWrapTextBox()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var longLine = new string('x', 500);
            var md = $"```\n{longLine}\n```";
            var doc = _renderer.Render(md);

            var textBox = FindCodeTextBox(doc!);
            textBox.Should().NotBeNull();

            textBox!.Text.Should().Be(longLine);
            textBox.TextWrapping.Should().Be(TextWrapping.NoWrap,
                "long code lines must use no-wrap to preserve visual alignment");
            textBox.HorizontalScrollBarVisibility.Should().Be(System.Windows.Controls.ScrollBarVisibility.Auto,
                "long code lines must support horizontal scrolling");
        });
    }
#endif

    [Fact]
    public void Render_DocumentFont_IncludesEmojiFallback()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "# Title 🔴\n\nSome text 🟡";
            var doc = _renderer.Render(md);

            doc.Should().NotBeNull();
            var fontFamily = doc!.FontFamily.Source;
            fontFamily.Should().Contain("Segoe UI", "document font should include Segoe UI");
            fontFamily.Should().Contain("Segoe UI Emoji", "document font should include Segoe UI Emoji fallback");
            fontFamily.Should().Contain("Segoe UI Symbol", "document font should include Segoe UI Symbol fallback");
        });
    }

#if SIEMENS
    [Fact]
    public void Render_RepresentativePayload_PreservesAllElements()
    {
        StaTestHelper.RunOnSta(() =>
        {
            var md = "# Análise 🔴\n\nO código contém uma **condição crítica**.\n\n" +
                     "- Estado: 🟡 Atenção\n- Saída: `Q0.0`\n\n" +
                     "```scl\nIF #ação THEN\n    #saída := TRUE; // 🟢\nEND_IF;\n```\n\n" +
                     "| Estado | Ícone |\n|--------|-------|\n| Alarme | 🔴 |";
            var doc = _renderer.Render(md);

            doc.Should().NotBeNull();
            doc!.Blocks.Count.Should().BeGreaterThanOrEqualTo(1,
                "representative payload should produce at least one block");

            // Verify code block exists with TextBox
            var textBox = FindCodeTextBox(doc);
            textBox.Should().NotBeNull("representative payload should contain a fenced code block");
            textBox!.Text.Should().Contain("IF #ação THEN");

            // Verify the document font includes emoji fallback
            doc.FontFamily.Source.Should().Contain("Segoe UI Emoji");
        });
    }
#endif

    /// <summary>
    /// Recursively searches for a TextBox inside a BlockUIContainer in the document.
    /// Code blocks are wrapped in Section > BlockUIContainer > TextBox.
    /// </summary>
    private static System.Windows.Controls.TextBox? FindCodeTextBox(System.Windows.Documents.FlowDocument doc)
    {
        return FindCodeTextBoxInBlocks(doc.Blocks);
    }

    private static System.Windows.Controls.TextBox? FindCodeTextBoxInBlocks(System.Windows.Documents.BlockCollection blocks)
    {
        foreach (var block in blocks)
        {
            if (block is System.Windows.Documents.BlockUIContainer bui &&
                bui.Child is System.Windows.Controls.TextBox tb)
                return tb;

            if (block is System.Windows.Documents.Section section)
            {
                var found = FindCodeTextBoxInBlocks(section.Blocks);
                if (found != null) return found;
            }
        }
        return null;
    }

    /// <summary>
    /// Extracts text from a WPF Inline element for assertions.
    /// </summary>
    private static string GetInlineText(System.Windows.Documents.Inline inline)
    {
        return inline switch
        {
            System.Windows.Documents.Run r => r.Text,
            System.Windows.Documents.Span s => string.Concat(s.Inlines.Select(i => GetInlineText(i))),
            _ => ""
        };
    }
}
