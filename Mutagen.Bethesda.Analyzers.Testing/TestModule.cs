using System.IO.Abstractions;
using Autofac;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Mutagen.Bethesda.Analyzers.Cli.Modules;
using Mutagen.Bethesda.Analyzers.Config;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Environments.DI;
using NSubstitute;

namespace Mutagen.Bethesda.Analyzers.Testing;

public class TestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<RunAnalyzerModule>();
        builder.RegisterInstance(new FileSystem())
            .As<IFileSystem>();
        builder.RegisterGeneric(typeof(NullLogger<>))
            .As(typeof(ILogger<>))
            .SingleInstance();
        builder.RegisterInstance(new TestDropoff())
            .AsSelf()
            .AsImplementedInterfaces();
        builder.RegisterInstance(new GameReleaseInjection(GameRelease.SkyrimSE))
            .AsImplementedInterfaces();
        var minSev = Substitute.For<IMinimumSeverityConfiguration>();
        minSev.MinimumSeverity.Returns(Severity.Suggestion);
        builder.RegisterInstance(minSev).As<IMinimumSeverityConfiguration>();
    }
}