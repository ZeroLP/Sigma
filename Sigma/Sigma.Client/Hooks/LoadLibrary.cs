using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Sigma.Client.CommonUtils;

namespace Sigma.Client.Hooks
{
    internal class LoadLibrary : IDisposable
    {
        internal static LoadLibrary instance { private get; set; } = new LoadLibrary();
        private static DetourEngine dEngine;

        internal delegate IntPtr LoadLibraryDelegate([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        private static LoadLibraryDelegate hookedInstance;
        private static LoadLibraryDelegate originalInstance;

        public void Dispose() { }

        internal bool initialised
        {
            get
            {
                return dEngine != null;
            }
        }

        internal static void InstallLoadLibraryHook()
        {
            var funcAddr = HookUtils.GetFunctionAddress("Kernel32.dll", "LoadLibraryA");

            hookedInstance = HLoadLibrary;
            originalInstance = Marshal.GetDelegateForFunctionPointer<LoadLibraryDelegate>(funcAddr);

            dEngine = new DetourEngine(funcAddr, Marshal.GetFunctionPointerForDelegate(hookedInstance));
            dEngine.Install();

            Logger.Log("Installed LoadLibrary hook");
        }

        private static IntPtr HLoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName)
        {
            //Heuristic actions...

            Console.WriteLine($"LoadLibrary is being called to load: {lpFileName}");

            return dEngine.CallOriginal<IntPtr>(originalInstance, new object[] { lpFileName });
        }
    }
}
