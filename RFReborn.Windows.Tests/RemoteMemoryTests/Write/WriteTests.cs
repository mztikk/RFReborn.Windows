using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFReborn.Windows.Memory;

namespace RFReborn.Windows.Tests.RemoteMemoryTests.Write
{
    [TestClass]
    public unsafe class WriteTests : RemoteMemoryBaseTest
    {
        [DataTestMethod]
        [DataRow(1, 2)]
        [DataRow(10, 20)]
        [DataRow(100, 200)]
        [DataRow(0, 246234636)]
        [DataRow(-51851, 246234636)]
        [DataRow(-51851, -85418187)]
        [DataRow(0, -85418187)]
        [DataRow(int.MinValue, int.MaxValue)]
        [DataRow(int.MaxValue, int.MinValue)]
        public void WriteInt(int n1, int n2)
        {
            int test = n1;
            using (var memory = new RemoteMemory(GetProcess()))
            {
                Assert.AreEqual(n1, test);
                memory.Write(new IntPtr(&test), n2);
                Assert.AreNotEqual(n1, test);
                Assert.AreEqual(n2, test);
            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        [DataRow(10, 20)]
        [DataRow(100, 200)]
        [DataRow(0, 246234636)]
        [DataRow(-51851, 246234636)]
        [DataRow(-51851, -85418187)]
        [DataRow(0, -85418187)]
        [DataRow(long.MinValue, long.MaxValue)]
        [DataRow(long.MaxValue, long.MinValue)]
        public void WriteLong(long n1, long n2)
        {
            long test = n1;
            using (var memory = new RemoteMemory(GetProcess()))
            {
                Assert.AreEqual(n1, test);
                memory.Write(new IntPtr(&test), n2);
                Assert.AreNotEqual(n1, test);
                Assert.AreEqual(n2, test);
            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        [DataRow(10, 20)]
        [DataRow(100, 200)]
        [DataRow(0, 246234636)]
        [DataRow(-51851, 246234636)]
        [DataRow(-51851, -85418187)]
        [DataRow(0, -85418187)]
        [DataRow(818.81821f, 36.5650106f)]
        [DataRow(748794.8941f, 12.85484f)]
        [DataRow(float.MinValue, float.MaxValue)]
        [DataRow(float.MaxValue, float.MinValue)]
        public void WriteFloat(float n1, float n2)
        {
            float test = n1;
            using (var memory = new RemoteMemory(GetProcess()))
            {
                Assert.AreEqual(n1, test);
                memory.Write(new IntPtr(&test), n2);
                Assert.AreNotEqual(n1, test);
                Assert.AreEqual(n2, test);
            }
        }

        [DataTestMethod]
        [DataRow((short)1, (short)2)]
        [DataRow((short)10, (short)20)]
        [DataRow((short)100, (short)200)]
        [DataRow((short)0, (short)2423)]
        [DataRow((short)-5851, (short)2424)]
        [DataRow((short)-5181, (short)-8541)]
        [DataRow((short)0, (short)-8547)]
        [DataRow(short.MinValue, short.MaxValue)]
        [DataRow(short.MaxValue, short.MinValue)]
        public void WriteShort(short n1, short n2)
        {
            short test = n1;
            using (var memory = new RemoteMemory(GetProcess()))
            {
                Assert.AreEqual(n1, test);
                memory.Write(new IntPtr(&test), n2);
                Assert.AreNotEqual(n1, test);
                Assert.AreEqual(n2, test);
            }
        }
    }
}
