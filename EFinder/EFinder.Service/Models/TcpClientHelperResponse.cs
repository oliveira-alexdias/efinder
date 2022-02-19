namespace EFinder.Service.Models;

public class TcpClientHelperResponse
{
    public TcpClientHelperResponse(int status, string message)
    {
        Status = status;
        Message = message;
    }

    public TcpClientHelperResponse(string fullMessage)
    {
        Status = int.Parse(fullMessage[..3]);
        Message = fullMessage;
    }

    public int Status { get; set; }
    public string Message { get; set; }
}