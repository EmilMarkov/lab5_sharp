using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace lab5
{
    class Program
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static public KeyValuePair<Edition, Magazine> generator(int j)
        {
            string value = (j * j * j * j * j * j).ToString();
            List<Person> persons = new List<Person>();
            List<Article> articles = new List<Article>();
            persons.Add(new Person(RandomString(j), RandomString(j), DateTime.Now));
            articles.Add(new Article(persons[0], RandomString(j), random.NextDouble()));
            Edition edition = new Edition(RandomString(j), j, DateTime.Now);
            Magazine magazine = new Magazine(RandomString(j), Frequency.Monthly, DateTime.Now, j, persons, articles);
            return new KeyValuePair<Edition, Magazine>(edition, magazine);
        }

        public static void Main()
        {
            Person person_1 = new Person("Игорь", "Дубов", new DateTime(1998, 7, 12));
            Person person_2 = new Person("Маша", "Берёзова", new DateTime(2003, 2, 1));
            Person person_3 = new Person("Саша", "Бочкин", new DateTime(1985, 1, 7));
            Person person_4 = new Person("Настя", "Летова", new DateTime(2000, 8, 21));


            Article article_1 = new Article(person_1, "Книга Игоря", 4.5);
            Article article_2 = new Article(person_2, "Книга Маши", 3.2);
            Article article_3 = new Article(person_3, "Книга Саши", 4);
            Article article_4 = new Article(person_4, "Книга Насти", 5);


            Magazine magazine_1 = new Magazine("Metro", Frequency.Monthly, DateTime.Now, 123);
            magazine_1.AddArticles(article_1, article_2, article_3, article_4);
            magazine_1.AddPersons(person_1, person_2, person_3, person_4);


            Console.WriteLine("\n------------------------\n");


            Magazine deep_copy = magazine_1.DeepCopy();
            Console.WriteLine("Оригинал: ");
            Console.WriteLine(magazine_1.ToString());
            Console.WriteLine("Копия: ");
            Console.WriteLine(deep_copy.ToString());


            Console.WriteLine("\n------------------------\n");


            Console.Write("Введите имя файла: ");
            Magazine magazine_load = new Magazine();
            string userFileName = Console.ReadLine() + ".bin"; 

            if (File.Exists(userFileName))
            {
                magazine_load.Load(userFileName);
            }
            else
            {
                Console.WriteLine("Файла с таким названием не найдено. Файл будет создан.");
                using (File.Create(userFileName));
            }


            Console.WriteLine("\n------------------------\n");


            Console.WriteLine(magazine_load.ToString());


            Console.WriteLine("\n------------------------\n");


            magazine_load.AddFromConsole();      // input exampe:  Максим-Козлов-1998-8-4-Книга Максима-3,8
            magazine_load.Save(userFileName);
            Console.WriteLine(magazine_load.ToString());


            Console.WriteLine("\n------------------------\n");


            Magazine.Load(userFileName, magazine_load);
            magazine_load.AddFromConsole();
            Magazine.Save(userFileName, magazine_load);


            Console.WriteLine("\n------------------------\n");


            Console.WriteLine(magazine_load.ToString());
        }
    }
}