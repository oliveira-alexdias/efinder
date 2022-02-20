using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFinder.Service.Exceptions;
using EFinder.Service.Interfaces;
using EFinder.Service.Services;
using Moq;
using Xunit;

namespace EFinder.Test
{
    public class FinderService_FindValidEmail_Test
    {
        [Theory]
        [InlineData("Henry", "Traverso", "stackoverflow.com", "htraverso@stackoverflow.com")]
        [InlineData("Dale", "Cook", "stackoverflow.com", "dcook@stackoverflow.com")]
        [InlineData("Amy", "Roberts", "stackoverflow.com", "aroberts@stackoverflow.com")]
        [InlineData("David", "Haney", "stackoverflow.com", "dhaney@stackoverflow.com")]
        [InlineData("Michael", "Foree", "stackoverflow.com", "mforee@stackoverflow.com")]
        [InlineData("Matt", "Tomanek", "linkedin.com", "MTomanek@linkedin.com")]
        [InlineData("Adam", "Myers", "linkedin.com", "MAdam@linkedin.com")]
        [InlineData("Kasey", "Ourada", "linkedin.com", "KOurada@linkedin.com")]
        [InlineData("Tara", "Merithew", "linkedin.com", "TMerithew@linkedin.com")]
        [InlineData("Brooke", "Mannelly", "linkedin.com", "BMannelly@linkedin.com")]
        [InlineData("Joseane", "Aparecida", "rarolabs.com.br", "Joseane.Aparecida@rarolabs.com.br")]
        [InlineData("Fernanda", "Oliveira", "rarolabs.com.br", "Fernanda.Oliveira@rarolabs.com.br")]
        [InlineData("Ligia", "Picinin", "rarolabs.com.br", "Ligia.Picinin@rarolabs.com.br")]
        [InlineData("Breno", "Freitas", "rarolabs.com.br", "Breno.Freitas@rarolabs.com.br")]
        public async Task FindValidEmail_Should_Find_One_Valid_Email(string firstName, string lastName, string domain, string expected)
        {
            // Arrange
            var mockMx = new Mock<IMailExchangeService>();
            var mockEmail = new Mock<IEmailService>();
            var finderServiceMocked = new FinderService(mockEmail.Object, mockMx.Object);

            mockMx.Setup(x => x.GetMailExchangeServerBasedOnDomain(It.IsAny<string>())).Returns(new List<string> { string.Empty });
            mockEmail.Setup(x => x.EmailIsValid(
                                It.Is<string>(a => a.Equals(expected, StringComparison.CurrentCultureIgnoreCase)),
                                It.IsAny<string>()))
                                .ReturnsAsync(true);
            mockEmail.Setup(x => x.GetAllPossibleEmails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                        .Returns(new List<string>
                                                        {
                                                            $"{firstName}.{lastName}@{domain}",
                                                            $"{firstName}{lastName}@{domain}",
                                                            $"{firstName}-{lastName}@{domain}",
                                                            $"{firstName[0]}.{lastName}@{domain}",
                                                            $"{firstName[0]}{lastName}@{domain}",
                                                            $"{firstName[0]}-{lastName}@{domain}",
                                                            $"{lastName}.{firstName[0]}@{domain}",
                                                            $"{lastName}{firstName[0]}@{domain}",
                                                            $"{lastName}-{firstName[0]}@{domain}",
                                                            $"{firstName}.{lastName[0]}@{domain}",
                                                            $"{firstName}{lastName[0]}@{domain}",
                                                            $"{firstName}-{lastName[0]}@{domain}",
                                                            $"{lastName}.{firstName}@{domain}",
                                                            $"{lastName}{firstName}@{domain}",
                                                            $"{lastName}-{firstName}@{domain}",
                                                            $"{lastName[0]}.{firstName}@{domain}",
                                                            $"{lastName[0]}{firstName}@{domain}",
                                                            $"{lastName[0]}-{firstName}@{domain}"
                                                        });

            // Act
            var actual = await finderServiceMocked.FindValidEmail(firstName, lastName, domain);

            // Assert
            Assert.Equal(expected, actual.Emails?.FirstOrDefault(), ignoreCase: true);
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
            var mockMx = new Mock<IMailExchangeService>();
            var mockEmail = new Mock<IEmailService>();
            var finderServiceMocked = new FinderService(mockEmail.Object, mockMx.Object);

            mockMx.Setup(x => x.GetMailExchangeServerBasedOnDomain(It.IsAny<string>())).Returns(new List<string> { string.Empty });
            mockEmail.Setup(x => x.EmailIsValid(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            mockEmail.Setup(x => x.GetAllPossibleEmails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                                       .Returns(new List<string>
                                                       {
                                                            $"{firstName}.{lastName}@{domain}",
                                                            $"{firstName}{lastName}@{domain}",
                                                            $"{firstName}-{lastName}@{domain}",
                                                            $"{firstName[0]}.{lastName}@{domain}",
                                                            $"{firstName[0]}{lastName}@{domain}",
                                                            $"{firstName[0]}-{lastName}@{domain}",
                                                            $"{lastName}.{firstName[0]}@{domain}",
                                                            $"{lastName}{firstName[0]}@{domain}",
                                                            $"{lastName}-{firstName[0]}@{domain}",
                                                            $"{firstName}.{lastName[0]}@{domain}",
                                                            $"{firstName}{lastName[0]}@{domain}",
                                                            $"{firstName}-{lastName[0]}@{domain}",
                                                            $"{lastName}.{firstName}@{domain}",
                                                            $"{lastName}{firstName}@{domain}",
                                                            $"{lastName}-{firstName}@{domain}",
                                                            $"{lastName[0]}.{firstName}@{domain}",
                                                            $"{lastName[0]}{firstName}@{domain}",
                                                            $"{lastName[0]}-{firstName}@{domain}"
                                                       });

            // Act
            var actual = await finderServiceMocked.FindValidEmail(firstName, lastName, domain);

            // Assert
            Assert.Equal(expected, actual.Emails?.FirstOrDefault());
        }

        [Theory]
        [InlineData("myfakedomain.com")]
        [InlineData("itisinvaliddomain.com")]
        [InlineData("dddddddddd.com")]
        [InlineData("0000001111.com.br")]
        [InlineData("090909fake.com")]
        public async Task FindValidEmail_Should_Throw_MailExchangeServerNotFoundException(string domain)
        {
            // Arrange
            var mockMx = new Mock<IMailExchangeService>();
            var mockEmail = new Mock<IEmailService>();
            var finderServiceMocked = new FinderService(mockEmail.Object, mockMx.Object);

            mockMx.Setup(x => x.GetMailExchangeServerBasedOnDomain(domain))
                .Throws(new MailExchangeServerNotFoundException(domain));
            
            // Assert
            await Assert.ThrowsAsync<MailExchangeServerNotFoundException>(async () =>
                await finderServiceMocked.FindValidEmail(string.Empty, string.Empty, domain));
        }
    }
}
