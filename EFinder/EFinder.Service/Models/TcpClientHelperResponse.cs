namespace EFinder.Service.Models;

public class TcpClientHelperResponse
{
    public TcpClientHelperResponse(string fullMessage)
    {
        Status = int.Parse(fullMessage.Substring(0, 3));
        Message = fullMessage;
    }

    public int Status { get; set; }
    public string Message { get; set; }
}