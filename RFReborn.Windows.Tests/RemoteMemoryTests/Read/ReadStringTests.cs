using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFReborn.Windows.Memory;

namespace RFReborn.Windows.Tests.RemoteMemoryTests.Read
{
    [TestClass]
    public unsafe class ReadStringTests : RemoteMemoryBaseTest
    {
        [DataTestMethod]
        [DataRow("Hello World")]
        [DataRow("öäü")]
        [DataRow("#*+'`´?ß")]
        public void ReadString(string input)
        {
            using (var memory = new RemoteMemory(GetProcess()))
            {
                fixed (char* charPointer = input)
                {
                    Assert.AreEqual(input, memory.ReadString(new IntPtr(charPointer), GetEncoding()));
                }
            }
        }
    }
}
