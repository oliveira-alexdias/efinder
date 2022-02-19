using EFinder.Service.Interfaces;

namespace EFinder.Service.Services;

public class EmailService : IEmailService
{
    private readonly ITcpClientHelper _tcpClient;

    public EmailService(ITcpClientHelper tcpClient)
    {
        this._tcpClient = tcpClient;
    }

    public async Task<bool> EmailIsValid(string email, string mailServer)
    {
        var response = await _tcpClient.RunEmailCheckCommands(mailServer, email);
        return response.Status != 550;
    }
}