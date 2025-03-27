using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static severconnection connection;
        static void Main(string[] args)
        {
            connection = new severconnection("https://127.1.1.1:3000");
            start();
            Console.ReadKey();
        }
        static void start()
        {
            Console.WriteLine("mit akarsz");
            Console.WriteLine("");
            Console.WriteLine("1-create");
            Console.WriteLine("2-oszzes");


            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.Write("kolbasz neve: ");
                string name = Console.ReadLine();
                Console.Write("kolbasz erteke:");
                float rew = float.Parse(Console.ReadLine());
                Console.Write("kolbasz ara:");
                int price = Convert.ToInt32(Console.ReadLine());
                createkolbi(name,rew,price);
            }
            else if(input == "2")
            {
                kolbaszok();
            }


        }
        static async void createkolbi(string name, float rew,int price) {
            try
            {
                bool temp = await connection.createkolbasz(name, rew, price);
                if (temp) {
                    Console.WriteLine("sikeres letrehozas");
                    Console.Clear();
                    start();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
        }
        static async void kolbaszok() {
            Console.WriteLine(" ");
            Console.WriteLine("kolbazsok: ");
            List<kolbasz> all = await connection.allkolbasz();
            foreach (kolbasz item in all)
            {
                Console.WriteLine($"id:{item.id},nev:{item.kolbaszname},tetszes{item.kolbaszertekeles},ara:{item.kolbaszara}");
            }
        }
    }
}
