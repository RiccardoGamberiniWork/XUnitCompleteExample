namespace XUnitCompleteExample.Identity.Models;

public class Event
{
    public string Code { get; set; }
    public string? Message { get; set; }

    public Event(string code, string description = null)
    {
        Code = code;
        Message = description;
    }
}