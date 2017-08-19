using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neitzel.Tests
{
    /// <summary>
    /// Tests the exception extensions.
    /// </summary>
    [TestClass]
    public class ExceptionExtensionsTest
    {
        /// <summary>
        /// Tests the ExceptionExtensions.GetFullMessage method.
        /// </summary>
        [TestMethod]
        public void GetFullMessageTest()
        {
            var culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            try
            {
                try
                {
                    throw new Exception("msg1");
                }
                catch (Exception ex)
                {
                    throw new Exception("msg2", ex);
                }
            }
            catch (Exception ex)
            {
                var message = ex.GetFullMessage();
                var pattern = @"msg2    at Neitzel\.Tests\.ExceptionExtensionsTest\.GetFullMessageTest\(\) in .*\\Source\\v4.6.1\\Neitzel\.Tests\\ExceptionExtensionsTest\.cs:line [0-9]* \(msg1    at Neitzel\.Tests\.ExceptionExtensionsTest\.GetFullMessageTest\(\) in .*\\Source\\v4\.6\.1\\Neitzel\.Tests\\ExceptionExtensionsTest\.cs:line [0-9]*\)";
                Assert.IsTrue(Regex.IsMatch(message, pattern));
            }
        }
    }
}
