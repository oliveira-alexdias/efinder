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

    public async Task<SmtpResponseModel> RunEmailCheckCommands(string server, string email)
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
        _tcpClient = new TcpClient(server, Constants.SmtpConstants.MailExchangeServerPort);
        _netWorkStream = _tcpClient.GetStream();
        _streamReader = new StreamReader(_netWorkStream);
        await _streamReader.ReadLineAsync();
    }

    private async Task<SmtpResponseModel> RunHeloCommand()
    {
        return await RunCommand($"{Constants.SmtpConstants.HeloCommand} EFinder");
    }

    private async Task<SmtpResponseModel> RunMailFromCommand()
    {
        return await RunCommand($"{Constants.SmtpConstants.MailFromCommand}<{_configuration["EmailInfo:From"]}>");
    }

    private async Task<SmtpResponseModel> RunRcptToCommand(string email)
    {
        return await RunCommand($"{Constants.SmtpConstants.RcptToCommand}<{email}>");
    }

    private async Task RunQuiteCommand()
    {
        await RunCommand(Constants.SmtpConstants.QuiteCommand);
    }

    private async Task<SmtpResponseModel> RunCommand(string command)
    {
        var buffer = Encoding.ASCII.GetBytes(command + Constants.SmtpConstants.Crlf);
        await _netWorkStream.WriteAsync(buffer, 0, buffer.Length);
        var response = await _streamReader.ReadLineAsync();
        return new SmtpResponseModel(response);
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