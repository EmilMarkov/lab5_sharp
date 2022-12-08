using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    class ListEntry
    {
        public string Collection_name { get; set; }
        public Update Why { get; set; }
        public string Property_Name { get; set; }
        public string Text_Element_Key { get; set; }

        public ListEntry(string collectionName, Update why, string PropertyName, string textElementKey)
        {
            Collection_name = collectionName;
            Why = why;
            Property_Name = PropertyName;
            Text_Element_Key = textElementKey;
        }

        public override string ToString()
        {
            return "Change in collection: " + Collection_name + "\nChange type: " + Why + "\nChange in property: " + Property_Name + "\nElement key: " + Text_Element_Key;
        }
    }
}