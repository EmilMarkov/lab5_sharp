using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    public class MagazinesChangedEventArgs<TKey> : EventArgs
    {
        public string collectionName { get; set; }
        public Update updateType { get; }
        public string propertyName { get; set; }
        public TKey elementKey { get; set; }
        public MagazinesChangedEventArgs(string collectionNameValue, Update updateTypeValue, string propertyNameValue, TKey elementValue)
        {
            collectionName = collectionNameValue;
            updateType = updateTypeValue;
            propertyName = propertyNameValue;
            elementKey = elementValue;
        }

        public override string ToString()
        {
            return "Change in collection: " + collectionName + "\nChange type: " + updateType + "\nChange in property: " + propertyName + "\nElement key: " + elementKey;
        }
    }
}