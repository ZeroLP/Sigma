using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Client.CommonUtils;

namespace Sigma.Client.DetectionUnit
{
    internal class UnitBase
    {
        /// <summary>
        /// Common method
        /// </summary>
        internal virtual void StartUnit() { }

        /// <summary>
        /// Hides from inherited classes, for public internal unit initializing purpose
        /// </summary>
        internal class InvokeContainer
        {
            private List<UnitBase> initializedUnits;

            internal InvokeContainer()
            {
                try
                {
                    initializedUnits = new List<UnitBase>()
                    {
                        new InjectionUnit()
                    };
                }
                catch(Exception Ex) { Logger.Log($"Exception occured while initializing units: {Ex.Message}", Logger.LogSeverity.Danger); }
            }

            internal void StartAllUnit()
            {
                foreach(UnitBase uBase in initializedUnits)
                {
                    Logger.Log($"Starting unit: {uBase.GetType().Name}");
                    uBase.StartUnit();
                }
            }
        }
    }
}
