using FluentAssertions;
using TiaAgent.Bridge.Runtime;
using Xunit;

namespace TiaAgent.Bridge.Tests;

public class CommandResolverQuotingTests
{
    [Fact]
    public void ComposeArguments_CmdWrapper_QuotesTheCompleteCommand()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.cmd" => @"C:\nvm4w\nodejs\claude.cmd",
            _ => null
        });

        var arguments = resolved.ComposeArguments(
            "-p \"Process the complete request provided through stdin.\" --output-format json");

        arguments.Should().Be(
            "/d /s /c \"\"C:\\nvm4w\\nodejs\\claude.cmd\" -p \"Process the complete request provided through stdin.\" --output-format json\"");
    }

    [Fact]
    public void ComposeArguments_CmdWrapper_PreservesAPathContainingSpaces()
    {
        var resolved = CommandResolver.Resolve(@"C:\Program Files\nodejs\claude.cmd");

        var arguments = resolved.ComposeArguments("--version");

        arguments.Should().Be(
            "/d /s /c \"\"C:\\Program Files\\nodejs\\claude.cmd\" --version\"");
    }

    [Fact]
    public void ComposeArguments_CmdWrapper_WithNoExtraArguments_StillUsesCanonicalQuoting()
    {
        var resolved = CommandResolver.Resolve(@"C:\Program Files\nodejs\claude.cmd");

        var arguments = resolved.ComposeArguments("");

        arguments.Should().Be(
            "/d /s /c \"\"C:\\Program Files\\nodejs\\claude.cmd\"\"");
    }

    [Fact]
    public void Resolve_CmdWrapper_KeepsTargetOutOfTheWrapperPrefix()
    {
        var resolved = CommandResolver.Resolve("claude", name => name switch
        {
            "claude.cmd" => @"C:\nvm4w\nodejs\claude.cmd",
            _ => null
        });

        resolved.Arguments.Should().Equal("/d", "/s", "/c");
        resolved.ResolvedTargetPath.Should().Be(@"C:\nvm4w\nodejs\claude.cmd");
    }
}
