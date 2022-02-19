using System.Net.Sockets;
using System.Text;
using EFinder.Service.Interfaces;
using EFinder.Service.Models;

namespace EFinder.Service.Helpers;

public class TcpClientHelper : ITcpClientHelper, IDisposable
{
    private NetworkStream netWorkStream;
    private StreamReader streamReader;
    private TcpClient tcpClient;

    public async Task<TcpClientHelperResponse> RunEmailCheckCommands(string server, string email)
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
        tcpClient = new TcpClient(server, 25);
        netWorkStream = tcpClient.GetStream();
        streamReader = new StreamReader(netWorkStream);
        await streamReader.ReadLineAsync();
    }

    private async Task<TcpClientHelperResponse> RunHeloCommand()
    {
        return await RunCommand("HELO EFinder");
    }

    private async Task<TcpClientHelperResponse> RunMailFromCommand()
    {
        return await RunCommand("MAIL FROM:<oliveira.alexdias@gmail.com>");
    }

    private async Task<TcpClientHelperResponse> RunRcptToCommand(string email)
    {
        return await RunCommand($"RCPT TO:<{email}>");
    }

    private async Task RunQuiteCommand()
    {
        await RunCommand("QUITE");
    }

    private async Task<TcpClientHelperResponse> RunCommand(string command)
    {
        var CRLF = "\r\n";
        var buffer = Encoding.ASCII.GetBytes(command + CRLF);
        await netWorkStream.WriteAsync(buffer, 0, buffer.Length);
        var response = await streamReader.ReadLineAsync();
        return new TcpClientHelperResponse(response);
    }

    private void DisconnectFromServer()
    {
        tcpClient.Close();
    }

    public void Dispose()
    {
        netWorkStream.Dispose();
        streamReader.Dispose();
        tcpClient.Dispose();
    }
}