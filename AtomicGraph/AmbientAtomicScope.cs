using System;
using System.Collections.Generic;

namespace AtomicGraph
{
    public class AmbientAtomicScope : AtomicBase, IDisposable
    {
        private readonly Dictionary<object, Action> _previous;

        public AmbientAtomicScope()
        {
            _previous = TransactionLog.Value;
            TransactionLog.Value = new Dictionary<object, Action>();
        }

        public void Commit()
        {
            if (TransactionLog.Value != null)
            {
                TransactionLog.Value.Clear();
            }
        }

        public void Dispose()
        {
            if (TransactionLog.Value != null)
            {
                foreach (var revert in TransactionLog.Value.Values)
                {
                    revert();
                }
            }

            TransactionLog.Value = _previous;
        }
    }
}