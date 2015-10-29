using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringApp.Models
{
    public static class Logger
    {
        // speical thanks to: http://www.daveoncsharp.com/2009/09/create-a-logger-using-the-trace-listener-in-csharp/

        public static void Error(string message, string component)
        {
            WriteEntry(message, "error", component);
        }

        public static void Error(Exception ex, string component)
        {
            WriteEntry(ex.Message, "error", component);
        }

        public static void Warning(string message, string component)
        {
            WriteEntry(message, "warning", component);
        }

        public static void Info(string message, string component)
        {
            WriteEntry(message, "info", component);
        }

        private static void WriteEntry(string message, string type, string component)
        {
            Trace.WriteLine(
                    string.Format("{0},{1},{2},{3}",
                                  DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                                  type,
                                  component,
                                  message));
        }
    }

}
