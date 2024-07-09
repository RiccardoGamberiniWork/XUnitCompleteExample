#nullable enable

namespace XUnitCompleteExample.Identity.Dtos;

public class ErrorDto
{
    public string? EventCode { get; set; }
    public string Message { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;

    public ErrorDto()
    {
    }

    public ErrorDto(string userErrorMessage)
    {
        Message = userErrorMessage;
    }
    
    public ErrorDto(string userErrorMessage, DateTime dateTime) : this(userErrorMessage)
    {
        DateTime = dateTime;
    }
    
    public ErrorDto(Event evt, string userErrorMessage)
    {
        EventCode = evt.Code;
        Message = userErrorMessage;
    }
    
    public ErrorDto(Event evt, string userErrorMessage, DateTime dateTime) : this(evt, userErrorMessage)
    {
        DateTime = dateTime;
    }
}
