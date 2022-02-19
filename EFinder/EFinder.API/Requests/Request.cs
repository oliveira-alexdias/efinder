namespace EFinder.API.Requests;

public abstract class Request
{
    // This is a Template Method GoF Desing Pattern
    public bool HasErrors()
    {
        Validate();
        return Errors.Count > 0;
    }

    protected abstract void Validate();
    public List<string> Errors = new();
}