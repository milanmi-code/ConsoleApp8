using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp7
{
    class Program
    {
        static ServerConnection connection;

        static void Main(string[] args)
        {
            connection = new ServerConnection("http://127.1.1.1:3000");
            start();
            Console.ReadKey();

        }
        static void start()

        {
           
            Console.WriteLine(" 1 - Create");
            Console.WriteLine(" 2 - Lekérdezés");
            Console.WriteLine(" 3 - Törlés");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ");
            Console.Write("Choose: ");
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (input == "1")
            {
                Console.Write("Kolbász neve: ");
                string name = Console.ReadLine();
                Console.Write("Kolbász értékelése (1,1): ");
                float ertekeles = float.Parse(Console.ReadLine());
                Console.Write("Kolbász ára (Teljes szám legyen): ");
                int ar = Convert.ToInt32(Console.ReadLine());
                createKolbasz(name, ertekeles, ar);
            }
            else if (input == "2")
            {
                kolbaszok();

            }
            else if (input == "3")
            {
                Console.Write("Kolbász ID-je: ");
                int id = Convert.ToInt32(Console.ReadLine());
                deleteKolbasz(id);
            }
            else
            {
                Console.WriteLine("Indítsd újra");
            }
            Console.ReadKey();

        }
        static async void deleteKolbasz(int id)
        {
            try
            {
                bool temp = await connection.deleteKolbi(id);
                if (temp)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Sikeres törlés");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Indítsd újra :)");

                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static async void createKolbasz(string name, float ertekeles, int ar)
        {
            try
            {
                bool temp = await connection.createKolbi(name, ertekeles, ar);
                if (temp)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Sikeres létrehozás");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Indítsd újra :)");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        static async void kolbaszok()
        {
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Összes kolbász:");
            List<kolbasz> all = await connection.AllKolbi();
            foreach (kolbasz item in all)
            {

                Console.WriteLine($"ID: {item.id}, Név: {item.kolbaszNeve}, Értékelés: {item.kolbaszErtekelese}, Ár: {item.kolbaszAra}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------");
            Console.WriteLine("Indítsd újra :)");
        }
    }
}