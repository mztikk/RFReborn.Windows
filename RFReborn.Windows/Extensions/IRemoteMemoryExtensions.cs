﻿using System;
using System.Diagnostics;
using RFReborn.AoB;
using RFReborn.Windows.Memory;
using RFReborn.Windows.Native;

namespace RFReborn.Windows.Extensions
{
    /// <summary>
    /// Extends <see cref="IRemoteMemory"/>
    /// </summary>
    public static unsafe class IRemoteMemoryExtensions
    {
        #region AoB_Scanner
        /// <summary>
        /// Searches for a PEiD style string signature inside of the processes main module.
        /// </summary>
        /// <param name="remoteMem"><see cref="IRemoteMemory"/> used to read from the process.</param>
        /// <param name="signature">PEiD style string signature to search for.</param>
        /// <returns>The address of the <see cref="Signature"/> if it is found; -1 otherwise</returns>
        public static long FindSignature(this IRemoteMemory remoteMem, string signature) => FindSignature(remoteMem, new Signature(signature), remoteMem.MainModule);

        /// <summary>
        /// Searches for a given <see cref="Signature"/> inside of the processes main module.
        /// </summary>
        /// <param name="remoteMem"><see cref="IRemoteMemory"/> used to read from the process.</param>
        /// <param name="signature">The <see cref="Signature"/> to search for.</param>
        /// <returns>The address of the <see cref="Signature"/> if it is found; -1 otherwise</returns>
        public static long FindSignature(this IRemoteMemory remoteMem, Signature signature) => FindSignature(remoteMem, signature, remoteMem.MainModule);

        /// <summary>
        /// Searches for a given <see cref="Signature"/> inside of a <see cref="ProcessModule"/>.
        /// </summary>
        /// <param name="remoteMem"><see cref="IRemoteMemory"/> used to read from the process.</param>
        /// <param name="signature">The <see cref="Signature"/> to search for.</param>
        /// <param name="processModule">The <see cref="ProcessModule"/> to search in.</param>
        /// <returns>The address of the <see cref="Signature"/> if it is found; -1 otherwise</returns>
        public static long FindSignature(this IRemoteMemory remoteMem, Signature signature, ProcessModule processModule)
        {
            var dumpedModule = remoteMem.ReadBytes(processModule.BaseAddress, processModule.ModuleMemorySize);
            var found = Scanner.FindSignature(dumpedModule, signature);
            if (found == -1)
            {
                return -1;
            }

            return processModule.BaseAddress.ToInt64() + found;
        }

        internal static int ReadBytes(this IRemoteMemory memory, IntPtr address, void* buffer, int count)
        {
            if (!memory.ProcessHandle.IsInvalid
                && !memory.ProcessHandle.IsClosed
                && address != IntPtr.Zero
                && Kernel32.ReadProcessMemory(memory.ProcessHandle, address, buffer, count, out var numBytesRead))
            {
                return numBytesRead;
            }

            throw new Exception($"Couldn't read {count} bytes from 0x{address.ToString("X")}.");
        }
        #endregion
    }
}