using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab5
{
    [Serializable]
   public class Person
   {
        private string name;
        private string surname;
        private DateTime birthday;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public Person()
        {
            name = "Имя";
            surname = "Фамилия";
            birthday = new System.DateTime(2008, 5, 1);
        }

        public Person(string nameValue, string surnameValue, DateTime birthdayValue)
        {
            name = nameValue;
            surname = surnameValue;
            birthday = birthdayValue;
        }

        public virtual string ToString()
        {
            return this.name + " " + this.surname + " " + this.birthday.ToShortDateString();
        }

        public virtual string ToShortString()
        {
            return this.name + " " + this.surname;
        }

        public virtual bool Equals(Person obj)
        {
            if (obj is null) { return false; }
            else
            {
                return this.name == obj.name && this.surname == obj.surname && this.birthday == obj.birthday;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.name, this.surname, this.birthday);
        }

        public static bool operator ==(Person left, Person right)
        {
            if (right is null) return false;

            if (left.GetHashCode() != right.GetHashCode())
            {
                return false;
            }
            else
            {
                if (left.Equals(right)) return true;
                else return false;
            }
        }

        public static bool operator !=(Person left, Person right)
        {
            if (right is null) return false;

            if (left.GetHashCode() == right.GetHashCode())
            {
                if (left.Equals(right)) return false;
                else return true;
            }
            else
            {
                return true;
            }
        }

        public virtual object DeepCopy()
        {
            Person person = new Person();
            person.name = this.name;
            person.surname = this.surname;
            person.birthday = this.birthday;
            return (object)person;
        }
    }
}