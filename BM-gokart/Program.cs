using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM_gokart
{
    public class adat
    {
        public string vnev;
        public string knev;
        public DateTime szido;
        public bool elmult;
        public string azon;
        public string email;

        public adat(string vnev, string knev, DateTime szido, bool elmult)
        {
            this.vnev = vnev;
            this.knev = knev;
            this.szido = szido;
            this.elmult = elmult;
            this.azon = "GO-" + EkezetNelkul(vnev + knev) + "-" + szido.ToString("yyyyMMdd");
            this.email = EkezetNelkul(vnev.ToLower()) + "." + EkezetNelkul(knev.ToLower()) + "@gmail.com";
        }


        public static Random rnd = new Random();
        public static DateTime RandomDay()
        {
            DateTime start = new DateTime(1910, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }

        static string EkezetNelkul(string szoveg)
        {
            string ekezetes = "áéíóöőúüűÁÉÍÓÖŐÚÜŰ";
            string ekezetNelkul = "aeiooouuuAEIOOOUUU";
            for (int i = 0; i < ekezetes.Length; i++)
                szoveg = szoveg.Replace(ekezetes[i], ekezetNelkul[i]);
            return szoveg;
        }
    }

    public class Foglalas
    {
        public string azon;
        public DateTime kezdet;
        public int idotartam;

        public Foglalas(string azon, DateTime kezdet, int idotartam)
        {
            this.azon = azon;
            this.kezdet = kezdet;
            this.idotartam = idotartam;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*
            Gokart időpontfoglaló - Egyéni kisprojekt
            BM - 2025.09.15
            */

            string fejlec = "GOKART";
            string lob = "Üdvözlünk a Zsámó Gokartpályán";
            Console.WriteLine(fejlec);
            for (int i = 0; i < lob.Length; i++) Console.Write('-');
            Console.WriteLine();

            Console.WriteLine(lob);
            for (int i = 0; i < lob.Length; i++) Console.Write('-'); Console.WriteLine();
            Console.WriteLine($"Címünk:\t\t 3500 Miskolc, Áfonyás utca 46.");
            Console.WriteLine("Telefonszámunk:  +36-30-420-6969");
            Console.WriteLine("Weboldalunk: \t ZsamoGo.hu");
            for (int i = 0; i < lob.Length; i++) Console.Write('-'); Console.WriteLine();

            StreamReader vn = new StreamReader("vezeteknevek.txt");
            StreamReader kn = new StreamReader("keresztnevek.txt");
            List<string> vezeteknev = new List<string>();
            List<string> keresztnev = new List<string>();
            string sor;
            string[] reszek;

            while (!vn.EndOfStream)
            {
                sor = vn.ReadLine();
                reszek = sor.Replace("'", "").Split(',');
                for (int i = 0; i < reszek.Length; i++)
                    vezeteknev.Add(reszek[i].Trim());
            }
            vn.Close();

            while (!kn.EndOfStream)
            {
                sor = kn.ReadLine();
                reszek = sor.Replace("'", "").Split(',');
                for (int i = 0; i < reszek.Length; i++)
                    keresztnev.Add(reszek[i].Trim());
            }
            kn.Close();

            //Versenyzok generalasa 1-150 kozott
            int darab = adat.rnd.Next(1, 151);
            List<adat> versenyzok = new List<adat>();
            for (int i = 0; i < darab; i++)
            {
                string v = vezeteknev[adat.rnd.Next(vezeteknev.Count)];
                string k = keresztnev[adat.rnd.Next(keresztnev.Count)];
                DateTime szul = adat.RandomDay();
                bool nagykoru = (DateTime.Today.Year - szul.Year > 18 ||
                                (DateTime.Today.Year - szul.Year == 18 && DateTime.Today.DayOfYear >= szul.DayOfYear));
                versenyzok.Add(new adat(v, k, szul, nagykoru));
            }

            Console.WriteLine("\nGenerált versenyzők:");

            //Fejlec
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"| {"Név",-25} | {"Születési idő",-15} | {"Elmúlt e 18?",-15} | {"Azonosító",-35} | {"Email cím",-35} |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");

            // Sorok
            foreach (var v in versenyzok)
            {
                string nev = v.vnev + " " + v.knev;
                string szulido = v.szido.ToString("yyyy.MM.dd");
                string elmult18 = v.elmult ? "Igen" : "Nem";

                Console.WriteLine($"| {nev,-25} | {szulido,-15} | {elmult18,-15} | {v.azon,-35} | {v.email,-35} |");
            }

            //Also lezaras
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------");

            //Tablazat kiirasa
            List<Foglalas> foglalasok = new List<Foglalas>();
            IdopontTabla(foglalasok);

            // Foglalas
            while (true)
            {
                Console.WriteLine("\nMelyik napra szeretne foglalni? (1-" + DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month) + ", kilépéshez 0): ");
                int nap = 0;
                if (!int.TryParse(Console.ReadLine(), out nap) || nap < 0 || nap > DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month))
                {
                    Console.WriteLine("Hibás nap, próbáld újra!");
                    continue;
                }
                if (nap == 0) break;

                Console.Write("Adja meg a kezdő órát (8-18): ");
                int kezdOra = 0;
                if (!int.TryParse(Console.ReadLine(), out kezdOra) || kezdOra < 8 || kezdOra > 18)
                {
                    Console.WriteLine("Hibás óra, próbáld újra!");
                    continue;
                }

                Console.Write("Hány órára szeretne foglalni? (1 vagy 2): ");
                int idotartam = 0;
                if (!int.TryParse(Console.ReadLine(), out idotartam) || (idotartam != 1 && idotartam != 2))
                {
                    Console.WriteLine("Hibás időtartam, csak 1 vagy 2 lehet!");
                    continue;
                }

                Console.Write("Adja meg a versenyző azonosítóját: ");
                string azon = Console.ReadLine();

                DateTime napDatum = new DateTime(DateTime.Today.Year, DateTime.Today.Month, nap);
                foglalasok.Add(new Foglalas(azon, new DateTime(napDatum.Year, napDatum.Month, napDatum.Day, kezdOra, 0, 0), idotartam));

                Console.WriteLine("Foglalás rögzítve!");
                IdopontTabla(foglalasok);
            }


            Console.WriteLine("\nKilépéshez nyomjon meg bármit...");
            Console.ReadKey();
        }

        static void IdopontTabla(List<Foglalas> foglalasok)
        {
            DateTime mai = DateTime.Today;
            int napokSzama = DateTime.DaysInMonth(mai.Year, mai.Month);

            //ora sor
            Console.Write("Dátum       ");
            for (int ora = 8; ora < 19; ora++)
                Console.Write($"{ora,5}   ");
            Console.WriteLine();

            //nap oszlop
            for (int nap = mai.Day; nap <= napokSzama; nap++)
            {
                DateTime aktualisNap = new DateTime(mai.Year, mai.Month, nap);
                Console.Write($"{aktualisNap:yyyy.MM.dd} ");

                for (int ora = 8; ora < 19; ora++)
                {
                    DateTime kezdet = new DateTime(aktualisNap.Year, aktualisNap.Month, aktualisNap.Day, ora, 0, 0);

                    bool foglalt = foglalasok.Any(f => f.kezdet == kezdet || (f.idotartam == 2 && f.kezdet == kezdet.AddHours(-1)));

                    if (foglalt)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("     F  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("     S  ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
