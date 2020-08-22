using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RFReborn.Windows.Native;
using RFReborn.Windows.Native.Enums;

namespace RFReborn.Windows
{
    /// <summary>
    /// Adds various methods to interact with <see cref="Process"/>
    /// </summary>
    public static class ProcessHelpers
    {
        /// <summary>
        ///     Opens an existing local process object.
        /// </summary>
        /// <param name="accessFlags">
        ///     The access to the process object. This access right is checked against the security descriptor for the process.
        ///     This parameter can be one or more of the process access rights. If the caller has enabled the SeDebugPrivilege
        ///     privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="procId">
        ///     The identifier of the local process to be opened. If the specified process is the System Process(0x00000000), the
        ///     function fails and the last error code is ERROR_INVALID_PARAMETER.If the specified process is the Idle process or
        ///     one of the CSRSS processes, this function fails and the last error code is ERROR_ACCESS_DENIED because their access
        ///     restrictions prevent user-level code from opening them. If you are using GetCurrentProcessId as an argument to this
        ///     function, consider using GetCurrentProcess instead of OpenProcess, for improved performance.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is an open handle to the specified process. If the function fails, the
        ///     return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        public static Handle OpenProcess(ProcessAccessFlags accessFlags, int procId)
        {
            Handle handle = Kernel32.OpenProcess(accessFlags, false, procId);

            if (!handle.IsInvalid && !handle.IsClosed)
            {
                return handle;
            }

            throw new Exception($"Couldn't open process: {procId}");
        }

        /// <summary>
        ///     Opens an existing local process object.
        /// </summary>
        /// <param name="accessFlags">
        ///     The access to the process object. This access right is checked against the security descriptor for the process.
        ///     This parameter can be one or more of the process access rights. If the caller has enabled the SeDebugPrivilege
        ///     privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="proc">
        ///     The identifier of the local process to be opened. If the specified process is the System Process(0x00000000), the
        ///     function fails and the last error code is ERROR_INVALID_PARAMETER.If the specified process is the Idle process or
        ///     one of the CSRSS processes, this function fails and the last error code is ERROR_ACCESS_DENIED because their access
        ///     restrictions prevent user-level code from opening them. If you are using GetCurrentProcessId as an argument to this
        ///     function, consider using GetCurrentProcess instead of OpenProcess, for improved performance.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is an open handle to the specified process. If the function fails, the
        ///     return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Handle OpenProcess(ProcessAccessFlags accessFlags, Process proc) => OpenProcess(accessFlags, proc.Id);

        /// <summary>
        /// Determines whether the specified process is 64bit or not.
        /// </summary>
        /// <param name="proc">The process to check.</param>
        /// <returns><see langword="true"/> if 64bit, otherwise <see langword="false"/>.</returns>
        public static bool Is64Bit(Process proc)
        {
            Kernel32.IsWow64Process(proc.Handle, out bool iswow64);
            return !iswow64 && Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines whether the specified process exists.
        /// </summary>
        /// <param name="process">The process to check.</param>
        /// <returns><see langword="true"/> if process exists, otherwise <see langword="false"/>.</returns>
        public static bool ProcessExists(Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            try
            {
                Process? proc = Process.GetProcessById(process.Id);
                if (proc?.HasExited != false)
                {
                    return false;
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Win32Exception)
            {
                return false;
            }

            return true;
        }
    }
}
