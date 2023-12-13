using System;
using System.Linq;

namespace timetable_tape_alg
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Enter M - number of people:");
            int m = 2;//Int32.Parse(Console.ReadLine());; // number of people
            //Console.WriteLine("Enter N - number of Tasks:");
            int n = 4;//Int32.Parse(Console.ReadLine()); // number of tasks
            //Console.WriteLine("Enter Tasks with spaces");
            //string[] s = Console.ReadLine().Split(' ');
            int[] array = new int[] {1,2,5,4}; // tasks array
            // for (int i = 0; i < n; i++)
            //     array[i] = Int32.Parse(s[i]);
                
            int average = array.Sum() / m;
            Console.WriteLine($"sum : {array.Sum()}\naverage : {average}\narray : [{string.Join(", ", array)}]");
            
            for (int i = 0; i < n; i++)
            {
                for (int temps = 0; temps <= average;)
                {
                    temps += array[i];
                    if (temps > average)
                    {
                        array[i] = temps - average;
                        --i;
                    }
                    Console.Write(i+1 + " ");
                    ++i;
                }
                
                Console.WriteLine();
            }



        }
    }
}