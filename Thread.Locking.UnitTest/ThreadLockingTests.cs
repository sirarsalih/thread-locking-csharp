using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Thread.Locking.UnitTest
{
    [TestClass]
    public class ThreadLockingTests
    {
        [TestMethod]
        public void Executable_Process_Is_Thread_Safe()
        {
            const string executablePath = "Thread.Locking.exe";
            for (var i = 0; i < 1000; i++) {
                var process = new Process() {StartInfo = {FileName = executablePath}};
                process.Start();
                process.WaitForExit();
                if (process.ExitCode == 1) {
                    Assert.Fail();
                }
            }
        }
    }
}