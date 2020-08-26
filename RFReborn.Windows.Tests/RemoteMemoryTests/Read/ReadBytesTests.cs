using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFReborn.Windows.Memory;

namespace RFReborn.Windows.Tests.RemoteMemoryTests.Read
{
    [TestClass]
    public unsafe class ReadBytesTests : RemoteMemoryBaseTest
    {
        [TestMethod]
        public void ReadBytes()
        {
            byte[] bytes = new byte[] { 1, 2, 3 };
            using (var memory = new RemoteMemory(GetProcess()))
            {
                fixed (byte* bytePointer = bytes)
                {
                    CollectionAssert.AreEqual(bytes, memory.ReadBytes(new IntPtr(bytePointer), bytes.Length));
                }
            }
        }
    }
}
