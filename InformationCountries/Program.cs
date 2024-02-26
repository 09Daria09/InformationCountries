using InformationCountries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CodeFirst.LazyLoading
{
    class MainClass
    {
        static void Main()
        {
           
            try
            {
                while (true)
                {
                    Console.WriteLine("1. Показать всю информацию о странах");
                    Console.WriteLine("2. Показать названия стран");
                    Console.WriteLine("3. Показать названия столиц");
                    Console.WriteLine("4. Показать названия всех европейских стран");
                    Console.WriteLine("5. Показать названия стран с площадью больше N");
                    Console.WriteLine("6. Показать все страны, у которых в названии есть буквы 'a', 'е'");
                    Console.WriteLine("7. Показать все страны, у которых название начинается с буквы 'a'");
                    Console.WriteLine("8. Показать название стран, у которых площадь находится в указанном диапазоне");
                    Console.WriteLine("9. Показать название стран, у которых количество жителей больше указанного числа");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            ShowAllCountries();
                            break;
                        case 2:
                            ShowCountryNames();
                            break;
                        case 3:
                            ShowCapitalNames();
                            break;
                        case 4:
                            ShowEuropeanCountries();
                            break;
                        case 5:
                            AskForAreaAndShowCountries();
                            break;
                        case 6:
                            ShowCountriesWithLettersAE();
                            break;
                        case 7:
                            ShowCountriesStartingWithA();
                            break;
                        case 8:
                            AskForAreaRangeAndShowCountries();
                            break;
                        case 9:
                            AskForPopulationAndShowCountries();
                            break;
                        case 0:
                            return;
                    };
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AskForPopulationAndShowCountries()
        {
            Console.WriteLine("Введите минимальное количество жителей:");
            long minPopulation = long.Parse(Console.ReadLine()!);

            using (var db = new CountriesInfoContext())
            {
                var countriesWithPopulation = db.Countries
                                                .Where(c => c.Population >= minPopulation)
                                                .ToList();
                foreach (var country in countriesWithPopulation)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Население: {country.Population}");
                }
            }
        }


        private static void AskForAreaRangeAndShowCountries()
        {
            Console.WriteLine("Введите минимальную площадь:");
            double minArea = double.Parse(Console.ReadLine()!);
            Console.WriteLine("Введите максимальную площадь:");
            double maxArea = double.Parse(Console.ReadLine()!);

            using (var db = new CountriesInfoContext())
            {
                var countriesInRange = db.Countries
                                         .Where(c => c.Area >= minArea && c.Area <= maxArea)
                                         .ToList();
                foreach (var country in countriesInRange)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Площадь: {country.Area} кв. км");
                }
            }
        }

        private static void ShowCountriesStartingWithA()
        {
            using (var db = new CountriesInfoContext())
            {
                var countriesStartingWithA = db.Countries
                                                .Where(c => c.NameCountry.ToLower().StartsWith("a"))
                                                .ToList();
                foreach (var country in countriesStartingWithA)
                {
                    Console.WriteLine(country.NameCountry);
                }
            }
        }


        static void ShowCountriesWithLettersAE()
        {
            using (var db = new CountriesInfoContext())
            {
                var countriesWithAE = db.Countries
                                        .AsEnumerable() 
                                        .Where(c => c.NameCountry.Contains("a") && c.NameCountry.Contains("e"))
                                        .ToList();

                foreach (var country in countriesWithAE)
                {
                    Console.WriteLine(country.NameCountry);
                }
            }
        }


        static void ShowAllCountries()
        {
            using (var db = new CountriesInfoContext())
            {
                var countries = db.Countries.ToList();
                foreach (var country in countries)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Столица: {country.Capital}, Население: {country.Population}, Площадь: {country.Area} кв. км");
                }
            }
        }

        static void ShowCountryNames()
        {
            using (var db = new CountriesInfoContext())
            {
                var countryNames = db.Countries.Select(c => c.NameCountry).ToList();
                foreach (var name in countryNames)
                {
                    Console.WriteLine(name);
                }
            }
        }

        static void ShowCapitalNames()
        {
            using (var db = new CountriesInfoContext())
            {
                var capitalNames = db.Countries.Select(c => c.Capital).ToList();
                foreach (var capital in capitalNames)
                {
                    Console.WriteLine(capital);
                }
            }
        }

        static void ShowEuropeanCountries()
        {
            using (var db = new CountriesInfoContext())
            {
                var europeanCountries = db.Countries
                                          .Where(c => c.BigContinents.NameContinent == "Европа")
                                          .ToList();
                foreach (var country in europeanCountries)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Столица: {country.Capital}");
                }
            }
        }

        static void AskForAreaAndShowCountries()
        {
            Console.WriteLine("Введите минимальную площадь:");
            double minArea = double.Parse(Console.ReadLine()!);
            Console.WriteLine("Введите максимальную площадь:");
            double maxArea = double.Parse(Console.ReadLine()!);

            using (var db = new CountriesInfoContext())
            {
                var countriesByArea = db.Countries
                                        .Where(c => c.Area >= minArea && c.Area <= maxArea)
                                        .ToList();
                foreach (var country in countriesByArea)
                {
                    Console.WriteLine($"Страна: {country.NameCountry}, Площадь: {country.Area} кв. км");
                }
            }
        }


    }
}
