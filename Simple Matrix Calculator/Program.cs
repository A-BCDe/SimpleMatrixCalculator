using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Matrix_Calculator.RealMatrix;
using System.Diagnostics;

namespace Simple_Matrix_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(new double[2, 2] { { 1, 2 }, { 3, 4 } });
            
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Reset();
            stopwatch.Start();
            foo(A);
            Console.WriteLine(A.Det);
            Console.WriteLine(A.Tr);
            foo(A.Inverse);
            foo(A);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
        private static void foo(Matrix A)
        {
            Console.WriteLine(A);
            return;
        }
    }
}
