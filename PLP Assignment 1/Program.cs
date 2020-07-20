using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PLP_Assignment
{
    public class Program
    {
        public static int Main(string[] args)
        {   
            Console.Write("Choose which scenario to execute (1, 2, 3): ");
            int choice = Convert.ToInt32(Console.ReadLine());
            
            switch (choice) 
            {
                case 1:
                FirstScenario.Execute();
                break;

                case 2:
                SecondScenario.Execute();
                break;

                case 3:
                ThirdScenario.Execute();
                break;
            }
            return 0;
        }
    }
}

