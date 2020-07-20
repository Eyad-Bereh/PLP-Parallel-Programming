using System;
using System.Threading;

namespace PLP_Assignment
{
    public class SecondScenario
    {
        private static Barrier barrier          = new Barrier(2);
        private static SemaphoreSlim semaphore  = new SemaphoreSlim(2);
        private static void ProcessStudent(object student_number)
        {
            semaphore.Wait();
            for (int i = 1; i <= 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Asking student {0} question {1}", student_number, i);        
            }
            semaphore.Release();
        }

        public static void Execute()
        {
            for (int i = 1; i <= 16; i++)
            {
                new Thread(() => {
                    ProcessStudent(i);
                }).Start();
                if (barrier.ParticipantCount < 2) 
                {
                    barrier.SignalAndWait();
                }
            }
        }
    }
}

