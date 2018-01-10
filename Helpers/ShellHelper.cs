using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PiManagment.Helpers
{
    public static class ShellHelper
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            bool isWind = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var process = new Process()
            {

                StartInfo = new ProcessStartInfo
                {
                    FileName =  isWind?  "cmd": "/bin/bash",
                    Arguments = isWind ? $"/c {cmd}":  $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}
