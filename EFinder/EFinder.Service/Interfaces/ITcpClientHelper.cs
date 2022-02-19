using EFinder.Service.Models;

namespace EFinder.Service.Interfaces;

public interface ITcpClientHelper
{
    Task<TcpClientHelperResponse> RunEmailCheckCommands(string server, string email);
}