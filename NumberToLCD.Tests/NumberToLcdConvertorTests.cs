using System;
using System.Collections.Generic;
using System.Linq;
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

    public class Digit
    {
        public string[] Lines { get; }

        public static readonly Digit Zero = new Digit(" _ ",
                                                      "| |",
                                                      "|_|");

        public static readonly Digit One = new Digit("   ",
                                                     "  |",
                                                     "  |");

        public static readonly Digit Two = new Digit(" _ " ,
                                                     " _|" ,
                                                     "|_ ");

        public static readonly Digit Three = new Digit(" _ " ,
                                                       " _|" ,
                                                       " _|");

        public static readonly Digit Four = new Digit("   " ,
                                                      "|_|" ,
                                                      "  |");

        public static readonly Digit Five = new Digit(" _ "  ,
                                                      "|_ " ,
                                                      " _|"); 

        public static readonly Digit Six = new Digit(" _ "  ,
                                                     "|_ " ,
                                                     "|_|"); 

        public static readonly Digit Seven = new Digit(" _ "  ,
                                                       "  |" ,
                                                       "  |"); 

        public static readonly Digit Eight = new Digit(" _ "  ,
                                                       "|_|" ,
                                                       "|_|"); 

        public static readonly Digit Nine = new Digit(" _ "  ,
                                                      "|_|" ,
                                                      " _|");

        private Digit(params string[] lines)
        {
            Lines = lines;
        }
    }

    public class Digits
    {
        private readonly List<Digit> _digits = new List<Digit>();

        public static Digits operator +(Digits a, Digit b)
        {
            a._digits.Add(b);
            return a;
        }


        public override string ToString()
        {
            var line1 = new StringBuilder();
            var line2 = new StringBuilder();
            var line3 = new StringBuilder();

            foreach (var digit in _digits)
            {
                line1.Append(digit.Lines[0]);
                line2.Append(digit.Lines[1]);
                line3.Append(digit.Lines[2]);
            }

            return line1 + Environment.NewLine + line2 + Environment.NewLine + line3;
        }
    }

    public class NumberToLcdConvertor
    {
        public string Convert(int s)
        {
            var digits = s.ToString();

            var builder = new Digits();

            builder = digits.Select(c => (c switch
                {
                    '0' => Digit.Zero,
                    '1' => Digit.One,
                    '2' => Digit.Two,
                    '3' => Digit.Three,
                    '4' => Digit.Four,
                    '5' => Digit.Five,
                    '6' => Digit.Six,
                    '7' => Digit.Seven,
                    '8' => Digit.Eight,
                    '9' => Digit.Nine,
                    _ => throw null
                }))
                .Aggregate(builder, (current, stringDigit) => current + stringDigit);

            return builder.ToString();

        }
    }
}


