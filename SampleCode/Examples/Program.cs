using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples
{
    class Program
    {
        private static List<bool> wins = new List<bool>();
        private static int roundsPlayed = 0;
        private static int roundsWon = 0;
        private static string[] menu = new string[] { "Play A Game", "Best out of 3", "Best out of __", "Check Score", "Quit"};
        private static Random r = new Random();
        private static Dictionary<string, List<string>> objs = new Dictionary<string, List<string>>() { 
            //____ beats ____
            { "rock", new List<string>() {"scissors"} },
            { "paper",new List<string>() {"rock" } },
            { "scissors", new List<string>(){"paper" } }
        };

        static void Main(string[] args)
        {
            bool inProg = true;
            while (inProg)
            {
                if(roundsPlayed!= 0)
                    Console.WriteLine($"Won {roundsWon} out of {roundsPlayed}");

                for (int i = 0; i < menu.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {menu[i]}");
                }

                int option;
                if(int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            PlayRound();
                            break;
                        case 2:
                            PlayRounds(3);
                            break;
                        case 3:
                            int rounds;
                            Console.Write("How Many Games: ");
                            if(int.TryParse(Console.ReadLine(), out rounds))
                            {
                                PlayRounds(rounds);
                            }

                            break;
                        case 4:
                            CheckWins();
                            break;
                        case 5:
                            inProg = false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                }
                
            }
            

        }
        private static void PlayRounds(int rounds)
        {
            double winsNeeded = Math.Round(rounds / 2d, MidpointRounding.AwayFromZero);
            int pWins = 0, cpuWins = 0;
            for (int i = 0; i < rounds && pWins < winsNeeded && cpuWins < winsNeeded ; i++)
            {
                bool didWin = PlayRound();
                if (didWin)
                    pWins++;
                else
                    cpuWins++;
                wins.Add(didWin);
            }
        }
        

        private static bool PlayRound()
        {
            bool repeat = false;
            bool result = false;
            string input;
            string cpuInput;


            do
            {
                repeat = false;
            Console.Clear();
            Console.WriteLine("Rock, Paper, or Scissors?");

            input = Console.ReadLine().ToLower();
            cpuInput = objs.Keys.ElementAt(r.Next(objs.Count));

            Console.WriteLine($"CPU chose {cpuInput}");
            if (input.Equals(cpuInput))
            {
                Console.WriteLine("You tied, try again!");
                    Console.ReadLine();
                    repeat = true;
            }
             else if (!objs.ContainsKey(input))
                {
                    Console.WriteLine("Invalid Input, try again!");
                    Console.ReadLine();
                    repeat = true;
                }
            } while (repeat);

            bool wasFound = false;
            foreach (var beat in objs[input])
            {
                if (beat.Equals(cpuInput))
                {
                    wasFound = true;
                    result = true;
                    wins.Add(true);
                    roundsWon++;
                    Console.WriteLine($"{input} beats {cpuInput}");
                    Console.WriteLine("You Won!");
                }
            }



            roundsPlayed++;
            if(!wasFound)
            {
                wins.Add(false);
                Console.WriteLine($"{cpuInput} beats {input}");
                Console.WriteLine("You Lost :(");
                result = false;
            }

            Console.WriteLine("\n\n\nPress Any Key To Continue...");
            Console.Read();
            return result;
        }

        private static void CheckWins()
        {
            Console.Clear();
            string otherStuff = "";
            Console.Write("|");
            for (int i = 0; i < wins.Count; i++)
            {
                Console.Write($"{i+1}|");
                otherStuff += "--";
            }
            Console.Write("\n" + otherStuff + "\n|");
            foreach (var win in wins)
            {
                if (win)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("W");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("L");
                }
            Console.ResetColor();
            Console.Write("|");


                Console.WriteLine("\n\n\nPress Any Key To Continue...");
                Console.Read();
            }
        }
    }
}
