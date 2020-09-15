using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SigmaTestLoader.CommonUtils;

namespace SigmaTestLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Loader LDR = new Loader("Among Us");

            LDR.LoadAndCallMethod($"{Directory.GetCurrentDirectory()}//Sigma.Client.dll", "InitializeSigma");
        }
    }
}
