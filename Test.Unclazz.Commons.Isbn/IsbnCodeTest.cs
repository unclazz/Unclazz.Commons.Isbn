﻿using Unclazz.Commons.Isbn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Unclazz.Commons.Isbn
{
    [TestFixture]
    public class IsbnCodeTest
    {
        [Test]
        public void Parse_WhenStringPresentsValidIsbnCode_ReturnsInstance()
        {
            // Arrange
            // Act
            var isbn = IsbnCode.Parse("978-4-650-01092-3");

            // Assert
            Assert.That(isbn.Flag, Is.EqualTo(Digits.Of("978")));
            Assert.That(isbn.Group, Is.EqualTo(Digits.Of("4")));
            Assert.That(isbn.Publisher, Is.EqualTo(Digits.Of("650")));
            Assert.That(isbn.Title, Is.EqualTo(Digits.Of("01092")));
            Assert.That(isbn.ToString(), Is.EqualTo("ISBN-13 978-4-650-01092-3"));
            Assert.That(isbn.CheckDigit, Is.EqualTo('3'));
        }

        [Test]
        public void Parse_WhenStringPresentsValidIsbnCode_ReturnsInstance_2()
        {
            // Arrange
            // Act
            var isbn = IsbnCode.Parse("979-10-19-01092-3"); // fictional code

            // Assert
            Assert.That(isbn.Flag, Is.EqualTo(Digits.Of("979")));
            Assert.That(isbn.Group, Is.EqualTo(Digits.Of("10")));
            Assert.That(isbn.Publisher, Is.EqualTo(Digits.Of("19")));
            Assert.That(isbn.Title, Is.EqualTo(Digits.Of("01092")));
            Assert.That(isbn.ToString(), Is.EqualTo("ISBN-13 979-10-19-01092-3"));
            Assert.That(isbn.CheckDigit, Is.EqualTo('3'));
        }

        [TestCase("978-4-650-01092-3")]
        [TestCase("978-2707318251")]
        [TestCase("9782707318251")]
        [TestCase("979-10-19-01092-3")] // fictional code
        [TestCase("97910-19-01092-3")] // fictional code
        public void TryParse_WhenStringRepresentsValidIsbnCode_ReturnsTrue(string s)
        {
            // Arrange
            // Act
            IsbnCode isbn;
            bool result = IsbnCode.TryParse(s, out isbn);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(isbn, Is.EqualTo(IsbnCode.Parse(s)));
        }

        [TestCase("978-4-650-0109X-3")]
        [TestCase("978-4-650-010921-3")]
        [TestCase("978-4-650-0109-3")]
        [TestCase("979-10-50-0109X-3")]
        [TestCase("979-10-50-010921-3")]
        [TestCase("979-10-50-0109-3")]
        public void TryParse_WhenStringDoesNotRepresentValidIsbnCode_ReturnsFalse(string s)
        {
            // Arrange
            // Act
            IsbnCode isbn;
            bool result = IsbnCode.TryParse(s, out isbn);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(isbn, Is.Null);
        }

        [TestCase("978-4-650-01092-3", "ISBN978-4-650-01092-3", ExpectedResult = true)]
        [TestCase("ISBN-13 9782707318251", "9782707318251", ExpectedResult = true)]
        [TestCase("ISBN 979-10-19-01092-3", "979-10-19-01092-3", ExpectedResult = true)] // fictional code
        [TestCase("978-4-650-01092-3", "978-4-650-01093-3", ExpectedResult = false)]
        [TestCase("ISBN-13 9782707318251", "ISBN-13 9782707318261", ExpectedResult = false)]
        [TestCase("979-10-19-01092-3", "979-10-19-01093-3", ExpectedResult = false)] // fictional code
        [TestCase("978-4-650-01093-3", null, ExpectedResult = false)]
        public bool Equals_ComparesTwoInstances_BasedOnThoseValues(string v0, string v1)
        {
            return (v0 == null ? null : IsbnCode.Parse(v0))
                == (v1 == null ? null : IsbnCode.Parse(v1));
        }

        [TestCase("978-4-650-01092-3", "ISBN978-4-650-01092-3", ExpectedResult = true)]
        [TestCase("ISBN-13 9782707318251", "9782707318251", ExpectedResult = true)]
        [TestCase("ISBN 979-10-19-01092-3", "979-10-19-01092-3", ExpectedResult = true)] // fictional code
        [TestCase("978-4-650-01092-3", "978-4-650-01093-3", ExpectedResult = false)]
        [TestCase("ISBN-13 9782707318251", "ISBN-13 9782707318261", ExpectedResult = false)]
        [TestCase("979-10-19-01092-3", "979-10-19-01093-3", ExpectedResult = false)] // fictional code
        [TestCase(null, "978-4-650-01093-3", ExpectedResult = false)]
        [TestCase("978-4-650-01093-3", null, ExpectedResult = false)]
        public bool OperatorEqualEqual_ComparesTwoInstances_BasedOnThoseValues(string v0, string v1)
        {
            return (v0 == null ? null : IsbnCode.Parse(v0))
                == (v1 == null ? null : IsbnCode.Parse(v1));
        }
    }
}
