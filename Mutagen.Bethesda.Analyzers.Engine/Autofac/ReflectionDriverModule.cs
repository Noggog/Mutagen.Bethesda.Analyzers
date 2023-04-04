using Autofac;
using Loqui;
using Mutagen.Bethesda.Analyzers.Drivers;
using Mutagen.Bethesda.Analyzers.Drivers.RecordFrame;
using Mutagen.Bethesda.Analyzers.Drivers.Records;
using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Noggog;

namespace Mutagen.Bethesda.Analyzers.Autofac;

public class ReflectionDriverModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        foreach (var analyzerType in TypeExt.GetInheritingFromGenericInterface(
                         typeof(IIsolatedRecordAnalyzer<>),
                         loadAssemblies: true)
                     .Select(x => x.Key.GetGenericArguments()[0])
                     .Select(x => LoquiRegistration.GetRegister(x).GetterType)
                     .Distinct())
        {
            builder.RegisterType(typeof(ByGenericTypeRecordIsolatedDriver<>).MakeGenericType(analyzerType))
                .As<IIsolatedDriver>();
            builder.RegisterType(typeof(ByGenericTypeRecordContextualDriver<>).MakeGenericType(analyzerType))
                .As<IContextualDriver>();
            builder.RegisterType(typeof(ByGenericTypeRecordFrameIsolatedDriver<>).MakeGenericType(analyzerType))
                .As<IIsolatedRecordFrameAnalyzerDriver>();
            builder.RegisterType(typeof(ByGenericTypeRecordFrameContextualDriver<>).MakeGenericType(analyzerType))
                .As<IContextualRecordFrameAnalyzerDriver>();
        }
    }
}