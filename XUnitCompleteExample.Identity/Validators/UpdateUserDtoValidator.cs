namespace XUnitCompleteExample.Identity.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        // Username
        RuleFor(updateUtenteDto => updateUtenteDto.Username).EmailAddress();
    }
}