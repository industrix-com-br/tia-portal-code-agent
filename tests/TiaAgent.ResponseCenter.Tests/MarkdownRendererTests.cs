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
        var document = MarkdownRenderer.Render("# My Heading");

        document.Should().NotBeNull();
        var renderedText = GetText(document!);
        renderedText.Should().Contain("My Heading");
        renderedText.Should().NotContain("LinkReferenceDefinition");
    }

    [Fact]
    public void Render_DoesNotContainMarkdigTypeNames()
    {
        var markdown = @"# Title

Some paragraph.

## Another Heading

More content here.";
        var document = MarkdownRenderer.Render(markdown);

        document.Should().NotBeNull();
        var renderedText = GetText(document!);
        renderedText.Should().NotContain("Markdig.");
        renderedText.Should().NotContain("HeadingLinkReferenceDefinition");
        renderedText.Should().NotContain("LinkReferenceDefinitionGroup");
    }

    [Fact]
    public void Render_HeadingsAndContentRemainPresent()
    {
        var document = MarkdownRenderer.Render("# First\n\nParagraph one.\n\n## Second\n\nParagraph two.");

        document.Should().NotBeNull();
        var renderedText = GetText(document!);
        renderedText.Should().Contain("First");
        renderedText.Should().Contain("Paragraph one.");
        renderedText.Should().Contain("Second");
        renderedText.Should().Contain("Paragraph two.");
    }

    [Fact]
    public void Render_MultipleHeadingsDoNotCreateExtraAstMetadataParagraphs()
    {
        var markdown = "# H1\n## H2\n### H3\n### H4\n#### H5";
        var document = MarkdownRenderer.Render(markdown);

        document.Should().NotBeNull();
        var renderedText = GetText(document!);
        var paragraphCount = document!.Blocks.Count;
        renderedText.Should().NotContain("Markdig.");
        renderedText.Should().NotContain("Definition");
        paragraphCount.Should().Be(5);
    }

    [Fact]
    public void Render_SupportedMarkdownStructuresRenderWithoutExceptions()
    {
        var markdown = @"# Heading 1

Normal paragraph with **bold** and *italic*.

- Bullet 1
- Bullet 2

1. Numbered 1
2. Numbered 2

> Blockquote text

```csharp
var code = 1;
```

| A | B |
|---|---|
| 1 | 2 |

---

[link](https://example.com)

`inline code`";

        var act = () => MarkdownRenderer.Render(markdown);
        act.Should().NotThrow();

        var document = MarkdownRenderer.Render(markdown);
        document.Should().NotBeNull();
        var renderedText = GetText(document!);
        renderedText.Should().Contain("Heading 1");
        renderedText.Should().Contain("Normal paragraph");
        renderedText.Should().Contain("Bullet 1");
        renderedText.Should().Contain("Numbered 1");
        renderedText.Should().Contain("Blockquote text");
        renderedText.Should().Contain("var code = 1;");
        renderedText.Should().Contain("inline code");
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
