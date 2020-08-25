using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFReborn.Windows.Memory;

namespace RFReborn.Windows.Tests.RemoteMemoryTests
{
    [TestClass]
    public unsafe class WriteBytesTests : RemoteMemoryBaseTest
    {
        [TestMethod]
        public void WriteBytes()
        {
            byte[] bytes = new byte[] { 1, 2, 3 };
            byte[] preBytes = new byte[bytes.Length];
            byte[] newBytes = new byte[] { 4, 5, 6 };
            bytes.CopyTo(preBytes, 0);
            using (var memory = new RemoteMemory(GetProcess()))
            {
                CollectionAssert.AreEqual(bytes, preBytes);
                fixed (byte* bytePointer = bytes)
                {
                    memory.WriteBytes(new IntPtr(bytePointer), newBytes);
                }
                CollectionAssert.AreNotEqual(preBytes, bytes);
                CollectionAssert.AreEqual(newBytes, bytes);
            }
        }
    }
}
