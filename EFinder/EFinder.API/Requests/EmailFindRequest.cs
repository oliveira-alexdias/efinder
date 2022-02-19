using FluentValidation;

namespace EFinder.API.Requests;

public class EmailFindRequest : Request
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Domains { get; set; }

    protected override void Validate()
    {
        var validator = new InlineValidator<EmailFindRequest>();

        validator.RuleFor(i => i.FirstName).Matches(@"^([\w\.\-]+)$")
                                           .NotEmpty().NotNull()
                                           .MaximumLength(100);

        validator.RuleFor(i => i.LastName).Matches(@"^([\w\.\-]+)$")
                                          .NotEmpty().NotNull()
                                          .MaximumLength(100);

        validator.RuleFor(i => i.Domains).ForEach(i => 
                                          i.Matches(@"^([\w\-]+)((\.(\w){2,3})+)$")
                                           .NotEmpty().NotNull());

        foreach (var error in validator.Validate(this).Errors)
        {
            Errors.Add(error.ErrorMessage);
        }
    }
}