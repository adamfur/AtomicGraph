using System;
using System.Collections.Generic;
using System.Threading;

namespace AtomicGraph
{
    public abstract class AtomicBase
    {
        protected static AsyncLocal<Dictionary<object, Action>> TransactionLog = new AsyncLocal<Dictionary<object, Action>>();
    }
}