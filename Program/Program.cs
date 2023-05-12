using System;
using System.IO;

namespace Program
{
    internal class Program
    {
        private static void DataEntry(Zodiak[] people) 
        {
            for (int i = 0; i < people.Length; i++)
            {
                Console.WriteLine($"Введiть iнформацiю про людину №{i + 1}");
                Console.Write("Прiзвище: ");
                string lastName = Console.ReadLine();

                Console.Write("Iм'я: ");
                string firstName = Console.ReadLine();

                Console.Write("Дата народження (DD/MM/YYYY): ");
                DateTime birthDate;

                while (!DateTime.TryParseExact(Console.ReadLine(), "dd MM yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate))
                {
                    Console.WriteLine("Невiрний формат дати. Будь ласка, спробуйте ще раз!");
                    Console.Write("Дата народження (DD/MM/YYYY): ");
                }

                Console.Write("Знак зодiаку: ");
                string zodiacSign = Console.ReadLine();

                if (!Zodiak.CheckZodiacSign(birthDate, zodiacSign))
                {
                    Console.WriteLine("На жаль, дата народження не вiдповідає знаку зодiаку!");
                    Console.Write("Ви бажаєте змiнити дату або знак? (дата/знак/обидва): ");
                    string response = Console.ReadLine().ToLower();

                    while (response != "дата" && response != "знак" && response != "обидва")
                    {
                        Console.WriteLine("Невiрна вiдповiдь. Будь ласка, спробуйте ще раз!");
                        Console.Write("Ви бажаєте змiнити дату або знак? (дата/знак/обидва): ");
                        response = Console.ReadLine().ToLower();
                    }

                    if (response == "дата" || response == "обидва")
                    {
                        Console.Write("Нова дата народження (DD/MM/YYYY): ");

                        while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                        {
                            Console.WriteLine("Невiрний формат дати. Будь ласка, спробуйте ще раз!");
                            Console.Write("Нова дата народження (DD/MM/YYYY): ");
                        }
                    }

                    if (response == "знак" || response == "обидва")
                    {
                        Console.Write("Новий знак зодiаку: ");
                        zodiacSign = Console.ReadLine();
                    }
                }

                people[i] = new Zodiak(lastName, firstName, zodiacSign, birthDate);
            }
        }
        
        private static int SortPeople(Zodiak first, Zodiak second)
        {
            return first.BirthDate.CompareTo(second.BirthDate);
        }

        private static void SortedListOfPeople(Zodiak[] people, string textFile)
        {
            Console.WriteLine("Вiдсортований список людей:");
            foreach (Zodiak person in people)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
            }

            using (StreamWriter writer = new StreamWriter(textFile))
            {
                foreach (Zodiak person in people)
                {
                    writer.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
                }
            }
        }
        
        private static void SearchPeopleByLastName(Zodiak[] people)
        {
            Console.Write("Введiть прiзвище людини, яку шукаєте: ");
            string searchLastName = Console.ReadLine();
            bool found = false;

            foreach (Zodiak person in people)
            {
                if (person.FirstName == searchLastName)
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Людина з прiзвищем {searchLastName} не знайдена!");
            }
        }

        public static void Main()
        {
            const string textFile = "zodiac.txt";

            while (true)
            {
                Console.Write("Введiть кiлькiсть людей: ");
                int numberPeople = Convert.ToInt32(Console.ReadLine());
                
                if (numberPeople == 0) break;
                
                Zodiak[] people = new Zodiak[numberPeople];
    
                DataEntry(people);

                Array.Sort(people, SortPeople);
                
                SortedListOfPeople(people, textFile);
    
                SearchPeopleByLastName(people);
                
                Console.WriteLine("Введiть 0 для виходу з програми!");
            }
        }
    }
}