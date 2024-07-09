#nullable enable
                    
namespace XUnitCompleteExample.Identity.Models;

// Tmp. Information è un risultato la cui proprietà vanno bene in moltissimi casi. Se va restituito un oggetto diverso da questo si può creare un risultato specifico.
// Ad esempio ValidateRichiestaResult.
public class Information
{
    public string? EventCode { get; set; }
    public string? UserInformationMsg { get; set; }
    public string LogInformationMsg { get; set; }
    public Dictionary<string, object> Context { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;

    public Information() {}
    
    public Information(string logInformationMsg)
    {
        LogInformationMsg = logInformationMsg;
    }
    
    public Information(string logInformationMsg, Dictionary<string, object> context) : this(logInformationMsg)
    {
        Context = context;
    }
    
    public Information(string logInformationMsg, Dictionary<string, object> context, DateTime dateTime) : this(logInformationMsg, context)
    {
        DateTime = dateTime;
    }

    public Information(string logInformationMsg, string userInformationMsg, Dictionary<string, object> context) : this(logInformationMsg, context)
    {
        UserInformationMsg = userInformationMsg;
    }
    
    public Information(string logInformationMsg, string userInformationMsg, Dictionary<string, object> context, DateTime dateTime) : this(logInformationMsg, userInformationMsg, context)
    {
        DateTime = dateTime;
    }
    
    //TODO.
    // Se EventCodes contiene costanti di tipo stringa porta ad avere due costruttori con la stessa firma (Information(string eventCode, Dictionary<string, object> context) e
    // Information(string logInformationMsg, Dictionary<string, object> context)).
    // Per questo motivo temporaneamente nel programma sostituisco le chiamate al costruttoreInformation(string eventCode, Dictionary<string, object> context) con ..... new Information
    // {
    //   EventCode = eventCode,
    //   LogInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   UserInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   Context = aContext
    // }; ..... .
    // Il caso in cui voglia costruire un Information con il codice dell'evento e il contesto sarebbe meglio gestirlo con un costruttore e non con la forma new Information
    // {
    //   EventCode = eventCode,
    //   LogInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   UserInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   Context = aContext
    // }; ..... .
    // public Information(string eventCode, Dictionary<string, object> context)
    // {
    //     EventCode = eventCode;
    //     string eventCodeMsg = _texts.EventsCodesMsgs[eventCode];
    //     LogInformationMsg = eventCodeMsg;
    //     UserInformationMsg = eventCodeMsg;
    //     Context = context;
    // }
    
    //TODO.
    // Se EventCodes contiene costanti di tipo stringa porta ad avere due costruttori con la stessa firma (Information(string eventCode, Dictionary<string, object> context, DateTime dateTime) e
    // Information(string logInformationMsg, Dictionary<string, object> context, DateTime dateTime)).
    // Per questo motivo temporaneamente nel programma sostituisco le chiamate al costruttore Information(string eventCode, Dictionary<string, object> context, DateTime dateTime)
    // con ..... new Information
    // {
    //   EventCode = eventCode,
    //   LogInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   UserInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   Context = aContext,
    //   DateTime = dateTime
    // }; ..... .
    // Il caso in cui voglia costruire un Information con il codice dell'evento e il contesto sarebbe meglio gestirlo con un costruttore e non con la forma new Information
    // {
    //   EventCode = eventCode,
    //   LogInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   UserInformationMsg = _texts.EventsCodesMsgs[eventCode],
    //   Context = aContext,
    //   DateTime = dateTime
    // }; ..... .
    // public Information(string eventCode, Dictionary<string, object> context, DateTime dateTime) : this(eventCode, context)
    // {
    //     DateTime = dateTime;
    // }
}