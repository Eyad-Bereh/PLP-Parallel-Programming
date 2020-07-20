using System;
using System.Threading;

namespace PLP_Assignment
{
    public class FourthScenario
    {
        private const int LIMIT = 100;
        private static Barrier barrier          = new Barrier(LIMIT);
        private static SemaphoreSlim semaphore  = new SemaphoreSlim(LIMIT);
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
            for (int i = 1; i <= 1000; i++)
            {
                new Thread(() => {
                    ProcessStudent(i);
                }).Start();
                if (barrier.ParticipantCount < LIMIT) 
                {
                    barrier.SignalAndWait();
                }
            }
        }
    }
}

