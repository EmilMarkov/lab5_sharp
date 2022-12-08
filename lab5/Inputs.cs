using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace lab5
{
    public class Inputs
    {
        static public int inputInt(string message)
        {
            int result = 0;
            while (true)
            {
                try
                {
                    Console.Write(message);
                    result = Convert.ToInt32(Console.ReadLine());
                    if (result >= 0) return result;
                    else throw new Exception("Less than 0 value");
                }
                catch
                {
                    Console.WriteLine("\nYou tap on wrong buttons, boy!\n");
                }
            }
        }

        static public double inputDouble(string message)
        {
            double result = 0;
            while (true)
            {
                try
                {
                    Console.Write(message);
                    result = double.Parse(Console.ReadLine());
                    if (result >= 0) return result;
                    else throw new Exception("Less than 0 value");
                }
                catch
                {
                    Console.WriteLine("\nYou tap on wrong buttons, boy!\n");
                }
            }
        }

        static public string inputString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static public DateTime inputDate(string message)
        {
            bool isSuccess = false;
            string targetDateFormat = "dd.MM.yyyy";
            DateTime dt = new DateTime();

            while(!isSuccess)
            {
                try
                {
                    Console.Write(message);
                    string enteredDateString = Console.ReadLine();

                    dt = DateTime.ParseExact(enteredDateString, targetDateFormat, CultureInfo.InvariantCulture);
                    isSuccess = true;
                }
                catch
                {
                    Console.WriteLine("\nYou tap on wrong buttons, boy!\n");
                }
            }

            return dt;
        }

        static public Person inputPerson()
        {
            string name = inputString("Введите имя: ");
            string surname = inputString("Введите фамилию: ");
            DateTime birthday = inputDate("Введите дату рожденеия: ");

            Person person = new Person(name, surname, birthday);

            return person;
        }

        static public Article inputArticle(List<Person> persons)
        {
            Person author;
            Console.WriteLine("Выберите из списка автора статьи:");
            for (int i = 0; i < persons.Count; i ++)
            {
                Console.WriteLine("\t" + i.ToString() + ": "+persons[i].Name);
            }

            int choice = inputInt(">> ");
            while (choice < 0 && choice >= persons.Count)
            {
                choice = inputInt("Wrong value! >>");
            }

            author = persons[choice];

            string title = inputString("Введите название статьи: ");
            double rating = inputDouble("Введите рейтинг: ");

            Article article = new Article(author, title, rating);

            return article;
        }

        static public Magazine inputMagazine()
        {
            Frequency frequency = Frequency.Weekly;
            List<Person> persons = new List<Person>();
            List<Article> articles = new List<Article>();
            DateTime date = DateTime.Now;

            String title = inputString("Введите название журнала: ");
            int edition = inputInt("Введите номер издания: ");

            int countPersons = inputInt("Введите количество авторов: ");
            for (int i = 0; i < countPersons; i++)
            {
                persons.Add(inputPerson());
            }

            int countArticles = inputInt("Введите количество статьей: ");
            for (int i = 0; i < countArticles; i++)
            {
                articles.Add(inputArticle(persons));
            }

            Magazine magazine = new Magazine(title, frequency, date, edition, persons, articles);

            return magazine;
        }
    }
}