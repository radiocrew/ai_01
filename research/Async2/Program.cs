using System;
using System.Threading.Tasks;
using System.Threading;
using Async2.Material;

namespace Async2
{
    class Program
    {

        // 단독으로 사용할 때는 void를 사용한다.
        static async Task<int> AsyncTest()
        {
            var task = new Task<int>(() =>
            {
                int sum = 0;
                for (int i = 0; i < 10; i++)
                {
                    sum += i;
                    // 0.1초 단위로 1씩 증감
                    //Console.WriteLine(i);
                    Thread.Sleep(500);
                }
                return sum;
            });
            task.Start();


            //외부에서 await에서 기다린다.
            await task; // 이걸 만나면 통과 시키네. 


            // Wait가 호출되면 통과된다.
            Console.WriteLine(task.Result); //여기서 기다림. 
            return 10;
        }

        static void Main(string[] args)
        {

            var task = AsyncTest();


            Console.WriteLine("pass await 1");
            // Wait
            //task.Wait();
            Console.WriteLine("pass await 2");
            //결국 Return까지 기다린다.
            int result = task.Result;
            //결과 같은 10이다.
            Console.WriteLine(result);
            Console.WriteLine("Press Any Key...");
            Console.ReadKey();


        
            Thread.Sleep(TimeSpan.FromSeconds(100));
            
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static Toast ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static Bacon FryBacon(int slices)
        {
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static Egg FryEggs(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }
}
