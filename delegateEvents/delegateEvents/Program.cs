using System;
using static System.Console;

namespace delegatesAndEvents
{
    public delegate void raceWinner(int champ);

    public class Race
    {
        public event raceWinner raceCompleted;

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;
            // first to finish specified number of laps wins
            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {

                    if (participants[i] <= laps)
                    {
                        participants[i] += r.Next(1, 5);
                    }
                    else
                    {
                        champ = i;
                        done = true;
                        continue;
                    }
                }

            }

            TheWinner(champ);
        }
        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
            raceCompleted(champ);

        }
    }
    class Program
    {
        public static void Main()
        {
            // create a class object
            Race race= new Race();
            // register with the footRace event
            race.raceCompleted += footRace;
            // trigger the event
            race.Racing(5, 5);
            // register with the carRace event
            race.raceCompleted-= footRace;
            race.raceCompleted += carRace;
            //trigger the event
            race.Racing(5, 5);
            // register a bike race event using a lambda expression
            race.raceCompleted -= carRace;
            race.raceCompleted += (champ)=> WriteLine($"Bike number {champ} is the winner.");
            // trigger the event
            race.Racing(5, 5);
        }

        // event handlers
        public static void carRace(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }
        public static void footRace(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
        }
    }
}