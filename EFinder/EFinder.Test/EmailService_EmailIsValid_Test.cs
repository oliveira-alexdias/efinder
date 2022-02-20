using System.Threading.Tasks;
using EFinder.Service.Interfaces;
using EFinder.Service.Models;
using EFinder.Service.Services;
using Moq;
using Xunit;

namespace EFinder.Test
{
    public class EmailService_EmailIsValid_Test
    {
        [Theory]
        [InlineData("htraverso@stackoverflow.com", "alt3.aspmx.l.google.com")]
        [InlineData("DCook@stackoverflow.com", "alt3.aspmx.l.google.com")]
        [InlineData("aroberts@stackoverflow.com", "alt3.aspmx.l.google.com")]
        [InlineData("dhaney@stackoverflow.com", "alt3.aspmx.l.google.com")]
        [InlineData("mforee@stackoverflow.com", "alt3.aspmx.l.google.com")]
        public async Task EmailIsValid_Should_Be_True(string email, string mailServer)
        {
            // Arrange
            var mock = new Mock<ISmtpService>();
            var mockFile = new Mock<IFiles>();
            var emailServiceMocked = new EmailService(mock.Object, mockFile.Object);
            var tcpResponse = new SmtpResponse(250, string.Empty);
            mock.Setup(x => x.RunEmailCheckCommands(mailServer, email)).ReturnsAsync(tcpResponse);

            // Act
            var actual = await emailServiceMocked.EmailIsValid(email, mailServer);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData("__111___aabbcc@stackoverflow.com", "alt3.aspmx.l.google.com")] 
        [InlineData("__222___aabbcc@stackoverflow.com", "alt3.aspmx.l.google.com")] 
        [InlineData("__333___aabbcc@stackoverflow.com", "alt3.aspmx.l.google.com")] 
        [InlineData("__444___aabbcc@stackoverflow.com", "alt3.aspmx.l.google.com")] 
        [InlineData("__555___aabbcc@stackoverflow.com", "alt3.aspmx.l.google.com")] 
        public async Task EmailIsValid_Should_Be_False(string email, string mailServer)
        {
            // Arrange
            var mock = new Mock<ISmtpService>();
            var mockFile = new Mock<IFiles>();
            var emailServiceMocked = new EmailService(mock.Object, mockFile.Object);
            var tcpResponse = new SmtpResponse(550, string.Empty);
            mock.Setup(x => x.RunEmailCheckCommands(mailServer, email)).ReturnsAsync(tcpResponse);

            // Act
            var actual = await emailServiceMocked.EmailIsValid(email, mailServer);

            // Assert
            Assert.False(actual);
        }
    }
}