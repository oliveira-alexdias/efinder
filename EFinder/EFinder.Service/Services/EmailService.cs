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
}