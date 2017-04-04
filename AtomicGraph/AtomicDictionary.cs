using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AtomicGraph
{
    public class AtomicDictionary<TKey, TValue> : AtomicBase, IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _concrete = new Dictionary<TKey, TValue>();

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _concrete.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Snapshot();
            _concrete.Add(item);
        }

        public void Clear()
        {
            Snapshot();
            _concrete.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _concrete.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Snapshot();
            _concrete.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            Snapshot();
            return _concrete.Remove(item);
        }

        public int Count => _concrete.Count;

        public bool IsReadOnly => _concrete.IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            Snapshot();
            _concrete.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _concrete.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            Snapshot();
            return _concrete.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _concrete.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get { return _concrete[key]; }
            set { _concrete[key] = value; }
        }

        public ICollection<TKey> Keys => _concrete.Keys;

        public ICollection<TValue> Values => _concrete.Values;

        private void Snapshot()
        {
            if (TransactionLog.Value != null)
            {
                if (!TransactionLog.Value.ContainsKey(this))
                {
                    var snapshot = _concrete.ToDictionary(x => x.Key, x => x.Value);
                    TransactionLog.Value[this] = () => _concrete = snapshot;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}