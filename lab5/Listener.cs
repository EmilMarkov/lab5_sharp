using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    class Listener
    {
        private System.Collections.Generic.List<ListEntry> changes_list = new List<ListEntry>();

        public void Add_changes(object source, EventArgs args)
        {
            var even = args as MagazinesChangedEventArgs<string>;
            changes_list.Add(new ListEntry(even.collectionName, even.updateType, even.propertyName, even.elementKey));
        }

        public override string ToString()
        {
            string result="";
            foreach(var elem in changes_list)
            {
                result += "\nChange in collection: " + elem.Collection_name + " |Change type: " + elem.Why + " |Change in property: " + elem.Property_Name + " |Element key: " + elem.Text_Element_Key;
            }
            return result;
        }
    }
}