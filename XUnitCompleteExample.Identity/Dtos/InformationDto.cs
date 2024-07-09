#nullable enable

namespace XUnitCompleteExample.Identity.Dtos;

public class InformationDto
{
    public string? EventCode { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;

    public InformationDto()
    {
    }

    public InformationDto(string userInformationMessage)
    {
        Message = userInformationMessage;
    }
    
    public InformationDto(string userInformationMessage, DateTime dateTime) : this(userInformationMessage)
    {
        DateTime = dateTime;
    }
    
    public InformationDto(Event evt, string userInformationMessage)
    {
        EventCode = evt.Code;
        Message = userInformationMessage;
    }
    
    public InformationDto(Event evt, string userInformationMessage, DateTime dateTime) : this(evt, userInformationMessage)
    {
        DateTime = dateTime;
    }
}