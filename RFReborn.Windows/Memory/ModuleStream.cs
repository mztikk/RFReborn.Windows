using System;
using System.Diagnostics;
using System.IO;
using RFReborn.Windows.Extensions;

namespace RFReborn.Windows.Memory
{
    public unsafe class ModuleStream : Stream
    {
        private readonly IRemoteMemory _memory;

        private readonly ProcessModule _module;
        private long _internalPos;

        public ModuleStream(IRemoteMemory memory, ProcessModule module)
        {
            _memory = memory;
            _module = module;

            _internalPos = 0;
        }

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => _module.ModuleMemorySize;

        public override long Position
        {
            get => _internalPos;
            set => _internalPos = value;
        }

        /// <summary>When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        /// <exception cref="ArgumentException">The sum of <paramref name="offset">offset</paramref> and <paramref name="count">count</paramref> is larger than the buffer length.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="buffer">buffer</paramref> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset">offset</paramref> or <paramref name="count">count</paramref> is negative.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException">The stream does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after the stream was closed.</exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            fixed (void* p = buffer)
            {
                var bytesRead = _memory.ReadBytes((IntPtr)(_module.BaseAddress.ToInt64() + _internalPos), (byte*)p + offset, buffer.Length - offset);
                _internalPos += bytesRead;
                return bytesRead;
            }
        }

        public override void Flush() => throw new NotImplementedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotImplementedException();
        public override void SetLength(long value) => throw new NotImplementedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotImplementedException();
    }
}
