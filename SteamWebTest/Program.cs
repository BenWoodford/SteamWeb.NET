using SteamWeb;
using SteamWeb.SteamWeb.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamWebTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SteamWebClient client = new SteamWebClient("test_web_client", "testingclient123", "https://localhost:8443");
            bool keepgoing = true;
            while (keepgoing)
            {
                Console.Write("Enter an instruction: ");
                string instruction = Console.ReadLine();

                switch (instruction)
                {
                    case "button":
                        Console.Write("Enter a Button: ");
                        string button = Console.ReadLine();
                        if (client.PressButton((Buttons)Enum.Parse(typeof(Buttons), button, true)))
                            Console.WriteLine("Success!");
                        else
                            Console.WriteLine("Doh.");
                        break;

                    case "games":
                        Dictionary<string, Game> games = client.GetGames();
                        if (games.Count > 0)
                            Console.WriteLine("First Game is: " + games.First().Value.Name);
                        break;

                    case "space":
                        Console.Write("Enter a Space: ");
                        string space = Console.ReadLine();
                        if (client.ChangeSpace((Spaces)Enum.Parse(typeof(Spaces), space, true)))
                            Console.WriteLine("Success!");
                        else
                            Console.WriteLine("Doh.");
                        break;

                    case "exit":
                        keepgoing = false;
                        break;

                    default:
                        Console.WriteLine("Invalid instruction.");
                        break;
                }
            }
        }
    }
}
