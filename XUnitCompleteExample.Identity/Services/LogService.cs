#nullable enable

namespace XUnitCompleteExample.Identity.Services;

public class LogService : ILogService
{
    private readonly ILogger<LogService> _logger;
    private readonly Texts _texts = Texts.GetInstance();

    public LogService(ILogger<LogService> logger)
    {
        _logger = logger;
    }
    
    public void LogInformation(string msg)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}",
            msg,
            DateTime.Now).ToString();
        _logger.LogInformation(text);
    }
    
    public void LogInformation(Information information)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}{2}",
            information.LogInformationMsg,
            BuildContextString(information.Context),
            information.DateTime).ToString();
        _logger.LogInformation(text);
    }

    public void LogInformation(string msg, Dictionary<string, object> context)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}{2}",
            msg,
            BuildContextString(context),
            DateTime.Now).ToString();
        _logger.LogInformation(text);
    }
    
    public void LogInformation(string msg, Dictionary<string, object> context, DateTime dateTime)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}{2}",
            msg,
            BuildContextString(context),
            dateTime).ToString();
        _logger.LogInformation(text);
    }
    
    //TODO.
    // Il fatto che EventCodes contiene costanti di tipo stringa porta ad avere 4 costruttori con la stessa firma.
    // Commento temporaneamente i construttori LogInformation(string eventCode, Dictionary<string, object> context) e
    // LogInformation(string eventCode, Dictionary<string, object> context, DateTime dateTime).
    // Cercare di far coesistere i 4 costruttori con conflitti cioè:
    // - LogInformation(string msg, Dictionary<string, object> context).
    // - LogInformation(string eventCode, Dictionary<string, object> context).
    // - LogInformation(string msg, Dictionary<string, object> context, DateTime dateTime).
    // - LogInformation(string eventCode, Dictionary<string, object> context, DateTime dateTime).
    
    // public void LogInformation(string eventCode, Dictionary<string, object> context)
    // {
    //     var stringBuilder = new StringBuilder();
    //     var text = stringBuilder.AppendFormat("{0}{1}{2}",
    //         BuildContextString(context),
    //         _texts.EventsCodesMsgs[eventCode],
    //         DateTime.Now).ToString();
    //     _logger.LogInformation(text);
    // }
    //
    // public void LogInformation(string eventCode, Dictionary<string, object> context, DateTime dateTime)
    // {
    //     var stringBuilder = new StringBuilder();
    //     var text = stringBuilder.AppendFormat("{0}{1}{2}",
    //         BuildContextString(context),
    //         _texts.EventsCodesMsgs[eventCode],
    //         dateTime).ToString();
    //     _logger.LogInformation(text);
    // }

    public void LogDebug(string msg, Dictionary<string, object> context)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}{2}",
            msg,
            BuildContextString(context)).ToString();
        _logger.LogDebug(text);
    }

    public void LogWarning(string msg, Dictionary<string, object> context)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}",
            msg,
            BuildContextString(context)).ToString();
        _logger.LogWarning(text);
    }
    
    // Nel caso in cui venga loggato un errore nel log ci sarà la data e l'ora di quando è stato scritto i log ma anche
    // la data e l'ora di quando è avvenuto l'errore perchè potrebbe essere momenti diversi.
    public void LogError(Error error)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}{2}",
            error.LogErrorMsg,
            BuildContextString(error.Context),
            BuildErrorDateTimeString(error.DateTime)).ToString();
        _logger.LogError(text);
    }

    // public void LogError(ErrorResult errorResult)
    // {
    //     
    //     errorResult.Errors.ForEach(error => LogError(error.LogErrorMsg, error.Context));
    // }
    
    public void LogError(string msg, Dictionary<string, object> context)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}",
            msg,
            BuildContextString(context)).ToString();
        _logger.LogError(text);
    }
    
    public void LogError(Exception ex, Dictionary<string, object> context)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}",
            ex,
            BuildContextString(context)).ToString();
        _logger.LogError(text);
    }
    
    public void LogError(string msg, Dictionary<string, object> context, Exception ex)
    {
        var stringBuilder = new StringBuilder();
        var text = stringBuilder.AppendFormat("{0}{1}{2}",
            msg,
            ex,
            BuildContextString(context)).ToString();
        _logger.LogError(text);
    }

    private string BuildContextString(Dictionary<string, object>? context)
    {
        if (context == null)
            return string.Empty;
        
        string separator;
        var stringBuilder = new StringBuilder();
        var count = 0;
        stringBuilder.AppendFormat(_texts.LogWhileExecuting, context[_texts.ClassName], context[_texts.MethodName]);
        stringBuilder.AppendFormat("{0}", _texts.With.ToLower());
        foreach (KeyValuePair<string, object> keyValuePair in context)
        {
            count++;
            if (count == context.Count)
                separator = _texts.Dot;
            else if (count == context.Count - 1)
                separator = _texts.And;
            else
                separator = _texts.Comma;
            
            stringBuilder.Append(keyValuePair.Key).AppendFormat("{0} {1}{2}", keyValuePair.Key, keyValuePair.Value, separator);
        }

        return stringBuilder.ToString();
    }

    private string BuildErrorDateTimeString(DateTime dateTime)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(_texts.LogErrorAt, dateTime);
        return stringBuilder.ToString();
    }
}

