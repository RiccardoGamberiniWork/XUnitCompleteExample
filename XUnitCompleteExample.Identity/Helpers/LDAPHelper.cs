namespace XUnitCompleteExample.Identity.Helpers
{
    public class LDAPHelper
    {

        private string _path;
        private string _filterAttribute;

        public LDAPHelper(string path)
        {
            _path = path;
        }

        //public bool IsAuthenticated(string domain, string username, string pwd)
        //{
        //    string domainAndUsername = domain + @"\" + username;
        //    DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

        //    try
        //    {
        //        // Bind to the native AdsObject to force authentication.			
        //        object obj = entry.NativeObject;
        //        DirectorySearcher search = new DirectorySearcher(entry);

        //        search.Filter = "(SAMAccountName=" + username + ")";
        //        search.PropertiesToLoad.Add("cn");
        //        SearchResult result = search.FindOne();
        //        // Dim pippo As SearchResultCollection = search.FindAll
        //        // For Each result In pippo
        //        // Dim a As Integer = 1
        //        // Next
        //        if ((result == null))
        //            return false;

        //        // Update the new path to the user in the directory.
        //        _path = result.Path;
        //        _filterAttribute = System.Convert.ToString(result.Properties["cn"][0]);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Throw New Exception("Error authenticating user. " & ex.Message)

        //        //Engine.LogEventi ev = new Engine.LogEventi();
        //        //ev.GuidUtente = Guid.Empty;
        //        //ev.TipoEvento = Engine.LogEventi.EventoLogInFailed;
        //        //ev.TipoOperazione = "Accesso Fallito";
        //        //ev.DescrizioneOperazione = "L'utente " + _path + " - " + domainAndUsername + " ha tentato di loggarsi a " + Environment.MachineName + ". (" + ex.Message + ")";
        //        //ev.Documenta();
        //        return false;
        //    }

        //    return true;
        //}

        //public string GetGroups()
        //{
        //    DirectorySearcher search = new DirectorySearcher(_path);
        //    search.Filter = "(cn=" + _filterAttribute + ")";
        //    search.PropertiesToLoad.Add("memberOf");
        //    StringBuilder groupNames = new StringBuilder();

        //    try
        //    {
        //        SearchResult result = search.FindOne();
        //        int propertyCount = result.Properties("memberOf").Count;

        //        string dn;
        //        int equalsIndex;
        //        int commaIndex;

        //        int propertyCounter;

        //        for (propertyCounter = 0; propertyCounter <= propertyCount - 1; propertyCounter++)
        //        {
        //            dn = System.Convert.ToString(result.Properties("memberOf")(propertyCounter));

        //            equalsIndex = dn.IndexOf("=", 1);
        //            commaIndex = dn.IndexOf(",", 1);
        //            if ((equalsIndex == -1))
        //                return null;

        //            groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
        //            groupNames.Append("|");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error obtaining group names. " + ex.Message);
        //    }

        //    return groupNames.ToString();
        //}

    }
}
