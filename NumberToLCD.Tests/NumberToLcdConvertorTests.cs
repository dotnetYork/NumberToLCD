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

        [Fact]
        public void ShouldReturnAllDigits()
        {
            var expected =
                           "    _  _     _  _  _  _  _ " + Environment.NewLine +
                           "  | _| _||_||_ |_   ||_||_|" + Environment.NewLine +
                           "  ||_  _|  | _||_|  ||_| _|";

            var actual = _convertor.Convert(123456789);

            actual.Should().Be(expected);
        }

        [Fact]
        public void ShouldReturnDigitAtCorrectSize()
        {
            var expected =
                 " ___ " + Environment.NewLine +
                 "    |" + Environment.NewLine +
                 " ___|" + Environment.NewLine +
                 "|    " + Environment.NewLine +
                 "|___ ";

            var actual = _convertor.Convert(2, 3, 2);

            actual.Should().Be(expected);
        }

        [Fact]
        public void ShouldReturnMultipleDigitsAtCorrectSize()
        {
            var expected =
                 " ____  ____ " + Environment.NewLine +
                 "     |     |" + Environment.NewLine +
                 "     |     |" + Environment.NewLine +
                 " ____| ____|" + Environment.NewLine +
                 "|          |" + Environment.NewLine +
                 "|          |" + Environment.NewLine +
                 "|____  ____|";

            var actual = _convertor.Convert(23, 4, 3);

            actual.Should().Be(expected);
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

        public static readonly Digit Two = new Digit(" _ ",
                                                     " _|",
                                                     "|_ ");

        public static readonly Digit Three = new Digit(" _ ",
                                                       " _|",
                                                       " _|");

        public static readonly Digit Four = new Digit("   ",
                                                      "|_|",
                                                      "  |");

        public static readonly Digit Five = new Digit(" _ ",
                                                      "|_ ",
                                                      " _|");

        public static readonly Digit Six = new Digit(" _ ",
                                                     "|_ ",
                                                     "|_|");

        public static readonly Digit Seven = new Digit(" _ ",
                                                       "  |",
                                                       "  |");

        public static readonly Digit Eight = new Digit(" _ ",
                                                       "|_|",
                                                       "|_|");

        public static readonly Digit Nine = new Digit(" _ ",
                                                      "|_|",
                                                      " _|");

        private Digit(params string[] lines)
        {
            Lines = lines;
        }
    }

    public class Digits
    {
        private readonly List<Digit> _digits = new List<Digit>();

        private int _width;
        private int _height;

        public Digits(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public static Digits operator +(Digits a, Digit b)
        {
            a._digits.Add(b);
            return a;
        }

        public override string ToString()
        {
            var lines = new StringBuilder[_height * 2 + 1];

            for (int l = 0; l < lines.Length; l++)
                lines[l] = new StringBuilder();


            foreach (var digit in _digits)
            {
                var digitLine1 = digit.Lines[0];
                var digitLine2 = digit.Lines[1];
                var digitLine3 = digit.Lines[2];

                digitLine1 = digitLine1[0] + new string(digitLine1[1], _width) + digitLine1[2]; ;
                
                digitLine2 = digitLine2[0] + new string(digitLine2[1], _width) + digitLine2[2]; ;
                var digitLine2a = digitLine2.Replace('_', ' ');

                digitLine3 = digitLine3[0] + new string(digitLine3[1], _width) + digitLine3[2]; ;
                var digitLine3a = digitLine3.Replace('_', ' ');

                int lineRow = 0;
                lines[lineRow++].Append(digitLine1);
                for (int row=0; row < _height - 1; row++)
                {
                    lines[lineRow++].Append(digitLine2a);
                }
                lines[lineRow++].Append(digitLine2);
                for (int row=0; row < _height - 1; row++)
                {
                    lines[lineRow++].Append(digitLine3a);
                }
                lines[lineRow].Append(digitLine3);
            }

            return string.Join(Environment.NewLine, lines.Select(s=>s.ToString()));
        
        }

    

    }

    public class NumberToLcdConvertor
    {
        public string Convert(int s, int width = 1, int height = 1)
        {
            return s.ToString().Select(c => (c switch
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
                .Aggregate(new Digits(width, height), (current, stringDigit) => current + stringDigit)
                .ToString();

        }

    }
}


