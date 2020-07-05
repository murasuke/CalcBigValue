using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalcBigValue;
using System.Collections.Generic;
using System;

namespace CalcTest
{
    [TestClass]
    public class UnitTest1
    {
        public Func<string, List<int>> _stb;

        public Func<List<int>, string> _bts;

        [TestInitialize]
        public void Setup()
        {
            this._stb = BigintCalculator.StringToBigint;
            this._bts = BigintCalculator.BigintToString;
        }

        [TestMethod]
        public void StrToIntTest()
        {
            var bigval = "123456789012345";
            var bigint = _stb(bigval);
            var actual = _bts(bigint);

            Assert.AreEqual(bigval, actual, "ãêëÂÇ»êîílïœä∑");
        }



        [TestMethod]
        public void AddTest()
        {

            var add1 = _bts(BigintCalculator.Addition(_stb("1234"), _stb("6420")));
            Assert.AreEqual(add1, "7654", "1234+6421");

            var add2 = _bts(BigintCalculator.Addition(_stb("99999999999999999"), _stb("1")));
            Assert.AreEqual(add2, "100000000000000000", "99999999999999999+1");
        }

        [TestMethod]
        public void SubTest()
        {
            var sub1 = _bts(BigintCalculator.Subtraction(_stb("6420"), _stb("1234")));
            Assert.AreEqual(sub1, "5186", "6420-1234");

            var sub2 = _bts(BigintCalculator.Subtraction(_stb("1000000000000000000"), _stb("1")));
            Assert.AreEqual(sub2, "999999999999999999", "1000000000000000000-1");

            var sub3 = _bts(BigintCalculator.Subtraction(_stb("1000000000000000000"), _stb("1000000000000000000")));
            Assert.AreEqual(sub3, "0", "1000000000000000000-1000000000000000000");
        }

        [TestMethod]
        public void CarryTest()
        {

        }
    }

}

