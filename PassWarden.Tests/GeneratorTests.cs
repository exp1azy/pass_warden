using PassWarden.Models;

namespace PassWarden.Tests
{
    public class GeneratorTests
    {
        private readonly PassWarden _passWarden = new();

        [Fact]
        public void GenerateRandomTest()
        {
            var password = _passWarden.Generator.GenerateRandom();
            Assert.NotNull(password);
        }

        [Fact]
        public void GenerateByRulesTest()
        {
            var rules = new PasswordGenerationRules
            {
                NumberOfDigits = 3,
                NumberOfLowercase = 3,
                NumberOfSpecialCharacters = 1,
            };
            
            var password = _passWarden.Generator.Generate(rules);
            Assert.NotNull(password);
            Assert.True(
                password.Where(char.IsDigit).Count() == rules.NumberOfDigits &&
                password.Where(char.IsLower).Count() == rules.NumberOfLowercase &&
                password.Where(char.IsUpper).Count() == rules.NumberOfUppercase &&
                password.Where(c => !char.IsLetterOrDigit(c)).Count() == rules.NumberOfSpecialCharacters);
        }

        [Fact]
        public void GenerateCamelTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.Camel);
            Assert.NotNull(password);
        }

        [Fact]
        public void GeneratePascalTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.Pascal);
            Assert.NotNull(password);
        }

        [Fact]
        public void GenerateSnakeTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.Snake);
            Assert.NotNull(password);
        }

        [Fact]
        public void GenerateScreamingSnakeTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.ScreamingSnake);
            Assert.NotNull(password);
        }

        [Fact]
        public void GenerateKebabTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.Kebab);
            Assert.NotNull(password);
        }

        [Fact]
        public void GenerateDotTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.Dot);
            Assert.NotNull(password);
        }

        [Fact]
        public void GenerateUpperTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.Upper);
            Assert.NotNull(password);
        }

        [Fact]
        public void GenerateLowerTest()
        {
            var password = _passWarden.Generator.GenerateMnemonic(Case.Lower);
            Assert.NotNull(password);
        }
    }
}
