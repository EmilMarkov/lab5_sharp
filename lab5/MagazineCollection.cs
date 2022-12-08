using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//TODO:
//1) Rename dict1 and dict2

namespace lab5
{
    public delegate void MagazinesChangedHandler<TKey>(object source, MagazinesChangedEventArgs<TKey> args);

    public class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> collection;
        private KeySelector<TKey> keyGenerator;
        public string collectionName { get; set; }
        public event MagazinesChangedHandler<TKey> MagazinesChanged;

        public MagazineCollection(KeySelector<TKey> method)
        {
            keyGenerator = method;
            collection = new Dictionary<TKey, Magazine>();
        }

        public void AddDefaults(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Magazine magazine = Inputs.inputMagazine();
                collection.Add(keyGenerator(magazine), magazine);
            }
        }

        public void AddMagazines(params Magazine[] value)
        {
            foreach (Magazine magazine in value)
            {
                collection.Add(keyGenerator(magazine), magazine);
                MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(collectionName, Update.Add, "Add magazine", keyGenerator(magazine)));
            }
        }

        public bool Replace(Magazine mold, Magazine mnew)
        {
            if (collection.ContainsValue(mold))
            {
                foreach(KeyValuePair<TKey, Magazine> magazine_pair in collection)
                {
                    if (magazine_pair.Value == mold)
                    {
                        collection[magazine_pair.Key] = mnew;
                        MagazinePropertyChanged(Update.Replace, "None", magazine_pair.Key);
                        mold.PropertyChanged -= PropertyChangeded;
                        mnew.PropertyChanged += PropertyChangeded;
                        break;
                    }
                }
                return true;
            }
            else return false;
        }

        private void MagazinePropertyChanged(Update update, string name, TKey key)
        {
            MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(collectionName, update, name, key));
        }

        private void PropertyChangeded(object sourse, EventArgs args)
        {
            MagazinePropertyChanged(Update.Property, (args as MagazinesChangedEventArgs<string>).propertyName,  keyGenerator((Magazine)sourse));
        }

        public Double MaxRatingElement
        {
            get
            {
                if (collection == null) return 0.0;
                else
                {
                    return collection.Values.Max(obj => obj.Rating);
                }
            }
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> grouping
        {
            get
            {
                return collection.GroupBy(obj => obj.Value.Frequency);
            }
        }

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return collection.Where(obj => obj.Value.Frequency == value);
        }

        override public string ToString()
        {
            foreach (KeyValuePair<TKey, Magazine> item in collection)
            {
                Console.WriteLine("Key:");
                Console.WriteLine(item.Key.ToString());
                Console.WriteLine("Magazine:");
                Console.WriteLine(item.Value.ToString());
            }

            return "";
        }

        public void ToShortString()
        {
            foreach (KeyValuePair<TKey, Magazine> item in collection)
            {
                Console.WriteLine("Key:");
                item.Key.ToString();
                Console.WriteLine("Magazine:");
                item.Value.ToShortString();
            }
        }
    }
}
