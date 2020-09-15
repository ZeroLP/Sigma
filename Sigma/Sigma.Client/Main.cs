using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Client.CommonUtils;

namespace Sigma.Client
{
    internal class Main
    {
        /// <summary>
        /// Initializes Sigma's core.
        /// </summary>
        internal void InitializeSigma()
        {
            Task.Run(async () =>
            {
                await Task.Run(() => Logger.CreateLoggerInstance());
                await Task.Run(() => Logger.Log("Successfully loaded into Among Us."));

                await Task.Run(() => new DetectionUnit.UnitBase.InvokeContainer().StartAllUnit());
            });
        }
    }
}
