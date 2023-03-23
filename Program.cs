using System.Security.Policy;
using System.Text;

namespace EvidencePojisteni
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Zajistí správné zobrazení diakritiky i pro OS v AJ
            Console.OutputEncoding = Encoding.UTF8;

            // Vytvori instanci Evidence k praci se pojistenymi
            Evidence evidence = new Evidence();

            // Cyklus a promenna ridici beh programu
            bool konecProgramu = false;
            while (!konecProgramu)
            {
                // Promenne pro praci s uzivatelskymi vstupy
                string jmeno = "";
                string prijmeni = "";
                int vek;
                string telefonniCislo;
                int akce;

                // Cyklus na vyber akce v rozmezi 1 - 4
                do
                {
                    VypisZahlavi();
                    // Kontroluje uzivatelsky vstup na datovy typ a existenci cisla akce
                } while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out akce) || akce < 1 || akce > 4);
                Console.Write("\n\n");

                // Provede vybranou akci
                switch (akce)
                {
                    // Pridat noveho pojisteneho
                    case 1:
                        jmeno = ZjistiJmeno();
                        prijmeni = ZjistiPrijmeni();

                        // Cyklus na zadani ciselne hodnoty veku v rozmezi 0 - 150 let
                        do
                        {
                            Console.WriteLine($"Zadejte věk v letech (rozmezí 0 - {Osoba.maxVek}):");
                        } while (!int.TryParse(Console.ReadLine(), out vek) || vek < 0 || vek > 150);

                        Console.WriteLine("Zadejte telefonní číslo:");
                        telefonniCislo = Console.ReadLine();

                        // Prida Osobu do Evidence pojistenych
                        evidence.PridejPojisteneho(jmeno, prijmeni, vek, telefonniCislo);
                        Console.WriteLine("\nData byla uložena. Pokračujte libovolnou klávesou...");
                        Console.ReadKey();
                        break;

                    // Vypsat vsechny pojistene
                    case 2:
                        VypisZaznamyPojistenych(evidence, jmeno, prijmeni);
                        break;

                    // Vyhledat pojisteneho
                    case 3:
                        jmeno = ZjistiJmeno();
                        prijmeni = ZjistiPrijmeni();
                        VypisZaznamyPojistenych(evidence, jmeno, prijmeni);
                        break;

                    // Konec
                    case 4:
                        konecProgramu = true;
                        Console.WriteLine("Aplikace bude ukončena. Pokračujte libovolnou klávesou...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Vyzve uzivatele k zadani jmena
        /// </summary>
        /// <returns>Vstup uživatele</returns>
        private static string ZjistiJmeno()
        {
            Console.WriteLine($"Zadejte jméno pojištěného (max {Osoba.maxDelkaJmena} znaků):");
            return Console.ReadLine();
        }
        /// <summary>
        /// Vyzve uzivatele k zadani prijmeni
        /// </summary>
        /// <returns>Vstup uživatele</</returns>
        private static string ZjistiPrijmeni()
        {
            Console.WriteLine($"Zadejte příjmení (max {Osoba.maxDelkaJmena} znaků):");
            return Console.ReadLine();
        }

        /// <summary>
        /// Vypise pojistene osoby (akce 2 nebo 3) do konzole
        /// </summary>
        /// <param name="evidence">Evidence ve které metoda hleda</param>
        /// <param name="jmeno">Krestni jmeno pojisteneho</param>
        /// <param name="prijmeni">Prijmeni pojisteneho</param>
        private static void VypisZaznamyPojistenych(Evidence evidence, string jmeno, string prijmeni)
        {
            string[] detailyPojistenych;

            // Kontroluje, zda bude treba filtrovat podle jmena a/nebo prijmeni
            if (string.IsNullOrEmpty(jmeno) && string.IsNullOrEmpty(prijmeni))
            {
                // Ulozi do pole vsechny existujici zaznamy
                detailyPojistenych = evidence.VypisPojistene();
            }
            else
            {
                // Ulozi do pole zaznamy odpovidajici predanym parametrum
                detailyPojistenych = evidence.VypisPojistene(jmeno, prijmeni);
            }

            int pocetZaznamu = detailyPojistenych.Length;
            if (pocetZaznamu > 0)
            {
                // Cyklus vypise pojistene, pokud byly nalezeny nejake zaznamy
                foreach (string detailPojisteneho in detailyPojistenych)
                {
                    Console.WriteLine(detailPojisteneho);
                }
            }

            // Shrnuti pro uzivalete aplikace
            Console.WriteLine($"\nNalezeno {pocetZaznamu} záznam(ů). Pokračujte libovolnou klávesou...");
            Console.ReadKey();
        }

        /// <summary>
        /// Vypise zahlavy programu s akcemi uzivatele do konzole
        /// </summary>
        private static void VypisZahlavi()
        {
            Console.Clear();
            string podtrzeni = new string('-', 20);
            string[] nazvyAkci = new string[] { "1 - Přidat nového pojištěného", "2 - Vypsat všechny pojištěné", "3 - Vyhledat pojištěného", "4 - Konec" };
            Console.WriteLine($"{podtrzeni}\nEvidence pojištěných\n{podtrzeni}\n");
            Console.WriteLine("Vyberte si akci:");
            foreach (string nazevAkce in nazvyAkci)
            {
                Console.WriteLine(nazevAkce);
            }
            Console.WriteLine("Vybraná akce:");
        }
    }
}