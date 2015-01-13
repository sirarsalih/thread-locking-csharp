using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Thread.Locking
{
    public class Program
    {
        private const int NumOfObjects = 100;
        private const int NumOfThreads = 10000;

        public static void Main(string[] args)
        {
            var objects = new List<Object>();
            for (var i = 0; i < NumOfObjects; i++) {
                objects.Add(new object());
            }
            var objectStack = new ObjectStack(objects);
            Parallel.For(0, NumOfThreads, x => objectStack.ThreadSafeMultiPop());
        }
    }
}
