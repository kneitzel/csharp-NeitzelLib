using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neitzel.Win32;

namespace Neitzel.Tests
{
    [TestClass]
    public class Win32Tests
    {
        public const string Win32ExampleCommand = "Neitzel.Win32.Example.exe";
        public const string WindowName = "Neitzel.Win32.Example";

        [TestMethod]
        [DeploymentItem("Resources\\" + Win32ExampleCommand)]
        public void Win32TestsWithExampleWindow()
        {
            // Start the Process.
            var process = Process.Start(Win32ExampleCommand);
            Thread.Sleep(1000);
            Assert.IsFalse(process.HasExited);

            // Get the main window through process
            Win32Window window = new Win32Window(process.MainWindowHandle);
            Assert.AreNotEqual(IntPtr.Zero, window.Handle);

            var searchTest = Win32Window.FindWindow("#32770", WindowName);
            Assert.AreNotEqual(IntPtr.Zero, searchTest.Handle);

            // Get the test button
            Win32Button testButton = Win32Button.FindButton(window.Handle, "TestButton");
            Assert.AreNotEqual(IntPtr.Zero, testButton.Handle);
            Assert.AreEqual("TestButton", testButton.Text);
            testButton.Click();
            Assert.AreNotEqual("TestButton", testButton.Text);

            // Get the ok button.
            Win32Button okButton = Win32Button.FindButton(window.Handle, "OK");
            Assert.AreNotEqual(IntPtr.Zero, okButton.Handle);
            Thread.Sleep(100);
            okButton.Click();

            // Check that the process ended.
            Thread.Sleep(100);
            Assert.IsTrue(process.HasExited);
        }
    }
}
