using System;

namespace Lab4
{
    class Program
    {
        private const int NumberOfSensors = 4;
        private const double Eps = 0.0001;

        private static double[] xWave = new double[NumberOfSensors];
        private static double[] xDelta = new double[NumberOfSensors];
        private static double[] dispersion = new double[NumberOfSensors];
        private static double[] p = new double[NumberOfSensors];
        private static double[] q = new double[NumberOfSensors];
        private static double[] qDelta = new double[NumberOfSensors];
        private static double k = 0;
        private static double l = 0;
        private static double lStar = 0;
        private static double lambda = 0;

        static void Main(string[] args)
        {
            xWave[0] = 12;
            xWave[1] = 10;
            xWave[2] = 14;
            xWave[3] = 34.2;
            lStar = 1.6;
            xDelta[0] = 0.45;
            xDelta[1] = 0.45;
            xDelta[2] = 0.45;
            xDelta[3] = 0.64;
            dispersion[0] = 0.3;
            dispersion[1] = 0.2;
            dispersion[2] = 0.36;
            dispersion[3] = 0.34;



            l = xWave[0] + xWave[1] + xWave[2] - xWave[3];
            if(Math.Abs(l) > lStar)
            {
                double sum = 0;
                for (int i = 0; i < NumberOfSensors; i++)
                    sum += OneDivSquare(dispersion[i]);
                k = 1.0 / sum;
                Console.WriteLine($"k = {k}");

                for(int i = 0; i < NumberOfSensors; i++)
                {
                    p[i] = k * OneDivSquare(dispersion[i]);
                }
                OutputArray(p);

                lambda = l * (2 * p[0] * p[1] * p[2] * p[3]) /
                    (p[0] * p[1] * p[2] - p[1] * p[2] * p[3] - p[0] * p[2] * p[3] - p[0] * p[1] * p[3]);
                Console.WriteLine($"lambda = {lambda}");

                for (int i = 0; i < NumberOfSensors; i++)
                {
                    qDelta[i] = - lambda / (2 * p[i]);
                }
                OutputArray(qDelta);

                for (int i = 0; i < NumberOfSensors; i++)
                {
                    q[i] = xWave[i] - qDelta[i];
                }

                double check = q[0] + q[1] + q[2] - q[3];
                if (check < Eps)
                {
                    Console.WriteLine("Everything is OK.");
                }
                else
                {
                    Console.WriteLine("Got some error");
                }
                OutputArray(q);
                Console.WriteLine($"Check is {check}");
            }
            else
            {
                Console.WriteLine("Everything is OK.");
            }
        }

        private static double OneDivSquare(double value)
        {
            return 1.0 / (value * value);
        }

        private static void OutputArray(double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
    }
}
