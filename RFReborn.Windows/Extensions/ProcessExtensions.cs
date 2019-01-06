using System.Diagnostics;
using System.Management;

namespace RFReborn.Windows.Extensions
{
    /// <summary>
    /// Extends <see cref="Process"/>
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// Gets all commandline arguments a process was started with.
        /// </summary>
        /// <param name="proc">Process to get arguments from.</param>
        /// <returns>String of the commandline arguments if successfull, <see cref="string.Empty"/> otherwise.</returns>
        public static string GetCommandLine(this Process proc)
        {
            using (var searcher = new ManagementObjectSearcher(
                          $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {proc.Id}"))
            {
                foreach (ManagementBaseObject item in searcher.Get())
                {
                    var cmd = item["CommandLine"];
                    if (cmd == null)
                    {
                        continue;
                    }

                    return cmd.ToString();
                }
            }

            return string.Empty;
        }
    }
}
