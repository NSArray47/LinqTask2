using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;


namespace LinqProj
{
    public class Client
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int TrainingsTime { get; set; }

        public void InputClient(StreamReader reader)
        {
            var list = reader.ReadLine().Split(' ').ToList().Select(Int32.Parse).ToList();
            this.Id = list[0];
            this.Year = list[1];
            this.Month = list[2];
            this.TrainingsTime = list[3];
        }
    }


    public class Program
    {
        private static bool Sure()
        {
            Console.WriteLine("Are you sure to exit? Press y, if you are");
            var choise = "n";
            choise = Console.ReadLine();
            return choise == "y";
        }

        public static void Main(string[] args)
        {
            var choice = "q";
            do
            {
                Console.Write("\nSelect task\n1 - LinqBegin16\n2 - LinqBegin17\n3 - " +
                              "LinqBegin18\n4 - LinqBegin19\n5 - LinqBegin20\n6 - LinqBegin44\n7 - LinqBegin45\n8 - LinqObj1\nq - Exit.\n");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        LinqBegin16();
                        break;
                    case "2":
                        LinqBegin17();
                        break;
                    case "3":
                        LinqBegin18();
                        break;
                    case "4":
                        LinqBegin19();
                        break;
                    case "5":
                        LinqBegin20();
                        break;
                    case "6":
                        LinqBegin44();
                        break;
                    case "7":
                        LinqBegin45();
                        break;
                    case "8":
                        LinqObj1();
                        break;
                    case "q":
                    {
                        if (Sure() == false)
                        {
                            choice = "n";
                        }

                        break;
                    }
                    default:
                        Console.WriteLine("Wrong input. Try again");
                        continue;
                }
            } while (choice != "q");

            Console.WriteLine("Bye-bye! ^)");
        }

        private static void LinqBegin16()
        {
            int[] arr = {1, 5, 2, 8, -2, -6, 5, 7, 4};
            IEnumerable<int> res = arr.Where(i => i > 0);
            foreach (var elem in res)
            {
                Console.Write($"{elem} ");
            }
        }

        private static void LinqBegin17()
        {
            int[] arr = {1, 5, 2, 3, 5, 7, 4, 7};
            IEnumerable<int> res = arr.Where(i => i % 2 != 0).Distinct();
            foreach (var elem in res)
            {
                Console.Write($"{elem} ");
            }
        }

        private static void LinqBegin18()
        {
            int[] arr = {-34, 6, 26, 8, 28, -4, 25 - 65, 34};
            var res = arr.Where(i => (i > 0 && (i.ToString().Length == 2 && i.ToString()[0] != '0'))).OrderBy(i => i);
            foreach (var elem in res)
            {
                Console.Write($"{elem} ");
            }
        }

        private static void LinqBegin19()
        {
            string[] arr = {"wiskas", "e", "rexmac", "corns", "arrow", "powersharp", "lwiv", "zeta"};
            var res = arr.OrderBy(i => i.Length).ThenBy(i => i);
            foreach (var elem in res)
            {
                Console.Write($"{elem} ");
            }
        }

        private static void LinqBegin20()
        {
            int[] arr = {1, 4, 74, -7, -4, 7, -47, 4, 74};
            const int d = 3;
            var res = arr.SkipWhile(i => i <= d).Where(i => (i > 0 && i % 2 != 0)).Reverse();
            foreach (var elem in res)
            {
                Console.Write($"{elem} ");
            }
        }

        private static void LinqBegin44()
        {
            int[] A = {4, 7, 47, -4, 7, -7, -42, 7, 1};
            int[] B = {25, 12, -56, -21, 64, 14, 74, -4, 7};
            const int k1 = 3;
            const int k2 = 5;
            var res = A.Where(i => i > k1).Concat(B.Where(i => i < k2)).OrderBy(i => i);
            foreach (var elem in res)
            {
                Console.Write($"{elem} ");
            }
        }

        private static void LinqBegin45()
        {
            string[] A = {"lpsdf", "erash5c", "2reahxahmac", "orn3cs", "12ab5acw", "polegon523"};
            string[] B = {"wefg", "4tfb", "45y6mac", "ys245orncs", "a35w", "i35or4mv", "yv92", "10asz2g5"};
            const int l1 = 5;
            const int l2 = 4;
            var res = A.Where(i => i.Length == l1).Concat(B.Where(i => i.Length == l2)).OrderByDescending(i => i);
            foreach (var elem in res)
            {
                Console.Write($"{elem} ");
            }
        }

        private static Client FindMaximumClient(List<Client> clientsList)
        {
            int maxVal = clientsList.Max(it => it.TrainingsTime);
            Client result = clientsList.First(it => it.TrainingsTime == maxVal);
            return result;
        }
        
        private static Client FindMinimumClient(List<Client> clientsList)
        {
            int minVal = clientsList.Min(it => it.TrainingsTime);
            Client result = clientsList.First(it => it.TrainingsTime == minVal);
            return result;
        }

        
        private static List<Client> ReadClients(StreamReader reader)
        {
            var n = int.Parse(reader.ReadLine());
            List<Client> returnList = new List<Client>();
            for (int i = 0; i < n; i++)
            {
                Client c = new Client();
                c.InputClient(reader);
                returnList.Add(c);
            }

            return returnList;
        }

        private static int YearWithMaxTrainings(List<Client> clientsList)
        {
            IEnumerable<IGrouping<int, Client>> years = clientsList.GroupBy(c => c.Year);
            IEnumerable<IGrouping<int, Client>> sortedYears = years.OrderByDescending(g => g.Sum(c => c.TrainingsTime)).ThenBy(c => c.Min(cl => cl.Year));
            return sortedYears.First().Key;
        }

        private static SortedDictionary<int, int> SumTime(List<Client> clientsList)
        {
            IEnumerable<IGrouping<int, Client>> groupedClients = clientsList.GroupBy(t => t.Id);
            SortedDictionary<int ,int> res = new SortedDictionary<int, int>();
            foreach (var it in groupedClients)
            {
                int suma = it.Sum(t => t.TrainingsTime);
                res.Add(it.Key, suma);
            }

            return res;
        }
        
        private static SortedDictionary<int, int> SumMonth(List<Client> clientsList)
        {
            IEnumerable<IGrouping<int, Client>> groupedClients = clientsList.GroupBy(t => t.Id);
            SortedDictionary<int ,int> res = new SortedDictionary<int, int>();
            foreach (var it in groupedClients)
            {
                int suma = it.Sum(t => t.Month);
                res.Add(it.Key, suma);
            }

            return res;
        }
        
        private static void LinqObj1()
        {
            StreamReader reader = new StreamReader(new FileStream("clients.txt", FileMode.Open));
            List<Client> clientsList = ReadClients(reader);
            StreamWriter writer = new StreamWriter(new FileStream("result.txt", FileMode.Create));
            writer.WriteLine($"Client with minimum trainings: {FindMinimumClient(clientsList)}");
            writer.WriteLine($"Client with maximum trainings: {FindMaximumClient(clientsList)}");
            writer.WriteLine($"The year with maximal total trainings time: {YearWithMaxTrainings(clientsList)}");
            writer.WriteLine();
            SortedDictionary<int, int> dictionary = SumTime(clientsList);
            foreach (var it in dictionary)
            {
                writer.WriteLine($"Client with ID: {it.Key} had trained for {it.Value} hours.");
            }
            writer.WriteLine();
            
            dictionary = SumMonth(clientsList);
            foreach (var it in dictionary)
            {
                writer.WriteLine($"Client with ID: {it.Key} had trained for {it.Value} month(s).");
            }
            writer.WriteLine();
            writer.Close();
            reader.Close();
            Console.WriteLine("Success!");
        }
    }
}