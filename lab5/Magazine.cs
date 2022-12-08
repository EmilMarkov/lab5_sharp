using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace lab5
{
    [Serializable]
    public class Magazine : Edition, IRateAndCopy
    {
        private Frequency frequency;
        private List<Person> persons = new List<Person>();
        private List<Article> articles = new List<Article>();

        public Magazine()
        {
            this.name = "Название журнала";
            this.frequency = Frequency.Weekly;
            this.date = new DateTime(2008, 5, 1);
            this.circulation = 0;
            this.articles.Add(new Article());
            this.persons.Add(new Person());
        }

        public Magazine(string titleValue, Frequency frequencyValue, DateTime dateValue, int editionValue, List<Person> personsValue, List<Article> articlesValue)
        {
            this.name = titleValue;
            this.frequency = frequencyValue;
            this.date = dateValue;
            this.circulation = editionValue;
            this.persons = personsValue;
            this.articles = articlesValue;
        }
        
        public Magazine(string titleValue, Frequency frequencyValue, DateTime dateValue, int editionValue)
        {
            this.name = titleValue;
            this.frequency = frequencyValue;
            this.date = dateValue;
            this.circulation = editionValue;
            this.persons = new List<Person>();
            this.articles = new List<Article>();
        }

        public List<Article> Articles
        {
            get => articles;
            set { this.articles = value; }
        }

        public double Rating { get; }

        public Frequency Frequency { get; }

        public List<Person> Persons
        {
            get => persons;
            set { this.persons = value; }
        }

        public double avgRating()
        {
            int N = 0;
            double sumRating = 0.0;
            foreach (Article article in articles)
            {
                sumRating += article.Rating;
                N++;
            }

            return sumRating / N;
        }

        public bool this[Frequency index]
        {
            get => index == frequency;
        }

        public void addArticle(Article article)
        {
            this.articles.Add(article);
        }
        
        public void AddArticles(params Article[] newArticles)
        {
            if (articles == null) articles = new List<Article>();
            articles.AddRange(newArticles);
        }
        
        public void AddPersons(params Person[] newEditors)
        {
            if (persons == null) persons = new List<Person>();
            persons.AddRange(newEditors);
        }

        public void addEditors(Person person)
        {
            this.persons.Add(person);
        }

        public Edition edition
        {
            get
            {
                Edition edition = new Edition(this.name, this.circulation, this.date);
                return edition;
            }
            set
            {
                this.name = value.Name;
                this.circulation = this.Circulation;
                this.date = value.Date;
            }
        }

        public int GetHashCode()
        {
            return HashCode.Combine(name.GetHashCode(), frequency.GetHashCode(), date.GetHashCode(), circulation.GetHashCode(), articles.GetHashCode(), persons.GetHashCode());
        }


        public override string ToString()
        {
            string result = "";
            double ratingSum = 0.0;

            foreach (Article article in articles)
            {
                ratingSum += article.Rating;
            }

            result += "\n";
            result += "*****************************\n";
            result += "Название журнала: " + this.name + '\n';
            result += "Периодичность выхода журнала: " + this.frequency + '\n';
            result += "Дата публикации: " + this.date.ToShortDateString() + '\n';
            result += "Издание: " + this.circulation + '\n';
            if (articles.Count != 0)
            {
                foreach (Article article in articles)
                {
                    result += "------------------------------\n";
                    result += "Автор статьи: " + article.Author.Name + " " + article.Author.Surname + '\n';
                    result += "Название статьи: " + article.Title + '\n';
                    result += "Рейтинг статьи: " + article.Rating + '\n';
                }
            }
            else
            {
                Console.WriteLine("Статьи не обнаружены!");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            result += "*****************************\n";
            Console.ForegroundColor = ConsoleColor.White;
            result += "\n";
            return result;
        }

        public virtual void ToShortString()
        {
            string result = "";
            double ratingSum = 0.0;

            foreach (Article article in articles)
            {
                ratingSum += article.Rating;
            }

            result += "\n";
            result += "*****************************\n";
            result += "Название журнала: " + this.name + '\n';
            result += "Периодичность выхода журнала: " + this.frequency + '\n';
            result += "Дата публикации: " + this.date.ToShortDateString() + '\n';
            result += "Издание: " + this.circulation + '\n';
            if (articles.Count != 0)
            {
                foreach (Article article in articles)
                {
                    result += "------------------------------\n";
                    result += "Автор статьи: " + article.Author.Name + " " + article.Author.Surname + '\n';
                    result += "Название статьи: " + article.Title + '\n';
                }
            }
            else
            {
                Console.WriteLine("Статьи не обнаружены!");
            }
            result += "------------------------------\n";
            result += "Средний рейтинг статей: " + this.avgRating() + '\n';
            result += "*****************************\n";
            result += "\n";
        }

        public new Magazine DeepCopy()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (Magazine)formatter.Deserialize(stream);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка копирования: " + e.Message);
                Console.ResetColor();
                return new Magazine();
            }

        }
        
        
        // SORT

        public IEnumerable<Article> byRating(double ratingValue)
        {
            foreach (Article a in this.articles)
            {
                if (a.Rating >= ratingValue)
                    yield return a;
            }
        }

        public IEnumerable<Article> byNameSubstring(string subString)
        {
            foreach (Article a in this.articles)
            {
                if (a.Author.Name.IndexOf(subString) > -1)
                    yield return a;
            }
        }

        public void sortByRating()
        {
            articles.Sort();
        }

        public void sortBySurname()
        {
            ArticleComparerBySurname comparer = new ArticleComparerBySurname();
            articles.Sort(comparer);
        }

        public void sortByTitle()
        {
            ArticleComparerByTitle comparer = new ArticleComparerByTitle();
            articles.Sort(comparer);
        }
        
        // FILE
        public bool Save(string filename)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в экземплярном сохранении: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        
        public bool Load(string filename)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    if (fs.Length != 0)
                    {
                        var m = (Magazine)formatter.Deserialize(fs);
                        name = m.name;
                        date = m.date;
                        circulation = m.circulation;
                        frequency = m.frequency;
                        if (articles != null) articles.Clear();
                        articles = new List<Article>();
                        if (m.articles != null) articles.AddRange(m.articles);
                        if (persons != null) persons.Clear();
                        persons = new List<Person>();
                        if (m.persons != null) persons.AddRange(m.persons);
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в экземплярной загрузке: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        
        //статический метод для сохранения данных объекта в файле с помощью сериализации
        public static bool Save(string filename, Magazine obj)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    formatter.Serialize(fs, obj);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в статическом сохранении: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }

        //статический метод для инициализации объекта данными из файла с помощью десериализации
        public static bool Load(string filename, Magazine obj)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    var m = (Magazine)formatter.Deserialize(fs);
                    obj.name = m.name;
                    obj.date = m.date;
                    obj.circulation = m.circulation;
                    obj.frequency = m.frequency;
                    if (obj.articles != null) obj.articles.Clear();
                    obj.articles = new List<Article>();
                    if (m.articles != null) obj.articles.AddRange(m.articles);
                    if (obj.persons != null) obj.persons.Clear();
                    obj.persons = new List<Person>();
                    if (m.persons != null) obj.persons.AddRange(m.persons);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка в статической загрузке: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }
        
        public bool AddFromConsole()
        {
            try
            {
                Console.WriteLine(
                    $"\tВведите информацию о добавляемой статье через тире(-) в представленном порядке:" +
                    $"\n\t\t1)Имя автора" +
                    $"\n\t\t2)Фамилия автора" +
                    $"\n\t\t3)Год (дата рождения)" +
                    $"\n\t\t4)Месяц (дата рождения)" +
                    $"\n\t\t5)День (дата рождения)" +
                    $"\n\t\t6)Название статьи" +
                    $"\n\t\t7)Рейтинг статьи"
                );
                string[] words = Console.ReadLine().Split('-', (char)StringSplitOptions.RemoveEmptyEntries);
                var tempAuthor = new Person(words[0], words[1],new DateTime(Convert.ToInt32(words[2]),Convert.ToInt32(words[3]),Convert.ToInt32(words[4])));
                if (articles == null) articles = new List<Article>();
                articles.Add(new Article(tempAuthor, words[5], Convert.ToDouble(words[6])));
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка вводе: " + e.Message);
                Console.ResetColor();
                return false;
            }

        }
    }
}