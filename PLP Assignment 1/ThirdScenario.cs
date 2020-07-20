using System;
using System.Threading;

namespace PLP_Assignment
{
    public class ThirdScenario
    {
        private static Barrier barrier          = new Barrier(4);
        private static SemaphoreSlim semaphore  = new SemaphoreSlim(4);
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
                if (barrier.ParticipantCount < 4) 
                {
                    barrier.SignalAndWait();
                }
            }
        }
    }
}

