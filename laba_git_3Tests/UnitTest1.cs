using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using laba_git_3;
using System.IO;
namespace laba_git_3Tests
{
    [TestClass]
    public class UnitTest1
    {
        public class Contracts
        {
            public interface IUrlManager
            {
                UrlList GetByUserGuid(Guid id);

            }

            public enum UrlList
            {
                First = 1,
                Second = 2,
            }
        }
        [TestMethod]
        public void input_20000_output_1969()
        {
            Shedule sa = new Shedule() { _summ = 20000,_persent = 32,_period= 12};
            Assert.AreEqual(Math.Round(sa._monthlyPay(), 2), 1969.47);
        }

        [TestMethod]
        public void input_1782_output_83()
        {
            Shedule sa = new Shedule() { _summ = 1782, _persent = 12, _period = 24 };
            Assert.AreEqual(Math.Round(sa._monthlyPay(), 2), 83.88);
        }

        [TestMethod]
        public void input_25000_Notoutput_2571()
        {
            Shedule sa = new Shedule() { _summ = 25000, _persent = 7, _period = 8 };
            Assert.AreNotEqual(Math.Round(sa._monthlyPay(), 2),2571.59);
        }
    }
}
