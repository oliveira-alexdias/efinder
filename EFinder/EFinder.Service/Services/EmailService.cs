using EFinder.Service.Interfaces;

namespace EFinder.Service.Services;

public class EmailService : IEmailService
{
    private readonly ITcpClientHelper tcpClient;

    public EmailService(ITcpClientHelper tcpClient)
    {
        this.tcpClient = tcpClient;
    }

    public async Task<bool> EmailIsValid(string email, string mailServer)
    {
        var response = await tcpClient.RunEmailCheckCommands(mailServer, email);
        return response.Status != 550;
    }

    public List<string> EmailListBuilder(string firstName, string lastName, string domain)
    {
        var listOfEmails = new List<string>
        {
            $"{firstName}.{lastName}{domain}",
            $"{firstName}{lastName}{domain}",
            $"{firstName}-{lastName}{domain}",
            $"{firstName[0]}.{lastName}{domain}",
            $"{firstName[0]}{lastName}{domain}",
            $"{firstName[0]}-{lastName}{domain}",
            $"{lastName}.{firstName[0]}{domain}",
            $"{lastName}{firstName[0]}{domain}",
            $"{lastName}-{firstName[0]}{domain}",
            $"{firstName}.{lastName[0]}{domain}",
            $"{firstName}{lastName[0]}{domain}",
            $"{firstName}-{lastName[0]}{domain}",
            $"{lastName}.{firstName}{domain}",
            $"{lastName}{firstName}{domain}",
            $"{lastName}-{firstName}{domain}",
            $"{lastName[0]}.{firstName}{domain}",
            $"{lastName[0]}{firstName}{domain}",
            $"{lastName[0]}-{firstName}{domain}"
        };

        return listOfEmails;
    }
}