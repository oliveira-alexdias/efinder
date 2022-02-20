using System.Net.Sockets;
using System.Text;
using EFinder.Service.Interfaces;
using EFinder.Service.Models;
using Microsoft.Extensions.Configuration;

namespace EFinder.Service.Services;

public class SmtpService : ISmtpService, IDisposable
{
    private NetworkStream _netWorkStream;
    private StreamReader _streamReader;
    private TcpClient _tcpClient;
    private readonly IConfiguration _configuration;

    public SmtpService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<SmtpResponse> RunEmailCheckCommands(string server, string email)
    {
        await ConnectToServer(server);
        await RunHeloCommand();
        await RunMailFromCommand();
        var rcptToResponse = await RunRcptToCommand(email);
        await RunQuiteCommand();
        DisconnectFromServer();
        return rcptToResponse;
    }

    private async Task ConnectToServer(string server)
    {
        _tcpClient = new TcpClient(server, Constants.Constants.MailExchangeServerPort);
        _netWorkStream = _tcpClient.GetStream();
        _streamReader = new StreamReader(_netWorkStream);
        await _streamReader.ReadLineAsync();
    }

    private async Task<SmtpResponse> RunHeloCommand()
    {
        return await RunCommand("HELO EFinder");
    }

    private async Task<SmtpResponse> RunMailFromCommand()
    {
        return await RunCommand($"MAIL FROM:<{_configuration["EmailInfo:From"]}>");
    }

    private async Task<SmtpResponse> RunRcptToCommand(string email)
    {
        return await RunCommand($"RCPT TO:<{email}>");
    }

    private async Task RunQuiteCommand()
    {
        await RunCommand("QUITE");
    }

    private async Task<SmtpResponse> RunCommand(string command)
    {
        var buffer = Encoding.ASCII.GetBytes(command + Constants.Constants.Crlf);
        await _netWorkStream.WriteAsync(buffer, 0, buffer.Length);
        var response = await _streamReader.ReadLineAsync();
        return new SmtpResponse(response);
    }

    private void DisconnectFromServer()
    {
        _tcpClient.Close();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _netWorkStream?.Dispose();
            _streamReader?.Dispose();
            _tcpClient?.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}