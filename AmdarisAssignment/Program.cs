using System;
using System.Diagnostics;

namespace AmdarisAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PrintFactorial(-2);
            }
            catch (MyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Very unexpected exception: {0}", ex.Message);
            }
            finally
            {
                Console.WriteLine("The program finished");
            }
        }

        private static void PrintFactorial(int n)
        {
            try
            {
                int ans = Factorial(n);
                Console.WriteLine("Factorial of {0} is {1}", n, ans);
            }
            catch (MyException ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public static int Factorial(int n)
        {
            if (n < 0)
                throw new MyException("n should be positive");
            int ans = 1;
            for (int i = 2; i <= n; i++)
                ans *= i;
            return ans;
        }
    }

    public class MyException : Exception
    {
        public MyException(string message) : base(message) {}
        public MyException(string message, Exception exception) : base(message, exception) {}
    }
}
