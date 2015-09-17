using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Given_A_StringCalculator
{
    [TestFixture]
    public class When_I_Call_Add_With_Empty_String
    {
        [Test]
        public void Then_The_Return_Value_Is_Zero()
        {
            const int expected = 0;
            var stringCalculator = new StringCalculator();

            int actual = stringCalculator.Add(string.Empty);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class When_I_Call_Add_With_One_Number
    {
        [Test]
        //[TestCase("A", 0), ExpectedException(typeof(FormatException))]
        [TestCase("10",10)]
        [TestCase("0", 0)]
        [TestCase("-9", -9)]
        public void Then_The_Return_Value_Is_Same_As_The_Input(string stringNumber, int expectedNumber)
        {
            var stringCalculator = new StringCalculator();


            int actual = stringCalculator.Add(stringNumber);

            Assert.That(actual, Is.EqualTo(expectedNumber));
        }
    }

    public class StringCalculator
    {
        public int Add(string number)
        {
            if (string.IsNullOrEmpty(number)) return 0;

            return Convert.ToInt32(number);

        }
    }
}
