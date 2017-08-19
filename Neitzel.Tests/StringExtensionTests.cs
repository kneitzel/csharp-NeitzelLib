using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neitzel.Tests
{
    /// <summary>
    /// Tests the StringExtension class.
    /// </summary>
    [TestClass]
    public class StringExtensionTests
    {
        /// <summary>
        /// Tests the StringExtensions.Contains() method.
        /// </summary>
        [TestMethod]
        public void TestContains()
        {
            Assert.IsTrue("abcde".Contains(new [] {'h','i','d','z'}));
            Assert.IsFalse("abcd".Contains(new [] {'x', 'y', 'z'}));
        }

        /// <summary>
        /// Tests the StringExtensions.FirstIndexOf method.
        /// </summary>
        [TestMethod]
        public void TestFirstIndexOf()
        {
            Assert.AreEqual(-1, "abcd".FirstIndexOf(new[] {'x', 'y', 'z'}));
            Assert.AreEqual(2, "abcd".FirstIndexOf(new[] {'x', 'y', 'c'}));
        }
    }
}
