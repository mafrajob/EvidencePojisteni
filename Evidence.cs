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
        /// Vypise detaily vsech pojistenych osob
        /// </summary>
        /// <returns>Pole stringu s detaily pojistenych</returns>
        public string[] VypisPojistene()
        {
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
            
            // Seradi existujici pojistene Osoby podle Prijmeni
            pojisteneOsoby = pojisteneOsoby.OrderBy(osoba => osoba.Prijmeni).ToList();

            // Pomocny List pro ulozeny odpovidajich Osob
            List<Osoba> odpovidajiciOsoby = new List<Osoba>();

            foreach (Osoba pojistenaOsoba in pojisteneOsoby)
            {
                // Osoby v Listu pojisteneOsoby jsou nyni serazeny podle Prijmeni. Pokud je prvni pismeno Prijemeni pojistene Osoby vetsi nez
                // prvni pismeno prijmeni predane parametrem, jiz nemusim List prochazet a vyskocim z cyklu.
                if (string.CompareOrdinal(pojistenaOsoba.Prijmeni.Substring(0, 1).ToLower(), prijmeni.Substring(0, 1)) > 0)
                {
                    break;
                }

                // Kontrola, zda odpovida prijmeni
                if (pojistenaOsoba.Prijmeni.ToLower().StartsWith(prijmeni))
                {
                    // Pokud opovida prijmeni, kontroluje krestni
                    if(pojistenaOsoba.Jmeno.ToLower().StartsWith(jmeno))
                    {
                        // Uklada odpovidajici Osobu do pomocneho Listu
                        odpovidajiciOsoby.Add(pojistenaOsoba);
                    }
                }
            }
            return PrevedListNaPole(odpovidajiciOsoby);
        }
    }
}
