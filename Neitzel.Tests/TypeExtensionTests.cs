using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Neitzel.Tests
{
    /// <summary>
    /// Tests for TypeExtensions
    /// </summary>
    [TestClass]
    public class TypeExtensionTests
    {
        /// <summary>
        /// Tests the TypeExtensions.CreateInstance method.
        /// </summary>
        [TestMethod]
        public void CreateInstanceTest()
        {
            Assert.IsNotNull(TypeExtensions.CreateInstance<MemoryStream>(typeof(MemoryStream).FullName));
            Assert.IsNotNull(TypeExtensions.CreateInstance<Dictionary<String, String>>(typeof(Dictionary<String, String>).FullName));
        }
    }
}
