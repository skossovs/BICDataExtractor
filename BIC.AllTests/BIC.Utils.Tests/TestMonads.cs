using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Utils.Monads;
using BIC.Utils;
namespace BIC.Utils.Tests
{
    [TestClass]
    public class TestMonads
    {
        [TestMethod]
        public void RefAndStructMaybeTryNullString()
        {
            int? i1 = 1;
            int? iNull = null;

            var iResult1     = i1.Map(i => i);
            var iResultNulll = iNull.Map(i => i);

            string s2 = "2";
            string sNull = null;

            var sResult2    = s2   .MapClassToNullable(s => new int?(Convert.ToInt32(s)));
            var sResultNull = sNull.MapClassToNullable(s => new int?(Convert.ToInt32(s)));

            Assert.IsTrue(sResult2.HasValue);
            Assert.IsTrue(sResult2 == 2);
            Assert.IsTrue(!sResultNull.HasValue);
        }

        [TestMethod]
        public void RefAndStructMaybeTryEmptyString()
        {
            string sEmpty = string.Empty;
            var sResultEmpty = sEmpty.MapClassToNullable(s => s.IsNullOrEmtpy() ? null : new int?(Convert.ToInt32(s)));
            Assert.IsTrue(!sResultEmpty.HasValue);
        }

        [TestMethod]
        public void RefAndStructMaybeTryNumberString()
        {
            string sValue = "0.15";
            var sResult = sValue.MapClassToNullable(s => s.IsNullOrEmtpy() ? null : new Decimal?(Convert.ToDecimal(s)));
            Assert.IsTrue(sResult.HasValue);
            Assert.AreEqual((Decimal)0.15, sResult);
        }
    }
}
