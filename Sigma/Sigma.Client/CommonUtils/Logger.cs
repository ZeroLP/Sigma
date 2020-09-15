using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Reflection;

namespace Sigma.Client.CommonUtils
{
    internal class Logger
    {
        /// <summary>
        /// Allocates a console instance to the running process
        /// </summary>
        internal static void CreateLoggerInstance()
        {
            NativeImport.AllocConsole();

            var outFile = NativeImport.CreateFile("CONOUT$", NativeImport.ConsolePropertyModifiers.GENERIC_WRITE
                                                            | NativeImport.ConsolePropertyModifiers.GENERIC_READ,
                                                            NativeImport.ConsolePropertyModifiers.FILE_SHARE_WRITE,
                                                            0, NativeImport.ConsolePropertyModifiers.OPEN_EXISTING, /*FILE_ATTRIBUTE_NORMAL*/0, 0);

            var safeHandle = new SafeFileHandle(outFile, true);

            NativeImport.SetStdHandle(-11, outFile);

            FileStream fs = new FileStream(safeHandle, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs) { AutoFlush = true };

            Console.SetOut(writer);

            if (NativeImport.GetConsoleMode(outFile, out var cMode)) NativeImport.SetConsoleMode(outFile, cMode | 0x0200);

            Console.Title = $"Log Window - {Assembly.GetExecutingAssembly().GetName().Name}";
        }

        /// <summary>
        /// Logs the given string to the console
        /// </summary>
        /// <param name="dataToLog"></param>
        /// <param name="severity"></param>
        internal static void Log(string dataToLog, LogSeverity severity = LogSeverity.Neutral)
        {
            var consoleColour = Console.ForegroundColor;

            switch (severity)
            {
                case LogSeverity.Neutral:
                    consoleColour = ConsoleColor.Cyan;
                    break;
                case LogSeverity.Information:
                    consoleColour = ConsoleColor.Green;
                    break;
                case LogSeverity.Warning:
                    consoleColour = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Danger:
                    consoleColour = ConsoleColor.Red;
                    break;
                default:
                    consoleColour = ConsoleColor.White;
                    break;
            }

            Console.ForegroundColor = consoleColour;

            if (string.IsNullOrEmpty(dataToLog))
                Console.WriteLine($"[-] StringNullOrEmpty occured while logging output.");
            else
                Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt")}]: {dataToLog}");
        }

        internal enum LogSeverity
        {
            Neutral,
            Information,
            Warning,
            Danger
        }
    }
}
