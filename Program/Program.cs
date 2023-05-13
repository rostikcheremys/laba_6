using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Program
{ 
    public class Program
    {
        private static void DataEntry(Zodiak[] people) 
        {
            for (int i = 0; i < people.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Введiть iнформацiю про людину №{i + 1}");
                Console.ResetColor();
                Console.Write("Прiзвище: ");
                string lastName = Console.ReadLine();

                Console.Write("Iм'я: ");
                string firstName = Console.ReadLine();

                Console.Write("Дата народження (DD/MM/YYYY): ");
                DateTime birthDate;

                while (!DateTime.TryParseExact(Console.ReadLine(), "dd MM yyyy", null, System.Globalization.DateTimeStyles.None, out birthDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невiрний формат дати. Будь ласка, спробуйте ще раз!");
                    Console.ResetColor();
                    Console.Write("Дата народження (DD/MM/YYYY): ");
                }

                Console.Write("Знак зодiаку: ");
                string zodiacSign = Console.ReadLine();

                if (!Zodiak.CheckZodiacSign(birthDate, zodiacSign))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("На жаль, дата народження не вiдповідає знаку зодiаку!");
                    Console.ResetColor();
                    Console.Write("Ви бажаєте змiнити дату або знак? (дата/знак/обидва): ");
                    
                    string response = Console.ReadLine().ToLower();

                    while (response != "дата" && response != "знак" && response != "обидва")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невiрна вiдповiдь. Будь ласка, спробуйте ще раз!");
                        Console.ResetColor();
                        Console.Write("Ви бажаєте змiнити дату або знак? (дата/знак/обидва): ");
                        
                        response = Console.ReadLine().ToLower();
                    }

                    if (response == "дата" || response == "обидва")
                    {
                        Console.Write("Нова дата народження (DD/MM/YYYY): ");

                        while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Невiрний формат дати. Будь ласка, спробуйте ще раз!");
                            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Вiдсортований список людей:");
            Console.ResetColor();
            
            Array.Sort(people, SortPeople);
            
            foreach (Zodiak person in people)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
                Console.ResetColor();
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

        private static Zodiak[] WritingToAXmlFile(Zodiak[] people, string xmlFile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Zodiak[]));
            
            using (StreamWriter writer = new StreamWriter(xmlFile))
            {
                serializer.Serialize(writer, people);
            }

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
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{person.FirstName} {person.LastName} {person.Sign} {person.BirthDate:dd MM yyyy}");
                    Console.ResetColor();
                    
                    found = true;
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Людина з прiзвищем {search} не знайдена!");
                Console.ResetColor();
            }
        }

        private static void Choice(Zodiak[] people, string textFile, string jsonFile, string xmlFile)
        {
            Console.WriteLine("Введiть 1 для пошуку в Text File:\n" +
                                  "Введiть 2 для пошуку в JSON File:\n" +
                                  "Введiть 3 для пошуку в XMl File:\n" +
                                  "Введiть 4 для пошуку в усіх Files");
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
                        WritingToAXmlFile(people, xmlFile);
                        SearchPeopleByLastName(people);
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Пошук в Text File:");
                        Console.ResetColor();
                        SearchPeopleByLastName(WritingToATextFile(people, textFile));
        
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Пошук в JSON File:");
                        Console.ResetColor();
                        SearchPeopleByLastName(WritingToAJsonFile(people, jsonFile));
                        
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Пошук в XML File:");
                        Console.ResetColor();
                        SearchPeopleByLastName(WritingToAXmlFile(people, xmlFile));
                        break;
                    default:
                        return;
                }
        }
        
        private static void Task(string textFile, string jsonFile, string xmlFile)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            while (true)
            {
                Console.Write("Введiть кiлькiсть людей: ");
                int numberPeople = Convert.ToInt32(Console.ReadLine());

                if (numberPeople == 0) break;

                Zodiak[] people = new Zodiak[numberPeople];

                DataEntry(people);
                SortedListOfPeople(people);
                Choice(people, textFile, jsonFile, xmlFile);
                
                Console.WriteLine("Введiть 0 для виходу з програми!");
            }
        }
        public static void Main()
        {
            const string textFile = "zodiac.txt";
            const string jsonFile = "zodiac.json";
            const string xmlFile = "zodiac.xml";
            
            Task(textFile, jsonFile, xmlFile);
        }
    }
}