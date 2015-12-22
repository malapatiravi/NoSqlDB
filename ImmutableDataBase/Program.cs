using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ImmutableDataBase
{

    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        IDictionary<TKey, TValue> _dict;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> backingDict)
        {
            _dict = backingDict;
        }

        public void Add(TKey key, TValue value)
        {
            throw new InvalidOperationException();
        }

        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _dict.Keys; }
        }

        public bool Remove(TKey key)
        {
            throw new InvalidOperationException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _dict.Values; }
        }

        public TValue this[TKey key]
        {
            get { return _dict[key]; }
            set { throw new InvalidOperationException(); }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException();
        }

        public void Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dict.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dict.CopyTo(array, arrayIndex);
        }
        
    }
    public class ImmutableDatabase
    {

        static void Main(string[] args)
        {
        }
    }
}
