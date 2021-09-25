using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda.Analyzers.Drivers;
using Mutagen.Bethesda.Analyzers.Reporting;
using Mutagen.Bethesda.Plugins.Records;

namespace Mutagen.Bethesda.Analyzers.Engines
{
    public class Engine
    {
        private readonly IModDriver[] _modDrivers;

        public Engine(IEnumerable<IModDriver> modDrivers)
        {
            _modDrivers = modDrivers
                .Where(x => x.Applicable)
                .ToArray();
        }

        public void RunOn(IModGetter modGetter, IReportDropbox reportDropbox)
        {
            foreach (var driver in _modDrivers)
            {
                driver.Drive(modGetter, reportDropbox);
            }
        }
    }
}
