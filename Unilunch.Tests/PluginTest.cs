﻿#region using directives

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnilunchData;

#endregion

namespace Unilunch.Tests
{
    [TestClass]
    public class PluginTest
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ItShouldThrowExceptionWhenInitializedWithNull()
        {
// ReSharper disable ObjectCreationAsStatement
            new Sonaatti(null);
// ReSharper restore ObjectCreationAsStatement
        }
    }
}