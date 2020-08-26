using System.Diagnostics;
using System.Text;

namespace RFReborn.Windows.Tests.RemoteMemoryTests
{
    public class RemoteMemoryBaseTest
    {
        public Process GetProcess() => Process.GetCurrentProcess();
        // C# uses Unicode / UTF-16
        public Encoding GetEncoding() => Encoding.Unicode;
    }
}
