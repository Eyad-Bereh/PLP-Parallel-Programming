using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPI;
using MPIUtils;

namespace PLP_Assignment
{
    class Program
    {
        private const string PATH = @"input.txt";   //  Path to text file
        private const int ROOT_PROCESS_ID = 0;  //  Define Root Process ID
        public static int Main(string[] args)
        {
            using (new MPI.Environment(ref args))   //  Initialize MPI Environment
            {
                Intracommunicator comm = Communicator.world;    //  Define a shortcut to world communicator
                string[] contents = null;   //  Right here, we will store the lines of the text file
                char what = '\0';   //  The character which the user enters

                int totalCharacters = 0;    //  Total characters per process
                int totalOccurrences = 0;   //  Total occurrences per process
                double totalTime = 0;       //  Total time per process
                bool verbose = false;       //  Verbose output on command line
                if (args.Length > 0 && args[0].ToLower() == "--verbose")    //  If the flag "--verbose" exists, set verbose = true
                {
                    verbose = true;
                }

                int allCharacters;  //  All characters by all processes
                int allOccurrences; //  All occurrencess by all processes

                //  If we're in the root process
                if (comm.Rank == ROOT_PROCESS_ID)
                {
                    //  Ask the user to enter the character he want to search for
                    Console.Write("Enter the character you want to search for: ");
                    what = (char)Console.Read();
                    contents = File.ReadAllLines(PATH); //  Load contents of file inside the array
                }
                comm.Broadcast(ref contents, ROOT_PROCESS_ID);  //  Broadcast contents from root
                comm.Broadcast(ref what, ROOT_PROCESS_ID);      //  Broadcast character from root

                //  All processes must reach this point before going on
                //  This is important for time measurement
                comm.Barrier();
                double start = MPI.Environment.Time;    //  The start of time measuring

                //  Let's divide the load for processes, each process is responsible for a specific number of lines
                for (int i = comm.Rank; i < contents.Length; i += comm.Size)
                {
                    if (verbose)
                        Console.WriteLine("Process ID = {0}, Line number = {1}", comm.Rank, i);
                    for (int j = 0; j < contents[i].Length; j++)
                    {
                        if (contents[i][j] == what) //  An occurrence has been found
                        {
                            if (verbose)
                                Console.WriteLine("Process {0} has found the character at {1}:{2}", comm.Rank, i + 1, j + 1);
                            totalOccurrences += 1;
                        }
                        totalCharacters += 1;
                    }
                }

                //  All processes must reach here before we stop the timer
                comm.Barrier();
                double end = MPI.Environment.Time;  //  The end of time measurement

                allOccurrences = comm.Reduce(totalOccurrences, Operation<int>.Add, ROOT_PROCESS_ID);    //  Accumulate all occurrencess
                allCharacters = comm.Reduce(totalCharacters, Operation<int>.Add, ROOT_PROCESS_ID);      //  Accumulate all characters
                totalTime = comm.Reduce(end - start, Operation<double>.Max, ROOT_PROCESS_ID);           //  The overall time is the longest time of all processes

                Console.WriteLine("Total occurrences found by process {0} is {1}", comm.Rank, totalOccurrences);
                Console.WriteLine("Total characters processed by {0} is {1}", comm.Rank, totalCharacters);

                //  Finally, if we're here, print all information
                if (comm.Rank == ROOT_PROCESS_ID)
                {              
                    Console.WriteLine("Overall occurrences = {0}", allOccurrences);
                    Console.WriteLine("Overall characters = {0}", allCharacters);
                    Console.WriteLine("Overall time = {0}s", totalTime);
                }
            }
            return 0;
        }
    }
}
