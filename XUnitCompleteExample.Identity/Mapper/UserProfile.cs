namespace XUnitCompleteExample.Identity.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AddUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, LoggedUserDto>();
        CreateMap<User, UserDto>();
    }
}