using System;
using System.Collections.Generic;

namespace Thread.Locking
{
    enum ExitCode
    {
        Success = 0,
        Error = 1
    }

    public class ObjectStack
    {
        private readonly Stack<Object> _objects = new Stack<object>();
        private readonly Object _lockObject = new Object();
        private const int NumOfPopIterations = 1000;

        public ObjectStack(IEnumerable<object> objects)
        {
            foreach (var anObject in objects) {
                Push(anObject);
            }
        }

        public void Push(object anObject)
        {
            _objects.Push(anObject);
        }

        public void Pop()
        {
            _objects.Pop();
        }

        public void ThreadSafeMultiPop()
        {
            for (var i = 0; i < NumOfPopIterations; i++) {
                lock (_lockObject) {
                    try {
                        Pop();
                    }
                    //Because of lock, the stack will be emptied safely and no exception is ever caught
                    catch (InvalidOperationException) {
                        Environment.Exit((int)ExitCode.Error);
                    }
                    if (_objects.Count == 0) {
                        Environment.Exit((int)ExitCode.Success);
                    }
                }
            }
        }

        public void ThreadUnsafeMultiPop()
        {
            for (var i = 0; i < NumOfPopIterations; i++) {
                    try {
                        Pop();
                    }
                    //Because there is no lock, an exception is caught when popping an already empty stack
                    catch (InvalidOperationException) {
                        Environment.Exit((int)ExitCode.Error);
                    }
                    if (_objects.Count == 0) {
                        Environment.Exit((int)ExitCode.Success);
                    }
                }
        }
    }
}
