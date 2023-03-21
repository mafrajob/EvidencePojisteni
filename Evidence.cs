namespace EvidencePojisteni
{
    /// <summary>
    /// Reprezentuje kolekci pojistenych osoba a metod k manipulaci s nimi
    /// </summary>
    internal class Evidence
    {
        /// <summary>
        /// Kolekce pojistenych osob
        /// </summary>
        private List<Osoba> pojisteneOsoby = new List<Osoba>();

        /// <summary>
        /// Datum posledni upravy polozek Listu pojisteneOsoby
        /// </summary>
        private DateTime posledniUprava = DateTime.MinValue;

        /// <summary>
        /// Datum posledniho serazeni polozek Listu pojisteneOsoby
        /// </summary>
        private DateTime posledniSerazeni = DateTime.MinValue;

        /// <summary>
        /// Ulozi novou osobu do seznamu pojistenych
        /// </summary>
        /// <param name="jmeno">Krejstni jmeno max delky 25 znaku</param>
        /// <param name="prijmeni">Prijmeni max delky 25 znaku</param>
        /// <param name="vek">Vek v rozmezi 0 - 150 let</param>
        /// <param name="telefonniCislo">Telefonni cislo max delky 30 znaku</param> 
        public void PridejPojisteneho(string jmeno, string prijmeni, int vek, string telefonniCislo)
        {
            // Z metode predanych parametru vytvori novou instanci Osoby a ulozi ji do Listu
            pojisteneOsoby.Add(new Osoba(jmeno, prijmeni, vek, telefonniCislo));
            // Zaznamena posledni upravu Listu pojisteneOsoby do atribudu
            posledniUprava = DateTime.Now;
        }

        /// <summary>
        /// Prevede parametrem predany List<Osoba> na pole stringu
        /// </summary>
        /// <returns>Pole stringu s detaily pojistenych</returns>
        private string[] PrevedListNaPole(List<Osoba> listOsob)
        {
            // Na kazde Osobe v Listu zavola metodu ToString() a pak prevede na pole stringu
            return listOsob.ConvertAll(osoba => osoba.ToString()).ToArray();
        }

        /// <summary>
        /// Zajisti serazeni Listu pojisteneOsoby dle Prijmeni
        /// </summary>
        private void SeradPojisteneOsoby()
        {
            // Seradi existujici pojistene Osoby podle Prijmeni, pokud neni jiste, ze uz jsou serazene
            if (posledniSerazeni < posledniUprava)
            {
                pojisteneOsoby = pojisteneOsoby.OrderBy(osoba => osoba.Prijmeni).ToList();
                // Zaznamena posledni serazeni Listu pojisteneOsoby do atribudu
                posledniSerazeni = DateTime.Now;
            }
        }

        /// <summary>
        /// Vypise detaily vsech pojistenych osob
        /// </summary>
        /// <returns>Pole stringu s detaily pojistenych</returns>
        public string[] VypisPojistene()
        {
            // Overi, zda jsou Osoby v pojisteneOsoby serazene dle Prijmeni
            SeradPojisteneOsoby();
            // Prevede na pole stringu
            return PrevedListNaPole(pojisteneOsoby);
        }

        /// <summary>
        /// Vypise detaily pojistenych osob odpovidajicich predanym parametrum
        /// </summary>
        /// <param name="jmeno">Krejstni jmeno max delky 25 znaku</param>
        /// <param name="prijmeni">Prijmeni max delky 25 znaku</param>
        /// <returns>Pole stringu s detaily pojistenych</returns>
        public string[] VypisPojistene(string jmeno, string prijmeni) 
        {
            // Normalizuji predane parametry
            jmeno = jmeno.Trim().ToLower();
            prijmeni = prijmeni.Trim().ToLower();

            // Overi, zda jsou Osoby v pojisteneOsoby serazene dle Prijmeni
            SeradPojisteneOsoby();

            // Pomocny List pro ulozeny odpovidajich Osob
            List<Osoba> odpovidajiciOsoby = new List<Osoba>();

            foreach (Osoba pojistenaOsoba in pojisteneOsoby)
            {
                // Osoby v Listu pojisteneOsoby jsou nyni serazeny podle Prijmeni. Pokud je prvni pismeno Prijemeni pojistene Osoby vetsi nez
                // prvni pismeno prijmeni predane parametrem, jiz nemusim List prochazet a vyskocim z cyklu.
                int porovnaniPrvnihoPismena;

                // Predane prijmeni by melo mit alespon 1 znak, pokud ma 0 znaku (prazdny string), v hledani pokracujeme (vubec podle prijmeni nechceme filtrovat)
                if (prijmeni.Length > 0)
                {
                    porovnaniPrvnihoPismena = string.CompareOrdinal(pojistenaOsoba.Prijmeni.Substring(0, 1).ToLower(), prijmeni.Substring(0, 1));
                }
                else
                {
                    porovnaniPrvnihoPismena = 0;
                }

                // Hledane prijmeni uz se v pojisteneOsoby nemuze vyskytovat
                if (porovnaniPrvnihoPismena > 0) 
                {
                    break;
                }

                // Prvni pismeno hledaneho prijmeni odpovida zaznamu v pojisteneOsoby
                if (porovnaniPrvnihoPismena == 0) 
                {
                    // Kontrola, zda odpovidaji dalsi pismena prijmeni
                    if (pojistenaOsoba.Prijmeni.ToLower().StartsWith(prijmeni))
                    {
                        // Pokud opovida prijmeni, kontroluje krestni
                        if (pojistenaOsoba.Jmeno.ToLower().StartsWith(jmeno))
                        {
                            // Uklada odpovidajici Osobu do pomocneho Listu
                            odpovidajiciOsoby.Add(pojistenaOsoba);
                        }
                    }
                }
            }
            return PrevedListNaPole(odpovidajiciOsoby);
        }
    }
}
