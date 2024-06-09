using EnovationApp.DataAccess.Models;
using EnovationApp.Domain.Enums;
using EnovationAssignment.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EnovationAssignment.Tests
{
    public class AnimalValidationTest
    {

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(10, true)]
        [InlineData(100, true)]
        [InlineData(101, false)]
        public void ShouldHaveErrorWhenNameToLongOrToShortTest(int length, bool good)
        {
            var validator = new AnimalValidator();
            var Animal = new AnimalDto { Id = 0, Name = RandomString(length), Legs = 1, Age = 3, Type=AnimalEnum.Dog };

            Assert.Equal(good, validator.Validate(Animal).IsValid);
        }
        [Theory]
        [InlineData(-101, false)]
        [InlineData(0, false)]
        [InlineData(10, true)]
        [InlineData(100, true)]
        public void ShouldHaveErrorWhenAgeLessThanZeroTest(int age, bool good)
        {
            var validator = new AnimalValidator();
            var Animal = new AnimalDto { Id = 0, Name = "Leo", Legs = 1, Age = age, Type = AnimalEnum.Dog };

            Assert.Equal(good, validator.Validate(Animal).IsValid);
        }
        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(10, true)]
        [InlineData(17, false)]
        public void ShouldHaveErrorWhenLegsLessThanZeroOrGreaterThanSixteenTest(int legs, bool good)
        {
            var validator = new AnimalValidator();
            var Animal = new AnimalDto { Id = 0, Name = "Leo", Legs = legs, Age = 4, Type = AnimalEnum.Dog };

            Assert.Equal(good, validator.Validate(Animal).IsValid);
        }
        [Theory]
        [InlineData(AnimalEnum.None, false)]
        [InlineData(AnimalEnum.Dog, true)]
        [InlineData(AnimalEnum.Cow, true)]
        [InlineData(AnimalEnum.Cat, true)]
        public void ShouldHaveErrorWhenTypeIsNoneTest(AnimalEnum type, bool good)
        {
            var validator = new AnimalValidator();
            var Animal = new AnimalDto { Id = 0, Name = "Leo", Legs = 4, Age = 4, Type = type };

            Assert.Equal(good, validator.Validate(Animal).IsValid);
        }
    }
}
