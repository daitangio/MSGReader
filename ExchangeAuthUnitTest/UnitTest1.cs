using System;
using SharpUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeAuthUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void testEnv()
        {
            System.Environment.SetEnvironmentVariable("APP_DUMMY_EVA_DEBUG_MODE", "");
            Assert.AreEqual("C:\\Windows", System.Environment.GetEnvironmentVariable("windir"));
            Assert.IsFalse( Env.getInstance("DUMMY_EVA").isDebugEnabled());
            System.Environment.SetEnvironmentVariable("APP_DUMMY_EVA_DEBUG_MODE", "true");
            Assert.IsTrue(Env.getInstance("DUMMY_EVA").isDebugEnabled());
        }

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    Exchange101.Service.ConnectToService(true);
        //}
    }
}
