using Mutagen.Bethesda.Analyzers.SDK.Topics;

namespace Mutagen.Bethesda.Analyzers.Tests.Engine;

public class TopicIdTests
{
    [Fact]
    public void ParseTypical()
    {
        var id = TopicId.Parse("A123");
        id.Id.Should().Be(123);
        id.Prefix.String.Should().Be("A");
    }

    [Fact]
    public void ParseRejectsLongPrefix()
    {
        var prefix = "";
        for (int i = 0; i < TopicPrefix.MaxPrefixSize + 1; i++)
        {
            prefix += "A";
        }

        Assert.Throws<ArgumentException>(() =>
        {
            TopicId.Parse($"{prefix}123");
        });
    }

    [Fact]
    public void ParseRejectsIdWithNoNumbers()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            TopicId.Parse($"AB");
        });
    }

    [Fact]
    public void ParseRejectsIdWithNumbersInPrefix()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            TopicId.Parse($"1A1");
        });
    }

    [Fact]
    public void ParseRejectsIdWithPeriodInPrefix()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            TopicId.Parse($"A.B1");
        });
    }
}
