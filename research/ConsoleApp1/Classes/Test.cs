using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Classes
{

    public class B
    { 
    }

    public class D : B
    { 
    }



    public class Test
    {

        static (T sought, int index) FindFirstOccurrence<T>(IEnumerable<T> enumrable, T value)
        {
            int index = 0;
            foreach (T element in enumrable)
            {
                if (element.Equals(value))
                {
                    return (value, index);
                }
                index++;
            }

            return (default(T), -1);
        }

        public IEnumerator<int> GetEnumerator()
        {
            B b = new B();
            D d = new D();

            var t = d as B;
            if (null == t)
            {
                Console.WriteLine("");
            }

            var k = b as D;
            if (null == k)
            {
                Console.WriteLine("");
            }

            int i = 0;
            
            while (i < data.Length)
            {
                yield return data[i];
                ++i;
            }
            
        }

        private int[] data = { 1, 2, 3, 4 };
    }
}
