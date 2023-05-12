using System;

namespace Program
{
    struct Zodiak
    {
        public string FirstName;
        public string LastName;
        public string Sign;
        public DateTime BirthDate;

        public Zodiak(string firstName, string lastName, string zodiacSign, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Sign = zodiacSign;
            BirthDate = birthDate;
        }

        public static bool CheckZodiacSign(DateTime date, string zodiacSign)
        {
            int month = date.Month;
            int day = date.Day;
            switch (zodiacSign.ToLower())
            {
                case "водолій":
                    return month == 1 && day >= 20 || month == 2 && day <= 18;
                case "риби":
                    return month == 2 && day >= 19 || month == 3 && day <= 20;
                case "овен":
                    return month == 3 && day >= 21 || month == 4 && day <= 19;
                case "телець":
                    return month == 4 && day >= 20 || month == 5 && day <= 20;
                case "близнюки":
                    return month == 5 && day >= 21 || month == 6 && day <= 20;
                case "рак":
                    return month == 6 && day >= 21 || month == 7 && day <= 22;
                case "лев":
                    return month == 7 && day >= 23 || month == 8 && day <= 22;
                case "діва":
                    return month == 8 && day >= 23 || month == 9 && day <= 22;
                case "терези":
                    return month == 9 && day >= 23 || month == 10 && day <= 22;
                case "скорпіон":
                    return month == 10 && day >= 23 || month == 11 && day <= 21;
                case "стрілець":
                    return month == 11 && day >= 22 || month == 12 && day <= 21;
                case "козеріг":
                    return month == 12 && day >= 22 || month == 1 && day <= 19;
                default:
                    return false;
            }
        }
    }
}
