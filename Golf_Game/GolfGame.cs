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
                double[] inputs = GetPlayerInput();
                Console.WriteLine($"Shooting at cup at a {inputs[0]} degree angle at {inputs[1]}m/s");
                double distanceShot = Distance(inputs[1], inputs[0]);
                Console.WriteLine($"You shot {distanceShot}m");
                nrOfSwings++;

                distanceToCup = CalculateNewDistance(distanceShot, distanceToCup);

                allDistances[nrOfSwings] = distanceShot;

            } while (nrOfSwings < 20 && distanceToCup != 0);

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

            Console.WriteLine($"You shot at the cup {nrOfSwings} nr of times");

            foreach (double distance in distances)
            {
                Console.WriteLine($"{distance}m");
            }

        }

        public static double GetInitialDistance()
        {
            return random.Next(1000, 2501);
        }

        public static double[] GetPlayerInput()
        {
            Console.WriteLine("Angle?");
            Double.TryParse(Console.ReadLine(), out double angle);
            Console.WriteLine("Velocity? m/s");
            Double.TryParse(Console.ReadLine(), out double velocity);
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
