using System;
using System.Text;
using FluentAssertions;
using Xunit;

namespace NumberToLCD.Tests
{
    public class NumberToLcdConvertorTests
    {
        private readonly NumberToLcdConvertor _convertor;

        public NumberToLcdConvertorTests()
        {
            
            _convertor = new NumberToLcdConvertor();
        }



        [Theory]
        [InlineData(0, " _ ",
                       "| |",
                       "|_|")]
        [InlineData(1, "   ",
                       "  |",
                       "  |")]
        [InlineData(2, " _ ",
                       " _|",
                       "|_ ")]
        [InlineData(3, " _ ",
                       " _|",
                       " _|")]
        [InlineData(4, "   ",
                       "|_|",
                       "  |")]
        [InlineData(5, " _ ",
                       "|_ ",
                       " _|")]
        [InlineData(6, " _ ",
                       "|_ ",
                       "|_|")]
        [InlineData(7, " _ ",
                       "  |",
                       "  |")]
        [InlineData(8, " _ ",
                       "|_|",
                       "|_|")]
        [InlineData(9, " _ ",
                       "|_|",
                       " _|")]
        public void ShouldBeAbleToConvertASingleDigitConvertor(int digit, string line1, string line2, string line3)
        {
            var actual = _convertor.Convert(digit);

            actual.Should().Be(
                line1 + Environment.NewLine +
                line2 + Environment.NewLine +
                line3);
        }

        [Theory]
        [InlineData(123, "    _  _ ",
                         "  | _| _|",
                         "  ||_  _|")]
        public void ShouldReturnThreeDigitsWhenNumberIsBetween100and999(int digit, string line1, string line2, string line3)
        {
            var actual = _convertor.Convert(digit);

            actual.Should().Be(
                line1 + Environment.NewLine +
                line2 + Environment.NewLine +
                line3);
        }

        [Theory]
        [InlineData(11, "      ",
               "  |  |",
               "  |  |")]
        [InlineData(12, "    _ ",
               "  | _|",
               "  ||_ ")]
        public void ShouldReturnTwoDigitsWhenNumberIsBetween10and99(int digit, string line1, string line2, string line3)
        {
            var actual = _convertor.Convert(digit);

            actual.Should().Be(
                line1 + Environment.NewLine +
                line2 + Environment.NewLine +
                line3);
        }
    }

    public class StringDigits
    {
        public static readonly string Zero = " _ " + Environment.NewLine +
                                             "| |" + Environment.NewLine +
                                             "|_|";

        public static readonly string One = "   " + Environment.NewLine +
                                            "  |" + Environment.NewLine +
                                            "  |";

        public static readonly string Two = " _ " + Environment.NewLine +
                                            " _|" + Environment.NewLine +
                                            "|_ ";

        public static readonly string Three = " _ " + Environment.NewLine +
                                              " _|" + Environment.NewLine +
                                              " _|";

        public static readonly string Four = "   " + Environment.NewLine +
                                             "|_|" + Environment.NewLine +
                                             "  |";

        public static readonly string Five =" _ "  + Environment.NewLine +
                                            "|_ " + Environment.NewLine +
                                            " _|"; 

        public static readonly string Six = " _ "  + Environment.NewLine +
                                            "|_ " + Environment.NewLine +
                                            "|_|"; 

        public static readonly string Seven = " _ "  + Environment.NewLine +
                                              "  |" + Environment.NewLine +
                                              "  |"; 

        public static readonly string Eight = " _ "  + Environment.NewLine +
                                              "|_|" + Environment.NewLine +
                                              "|_|"; 

        public static readonly string Nine =  " _ "  + Environment.NewLine +
                                              "|_|" + Environment.NewLine +
                                              " _|"; 
        //public static readonly string Zero = "abdefg";
        //public static readonly string One = "dg";
        //public static readonly string Two = "adcef";
        //public static readonly string Three = "adcgf";
        //public static readonly string Four = "bcdg"
    }

    public class NumberToLcdConvertor
    {
        public string Convert(int s)
        {
            var digits = s.ToString();
            var line1 = new StringBuilder();
            var line2 = new StringBuilder();
            var line3 = new StringBuilder();

            foreach (var c in digits)
            {
                var stringDigit = (c switch
                {
                    '0' => StringDigits.Zero,
                    '1' => StringDigits.One,
                    '2' => StringDigits.Two,
                    '3' => StringDigits.Three,
                    '4' => StringDigits.Four,
                    '5' => StringDigits.Five,
                    '6' => StringDigits.Six,
                    '7' => StringDigits.Seven,
                    '8' => StringDigits.Eight,
                    '9' => StringDigits.Nine,
                    _ => throw null
                }).Split(Environment.NewLine);

                line1.Append(stringDigit[0]);
                line2.Append(stringDigit[1]);
                line3.Append(stringDigit[2]);
            }
                
            return line1 + Environment.NewLine + line2 + Environment.NewLine + line3;
            
        }
    }
}


