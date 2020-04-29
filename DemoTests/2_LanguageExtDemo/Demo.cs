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
                return (onlyPositive(a), onlyPositive(b), onlyPositive(c)).Apply(sum);
            }

            // Act
            var result = AddNumbers(1, 2, 3);

            // Assert
            result.ShouldBeSuccess(x => x.Should().Be(6));
        }

        [Fact]
        public void Sum_validation_OneError()
        {
            // Arrange
            Func<int, int, int, int> sum = (a, b, c) => a + b + c;

            Func<int, Validation<Error, int>> onlyPositive = i
                => i > 0
                    ? Success<Error, int>(i)
                    : Fail<Error, int>(Error.New("ups"));

            Validation<Error, int> AddNumbers(int a, int b, int c)
            {
                return (onlyPositive(a), onlyPositive(b), onlyPositive(c)).Apply(sum);
            }

            // Act
            var result = AddNumbers(-1, 2, 3);

            // Assert
            result.ShouldBeFail(x => x.Should().BeEquivalentTo(Error.New("ups")));
        }

        [Fact]
        public void Sum_validation_ThreeErrors()
        {
            var error = Error.New("ups");

            // Arrange
            Func<int, int, int, int> sum = (a, b, c) => a + b + c;

            Func<int, Validation<Error, int>> onlyPositive = i
                => i > 0
                    ? Success<Error, int>(i)
                    : Fail<Error, int>(error);

            Validation<Error, int> AddNumbers(int a, int b, int c)
            {
                return (onlyPositive(a), onlyPositive(b), onlyPositive(c)).Apply(sum);
            }

            // Act
            var result = AddNumbers(-1, -2, -3);

            // Assert
            result.ShouldBeFail(x 
                => x.Should()
                    .BeEquivalentTo(new System.Collections.Generic.List<Error>{ error, error, error }));
        }
    }

    public class Error : NewType<Error, string>
    {
        public Error(string e) : base(e)
        {
        }
    }
}