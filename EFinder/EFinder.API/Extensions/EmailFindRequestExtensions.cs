using EFinder.API.Requests;

namespace EFinder.API.Extensions;

public static class EmailFindRequestExtensions
{
    public static string GetKeyForCache(this EmailFindRequest request, string domain)
    {
        return $"{request.FirstName}{request.LastName}{domain}";
    }
}