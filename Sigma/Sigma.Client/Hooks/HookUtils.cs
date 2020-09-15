using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Client.CommonUtils;

namespace Sigma.Client.Hooks
{
    internal class HookUtils
    {
        internal static IntPtr GetFunctionAddress(string LibraryName, string FunctionName)
        {
            IntPtr HModule = NativeImport.LoadLibrary(LibraryName);
            return NativeImport.GetProcAddress(HModule, FunctionName);
        }
    }
}
