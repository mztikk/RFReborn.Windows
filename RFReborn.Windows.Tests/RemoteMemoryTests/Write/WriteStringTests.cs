using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFReborn.Windows.Memory;

namespace RFReborn.Windows.Tests.RemoteMemoryTests.Write
{
    [TestClass]
    public unsafe class WriteStringTests : RemoteMemoryBaseTest
    {
        [DataTestMethod]
        [DataRow("", "Hello World!")]
        [DataRow("Hello World", "")]
        [DataRow("Hello World", "öäü")]
        [DataRow("#*+'`´?ß", "öäü")]
        [DataRow("öäü", " #*+'` ´?ß")]
        public void WriteString(string s1, string s2)
        {
            using (var memory = new RemoteMemory(GetProcess()))
            {
                fixed (char* charPointer = s1)
                {
                    memory.WriteString(new IntPtr(charPointer), s2, GetEncoding());
                    string written = new(charPointer);
                    Assert.AreNotEqual(s1, written);
                    Assert.AreEqual(s2, written);
                }
            }
        }
    }
}
