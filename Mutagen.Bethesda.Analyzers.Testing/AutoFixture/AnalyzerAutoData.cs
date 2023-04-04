using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Mutagen.Bethesda.Analyzers.Testing.AutoFixture;
using Mutagen.Bethesda.Testing.AutoData;
using Noggog.Testing.AutoFixture;
using Xunit;

namespace Mutagen.Bethesda.Analyzers.Tests;

public class AnalyzerAutoData : AutoDataAttribute
{
    public AnalyzerAutoData(
        bool ConfigureMembers = true,
        bool UseMockFileSystem = true,
        bool GenerateDelegates = false,
        bool OmitAutoProperties = false)
        : base(() =>
        {
            return Factory(
                UseMockFileSystem: UseMockFileSystem,
                ConfigureMembers: ConfigureMembers,
                GenerateDelegates: GenerateDelegates,
                OmitAutoProperties: OmitAutoProperties);
        })
    {
    }

    public static AutoFixture.IFixture Factory(
        bool ConfigureMembers = true,
        bool UseMockFileSystem = true,
        bool GenerateDelegates = false,
        bool OmitAutoProperties = false)
    {
        return new AutoFixture.Fixture()
            .Customize(new AnalyzerAutoDataCustomization(
                useMockFilesystem: UseMockFileSystem,
                configureMembers: ConfigureMembers,
                generateDelegates: GenerateDelegates,
                omitAutoProperties: OmitAutoProperties));
    }
}

public class AnalyzerInlineData : CompositeDataAttribute
{
    public AnalyzerInlineData(
        params object[] ExtraParameters)
        : base(
            new InlineDataAttribute(ExtraParameters),
            new AnalyzerAutoData())
    {
    }
}

public class AnalyzerAutoDataCustomization : ICustomization
{
    private readonly bool _useMockFilesystem;
    private readonly bool _generateDelegates;
    private readonly bool _omitAutoProperties;
    private readonly bool _configureMembers;

    public AnalyzerAutoDataCustomization(
        bool configureMembers,
        bool useMockFilesystem,
        bool generateDelegates,
        bool omitAutoProperties)
    {
        _useMockFilesystem = useMockFilesystem;
        _generateDelegates = generateDelegates;
        _omitAutoProperties = omitAutoProperties;
        _configureMembers = configureMembers;
    }

    public void Customize(IFixture fixture)
    {
        var autoMock = new AutoNSubstituteCustomization()
        {
            ConfigureMembers = _configureMembers,
            GenerateDelegates = _generateDelegates
        };
        fixture.Customize(autoMock);
        fixture.OmitAutoProperties = _omitAutoProperties;
        fixture.Customize(new MutagenBaseCustomization());
        fixture.Customize(new MutagenReleaseCustomization(GameRelease.SkyrimSE));
        fixture.Customize(new DefaultCustomization(_useMockFilesystem));
        fixture.Customizations.Add(new TopicPrefixBuilder());
    }
}