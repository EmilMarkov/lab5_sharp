using System;

namespace lab5
{
    public enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }

    [Serializable]
    public  class Article:IRateAndCopy, IComparable<Article>
    {
        private Person author;
        private string title;
        private double rating { get; set; }

        public Article()
        {
            author = new Person();
            author.Name = "Имя автора";
            author.Surname = "Фамилия автора";
            author.Birthday = new DateTime(2008, 5, 1);
            title = "Название статьи";
            rating = 0.0;
        }

        public Article(Person authorValue, string titleValue, double ratingValue)
        {
            author = authorValue;
            title = titleValue;
            rating = ratingValue;
        }

        public Person Author
        {
            get => author;
            set { this.author = value; }
        }

        public string Title
        {
            get => this.title;
            set { this.title = value; }
        }

        public double Rating
        {
            get => this.rating;
            set { this.rating = value; }
        }

        public int GetHashCode()
        {
            return HashCode.Combine(author.GetHashCode(), title.GetHashCode(), rating.GetHashCode());
        }


        public override string ToString()
        {
            return this.author.Name + " " + this.author.Surname + " " + this.author.Birthday.ToShortDateString() + " " + this.title + " " + this.rating;
        }

        public virtual object DeepCopy()
        {
            Article article = new Article();
            article.Author = this.Author;
            article.title = this.title;
            article.rating = this.rating;
            return (object)article;
        }

        public int CompareTo(Article obj)
        {
            Article temp = new Article();
            if (temp == null) throw new InvalidCastException("\nInvalid value!");
            else return this.Rating.CompareTo(temp.Rating);
        }
    }
}