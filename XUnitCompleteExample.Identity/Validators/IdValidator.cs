namespace XUnitCompleteExample.Identity.Validators;

public class IdValidator : AbstractValidator<long>
{
    public IdValidator()
    {
        RuleFor(id => id).NotEmpty();
    }
}