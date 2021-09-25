using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using Mutagen.Bethesda.Analyzers.SDK.Errors;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Order.DI;

namespace Mutagen.Bethesda.Analyzers.Engines.Live
{
    public class LiveEngine
    {
        private readonly ModListingWatcher.Factory _listingFactory;
        private readonly Engine _engine;
        private readonly ILiveLoadOrderProvider _liveLoadOrderProvider;

        public LiveEngine(
            ModListingWatcher.Factory listingFactory,
            Engine engine,
            ILiveLoadOrderProvider liveLoadOrderProvider)
        {
            _listingFactory = listingFactory;
            _engine = engine;
            _liveLoadOrderProvider = liveLoadOrderProvider;
        }

        public IObservable<IChangeSet<IError>> GetErrors(IScheduler scheduler)
        {
            return _liveLoadOrderProvider.Get(out _, scheduler)
                .Transform(x => _listingFactory(x))
                .TransformMany(x => GetErrors(x, scheduler));
        }

        private IObservableCollection<IError> GetErrors(ModListingWatcher watcher, IScheduler scheduler)
        {
            watcher.Update
                .Select()
        }
    }
}
