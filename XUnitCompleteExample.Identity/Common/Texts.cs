namespace XUnitCompleteExample.Identity.Common;

// TODO.
// - Cambiare il nome di Texts in TextsEn.
// - Tradurre in inglese il testo in italiano in modo da avere alcune traduzioni pronte.
// - Temporaneamente inserire in TextsEn i testi in italiano.
public class Texts
{
    private static Texts _instance;
    private static readonly object _lock = new();
    private Texts() { }
    
    public string Dot => ". ";
    public string Comma => ", ";
    public string And => " and ";
    public string With => "With ";
    public string Space => " ";
    public string MethodName => "MethodName";
    public string ClassName => "ClassName";
    public string NotValid => " isn't valid. ";
    public string AnErrorOccurredWhile => "An error occurred while {0}.";
    
    #region Texts for logs.
        #region Information level logs.
        public string LogInformationEndpointCall => "Endpoint call. ";
        #endregion
        #region Error level logs.
        public string LogGenericError => "An error occurred.";
        public string LogWhileExecuting => "While executing {0} of {1}. ";

        public string LogErrorAt => "At {0}.";
        #endregion
    #endregion
    #region Texts for HTTP responses.
        #region Texts for the user.
            public string UserGenericError => "An error occurred.";
            public string UserLowerCase => "user";
            public string Ok => "Ok.";
            public string RequestBodyNotNull = "Request body can't be null. ";
            public string MustNotEqualTo = "{0} can't be equal to {1}.";
            public string EqualtTo = "equal to";
            public string AnyFound = "Any {0} found.";
            
            // In tutti i punti del programma, per poter accedere al testo contenuto nelle costanti riguardanti i codici di errore propri di XUnitCompleteExample.Identity,
            // è necessario usare il seguente dizionario perchè accedere direttamente alle costanti dai vari punti del programma dove va usato il messaggio, non è possibile
            // si sta cercando di accedere da un variabile statica da un contesto non statico.
            public Dictionary<string, string> EvtsCodesMsgs = new()
            {
                { EventCodes.Ati1000EventA, Ati1000EventAMsg },
                { EventCodes.Ati1001EventB, Ati1001EventBMsg },
                { EventCodes.Ati1002EventC, Ati1002EventCMsg }
            };
            
            // TODO.
            // Sapere perchè non posso usare inserire in Texts le costanti di EventsCodes ma dovrei creare una classe differente da EventCodes solo per il fatto
            // che le costanti sono di tipo string.
            public const string Ati1000EventAMsg =  "Event A message." ;
            public const string Ati1001EventBMsg =  "Event B message.";
            public const string Ati1002EventCMsg =  "Event C message.";
            #endregion
    #endregion
    
    public static Texts GetInstance()
    {
        if (_instance != null) return _instance;
        lock (_lock)
            _instance ??= new Texts();
        return _instance;
    }
}