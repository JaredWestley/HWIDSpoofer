using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyAuth
{
    class Global
    {
        public static bool PCName = false;
        public static bool GUID = false;
        public static bool HWIDserial = false;
        public static bool ProductID = false;
        public static bool MacAddress = false;
        public static bool InstallTime = false;
        public static bool InstallDate = false;
        public static bool HwProfileGUID = false;

        public static void Restart()
        {
            StartShutDown("-f -r -t 5");
        }

        private static void StartShutDown(string param)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = "cmd";
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Arguments = "/C shutdown " + param;
            Process.Start(proc);
        }
    }
}
