using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;
using System.Collections.Generic;

namespace Unilunch.Tests
{

    [TestClass]
    public class PluginTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ItShouldThrowExceptionWhenInitializedWithNull()
        {
            new Sonaatti(null);
        }

    }
}
