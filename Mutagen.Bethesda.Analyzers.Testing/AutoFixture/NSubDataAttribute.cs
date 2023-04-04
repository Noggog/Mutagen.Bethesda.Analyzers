using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Mutagen.Bethesda.Testing.AutoData;
using Noggog.Testing.AutoFixture;

namespace Mutagen.Bethesda.Analyzers.Testing.AutoFixture;

public class NSubDataAttribute : AutoDataAttribute
{
    public NSubDataAttribute(
        bool ConfigureMembers = false,
        GameRelease Release = GameRelease.SkyrimSE,
        bool UseMockFileSystem = true)
        : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization()
            {
                ConfigureMembers = ConfigureMembers,
                GenerateDelegates = true
            });
            fixture.Customize(new MutagenBaseCustomization());
            fixture.Customize(new MutagenReleaseCustomization(Release));
            fixture.Customize(new DefaultCustomization(UseMockFileSystem));
            return fixture;
        })
    {
    }
}