#nullable enable

namespace XUnitCompleteExample.Identity.Interfaces;

public interface ILogService
{
    void LogInformation(string msg);
    void LogInformation(Information information);
    void LogInformation(string msg, Dictionary<string, object> context);
    void LogInformation(string msg, Dictionary<string, object> context, DateTime dateTime);
    // void LogInformation(string eventCode, Dictionary<string, object> context);
    // void LogInformation(string eventCode, Dictionary<string, object> context, DateTime dateTime);
    void LogDebug(string logDebugMsg, Dictionary<string, object> context);
    void LogWarning(string logWarningMsg, Dictionary<string, object> context);
    void LogError(Error error);
    // void LogError(ErrorResult errorResult);
    void LogError(string msg, Dictionary<string, object> context);
    void LogError(Exception ex, Dictionary<string, object> context);
    void LogError(string msg, Dictionary<string, object> context, Exception ex);
}