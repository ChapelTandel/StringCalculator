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
        [TestCase("A",0, ExpectedException = typeof(FormatException))]
        [TestCase("10",10)]
        [TestCase("0", 0)]
        public void Then_The_Return_Value_Is_Same_As_The_Input(string stringNumber, int expectedNumber)
        {
            var stringCalculator = new StringCalculator();

            int actualResult = stringCalculator.Add(stringNumber);

            Assert.That(actualResult, Is.EqualTo(expectedNumber));
        }
    }


    [TestFixture]
    public class When_I_Call_Add_With_Two_Or_More_Number
    {
        [TestCase("10,20",30)]
        [TestCase("5,10,20",35)]
        [TestCase("5,10,20,3,5,4,2,7,8,1,33,2,6,5,4,3",118)]
        [TestCase("abc,34,34,23", 91, ExpectedException = typeof(FormatException))]
        public void Then_The_Result_Is_Sum_Of_All_Numbers(string inputString, int resultNumber)
        {
            var stringCalculator= new StringCalculator();

            int actualResult = stringCalculator.Add(inputString);

            Assert.That(actualResult, Is.EqualTo(resultNumber));
        }
    }

    [TestFixture]
    public class When_I_Call_Add_With_A_String_Containing_New_Line_Character
    {
        [TestCase("10\n20", 30)]
        [TestCase("10\n20,3", 33)]
        public void Then_The_Result_Ignores_New_Line_Characters(string inputString, int expectedResult)
        {
            var stringCalculator = new StringCalculator();
            int actualResult = stringCalculator.Add(inputString);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }

    [TestFixture]
    public class When_I_Call_Add_With_Different_Delemiter
    {
        [TestCase("//;\n1;2",3)]

        public void Then_The_Result_Considers_Different_Delemeter(string inputString, int expectedResult)
        {
            var stringCalculator= new StringCalculator();
            int actualResult= stringCalculator.Add(inputString);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }

    [TestFixture]
    public class When_I_Call_Add_With_Negative_Numbers
    {
        [TestCase("10,-20,30",20,ExpectedException = typeof(Exception), ExpectedMessage = "-20")]
        [TestCase("-10,-20,30",20,ExpectedException = typeof(Exception), ExpectedMessage = "-10 -20")]
        [TestCase("10,-20,30, -2",20,ExpectedException = typeof(Exception), ExpectedMessage = "-20 -2")]
        public void Then_Nagative_Not_Allowed_Expection_Is_Thrown_And_Negative_Numbers_Are_Listed(string inputString, int expectedResult)
        {
            var stringCalculator = new StringCalculator();
            int result = stringCalculator.Add(inputString);
            Assert.That(result,Is.EqualTo(expectedResult));
        }
    }

    public class StringCalculator
    {
        public int Add(string number)
        {
            if (number ==string.Empty)return 0;

            if (number.StartsWith("//"))
            {
                var delemeter = number.Substring(2, number.IndexOf("\n") - 2);
                number = number.Replace(delemeter, ",");
                number = number.Substring(number.IndexOf("\n")+1);
            }

            var list = number.Split(',', '\n').Select(s => Convert.ToInt32(s)).ToList();

            string exceptionMessage = null;
            var negatives = (from x in list where x < 0 select x.ToString()).ToList();
            exceptionMessage = negatives.Aggregate(exceptionMessage, (current, negative) => current + " " + negative);
            if(exceptionMessage != null) throw new Exception(exceptionMessage.Trim());

            return list.Sum();
        }
    }

}
