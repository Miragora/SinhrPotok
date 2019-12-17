using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


namespace SinhrPotok
{
    class Program
    {
        static int[] GetDigits(int x)
        {
            var result = new List<int>((int)Math.Log10(x) + 1);
            while (x > 0)
            {
                result.Add(x % 10);
                x = x / 10;
            }
            return result.ToArray();

        }

        static int GetDigitsSum(int x)
        {
            var digits = GetDigits(x);
            return digits.Sum();
        }

        static void TimeUpdates()
        {
            while (true)
            {
                Console.Title = DateTime.Now.ToString();
                Thread.Sleep(100);
            }
        }
        static void Main()
        {
            const int x_min = 100;
            const int x_max = 500;
            var sums = new List<int>();


            Thread time_thread = new Thread(TimeUpdates);
            time_thread.Name = "Timer";
            time_thread.Priority = ThreadPriority.BelowNormal;
            time_thread.IsBackground = true;
            time_thread.Start();
            var start = DateTime.Now; //начало времени поиска 
            for (int x = x_min; x <= x_max; x++)
            {
                var sum = GetDigitsSum(x);
                sums.Add(sum);
                Console.Write("Sum({0}) = ", x);
                Console.WriteLine(sum);

            }
            Console.WriteLine("Total sum = {0}", sums.Sum());

            var stop = DateTime.Now; // конец процесса
            var time = stop - start;
            Console.WriteLine("время процесса {0}", time);
            Console.ReadLine();
        }

       

        static void Main2()
        {
            const int x_min = 100;
            const int x_max = 500;
            var sums = new List<int>();

            Thread time_thread = new Thread(TimeUpdates);
            time_thread.Name = "Timer";
            time_thread.Priority = ThreadPriority.BelowNormal;
            time_thread.IsBackground = true;
            time_thread.Start();
            var start = DateTime.Now; //начало времени поиска 
            for (int x = x_min; x <= x_max; x++)
            {
                var x0 = x;
                var thread = new Thread(() =>
               {
                   lock(sums)
                   {
                       Console.Write("Sum({0}) = ", x0);
                       var sum = GetDigitsSum(x0);
                       sums.Add(sum);
                       Console.WriteLine(sum);
                   }
               });
                thread.Start();
                

            }
            Console.WriteLine("Total sum = {0}", sums.Sum());
            
            var stop = DateTime.Now; // конец процесса
            var time = stop - start;
            Console.WriteLine("время процесса {0}", time);

            Console.ReadLine();
        }

        
    }

    }


