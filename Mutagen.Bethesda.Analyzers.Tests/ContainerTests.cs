using Mutagen.Bethesda.Analyzers.Engines;
using Mutagen.Bethesda.Analyzers.Testing;

namespace Mutagen.Bethesda.Analyzers.Tests;

public class ContainerTests
{
    [Fact]
    public void ResolvesIsolatedEngine()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<TestModule>();
        var container = builder.Build();

        container.Resolve<IsolatedEngine>();
    }

    [Fact]
    public void ResolvesContextualEngine()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<TestModule>();
        var container = builder.Build();

        container.Resolve<ContextualEngine>();
    }
}