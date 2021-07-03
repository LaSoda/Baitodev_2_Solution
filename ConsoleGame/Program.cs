using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcom to Hangman Game!");
            Console.WriteLine("if want stop game write (exit)");
            Console.WriteLine("");
            Console.WriteLine("");
            String UserWorld = string.Empty; 
            while (!UserWorld.ToLower().Equals("exit"))
            {
                Console.WriteLine("insert a number to play (0-9)");
                try
                {
                    var WorldUrl = "localhost:22058/api/word/" + Console.ReadLine();
                    using (var http = new HttpClient())
                    {
                        var response = await http.GetStringAsync(WorldUrl);
                        var WorlGet = JsonConvert.DeserializeObject<List<SelectedWord>>(response);
                        foreach (var p in WorlGet)
                        {
                            String[] GameWorl = new String[p.wordLenght];
                            for (int i = 0; i < p.wordLenght; i++)
                            {
                                GameWorl[i]=("_");
                            }
                            Console.WriteLine($"you word is: {GameWorl.ToString()}");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: write a valid option");
                }



            }

            
        }
    }
}
