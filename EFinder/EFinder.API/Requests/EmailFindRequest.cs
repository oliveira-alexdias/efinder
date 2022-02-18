namespace EFinder.API.Requests;

public class EmailFindRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Domains { get; set; }
}

