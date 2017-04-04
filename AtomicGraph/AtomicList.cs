using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AtomicGraph
{
    public class AtomicList<T> : AtomicBase, IList<T>
    {
        private IList<T> _concrete = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _concrete.GetEnumerator();
        }

        public void Add(T item)
        {
            Snapshot();
            _concrete.Add(item);
        }

        public void Clear()
        {
            Snapshot();
            _concrete.Clear();
        }

        public bool Contains(T item)
        {
            return _concrete.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Snapshot();
            _concrete.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            Snapshot();
            return _concrete.Remove(item);
        }

        public int Count => _concrete.Count;

        public bool IsReadOnly => _concrete.IsReadOnly;

        public int IndexOf(T item)
        {
            return _concrete.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Snapshot();
            _concrete.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Snapshot();
            _concrete.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _concrete[index]; }
            set { _concrete[index] = value; }
        }

        private void Snapshot()
        {
            if (TransactionLog.Value != null)
            {
                if (!TransactionLog.Value.ContainsKey(this))
                {
                    var snapshot = _concrete.ToList();
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