namespace EvidencePojisteni
{
    /// <summary>
    /// Reprezentuje osobu pojisteneho
    /// </summary>
    internal class Osoba
    {   
        /// <summary>
        /// Max delka Jmena nebo Prijemeni
        /// </summary>
        public const int maxDelkaJmena = 25;
        
        /// <summary>
        /// Max vek cloveka
        /// </summary>
        public const int maxVek = 150;

        /// <summary>
        /// Max delka telefonniho cisla
        /// </summary>
        public const int maxDelkaTelefonnihoCisla = 30;

        /// <summary>
        /// Krejstni jmeno max delky 25 znaku
        /// </summary>
        public string Jmeno
        {
            get { return jmeno; }
            set { jmeno = NormalizujString(value, maxDelkaJmena); }
        }
        private string jmeno;

        /// <summary>
        /// Prijmeni max delky 25 znaku
        /// </summary>
        public string Prijmeni
        {
            get { return prijmeni; }
            set { prijmeni = NormalizujString(value, maxDelkaJmena); }
        }
        private string prijmeni;

        /// <summary>
        /// Vek v rozmezi 0 - 150 let
        /// </summary>
        public int Vek 
        { 
            get { return vek; } 
            set { vek = ZkontrolujVek(value); } 
        }
        private int vek;

        /// <summary>
        /// Telefonni cislo max delky 30 znaku
        /// </summary>
        public string TelefonniCislo 
        { 
            get { return telefonniCislo; } 
            set { telefonniCislo = NormalizujString(value, maxDelkaTelefonnihoCisla); }
        }
        private string telefonniCislo;

        /// <summary>
        /// Vytvori novou instanci Osoby
        /// </summary>
        /// <param name="jmeno">Krejstni jmeno max delky 25 znaku</param>
        /// <param name="prijmeni">Prijmeni max delky 25 znaku</param>
        /// <param name="vek">Vek v rozmezi 0 - 150 let</param>
        /// <param name="telefonniCislo">Telefonni cislo max delky 30 znaku</param>
        public Osoba(string jmeno, string prijmeni, int vek, string telefonniCislo)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Vek = vek;
            TelefonniCislo = telefonniCislo;
        }

        /// <summary>
        /// Normalizuje text na pozadovanou delku a osetri NullOrEmpty hodnotu
        /// </summary>
        /// <param name="s">Text k oriznuti</param>
        /// <param name="maxDelka">Pozadovana max delka</param>
        /// <returns>Normalizovany text</returns>
        private string NormalizujString(string s, int maxDelka)
        {
            s = s.Trim();
            // Null nebo Empty string nahradi *
            if (string.IsNullOrEmpty(s))
            {
                return "*";
            }
            // Pokud je delsi nez stanovena max delka, orizne string zleva
            return s.Substring(0, Math.Min(s.Length, maxDelka));
        }

        /// <summary>
        /// Nastavi vek na rozmezi 0 - 150 let
        /// </summary>
        /// <param name="vek">Vek ke konrole</param>
        /// <returns>Zkontrolovany vek</returns>
        private int ZkontrolujVek(int vek)
        {
            if(vek < 0)
            {
                return 0;
            }
            else if(vek > maxVek)
            {
                return maxVek;
            }
            else
            {
                return vek;
            }
        }

        /// <summary>
        /// Vypise informace o instanci Osoby
        /// </summary>
        /// <returns>Jednotlive vlastnosti odsazene na 30 znaku</returns>
        public override string ToString()
        {
            // Pozadovana delka stringu jednotlivych vlastnosti
            int delkaZaznamu = 30;
            return string.Concat(Jmeno.PadRight(delkaZaznamu), Prijmeni.PadRight(delkaZaznamu), Vek.ToString().PadRight(delkaZaznamu), TelefonniCislo.PadRight(delkaZaznamu));
        }
    }
}
