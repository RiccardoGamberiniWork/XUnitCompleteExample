#nullable enable

namespace XUnitCompleteExample.Identity.Models;

public class Error
{
    public Event? Event { get; set; }
    public string? UserErrorMsg { get; set; }
    public string LogErrorMsg { get; set; }
    public Dictionary<string, object> Context { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public Exception? Ex { get; set; }
    private readonly Texts _texts = Texts.GetInstance();

    public Error()
    {
    }

    public Error(string logErrorMsg, Dictionary<string, object> context)
    {
        LogErrorMsg = logErrorMsg;
        Context = context;
    }
    
    public Error(string logErrorMsg, Dictionary<string, object> context, Exception ex) : this(logErrorMsg, context)
    {
        Ex = ex;
    }
    
    public Error(string logErrorMsg, Dictionary<string, object> context, DateTime dateTime) : this(logErrorMsg, context)
    {
        DateTime = dateTime;
    }
    
    public Error(string logErrorMsg, Dictionary<string, object> context, DateTime dateTime, Exception ex) : this(logErrorMsg, context, dateTime)
    {
        Ex = ex;
    }
    
    public Error(Dictionary<string, object> context, DateTime dateTime, Exception ex)
    {
        Context = context;
        DateTime = dateTime;
        Ex = ex;
    }

    public Error(string logErrorMsg, string userErrorMsg, Dictionary<string, object> context) : this(logErrorMsg, context)
    {
        UserErrorMsg = userErrorMsg;
    }

    public Error(string logErrorMsg, string userErrorMsg, Dictionary<string, object> context, DateTime dateTime) : this(logErrorMsg, userErrorMsg, context)
    {
        DateTime = dateTime;
    }
    
    public Error(string logErrorMsg, string userErrorMsg, Dictionary<string, object> context, DateTime dateTime, Exception ex) : this(logErrorMsg, userErrorMsg, context, dateTime)
    {
        Ex = ex;
    }
    
    //TODO.
    // Se EventCodes contiene costanti di tipo stringa porta ad avere costruttori costruttori con la stessa firma.
    // I costruttori in conflitto sono:
    // - Error(string logErrorMsg, Dictionary<string, object> context).
    // - Error(string eventCode, Dictionary<string, object> context).
    // - Error(string logErrorMsg, Dictionary<string, object> context, DateTime dateTime).
    // - Error(string eventCode, Dictionary<string, object> context, DateTime dateTime).
    // Ho sostituito le chiamate a Error(string eventCode, Dictionary<string, object> context) e a Error(string eventCode, Dictionary<string, object> context, DateTime dateTime)
    // con la forma new Error { ..... }.
    // Cercare di far coesistere i costruttori in conflitto.
    // public Error(string eventCode, Dictionary<string, object> context)
    // {
    //     EventCode = eventCode;
    //     string eventCodeMsg = _texts.EventsCodesMsgs[eventCode];
    //     LogErrorMsg = eventCodeMsg;
    //     UserErrorMsg = eventCodeMsg;
    //     Context = context;
    // }
    //
    // public Error(string eventCode, Dictionary<string, object> context, DateTime dateTime) : this(eventCode, context)
    // {
    //     DateTime = dateTime;
    // }
}