using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Client.CommonUtils;

namespace Sigma.Client.DetectionUnit
{
    /// <summary>
    /// Responsible for monitoring and detecting injections.
    /// </summary>
    internal class InjectionUnit : UnitBase
    {
        internal override void StartUnit()
        {
            InstallHooks();

            base.StartUnit();
        }

        private void InstallHooks()
        {
            Logger.Log("Installing hooks...");

            Hooks.LoadLibrary.InstallLoadLibraryHook();
            Hooks.GetModuleHandle.InstallGetModuleHandleHook();
        }
    }
}
