using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Utils
{
    public static class ProcessUtils
    {
        public static void ExecuteCommand(string exeFullPath, string[] args)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = exeFullPath;

            string composedArgs = string.Empty;
            foreach(var arg in args)
            {
                if (string.IsNullOrEmpty(composedArgs))
                {
                    composedArgs += arg;
                }
                else
                {
                    composedArgs += string.Format(" {0}", arg);
                }
            }
            startInfo.Arguments = composedArgs;

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
