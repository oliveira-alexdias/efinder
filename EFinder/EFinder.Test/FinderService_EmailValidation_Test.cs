using System;
using System.Linq;
using System.Threading.Tasks;
using EFinder.Service.Interfaces;
using EFinder.Service.Models;
using EFinder.Service.Services;
using Moq;
using Xunit;

namespace EFinder.Test
{
    public class FinderService_EmailValidation_Test
    {
        [Theory]
        [InlineData("Henry", "Traverso", "stackoverflow.com", "htraverso@stackoverflow.com")]
        [InlineData("Dale", "Cook", "stackoverflow.com", "dcook@stackoverflow.com")]
        [InlineData("Amy", "Roberts", "stackoverflow.com", "aroberts@stackoverflow.com")]
        [InlineData("David", "Haney", "stackoverflow.com", "dhaney@stackoverflow.com")]
        [InlineData("Michael", "Foree", "stackoverflow.com", "mforee@stackoverflow.com")]
        public async Task FindValidEmail_Should_Find_One_Valid_Email(string firstName, string lastName, string domain, string expected)
        {
            // Arrange
            var mockMx = new Mock<IMxService>();
            var mockEmail = new Mock<IEmailService>();
            var finderServiceMocked = new FinderService(mockEmail.Object, mockMx.Object);

            mockMx.Setup(x => x.GetMailServerBasedOnDomain(It.IsAny<string>())).Returns(string.Empty);
            mockEmail.Setup(x => x.EmailIsValid(
                                It.Is<string>(a => a.Equals(expected, StringComparison.CurrentCultureIgnoreCase)),
                                It.IsAny<string>()))
                                .ReturnsAsync(true);
            
            // Act
            var actual = await finderServiceMocked.FindValidEmail(firstName, lastName, domain);

            // Assert
            Assert.Equal(expected, actual.FirstOrDefault(), ignoreCase: true);
        }

        [Theory]
        [InlineData("123456", "_____", "stackoverflow.com", null)]
        [InlineData("lololo", "987654", "stackoverflow.com", null)]
        [InlineData("xxxxxxx", "xxxxxxx", "stackoverflow.com", null)]
        [InlineData("abcde", "12345", "stackoverflow.com", null)]
        [InlineData("_____", "00000", "stackoverflow.com", null)]
        public async Task FindValidEmail_Should_Not_Find_One_Valid_Email(string firstName, string lastName, string domain, string expected)
        {
            // Arrange
            var mockMx = new Mock<IMxService>();
            var mockEmail = new Mock<IEmailService>();
            var finderServiceMocked = new FinderService(mockEmail.Object, mockMx.Object);

            mockMx.Setup(x => x.GetMailServerBasedOnDomain(It.IsAny<string>())).Returns(string.Empty);
            mockEmail.Setup(x => x.EmailIsValid(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var actual = await finderServiceMocked.FindValidEmail(firstName, lastName, domain);

            // Assert
            Assert.Equal(expected, actual.FirstOrDefault());
        }
    }
}
