using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://www.toptal.com/c-sharp/interview-questions

namespace Advanced_CSharp
{

    //
    //  Extension Method
    //

    //https://www.csharpstar.com/extension-methods-csharp/
    //extension methods must be defined in a top level static class
    static class IntMethods
    {
        //extension methods must be static
        //the this keyword tells C# that this is an extension method
        public static bool IsPrime(this int number)
        {
            //check for evenness
            if (number % 2 == 0)
            {
                if (number == 2)
                    return true;
                return false;
            }
            //you don’t need to check past the square root
            int max = (int)Math.Sqrt(number);
            for (int i = 3; i <= max; i += 2)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }


    class ExtentionToInts
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; ++i)
            {
                if (i.IsPrime())
                {
                    Console.WriteLine(i);
                }
            }
            Console.ReadKey();
        }
    }


    class RefVsValueTypesInitialValues
    {
        static String location;
        static DateTime time;

        static void Main()
        {
            Console.WriteLine(location == null ? "location is null" : location);
            Console.WriteLine(time == null ? "time is null" : time.ToString());
        }
    }


    class FirstAwaitWillReturnInAsyncFunc
    {
        private static string result;

        static void Main()
        {
            SaySomething();
            Console.WriteLine(result);
        }

        static async Task<string> SaySomething()
        {
            await Task.Delay(5);
            result = "Hello world!";
            return “Something”;
        }
    }

    // OUTPUT is just a blank line , as result is null 





    //
    // static constructor called before any instance creation
    //
    public class TestStatic
    {
        public static int TestValue;

        public TestStatic()
        {
            if (TestValue == 0)
            {
                TestValue = 5;
            }
        }
        static TestStatic()
        {
            if (TestValue == 0)
            {
                TestValue = 10;
            }

        }

        public void Print()
        {
            if (TestValue == 5)
            {
                TestValue = 6;
            }
            Console.WriteLine("TestValue : " + TestValue);

        }
    }

    public class  MainTestStatic
    {
        void Test()
        {
            // What will this print ?  10 
            TestStatic t = new TestStatic();
            t.Print();
        }
    }

    class Advanced_CSharp
    {


        //
        // Lambda functions
        //


        // Given a list of ints, sum up all even numbers
        static long SumUpEvens( int[] A)
        {
            return A.Where(x => (x % 2) == 0).Sum( y => (long)y );
        }

        static long SumUpEvensAlt( int[] A)
        {
            return (from i in A where (i % 2) == 0 select (long)i).Sum();
        }

        //
        //  Function as params
        //
        public sealed class Circle
        {
            private double radius;

            public double Calculate(Func<double, double> op)
            {
                return op(radius);
            }
        }

        // To calc circumfrence ( no access to radius outside the class ) 
        // double circum = aCircle.Calculate( r =>  2 * Math.PI * r );


        //
        // closure 
        //
        delegate void Printer();

        static void Closure()
        {
            List<Printer> printers = new List<Printer>();
            int i = 0;
            for (; i < 10; i++)
            {
                printers.Add(delegate { Console.WriteLine(i); });
            }

            foreach (var printer in printers)
            {
                printer();
            }
        }

        // Will print '10'  10 times as i is packaged by ref. and all of them point to same outside i


        //
        // nullable types enable to check valid or invalid variables, before they are used.
        //
        int? myInt1 = 15;           // Implicitly convert int to int?
        int regInt = (int)myInt1;  // Explicitly convert int? to int




        //
        /// 
        /// Singleton 
        /// 
        /// https://www.csharpstar.com/singleton-design-pattern-csharp/
        /// 

        public sealed class Singleton
        {
            private static Singleton instance = null;
            private static readonly object Instancelock = new object();

            private Singleton() {

                // Improvement: Prevent object cleation by reflection
                if (instance != null)
                {
                    throw new Exception("Cannot create singleton instance through reflection");
                }

            }


            // Improvements
            // Prevent cloning by adding
            protected object MemberwiseClone()
            {
                throw new Exception("Cloning a singleton object is not allowed");
            }

            public static Singleton GetInstance
            {
                get
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton();
                        }
                        return instance;
                    }
                }
            }
        }
    
        




        static void Main(string[] args)
        {
        }
    }





}
