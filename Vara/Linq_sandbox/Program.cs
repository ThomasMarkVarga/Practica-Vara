namespace Linq_sandbox
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
    }

    public class Cumparate
    {
        public int No { get; set; }
        public int ItemId { get; set; }
        public int Cantitate { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Pr16();
        }
        // exercises 
        public static void Pr1() // even numbers 
        {
            int[] numbers = { 9, 11, 3, 7, 6, 14, 10, 12, 19, 5 };

            var query = from no in numbers
                        where no % 2 ==0
                        select no;

            foreach(int n in query)
            {
                Console.WriteLine(n);
            }
        }
        public static void Pr2() // positive numbers smaller then 12
        {
            int[] numbers = { 9, 17, -20, 0, 18, -16, 9, 11, -10, -12, 10 };

            var query = from no in numbers
                        where no >= 0 && no < 12
                        select no;

            foreach(var i in query)
            {
                Console.WriteLine(i);
            }
        }
        public static void Pr3() // number from array and its square root
        {
            int[] numbers = { 9, 8, 6, 5 };

            var query = from no in numbers
                        let noSqrt = no * no
                        select new
                        { no, noSqrt };

            foreach(var i in query)
            {
                Console.WriteLine(i);
            }
        }
        public static void Pr4() // number and its frequency
        {
            int[] numbers = { 11, 13, 14, 14, 20, 1, 9, 5, 7, 7 };

            var query = from no in numbers
                        group no by no into y
                        orderby y.Key
                        select y;

            foreach(var n in query)
            {
                Console.WriteLine("Number " + n.Key + " appears " + n.Count() + " times");
            }
        }
        public static void Pr5() // from a list of contries, takes the counties staring and ending with a specific letter
        {
            string[] cities = { "ROME", "LONDON", "NAIROBI", "CALIFORNIA", "ZURICH", "NEW DELHI", "AMSTERDAM", "ABU DHABI", "PARIS" };

            char startCh = 'N';
            char endCh = 'I';

            IEnumerable<string> query = from city in cities
                                        where city.StartsWith(startCh) && city.EndsWith(endCh)
                                        select city;

            foreach(string city in query)
            {
                Console.WriteLine(city);
            }

        }
        public static void Pr6() // numbers greater then 80
        {
            int[] numbers = { 55, 200, 740, 76, 230, 482, 95 };

            IEnumerable<int> query = from no in numbers
                                     where no > 80
                                     orderby no
                                     select no;
            
            foreach(int n in query)
            {
                Console.WriteLine(n + " is greater than 80");
            }

        }
        public static void Pr7() // enter a specified number of members and display values greater then an inserted number
        {
            int noOfMemebers, selectedNumber;
            List<int> memberList = new List<int>();

            Console.WriteLine("How many numbers do you want to insert?");
            noOfMemebers = int.Parse(Console.ReadLine());

            for(int i = 0; i < noOfMemebers; i++)
            {
                Console.WriteLine("Member {0} = ", i);
                int temp = int.Parse(Console.ReadLine());
                memberList.Add(temp);
            }

            Console.WriteLine("Insert threshhold number:");
            selectedNumber = int.Parse(Console.ReadLine());

            var query = from member in memberList
                        where member > selectedNumber
                        orderby member descending
                        select member;

            Console.WriteLine("The numbers greater than {0} are:", selectedNumber);
            foreach(int i in query)
            {
                Console.Write(i + " ");
            }

        }
        public static void Pr8() // display the top n-th records 
        {
            List<int> records = new List<int>{ 5, 7, 13, 24, 6, 9, 8, 7 };
            int displayRecords;

            Console.WriteLine("How many records do you want to see?");
            displayRecords = int.Parse(Console.ReadLine());

            records.Sort();
            records.Reverse();

            Console.WriteLine("The top {0} records are:", displayRecords);

            foreach(int rec in records.Take(displayRecords))
            {
                Console.Write(rec + "\t");
            }

        }
        public static void Pr9() // find uppercase words in a string
        {
            Console.WriteLine("Input string, include UPPERCASE words:");
            string input = Console.ReadLine();

            var upperWords = from word in input.Split(' ')
                         where word == word.ToUpper()
                         select word;

            //var upperWords = input.Split(' ').Where(x => String.Equals(x, x.ToUpper()));

            Console.WriteLine("UPPERCASE words:");
            foreach(string word in upperWords)
            {
                Console.WriteLine(word);
            }
        }
        public static void Pr10() // read file extension and group
        {
            string startFolder = "C:\\Users\\Tommy\\Desktop\\resurse\\fisiere_linq";

            DirectoryInfo dir = new DirectoryInfo(startFolder);

            var fileList = dir.GetFiles("*.*");

            var query = from file in fileList
                        group file by file.Extension;

            foreach(var i in query)
            {
                Console.WriteLine("Extension: " + i.Key + " file no: " + i.Count());
            }
        }
        public static void Pr11() // displays string of minumum lenght entered by user
        {
            string sentence = "This is an a string";
            string[] elements = sentence.Split(' ');

            Console.WriteLine("The starting sentence is: " + sentence);
            Console.WriteLine("Insert minimum length of string to display:");
            int minLength = int.Parse(Console.ReadLine());

            IEnumerable<string> strings = from item in elements
                                          where item.Length >= minLength
                                          orderby item.Length
                                          select item;
            Console.WriteLine("Words with min {0} letters:", minLength);
            foreach(string s in strings)
            {
                Console.WriteLine(s);
            }
        }
        public static void Pr12() // cartesian product with 2 sets
        {
            string[] letters = { "X", "Y", "Z" };
            int[] numbers = { 1, 2, 3, 4 };

            var query = from letter in letters
                        from number in numbers
                        select new
                        {
                            letter,
                            number
                        };

            foreach(var i in query)
            {
                Console.WriteLine(i);
            }
        }
        public static void Pr13() // cartesian product with 3 sets
        {
            string[] letters = { "X", "Y", "Z" };
            int[] numbers = { 1, 2, 3, 4 };
            string[] colors = { "Green", "Orange", "Yellow"};

            var query = from letter in letters
                        from number in numbers
                        from color in colors
                        select new
                        {
                            letter,
                            number,
                            color
                        };

            foreach(var item in query)
            {
                Console.WriteLine(item);
            }
        }
        public static void Pr14() // inner join on 2 data sets
        {
            List<Item> itemList = new List<Item>
            {
                new Item { ItemId = 1, ItemName = "Biscuiti"},
                new Item { ItemId = 2, ItemName = "Ciocolata"},
                new Item { ItemId = 3, ItemName = "Paine"},
                new Item { ItemId = 4, ItemName = "Lapte"}
            };

            List<Cumparate> purchList = new List<Cumparate>
            {
                new Cumparate{ No = 100, ItemId = 1, Cantitate = 20 },
                new Cumparate{ No = 101, ItemId = 2, Cantitate = 40 },
                new Cumparate{ No = 102, ItemId = 1, Cantitate = 30 },
                new Cumparate{ No = 103, ItemId = 3, Cantitate = 100},
                new Cumparate{ No = 104, ItemId = 2, Cantitate = 55 }
            };

            var query = from item in itemList
                        join f in purchList on item.ItemId equals f.ItemId
                        select new
                        {
                            id = item.ItemId,
                            name = item.ItemName,
                            qty = f.Cantitate
                        };

            foreach(var data in query)
            {
                Console.WriteLine(data.id + "\t" + data.name + " \t" + data.qty);
            }
        }
        public static void Pr15() // left join
        {
            List<Item> itemList = new List<Item>
            {
                new Item { ItemId = 1, ItemName = "Biscuiti"},
                new Item { ItemId = 2, ItemName = "Ciocolata"},
                new Item { ItemId = 3, ItemName = "Paine"},
                new Item { ItemId = 4, ItemName = "Lapte"}
            };

            List<Cumparate> purchList = new List<Cumparate>
            {
                new Cumparate{ No = 100, ItemId = 1, Cantitate = 20 },
                new Cumparate{ No = 101, ItemId = 2, Cantitate = 40 },
                new Cumparate{ No = 102, ItemId = 1, Cantitate = 30 },
                new Cumparate{ No = 103, ItemId = 3, Cantitate = 100},
                new Cumparate{ No = 104, ItemId = 2, Cantitate = 55 }
            };

            var query = from item in itemList
                        join prc in purchList
                        on item.ItemId equals prc.ItemId
                        into a
                        from b in a.DefaultIfEmpty(new Cumparate())
                        select new
                        {
                            id = item.ItemId,
                            name = item.ItemName,
                            qty = b.Cantitate
                        };

            foreach (var data in query)
            {
                Console.WriteLine(data.id + "\t" + data.name + " \t" + data.qty);
            }
        }
        public static void Pr16() // sort array of string according to the length of the string then by name, asc order
        {
            string[] cities = { "ROME","LONDON", "NAIROBI", "CALIFORNIA", "ZURICH", "NEW DELHI", "AMSTERDAM", "ABU DHABI",  "PARIS" };

            var query = from item in cities
                        orderby item.Length, item
                        select item;

            foreach(var item in query)
            {
                Console.WriteLine(item);
            }
        }


        // example 
        public static void Ex1()
        {
            // Specify the data source.
            int[] scores = { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            foreach (var i in scoreQuery)
            {
                Console.Write(i + " ");
            }

            // Output: 97 92 81
        }
        public static void Ex2()
        {
            // The Three Parts of a LINQ Query:
            // 1. Data source.
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            // 3. Query execution.
            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        public static void Ex3()
        {
/*            List<int> numbers;//= [1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20];

            IEnumerable<int> queryFactorsOfFour =
                from num in numbers
                where num % 4 == 0
                select num;

            // Store the results in a new variable
            // without executing a foreach loop.
            var factorsofFourList = queryFactorsOfFour.ToList();

            // Read and write from the newly created list to demonstrate that it holds data.
            Console.WriteLine(factorsofFourList[2]);
            factorsofFourList[2] = 0;
            Console.WriteLine(factorsofFourList[2]);*/
        }
        //Ex4 records
        record City(string Name, long Population);
        record Country(string Name, double Area, long Population, List<City> Cities);
        record Product(string Name, string Category);
        public static void Ex4()
        {

            City[] cities = {
                 new City("Tokyo", 37_833_000),
                 new City("Delhi", 30_290_000),
                 new City("Shanghai", 27_110_000),
                 new City("São Paulo", 22_043_000)
            };

            //Query syntax
            IEnumerable<City> queryMajorCities =
                from city in cities
                where city.Population > 100000
                select city;

            // Execute the query to produce the results
            foreach (City city in queryMajorCities)
            {
                Console.WriteLine(city);
            }

            // Output:
            // City { Population = 120000 }
            // City { Population = 112000 }
            // City { Population = 150340 }

            // Method-based syntax
            IEnumerable<City> queryMajorCities2 = cities.Where(c => c.Population > 100000);
        }
        public static void Ex5()
        {
            int[] numbers = { 5, 10, 8, 3, 6, 12 };

            //Query syntax:
            IEnumerable<int> numQuery1 =
                from num in numbers
                where num % 2 == 0
                orderby num
                select num;

            //Method syntax:
            IEnumerable<int> numQuery2 = numbers.Where(num => num % 2 == 0).OrderBy(n => n);

            foreach (int i in numQuery1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(System.Environment.NewLine);
            foreach (int i in numQuery2)
            {
                Console.Write(i + " ");
            }
        }
        public static void Ex6()
        {
            List<int> numbers = new List<int> { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // The query variables can also be implicitly typed by using var

            // Query #1.
            IEnumerable<int> filteringQuery =
                from num in numbers
                where num is < 3 or > 7
                select num;

            // Query #2.
            IEnumerable<int> orderingQuery =
                from num in numbers
                where num is < 3 or > 7
                orderby num ascending
                select num;

            // Query #3.
            string[] groupingQuery = { "carrots", "cabbage", "broccoli", "beans", "barley" };
            IEnumerable<IGrouping<char, string>> queryFoodGroups =
                from item in groupingQuery
                group item by item[0];

            Console.WriteLine("Filtering query");
            foreach(int i in filteringQuery)
            {
                Console.WriteLine(i + " ");
            }

            Console.WriteLine("Ordering query");
            foreach(int i in orderingQuery)
            {
                Console.WriteLine(i + " ");
            }

            Console.WriteLine("Grouping query");
            foreach(var i in queryFoodGroups)
            {
                Console.WriteLine(i + " ");
            }

        }
        public static void Ex7()
        {
            List<int> numbers1 = new List<int> { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            List<int> numbers2 = new List<int> { 15, 14, 11, 13, 19, 18, 16, 17, 12, 10 };

            // Query #4.
            double average = numbers1.Average();

            // Query #5.
            IEnumerable<int> concatenationQuery = numbers1.Concat(numbers2);

            Console.WriteLine("Average: " + average);

            Console.WriteLine("Concatenated");
            foreach(int i in concatenationQuery)
            {
                Console.WriteLine(i + " ");
            }
        }
        public static void Ex8()
        {
            string sentence = "the quick brown fox jumps over the lazy dog";
            // Split the string into individual words to create a collection.
            string[] words = sentence.Split(' ');

            // Using query expression syntax.
            var query = from word in words
                        group word.ToUpper() by word.Length into gr
                        orderby gr.Key
                        select new { Length = gr.Key, Words = gr };

            // Using method-based query syntax.
            var query2 = words.
                GroupBy(w => w.Length, w => w.ToUpper()).
                Select(g => new { Length = g.Key, Words = g }).
                OrderBy(o => o.Length);

            foreach (var obj in query)
            {
                Console.WriteLine("Words of length {0}:", obj.Length);
                foreach (string word in obj.Words)
                    Console.WriteLine(word);
            }
        }
        public static void Ex9()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            char[] letters = { 'a', 'b', 'c', 'd', 'e' };

            var query = from number in numbers
                        from letter in letters
                        select (number, letter);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
        public static void Ex10()
        {
            // An int array with 7 elements.
            IEnumerable<int> numbers = new int[]{1, 2, 3, 4, 5, 6, 7 };
            // A char array with 6 elements.
            IEnumerable<char> letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
            // A string array with 8 elements.
            IEnumerable<string> emoji = new string[] {"🤓", "🔥", "🎉", "👀", "⭐", "💜", "✔", "💯"};


            foreach ((int number, char letter, string em) in numbers.Zip(letters, emoji))
            {
                Console.WriteLine(
                    $"Number: {number} is zipped with letter: '{letter}' and emoji: {em}");
            }
        }
        public static void Ex11()
        {
            int chunkNumber = 1;
            foreach (int[] chunk in Enumerable.Range(0, 8).Chunk(3))
            {
                Console.WriteLine($"Chunk {chunkNumber++}:");
                foreach (int item in chunk)
                {
                    Console.WriteLine($"    {item}");
                }

                Console.WriteLine();
            }
        }
        public static void Ex12()
        {
            List<int> numbers = new List<int> { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

            IEnumerable<IGrouping<int, int>> query = from number in numbers
                                                     group number by number % 2;

            foreach (var group in query)
            {
                Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
                foreach (int i in group)
                {
                    Console.WriteLine(i);
                }
            }
        }
        public static void Ex13()
        {
            string startFolder = "C:\\Program Files\\dotnet\\sdk";
            // Or
            // string startFolder = "/usr/local/share/dotnet/sdk";

            int trimLength = startFolder.Length;

            DirectoryInfo dir = new DirectoryInfo(startFolder);

            var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

            var queryGroupByExt = from file in fileList
                                  group file by file.Extension.ToLower() into fileGroup
                                  orderby fileGroup.Count(), fileGroup.Key
                                  select fileGroup;

            // Iterate through the outer collection of groups.
            foreach (var filegroup in queryGroupByExt.Take(5))
            {
                Console.WriteLine($"Extension: {filegroup.Key}");
                var resultPage = filegroup.Take(20);

                //Execute the resultPage query
                foreach (var f in resultPage)
                {
                    Console.WriteLine($"\t{f.FullName.Substring(trimLength)}");
                }
                Console.WriteLine();
            }
        }
        public static void Ex14()
        {
            string startFolder = "C:\\Program Files\\dotnet\\sdk";
            // Or
            // string startFolder = "/usr/local/share/dotnet/sdk";

            DirectoryInfo dir = new DirectoryInfo(startFolder);

            var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

            string searchTerm = "change";

            var queryMatchingFiles = from file in fileList
                                     where file.Extension == ".txt"
                                     let fileText = File.ReadAllText(file.FullName)
                                     where fileText.Contains(searchTerm)
                                     select file.FullName;

            // Execute the query.
            Console.WriteLine("The term " + searchTerm + " was found in:");
            foreach (string filename in queryMatchingFiles)
            {
                Console.WriteLine(filename);
            }
        }
    }
}