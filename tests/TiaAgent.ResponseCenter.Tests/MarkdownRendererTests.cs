using System.Windows.Documents;
using FluentAssertions;
using TiaAgent.ResponseCenter.Views;
using Xunit;

namespace TiaAgent.ResponseCenter.Tests;

public class MarkdownRendererTests
{
    [Fact]
    public void Render_ReturnsNullForEmptyInput()
    {
        MarkdownRenderer.Render("").Should().BeNull();
        MarkdownRenderer.Render(null!).Should().BeNull();
        MarkdownRenderer.Render("   ").Should().BeNull();
    }

    [Fact]
    public void Render_ProducesFlowDocumentForValidMarkdown()
    {
        var document = MarkdownRenderer.Render("# Hello\n\nSome text.");

        document.Should().NotBeNull();
        document!.Blocks.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Render_PreservesEmphasisAndLinkText()
    {
        var document = MarkdownRenderer.Render(
            "Normal **bold text**, *italic text*, and [link text](https://example.com).");

        document.Should().NotBeNull();
        var renderedText = GetText(document!);

        renderedText.Should().Contain("Normal");
        renderedText.Should().Contain("bold text");
        renderedText.Should().Contain("italic text");
        renderedText.Should().Contain("link text");
    }

    [Fact]
    public void Render_HandlesHeadings()
    {
        var document = MarkdownRenderer.Render("# H1\n## H2\n### H3");

        document.Should().NotBeNull();
        document!.Blocks.Count.Should().BeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public void Render_HandlesCodeBlocksWithoutFakeCopyAction()
    {
        var markdown = "```csharp\nvar x = 1;\n```";
        var document = MarkdownRenderer.Render(markdown);

        document.Should().NotBeNull();
        document!.Blocks.Count.Should().BeGreaterThanOrEqualTo(1);
        GetText(document).Should().Contain("var x = 1;").And.NotContain("Copy");
    }

    [Fact]
    public void Render_HandlesBulletLists()
    {
        var markdown = "- Item 1\n- Item 2\n- Item 3";
        var document = MarkdownRenderer.Render(markdown);

        document.Should().NotBeNull();
        document!.Blocks.Count.Should().BeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public void Render_HandlesTables()
    {
        var markdown = "| Col1 | Col2 |\n|------|------|\n| A | B |";
        var document = MarkdownRenderer.Render(markdown);

        document.Should().NotBeNull();
        document!.Blocks.Count.Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void Render_HandlesBlockquotes()
    {
        var document = MarkdownRenderer.Render("> This is a quote");

        document.Should().NotBeNull();
        document!.Blocks.Count.Should().Be(1);
    }

    [Fact]
    public void Render_HandlesMixedContent()
    {
        var markdown = @"# Title

Some paragraph with **bold** and *italic*.

- List item 1
- List item 2

```python
print('hello')
```

> A blockquote

---

End.";
        var document = MarkdownRenderer.Render(markdown);

        document.Should().NotBeNull();
        document!.Blocks.Count.Should().BeGreaterThan(5);
    }

    [Fact]
    public void Render_DoesNotThrowOnMalformedMarkdown()
    {
        var cases = new[]
        {
            "```\nunclosed code block",
            "[broken link](",
            "![",
            "**unclosed bold",
            "| incomplete | table",
            new string('x', 10000),
        };

        foreach (var markdown in cases)
        {
            var act = () => MarkdownRenderer.Render(markdown);
            act.Should().NotThrow();
        }
    }

    [Fact]
    public void Render_HeadingDoesNotEmitLinkReferenceDefinitionText()
    {
        var document = MarkdownRenderer.Render("# Heading 1\n## Heading 2\n### Heading 3");
        
        document.Should().NotBeNull();
        var text = GetText(document!);
        
        text.Should().NotContain("LinkReferenceDefinition");
        text.Should().NotContain("HeadingLinkReferenceDefinition");
    }

    [Fact]
    public void Render_DoesNotContainMarkdigTypeNames()
    {
        var markdown = @"# Title

Some paragraph with **bold** and *italic*.

- List item 1
- List item 2

```python
print('hello')
```

> A blockquote

---

End.";
        var document = MarkdownRenderer.Render(markdown);
        
        document.Should().NotBeNull();
        var text = GetText(document!);
        
        text.Should().NotContain("Markdig.");
        text.Should().NotContain("AutoIdentifiers");
    }

    [Fact]
    public void Render_HeadingsAndContentRemainPresent()
    {
        var document = MarkdownRenderer.Render("# Heading 1\n## Heading 2\n### Heading 3\n\nParagraph text here.");
        
        document.Should().NotBeNull();
        var text = GetText(document!);
        
        text.Should().Contain("Heading 1");
        text.Should().Contain("Heading 2");
        text.Should().Contain("Heading 3");
        text.Should().Contain("Paragraph text here.");
    }

    [Fact]
    public void Render_MultipleHeadingsDoNotCreateExtraAstMetadataParagraphs()
    {
        var markdown = "# H1\n## H2\n### H3\n#### H4\n##### H5";
        var document = MarkdownRenderer.Render(markdown);
        
        document.Should().NotBeNull();
        var text = GetText(document!);
        
        // Should only contain the heading text, no AST metadata
        text.Should().NotContain("HeadingLinkReferenceDefinition");
        text.Should().NotContain("LinkReferenceDefinition");
        text.Should().NotContain("Markdig.");
        
        // All headings should be present
        text.Should().Contain("H1");
        text.Should().Contain("H2");
        text.Should().Contain("H3");
        text.Should().Contain("H4");
        text.Should().Contain("H5");
    }

    [Fact]
    public void Render_SupportedMarkdownStructuresRenderWithoutExceptions()
    {
        var markdown = @"# Heading 1

## Heading 2

### Heading 3

Normal paragraph with **bold** and *italic* text.

- Unordered list item 1
- Unordered list item 2

1. Ordered list item 1
2. Ordered list item 2

```csharp
var x = 1;
```

> Blockquote text

| Col1 | Col2 |
|------|------|
| A    | B    |

---

[Link text](https://example.com)

Line break  
Next line";

        var act = () => MarkdownRenderer.Render(markdown);
        act.Should().NotThrow();
        
        var document = MarkdownRenderer.Render(markdown);
        document.Should().NotBeNull();
        var text = GetText(document!);
        
        // Verify no Markdig type names leak
        text.Should().NotContain("Markdig.");
        text.Should().NotContain("LinkReferenceDefinition");
        
        // Verify content is present
        text.Should().Contain("Heading 1");
        text.Should().Contain("Heading 2");
        text.Should().Contain("Heading 3");
        text.Should().Contain("Normal paragraph");
        text.Should().Contain("Unordered list item 1");
        text.Should().Contain("Ordered list item 1");
        text.Should().Contain("var x = 1;");
        text.Should().Contain("Blockquote text");
        text.Should().Contain("Col1");
        text.Should().Contain("Col2");
        text.Should().Contain("Link text");
    }

    [Fact]
    public void CreatePlainTextFallback_ProducesDocument()
    {
        var document = MarkdownRenderer.CreatePlainTextFallback("Hello\nWorld");

        document.Should().NotBeNull();
        document.Blocks.Count.Should().Be(1);
    }

    private static string GetText(FlowDocument document)
    {
        return new TextRange(document.ContentStart, document.ContentEnd).Text;
    }
}
