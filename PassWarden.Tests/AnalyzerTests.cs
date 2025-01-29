using PassWarden.Models;

namespace PassWarden.Tests
{
    public class AnalyzerTests
    {
        private readonly PassWarden _passWarden = new();

        [Fact]
        public void PwnedTest()
        {
            string password = "test123";
            var result = _passWarden.Analyzer.IsPwned(password).GetAwaiter().GetResult();

            Assert.True(result);
        }

        [Fact]
        public void NotPwnedTest()
        {
            string password = "The-most-hardest-password!1029";
            var result = _passWarden.Analyzer.IsPwned(password).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]
        public void LowEntropyTest()
        {
            string password = "aaaaaa";
            var result = _passWarden.Analyzer.GetEntropy(password);

            Assert.True(result < 30);
        }

        [Fact]
        public void HighEntropyTest()
        {
            string password = "_QweRtY_ZXc60w21lkhh!";
            var result = _passWarden.Analyzer.GetEntropy(password);

            Assert.True(result > 100);
        }

        [Fact]
        public void SuperWeakStrengthTest()
        {
            string password = "123";
            var result = _passWarden.Analyzer.GetStrength(password);

            Assert.NotNull(result);
            Assert.Equal(PasswordGrade.SuperWeak, result.Grade);
        }

        [Fact]
        public void WeakStrengthTest()
        {
            string password = "abc123";
            var result = _passWarden.Analyzer.GetStrength(password);

            Assert.NotNull(result);
            Assert.Equal(PasswordGrade.Weak, result.Grade);
        }

        [Fact]
        public void RegularStrengthTest()
        {
            string password = "abc123!@#";
            var result = _passWarden.Analyzer.GetStrength(password);

            Assert.NotNull(result);
            Assert.Equal(PasswordGrade.Regular, result.Grade);
        }

        [Fact]
        public void StrongStrengthTest()
        {
            string password = "abc123!@#ABC";
            var result = _passWarden.Analyzer.GetStrength(password);

            Assert.NotNull(result);
            Assert.Equal(PasswordGrade.Strong, result.Grade);
        }

        [Fact]
        public void SuperStrongStrengthTest()
        {
            string password = "!0abc123!@#_QwerTY456!";
            var result = _passWarden.Analyzer.GetStrength(password);

            Assert.NotNull(result);
            Assert.Equal(PasswordGrade.SuperStrong, result.Grade);
        }

        [Fact]
        public void SymbolFrequencyMoreThanOneTest()
        {
            string password = "qwertyqaz11233";
            var result = _passWarden.Analyzer.GetSymbolFrequency(password);

            Assert.NotNull(result);
            Assert.True(
                result.GetValueOrDefault('q') == 2 && 
                result.GetValueOrDefault('1') == 2 && 
                result.GetValueOrDefault('3') == 2
            );
        }

        [Fact]
        public void GetSymbolFrequencyEqualsOneTest()
        {
            string password = "qwerty";
            var result = _passWarden.Analyzer.GetSymbolFrequency(password);

            Assert.NotNull(result);
            Assert.True(result.All((kv) => kv.Value == 1));
        }

        [Fact]
        public void RepeatingCharactersPatternTest()
        {
            string password = "bbbcccddd";
            var result = _passWarden.Analyzer.DetectPatterns(password);

            Assert.NotNull(result);
            Assert.Contains(Pattern.RepeatingCharacters, result);
        }

        [Fact]
        public void SequentialNumbersPatternTest()
        {
            string password = "123xyz";
            var result = _passWarden.Analyzer.DetectPatterns(password);

            Assert.NotNull(result);
            Assert.Contains(Pattern.SequentialNumbers, result);
        }

        [Fact]
        public void DateFormatPatternTest()
        {
            string password = "01012001qwerty";
            var result = _passWarden.Analyzer.DetectPatterns(password);

            Assert.NotNull(result);
            Assert.Contains(Pattern.DateFormat, result);
        }

        [Fact]
        public void AlternatingPatternTest()
        {
            var password = "abcababababdef";
            var result = _passWarden.Analyzer.DetectPatterns(password);

            Assert.NotNull(result);
            Assert.Contains(Pattern.AlternatingPattern, result);
        }

        [Fact]
        public void RepeatedPatternTest()
        {
            var password = "--**--**--**";
            var result = _passWarden.Analyzer.DetectPatterns(password);

            Assert.NotNull(result);
            Assert.Contains(Pattern.RepeatedPattern, result);
        }

        [Fact]
        public void SequentialLettersPatternTest()
        {
            var password = "ABCDE";
            var result = _passWarden.Analyzer.DetectPatterns(password);

            Assert.NotNull(result);
            Assert.Contains(Pattern.SequentialLetters, result);
        }

        [Fact]
        public void SimilarityLessThanFiftyTest()
        {
            string password1 = "test1234";
            string password2 = "text01";
            var result = _passWarden.Analyzer.GetPasswordSimilarity(password1, password2);

            Assert.True(result < 0.5d);
        }

        [Fact]
        public void SimilarityEqualsFiftyTest()
        {
            string password1 = "test1234";
            string password2 = "test";
            var result = _passWarden.Analyzer.GetPasswordSimilarity(password1, password2);

            Assert.Equal(0.5d, result);
        }

        [Fact]
        public void SimilarityMoreThanFiftyTest()
        {
            string password1 = "test1234";
            string password2 = "test123";
            var result = _passWarden.Analyzer.GetPasswordSimilarity(password1, password2);

            Assert.True(result > 0.5d);
        }

        [Fact]
        public void NotSimilarPasswordsTest()
        {
            string password1 = "1234";
            string password2 = "test1234";
            var result = _passWarden.Analyzer.GetPasswordSimilarity(password1, password2);

            Assert.Equal(0, result);
        }

        [Fact]
        public void SimilarPasswordsTest()
        {
            string password1 = "test1234";
            string password2 = "test1234";
            var result = _passWarden.Analyzer.GetPasswordSimilarity(password1, password2);

            Assert.Equal(1, result);
        }
    }
}
