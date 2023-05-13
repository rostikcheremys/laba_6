using System;
using System.IO;
using Newtonsoft.Json;

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

        private static void SortedListOfPeople(Zodiak[] people)
        {
            Console.WriteLine("Вiдсортований список людей:");
            
            Array.Sort(people, SortPeople);
            
            foreach (Zodiak person in people)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
            }
        }

        private static Zodiak[] WritingToATextFile(Zodiak[] people, string textFile)
        {
            using (StreamWriter writer = new StreamWriter(textFile))
            {
                foreach (Zodiak person in people)
                {
                    writer.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
                }
            }

            return people;
        }

        private static Zodiak[] WritingToAJsonFile(Zodiak[] people, string jsonFile)
        {
            string json = JsonConvert.SerializeObject(people, Formatting.Indented);
            File.WriteAllText(jsonFile, json);

            return people;
        }
        
        private static void SearchPeopleByLastName(Zodiak[] people)
        {
            Console.Write("Введiть прiзвище людини, яку шукаєте: ");
            string search = Console.ReadLine();
            bool found = false;

            foreach (Zodiak person in people)
            {
                if (person.FirstName == search)
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Людина з прiзвищем {search} не знайдена!");
            }
        }

        public static void Main()
        {
            const string textFile = "zodiac.txt";
            const string jsonFile = "zodiac.json";

            while (true)
            {
                Console.Write("Введiть кiлькiсть людей: ");
                int numberPeople = Convert.ToInt32(Console.ReadLine());
                
                if (numberPeople == 0) break;
                
                Zodiak[] people = new Zodiak[numberPeople];
    
                DataEntry(people);
                SortedListOfPeople(people);

                Console.WriteLine("Введiть 1 для пошуку в Text File:\nВведiть 2 для пошуку в JSON File:\nВведіть 3 для пошуку в обох Files");
                int choice = Convert.ToInt32(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        WritingToATextFile(people, textFile);
                        SearchPeopleByLastName(people);
                        break;
                    case 2:
                        WritingToAJsonFile(people, jsonFile);
                        SearchPeopleByLastName(people);
                        break;
                    case 3:
                        Console.WriteLine("Пошук в Text File:");
                        SearchPeopleByLastName(WritingToATextFile(people, textFile));
        
                        Console.WriteLine("Пошук в JSON File:");
                        SearchPeopleByLastName(WritingToAJsonFile(people, jsonFile));
                        break;
                    default:
                        return;
                }
                
                Console.WriteLine("Введiть 0 для виходу з програми!");
            }
        }
    }
}