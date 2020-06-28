using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace futureProsHW
{
    class UI
    {
        public static void showMainScreen()
       {
            Console.WriteLine("Hello, waiting for an array of numbers... (ex. [1,5,3,0,1][1,0,0,5])");
            string input = Console.ReadLine();
            Console.WriteLine();
            if (input == "history")
            {
                showHistory();
            }
            else
            {
                processArrays(input);
            }
            showMainScreen();
        }
        public static void processArrays(string input)
        {
            List<int[]> arrays = new List<int[]>();
            var mathes = Regex.Matches(input, @"\[(.*?)\]");
            foreach (Match m in mathes)
            {
                try
                {
                    arrays.Add(Array.ConvertAll(m.Groups[1].Value.Split(','), int.Parse));
                }
                catch (Exception exception)
                {
                    Console.WriteLine("bad input");
                }

            }
            searchInHistory(arrays);
        }
        public static void sendToSolve(List<int[]> arrays)
        {
            Pathfinder Pathfinder = new Pathfinder();
            List<Move> moves = new List<Move>();
            List<Move> result = new List<Move>();
            //cycling through given arrays
            foreach (var arr in arrays)
            {
                result = Pathfinder.solveProblem(arr, 0, 1, moves);
                Console.Write("[ ");
                //cycling through elements of each array
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }

                writeToFile(string.Join(',', arr));
                Console.WriteLine("]");
                showResults(result);
                Pathfinder.cleanBest ();
            }
        }
        public static void showResults(List<Move> result)
        {
            if (result.Count == 0)
            {
                Console.WriteLine("No path found =(");
                writeToFile("no path found");
            }
            else
            {
                foreach (var res in result)
                {
                    Console.WriteLine(res);
                    writeToFile(res.ToString());
                }
            }
            Console.WriteLine();
        }
        public static void writeToFile(string text)
        {

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var txtPath = Path.Combine(path, "futureProsData.txt");

            if (!File.Exists(txtPath))
            {
                using (StreamWriter sw = File.CreateText(txtPath));
            }
            using (StreamWriter sw = File.AppendText (txtPath))
            {
                sw.WriteLine(text);
            }
            
        }
        private static void searchInHistory(List<int[]> arrays)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var txtPath = Path.Combine(path, "futureProsData.txt");

            bool found = false;
            List<int[]> notFound = new List<int[]>();

            List<string> history = readFile();
            foreach (var arr in arrays)
            {
                if(arr.Length < 2)
                {
                    Console.WriteLine("array too short");
                    continue;
                }
                notFound.Add(arr);                                                      //user given aray not found in history so far
                foreach (var his in history)
                {
                    if (his == string.Join(',', arr))
                    {                        
                        Console.WriteLine('[' + his + "] found in history");                        //if array found in history, its removed from notFound
                        notFound.Remove(arr);
                        found = true;
                        continue;
                    }
                    if (found == true && !Char.IsDigit(his[0]))                             //only steps in history start with letters
                    {
                        Console.WriteLine(his);                       
                    }
                    if (found == true && Char.IsDigit(his[0]))                              //only array combinations start with digits in history
                    {
                        found = false;
                        continue;
                    }
                    
                }
            }
            sendToSolve(notFound);                                                          //arrays not found in history are sent to be solved
                
        }
        private static List<string> readFile()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var txtPath = Path.Combine(path, "futureProsData.txt");
            List<string> history = new List<string>();
            using (StreamReader sr = new StreamReader(txtPath))
            {
                string line;             
                    while ((line = sr.ReadLine()) != null)
                    {
                    history.Add(line);
                    }                
            }
            return history;
        }
        private static void showHistory()
        {
            List<string> history = readFile();

            foreach (var his in history)
            {
                Console.WriteLine(his);
            }
            Console.WriteLine();
        }
    }
}
