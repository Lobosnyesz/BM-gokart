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
        
        public adat(string vnev, string knev, DateTime szido, bool elmult)
        {
            this.vnev = vnev;
            this.knev = knev;
            this.szido = szido;
            this.elmult = elmult;
        }
        public static Random rnd = new Random();
        public static DateTime RandomDay()
        {
            DateTime start = new DateTime(1910, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
        public static bool elmultBool()
        {
            DateTime ma = DateTime.Today;
            DateTime szuletes = RandomDay();
            TimeSpan lobos = ma.Subtract(szuletes);
            if (Convert.ToInt32(lobos) < 18)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static void Main(string[] args)
        {
            /*
            Gokart
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

            StreamReader vn  = new StreamReader("vezeteknevek.txt");
            StreamReader kn  = new StreamReader("Keresztnevek.txt");
            List<string> vezeteknev = new List<string>();
            List<string> keresztnev = new List<string>();
            string sor;
            string[] reszek;
            while (!vn.EndOfStream)
            {
                sor = vn.ReadLine();
                reszek = sor.Replace("'","").Split(',');
                for(int i = 0; i < reszek.Length; i++)
                    vezeteknev.Add(reszek[i].Trim());
                sor = vn.ReadLine();
            }
            vn.Close();
            while (!kn.EndOfStream)
            {
                sor = kn.ReadLine();
                reszek = sor.Replace("'", "").Split(',');
                for(int i = 0; i < reszek.Length; i++)
                    keresztnev.Add(reszek[i].Trim());
                sor = kn.ReadLine();
            }
            kn.Close();
            bool elmult = elmultBool();
            Console.WriteLine(RandomDay().ToString("yyyy/MM/dd"));
            Console.WriteLine(elmult);

        Console.WriteLine();
            Console.WriteLine("kilépéshez nyomjon meg bármit");
            Console.ReadKey();
        }
    }
}

