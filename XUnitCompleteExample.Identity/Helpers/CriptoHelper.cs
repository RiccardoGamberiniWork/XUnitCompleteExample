namespace XUnitCompleteExample.Identity.Helpers
{
    internal class CriptoHelper
    {

        // costante CHIAVE per encrypt/decrypt
        private const long gCriptKey = 15072008;

        // Funzione che decripta una stringa
        internal static string StrDecode(string s)
        {
            int n;
            int i;
            long k1;
            long k2;
            long k3;
            long k4;

            int Hi;
            int Low;
            long[] sn;
            long key;
            System.Text.StringBuilder SBIn;
            System.Text.StringBuilder SBss;

            try
            {

                // prima di tutto torno ad unire i caratteri che erano stati spezzati in due nella
                // fase di codifica (vedi fase due di StrEncode)
                // Versione VB6
                // sIn = String(Len(s) / 2, "0")

                key = gCriptKey;
                SBIn = new System.Text.StringBuilder(s.Length);
                SBIn.Insert(0, "0", s.Length / 2);

                //for (i = 0; i <= (s.Length - 1); i += 2)
                //{
                //    Low = Strings.Asc(Strings.Mid(s, i + 1, 1)) - 65;
                //    Hi = Strings.Asc(Strings.Mid(s, i + 2, 1)) - 65;
                //    Hi = Hi * 16;
                //    // Mid(sIn, (i / 2) + 1, 1) = Chr(Hi + Low)
                //    SBIn.Replace("0", Strings.Chr(Hi + Low), (i / 2), 1);
                //}

                char c;
                int asc;
                for (i = 0; i <= (s.Length - 1); i += 2)
                {
                    c = Convert.ToChar(s.Substring(i + 1, 1));
                    asc = (int)c - 65;
                    Low = asc;

                    c = Convert.ToChar(s.Substring(i + 2, 1));
                    asc = (int)c - 65;
                    Hi = asc;
                    Hi *= 16;
                    // Mid(sIn, (i / 2) + 1, 1) = Chr(Hi + Low)
                    SBIn.Replace('0', Convert.ToChar(Hi + Low), (i / 2), 1);

                }


                // ora procedo alla decodifica vera e propria
                s = SBIn.ToString();
                n = s.Length;
                SBss = new System.Text.StringBuilder(n);
                sn = new long[n + 1];

                k1 = 11 + (key % 233);
                k2 = 7 + (key % 239);
                k3 = 5 + (key % 241);
                k4 = 3 + (key % 251);

                //char c = "s";
                //int asc = (int)c;

                for (i = 1; i <= n; i++){
                    c = Convert.ToChar(s.Substring(i, 1));
                    asc = (int)c;
                    sn[i] = asc;
                    
                    //sn[i] = String.Asc(Strings.Mid(s, i, 1));
                }

                for (i = 1; i <= n - 2; i++)
                    sn[i] = sn[i] ^ sn[i + 2] ^ (k4 * sn[i + 1]) % 256;
                for (i = n; i >= 3; i += -1)
                    sn[i] = sn[i] ^ sn[i - 2] ^ (k3 * sn[i - 1]) % 256;
                for (i = 1; i <= n - 1; i++)
                    sn[i] = sn[i] ^ sn[i + 1] ^ (k2 * sn[i + 1]) % 256;
                for (i = n; i >= 2; i += -1)
                    sn[i] = sn[i] ^ sn[i - 1] ^ (k1 * sn[i - 1]) % 256;

                for (i = 1; i <= n; i++)
                    // Mid(ss, i, 1) = Chr(sn(i))
                    SBss.Append((char)(sn[i]));

                return SBss.ToString();
            }
            catch (Exception ex)
            {
                //Engine.LogEventi ev = new Engine.LogEventi(ex);
                //ev.TipoOperazione = "Engine.Cripto.StrDecode(string)";
                throw new Exception(ex.Message);
            }
        }
    }
}