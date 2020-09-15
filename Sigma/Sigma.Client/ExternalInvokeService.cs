using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Client
{
    public class ExternalInvokeService
    {
        //Required for Native->C# assembly recognition
        public static void DllMain(IntPtr dllInstance, int reason, IntPtr reserved) { }

        [DllExport("InitializeSigma")]
        public static void InitializeSigma()
        {
            new Main().InitializeSigma();
        }
    }
}
