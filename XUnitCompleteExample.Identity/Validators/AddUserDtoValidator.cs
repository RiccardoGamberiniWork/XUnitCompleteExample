namespace XUnitCompleteExample.Identity.Validators;

public class AddUserDtoValidator : AbstractValidator<AddUserDto>
{
    public AddUserDtoValidator()
    {
        RuleFor(addUserDto => addUserDto.Username).EmailAddress();
        RuleFor(addUserDto => addUserDto.Password).Password();
    }
}