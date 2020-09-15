using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Sigma.Client.CommonUtils;

namespace Sigma.Client.Hooks
{
    internal class GetModuleHandle : IDisposable
    {
        internal static GetModuleHandle instance { private get; set; } = new GetModuleHandle();
        private static DetourEngine dEngine;

        internal delegate IntPtr GetModuleHandleDelegate(string lpModuleName);

        private static GetModuleHandleDelegate hookedInstance;
        private static GetModuleHandleDelegate originalInstance;

        public void Dispose() { }

        internal bool initialised
        {
            get
            {
                return dEngine != null;
            }
        }

        internal static void InstallGetModuleHandleHook()
        {
            var funcAddr = HookUtils.GetFunctionAddress("Kernel32.dll", "GetModuleHandleA");

            hookedInstance = HGetModuleHandle;
            originalInstance = Marshal.GetDelegateForFunctionPointer<GetModuleHandleDelegate>(funcAddr);

            dEngine = new DetourEngine(funcAddr, Marshal.GetFunctionPointerForDelegate(hookedInstance));
            dEngine.Install();

            Logger.Log("Installed GetModuleHandle hook");
        }

        private static IntPtr HGetModuleHandle(string lpModuleName)
        {
            //Spams Steam's VR related module handles
            //Console.WriteLine($"GetModuleHandle is being called to get module handle of: {lpModuleName}");

            return dEngine.CallOriginal<IntPtr>(originalInstance, new object[] { lpModuleName });
        }
    }
}
