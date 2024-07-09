using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace XUnitCompleteExample.Models
{
    public class ErrorBody : ApiResponseBody
    {
        public DateTime DateTime { get; } = DateTime.Now;
        public Dictionary<string, List<Error>> Errors { get; set; } = new();

        public ErrorBody()
        {
        }

        public ErrorBody(string key, Error error)
        {
            AddError(key, error);
        }
        // Prendiamo i dati dal ModelState e li usiamo per costruire il corpo delle risposte
        // generate automaticamente dal framework. Lo scopo è quello di uniformare
        // le risposte generate automaticamente dal framework e quelle costruite dallo sviluppatore
        // nei vari endpoint.
        public ErrorBody(ModelStateDictionary modelState)
        {
            foreach (string item in modelState.Keys)
            {
                List<ModelError> modelErrors = modelState[item].Errors.ToList();
                modelErrors.ForEach(modelError => AddError(item, modelError.ErrorMessage));
            }
        }

        public void AddError(string key, string message)
        {
            Error error = new() { Message = message };
            if (Errors.ContainsKey(key))
            {
                Errors[key].Add(error);
            }
            else
            {
                Errors.Add(key, new List<Error>() { error });
            }
        }
        public void AddError(string key, Error error)
        {
            if (Errors.ContainsKey(key))
            {
                Errors[key].Add(error);
            }
            else
            {
                Errors.Add(key, new List<Error>() { error });
            }
        }
    }
}
