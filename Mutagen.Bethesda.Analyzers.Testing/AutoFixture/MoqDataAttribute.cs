using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Mutagen.Bethesda.Testing.AutoData;
using Noggog.Testing.AutoFixture;

namespace Mutagen.Bethesda.Analyzers.Testing.AutoFixture;

public class MoqDataAttribute : AutoDataAttribute
{
    public MoqDataAttribute(
        bool ConfigureMembers = false,
        GameRelease Release = GameRelease.SkyrimSE,
        bool UseMockFileSystem = true)
        : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization()
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