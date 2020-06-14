using System;
using System.Diagnostics;
using System.Text;
using RFReborn.Windows.Memory.Exceptions;
using RFReborn.Windows.Native;
using RFReborn.Windows.Native.Enums;

namespace RFReborn.Windows.Memory
{
    /// <summary>
    /// Implementation of <see cref="IRemoteMemory"/>
    /// </summary>
    public unsafe class RemoteMemory : IRemoteMemory
    {
        /// <summary>
        /// If our process is 64Bit or not.
        /// </summary>
        public static readonly bool Self64 = IntPtr.Size == 8;

        /// <summary>
        /// If our and the process we attached to is 64Bit.
        /// </summary>
        public readonly bool Both64;

        /// <summary>
        /// If the process we attached to is 64Bit or not.
        /// </summary>
        public readonly bool Is64;

        /// <summary>
        /// The native process we're interacting with.
        /// </summary>
        public Process NativeProcess { get; }

        /// <summary>
        /// Handle to the process.
        /// </summary>
        public Handle ProcessHandle { get; }

        /// <summary>
        /// Main module of the process.
        /// </summary>
        public ProcessModule MainModule => NativeProcess.MainModule;

        /// <summary>
        /// Initializes a new instance of <see cref="RemoteMemory"/>.
        /// </summary>
        /// <param name="proc"><see cref="Process"/> to open.</param>
        public RemoteMemory(Process proc)
        {
            if (!ProcessHelpers.ProcessExists(proc))
            {
                throw new ArgumentException("Process not valid");
            }

            NativeProcess = proc;
            ProcessHandle = ProcessHelpers.OpenProcess(ProcessAccessFlags.AllAccess, NativeProcess.Id);
            Is64 = ProcessHelpers.Is64Bit(NativeProcess);
            Both64 = Is64 && Self64;
        }

        /// <summary>
        /// Reads a value of type <typeparamref name="T" /> from the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="address">Address where to read the value from.</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>Value read from the remote memory.</returns>
        public T Read<T>(IntPtr address, bool relative = false) where T : unmanaged
        {
            int size;
            if (typeof(T) == typeof(IntPtr))
            {
                size = Both64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            byte[] bytes = ReadBytes(address, size, relative);
            fixed (void* bp = bytes)
            {
                return *(T*)bp;
            }
        }

        /// <summary>
        /// Reads an array of values of type <typeparamref name="T" /> from the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <param name="address">Address where to read the values from.</param>
        /// <param name="count">Number of values to read, size of the return array.</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>Array of values read from the remote memory.</returns>
        public T[] Read<T>(IntPtr address, int count, bool relative = false) where T : unmanaged
        {
            int size;
            if (typeof(T) == typeof(IntPtr))
            {
                size = Both64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            size *= count;

            byte[] bytes = ReadBytes(address, size, relative);
            T[] result = new T[count];

            fixed (void* bp = bytes, rp = result)
            {
                Buffer.MemoryCopy(bp, rp, size, size);
            }

            return result;
        }

        /// <summary>
        /// Writes the value of type <typeparamref name="T" /> to the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="address">Address where to write the value to.</param>
        /// <param name="value">Value to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        public void Write<T>(IntPtr address, T value, bool relative = false) where T : unmanaged
        {
            int size;
            if (typeof(T) == typeof(IntPtr))
            {
                size = Is64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            WriteBytes(address, &value, size, relative);
        }

        /// <summary>
        /// Writes the array of values of type <typeparamref name="T" /> to the remote process at a specified address.
        /// </summary>
        /// <typeparam name="T">Type of values.</typeparam>
        /// <param name="address">Address where to write the values to.</param>
        /// <param name="values">Values to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        public void Write<T>(IntPtr address, T[] values, bool relative = false) where T : unmanaged
        {
            Type type = typeof(T);
            int size;
            if (type == typeof(IntPtr))
            {
                size = Is64 ? 8 : 4;
            }
            else
            {
                size = sizeof(T);
            }

            fixed (void* vp = values)
            {
                WriteBytes(address, vp, values.Length * size, relative);
            }
        }

        /// <summary>
        /// Reads a string in the remote process memory.
        /// </summary>
        /// <param name="address">Address of the values.</param>
        /// <param name="encoding"><see cref="Encoding" /> to use to get the bytes from the string.</param>
        /// <param name="maxLength">Maximum length of the string(bytes to be read).</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>String at the address.</returns>
        public string ReadString(IntPtr address, Encoding encoding, int maxLength = 512, bool relative = false)
        {
            byte[] bytes = ReadBytes(address, maxLength, relative);
            string data = encoding.GetString(bytes);
            int eosPos = data.IndexOf('\0');

            return eosPos == -1 ? data : data.Substring(0, eosPos);
        }

        /// <summary>
        /// Reads a string with a default <see cref="Encoding" /> in the remote process memory.
        /// </summary>
        /// <param name="address">Address of the values.</param>
        /// <param name="maxLength">Maximum length of the string(bytes to be read).</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>String at the address.</returns>
        public string ReadString(IntPtr address, int maxLength = 512, bool relative = false) => ReadString(address, Encoding.UTF8, maxLength, relative);

        /// <summary>
        /// Writes a string, using the specified <paramref name="encoding" />, to the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to write the string to.</param>
        /// <param name="text">String to write.</param>
        /// <param name="encoding"><see cref="Encoding" /> to use to get the bytes from the string.</param>
        /// <param name="relative">If the address is relative or not.</param>
        public void WriteString(IntPtr address, string text, Encoding encoding, bool relative = false) => WriteBytes(address, encoding.GetBytes(text + '\0'), relative);

        /// <summary>
        /// Writes a string, using a default <see cref="Encoding" />, to the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to write the string to.</param>
        /// <param name="text">String to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        public void WriteString(IntPtr address, string text, bool relative = false) => WriteString(address, text, Encoding.UTF8, relative);

        /// <summary>
        /// Reads bytes from the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to read the bytes from.</param>
        /// <param name="count">Number of bytes to read.</param>
        /// <param name="relative">If the address is relative or not.</param>
        /// <returns>Array of size <paramref name="count" /> with the bytes read.</returns>
        public byte[] ReadBytes(IntPtr address, int count, bool relative = false) => ReadBytes(ProcessHandle, relative ? ToAbsolute(address) : address, count);

        /// <summary>
        /// Writes bytes to the remote process at a specified address.
        /// </summary>
        /// <param name="address">Address where to write the bytes to.</param>
        /// <param name="bytes">Bytes to write.</param>
        /// <param name="relative">If the address is relative or not.</param>
        public void WriteBytes(IntPtr address, byte[] bytes, bool relative = false) => WriteBytes(ProcessHandle, relative ? ToAbsolute(address) : address, bytes);

        private void WriteBytes(IntPtr address, void* bytes, int numOfBytes, bool relative = false) => WriteBytes(ProcessHandle, relative ? ToAbsolute(address) : address, bytes, numOfBytes);

        private static byte[] ReadBytes(Handle procHandle, IntPtr address, int size)
        {
            if (!procHandle.IsInvalid && !procHandle.IsClosed && address != IntPtr.Zero)
            {
                byte[] buffer = new byte[size];
                if (Kernel32.ReadProcessMemory(procHandle, address, buffer, size, out int numBytesRead)
                    && size == numBytesRead)
                {
                    return buffer;
                }
            }

            throw new ReadMemoryException(address, size);
            //throw new Exception($"Couldn't read {size} bytes from 0x{address.ToString("X")}.");
        }

        private static void WriteBytes(Handle procHandle, IntPtr address, byte[] bytes)
        {
            if (procHandle.IsInvalid || procHandle.IsClosed || address == IntPtr.Zero)
            {
                throw new ArgumentException("Handle invalid/closed or address is zero");
            }

            bool result = Kernel32.WriteProcessMemory(procHandle, address, bytes, bytes.Length, out int numBytesWritten);

            if (!result || numBytesWritten != bytes.Length)
            {
                throw new WriteMemoryException(address, bytes.Length);
                //throw new Exception($"Couldn't write {bytes.Length} bytes to 0x{address.ToString("X")}");
            }
        }

        private static void WriteBytes(Handle procHandle, IntPtr address, void* bytes, int numOfBytesToWrite)
        {
            if (procHandle.IsInvalid || procHandle.IsClosed || address == IntPtr.Zero)
            {
                throw new ArgumentException("Handle invalid/closed or address is zero");
            }

            bool result = Kernel32.WriteProcessMemory(procHandle, address, bytes, numOfBytesToWrite, out int numBytesWritten);

            if (!result || numBytesWritten != numOfBytesToWrite)
            {
                throw new WriteMemoryException(address, numOfBytesToWrite);
                //throw new Exception($"Couldn't write {numOfBytesToWrite} bytes to 0x{address.ToString("X")}");
            }
        }

        /// <summary>
        /// Converts a relative address to an absolute one.
        /// </summary>
        /// <param name="address">Relative address.</param>
        /// <returns>Absolute address</returns>
        public IntPtr ToAbsolute(IntPtr address)
        {
            if (address.ToInt64() > MainModule.ModuleMemorySize)
            {
                throw new ArgumentOutOfRangeException(nameof(address),
                    "Relative address greater than main module size.");
            }

            return new IntPtr(MainModule.BaseAddress.ToInt64() + address.ToInt64());
        }

        /// <summary>
        /// Walks a pointer(base + offsets) and returns the final address.
        /// </summary>
        /// <param name="address">BaseAddress</param>
        /// <param name="offsets">Offsets to walk</param>
        /// <returns>The final address of the pointer.</returns>
        public IntPtr GetAddress(IntPtr address, int[] offsets)
        {
            address = Read<IntPtr>(address);
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                address = Read<IntPtr>(address + offsets[i]);
            }

            return address + offsets[^1];
        }

        /// <summary>
        /// Allocates memory in the remote process and returns the address of it.
        /// </summary>
        /// <param name="size">Size of memory to allocate.</param>
        /// <param name="protFlags">Protection flags of the newly allocated memory. Default is <see cref="F:RFReborn.Windows.Native.Enums.MemoryProtection.EXECUTE_READWRITE" /></param>
        /// <returns>The address to the allocated memory.</returns>
        public IntPtr Alloc(int size, MemoryProtection protFlags = MemoryProtection.EXECUTE_READWRITE) => Alloc(ProcessHandle, size, protFlags);

        private static IntPtr Alloc(Handle procHandle, int size, MemoryProtection protFlags = MemoryProtection.EXECUTE_READWRITE, MemoryAllocation allocFlags = MemoryAllocation.Commit)
        {
            if (!procHandle.IsInvalid && !procHandle.IsClosed)
            {
                IntPtr rtn = Kernel32.VirtualAllocEx(procHandle, IntPtr.Zero, size, allocFlags, protFlags);
                if (rtn != IntPtr.Zero)
                {
                    return rtn;
                }
            }

            throw new Exception($"Couldn't allocate {size} bytes of memory.");
        }

        /// <summary>
        /// Frees allocated memory(ex with <see cref="M:RFReborn.Windows.Memory.IRemoteMemory.Alloc(System.Int32,RFReborn.Windows.Native.Enums.MemoryProtection)" />
        /// </summary>
        /// <param name="address">Address of the allocated memory to free.</param>
        public void Free(IntPtr address) => Free(ProcessHandle, address);

        private static void Free(Handle procHandle, IntPtr address)
        {
            if (!procHandle.IsInvalid && !procHandle.IsClosed && address != IntPtr.Zero)
            {
                if (Kernel32.VirtualFreeEx(procHandle, address, 0, MemoryRelease.Release))
                {
                    return;
                }
            }

            throw new Exception($"Couldn't free memory at 0x{address.ToString("X")}.");
        }

        #region IDisposable Support
        /// <summary>
        /// If this object has been disposed
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    ProcessHandle?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                IsDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RemoteMemory()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        /// <inheritdoc />
        public void Dispose() =>
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);// TODO: uncomment the following line if the finalizer is overridden above.// GC.SuppressFinalize(this);
        #endregion
    }
}
