namespace EFinder.Service.Extensions;

public static class StringExtensions
{
    public static string ReplaceForEmail(this string @string, string firstName, string lastName, string domain)
    {
        return @string.Replace("{domain}", domain)
                      .Replace("{FN}", firstName)
                      .Replace("{FI}", firstName[0].ToString())
                      .Replace("{LN}", lastName)
                      .Replace("{LI}", lastName[0].ToString());
    }
}