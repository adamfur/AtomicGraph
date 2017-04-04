namespace AtomGraph
{
    public class Atomic<T> : AtomicBase
    {
        private T _concrete;

        public Atomic(T value = default(T))
        {
            _concrete = value;
        }

        public static implicit operator T(Atomic<T> self)
        {
            return self._concrete;
        }

        public static implicit operator Atomic<T>(T value)
        {
            return new Atomic<T>(value);
        }

        public T Value
        {
            get { return _concrete; }
            set
            {
                if (TransactionLog.Value != null)
                {
                    if (!TransactionLog.Value.ContainsKey(this))
                    {
                        var copy = _concrete;
                        TransactionLog.Value[this] = () => _concrete = copy;
                    }
                }
                _concrete = value;
            }
        }
    }
}