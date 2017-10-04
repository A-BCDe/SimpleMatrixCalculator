using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Matrix_Calculator.RealMatrix;
using Simple_Matrix_Calculator.ComplexValueMatrix;
using System.Diagnostics;

namespace Simple_Matrix_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexMatrix A = new ComplexMatrix(3, 3, ComplexSpecialMatrix.Random);
            // Matrix B = new Matrix(700, 700, SpecialMatrix.Random);
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Reset();
            stopwatch.Start();
            Console.WriteLine(A.LMatrix);
            Console.WriteLine(A.UMatrix);


            // Console.WriteLine(A.Inverse);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();
            /*
            Console.WriteLine(A.Tr);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();
            Console.WriteLine(A.Det);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            //foo(A.Inverse);

            stopwatch.Reset();
            stopwatch.Start();
            Console.WriteLine(A.Inverse.Det);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            // foo(A);
            */
        }
        private static void foo(Matrix A)
        {
            // Console.WriteLine(A);
            return;
        }
    }
}
