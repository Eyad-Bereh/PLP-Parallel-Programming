using System;
using System.Threading;
using System.Diagnostics;

namespace PLP_Assignment
{
    public class FirstScenario
    {
        private static SemaphoreSlim single = new SemaphoreSlim(1);
        private static void ProcessStudent(int student_number)
        {
            single.Wait();
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Asking student {0} question {1}", student_number, i);
                Thread.Sleep(1000);
            }
            single.Release();
        }

        public static void Execute()
        {
            for (int i = 1; i <= 16; i++)
            {
                new Thread(() => {
                    ProcessStudent(i);       
                }).Start();
            }
        }
    }
}

