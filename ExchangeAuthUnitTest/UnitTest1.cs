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

        [TestMethod]
        public void iniDemo1()
        {
            var i = new SharpUtils.IniReader("ini-reader-test.properties");
            Assert.AreEqual("Administrator@contoso.com", i.GetValue("login"));
        }

        [TestMethod]
        public void iniDemo2()
        {
            var i = new SharpUtils.IniReader("ini-reader-test.properties");
            Assert.AreEqual("", i.GetValue("commented"));
        }

        [TestMethod]
        public void iniDemo3()
        {
            var i = new SharpUtils.IniReader("ini-reader-test.properties");
            Assert.AreEqual("true", i.GetValue("isTopSecret", "TopSecret"));
            Assert.AreEqual("false", i.GetValue("isTopSecret"));
        }


        //[TestMethod]
        //public void TestMethod1()
        //{
        //    Exchange101.Service.ConnectToService(true);
        //}
    }
}
