using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFReborn.Windows.Memory;

namespace RFReborn.Windows.Tests.RemoteMemoryTests.Read
{
    [TestClass]
    public unsafe class ReadTests : RemoteMemoryBaseTest
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1781818)]
        [DataRow(-1781818)]
        [DataRow(int.MaxValue)]
        [DataRow(int.MinValue)]
        public void ReadInt(int input)
        {
            using (var memory = new RemoteMemory(GetProcess()))
            {
                Assert.AreEqual(input, memory.Read<int>(new IntPtr(&input)));
            }
        }

        [DataTestMethod]
        [DataRow(0f)]
        [DataRow(178.1818f)]
        [DataRow(-178.1818f)]
        [DataRow(float.MaxValue)]
        [DataRow(float.MinValue)]
        public void ReadFloat(float input)
        {
            using (var memory = new RemoteMemory(GetProcess()))
            {
                Assert.AreEqual(input, memory.Read<float>(new IntPtr(&input)));
            }
        }
    }
}
