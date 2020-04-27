using System;
using FluentAssertions;
using LanguageExt;
using LanguageExt.UnitTesting;
using Xunit;
using static LanguageExt.Prelude;

namespace DemoTests._2_LanguageExtDemo
{
    public class Demo
    {
        [Fact]
        public void Sum_validation()
        {
            // Arrange
            Func<int, int, int, int> sum = (a, b, c) => a + b + c;

            Func<int, Validation<Error, int>> onlyPositive = i
                => i > 0
                    ? Success<Error, int>(i)
                    : Fail<Error, int>(Error.New("ups"));

            Validation<Error, int> AddNumbers(int a, int b, int c)
            {
                // how to lift `sum` into an applicative which other functions
                // can apply to??
                return null;
            }


            // Act
            var result = AddNumbers(1, 2, 3);

            // Assert
            result.ShouldBeSuccess(x => x.Should().Be(6));
        }
    }

    public class Error : NewType<Error, string>
    {
        public Error(string e) : base(e)
        {
        }
    }
}