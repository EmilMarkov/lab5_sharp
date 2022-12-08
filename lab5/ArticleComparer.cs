using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5
{
    public class ArticleComparer:IComparer<Article>
    {
        public int Compare(Article leftArticle, Article rightArticle)
        {
            if (leftArticle.Rating < rightArticle.Rating)
            {
                return -1;
            }
            else if (leftArticle.Rating > rightArticle.Rating)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public class ArticleComparerBySurname:IComparer<Article>
    {
        public int Compare(Article leftArticle, Article rightArticle)
        {
            return leftArticle.Author.Surname.CompareTo(rightArticle.Author.Surname);
        }
    }

    public class ArticleComparerByTitle : IComparer<Article>
    {
        public int Compare(Article leftArticle, Article rightArticle)
        {
            return leftArticle.Title.CompareTo(rightArticle.Title);
        }
    }
}
