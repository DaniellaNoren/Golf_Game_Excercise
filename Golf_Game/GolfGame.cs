using System;

namespace Golf_Game
{
    class GolfGame
    {

        private static readonly double GRAVITY = 9.8;
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            try
            {
                PlayGame();
            }
            catch (Exception)
            {
                Console.WriteLine("Exiting program...");
            }
        }

        public static void PlayGame()
        {
            Console.WriteLine("Play some golf biatch");
            double distanceToCup = GetInitialDistance();
            int nrOfSwings = 0;
            double[] allDistances = new double[20];

            do
            {
                Console.WriteLine($"Cup is {distanceToCup} m away");

                double[] inputs = GetTwoPlayerInputs();

                Console.WriteLine($"Shooting at cup at a {inputs[0]} degree angle at {inputs[1]}m/s");
                double distanceShot = Distance(inputs[1], AngleInRadians(inputs[0]));
                Console.WriteLine($"You shot {distanceShot}m");

                allDistances[nrOfSwings] = distanceShot;
                nrOfSwings++;

                distanceToCup = CalculateNewDistance(distanceShot, distanceToCup);

                

            } while (nrOfSwings < 20 && (distanceToCup > 0 && distanceToCup > 1));

            PrintResults(nrOfSwings, allDistances);

        }

        public static double CalculateNewDistance(double distanceShot, double distanceToCup)
        {

            if (distanceShot > distanceToCup)
            {
                if (distanceShot - distanceToCup > distanceToCup + 1000)
                {
                    throw new Exception("Shot too far!");
                }

                return distanceShot - distanceToCup;
            }
            else if (distanceShot < distanceToCup)
            {
                return distanceToCup - distanceShot;
            }
            else
            {
                return 0;
            }
        }
        public static void PrintResults(int nrOfSwings, double[] distances)
        {

            if (nrOfSwings < 20)
            {
                Console.WriteLine("You did it!!!");
            }
            else
            {
                Console.WriteLine("Failed! Took too many swings!");
            }

            Console.WriteLine($"You shot at the cup {nrOfSwings} times");

            foreach (double distance in distances)
            {
                if(distance > 0)
                    Console.WriteLine($"{distance}m");
            }

        }

        public static double GetInitialDistance()
        {
            return random.Next(1000, 2501);
        }

        public static double GetPlayerInput()
        {
            bool correctInput;
            double input;

            do
            {
                correctInput = Double.TryParse(Console.ReadLine().Trim().Replace('.', ','), out input);

                if (!correctInput)
                {
                    Console.WriteLine("Invalid input, try again");
                }

            } while (!correctInput);

            return input;

        }

        public static double[] GetTwoPlayerInputs()
        {
            Console.WriteLine("Angle?");
            double angle = GetPlayerInput();
            Console.WriteLine("Velocity? m/s");
            double velocity = GetPlayerInput();
            return new double[] { angle, velocity };
        }

        public static double AngleInRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double Distance(double velocity, double angleInRadians)
        {
            return Math.Pow(velocity, 2) / GRAVITY * Math.Sin(2 * angleInRadians);
        }
    }
}
