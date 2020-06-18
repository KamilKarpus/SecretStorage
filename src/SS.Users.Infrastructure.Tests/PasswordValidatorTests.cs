using SS.Users.Infrastructure.Validators.CustomValidators;
using Xunit;

namespace SS.Users.Infrastructure.Tests
{
    public class PasswordValidatorTests
    {
        [Theory]
        [InlineData("Kamil123.",true)]
        [InlineData("test1234.", false)]
        [InlineData("Janussss", false)]
        [InlineData("jaNusz13w4e", false)]
        [InlineData("m.Ichal19", true)]
        [InlineData("    @1233", false)]
        [InlineData("Kamil@..13!", true)]
        public void ValidatPassword_Test(string password, bool isValid)
        {
            var validator = new PasswordValidator();
            var result = validator.ValidatePassword(password);
            Assert.Equal(isValid,result);
        }
    }
}
