using System;
using System.IO;
using System.IO.Abstractions;
using System.Reactive;
using System.Reactive.Linq;
using Mutagen.Bethesda.Environments.DI;
using Mutagen.Bethesda.Plugins.Order;
using Noggog;

namespace Mutagen.Bethesda.Analyzers.Engines.Live
{
    public class ModListingWatcher
    {
        private readonly IFileSystemWatcherFactory _watcherFactory;
        private readonly IDataDirectoryProvider _dataDirectoryProvider;
        private IModListingGetter Listing { get; }

        public delegate ModListingWatcher Factory(IModListingGetter listing);

        public FilePath Path => System.IO.Path.Combine(_dataDirectoryProvider.Path, Listing.ModKey.FileName);

        public ModListingWatcher(
            IFileSystemWatcherFactory watcherFactory,
            IDataDirectoryProvider dataDirectoryProvider,
            IModListingGetter modListingGetter)
        {
            _watcherFactory = watcherFactory;
            _dataDirectoryProvider = dataDirectoryProvider;
            Listing = modListingGetter;
        }

        public IObservable<Unit> Update => ObservableExt.WatchFile(Path, fileWatcherFactory: _watcherFactory)
            .StartWith(Unit.Default);
    }
}
