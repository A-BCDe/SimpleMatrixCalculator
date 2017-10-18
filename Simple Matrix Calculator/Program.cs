using System;
using System.Collections.Generic;
using System.IO;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            ///
            /// Example : Gradient Descent
            ///
            List<RowVector> samples;
            RowVector correspondingValues;
            CoefficientMatrix samplesMatrix;
            int n, m;
            string FileName = "test.txt";
            if (File.Exists(FileName))
            {
                ///
                /// File Name : test.txt
                /// n m
                /// x11 x12 ... x1m y1
                /// x21 x22 ... x2m y2
                /// ..................
                /// xn1 xn2 ... xnm yn
                ///
                using (StreamReader sr = new StreamReader(FileName))
                {
                    string str = sr.ReadLine();
                    string[] split = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    n = int.Parse(split[0]);
                    m = int.Parse(split[1]);
                    samples = new List<RowVector>();
                    correspondingValues = new RowVector(n);
                    for (int i = 0; i < n; i++)
                    {
                        str = sr.ReadLine();
                        split = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        RowVector sample = new RowVector(m);
                        for(int j = 0; j < m; j++)
                        {
                            sample[j] = int.Parse(split[j]);
                        }
                        samples.Add(sample);
                        correspondingValues[i] = int.Parse(split[m]);
                    }
                }
            }
            else
            {
                n = 1000;
                m = 999;
                samples = new List<RowVector>(n);
                correspondingValues = new RowVector(n, SpecialMatrix.Random);

                for (int i = 0; i < n; i++)
                {
                    samples.Add(new RowVector(m, SpecialMatrix.Random));
                }
            }
            samplesMatrix = new CoefficientMatrix(samples, true);
            
            RowVector result = ((samplesMatrix.Transpose * samplesMatrix).Inverse * samplesMatrix.Transpose * correspondingValues).ToRowVector();
            Console.WriteLine(result);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            
            Matrix A = new Matrix(new double[,]
            {
                { 1, -1, -1, 0, 0, 0 },
                { 0, 1, 0, -1, -1, 0 },
                { 0, 0, 1, 1, 0, -1 },
                { 0, 1, -2, 1, 0, 0 },
                { 0, 0, 0, 1, -2, 1 },
                { 0, 1, 0, 0, 2, 0 }
            });
            RowVector b = new RowVector(new double[]
            {
                0, 0, 0, 0, 0, 7
            });
            Console.WriteLine((A.Inverse * b).ToRowVector());
            Console.WriteLine(A.SolveWith(b));
        }
        private static void foo(Matrix A)
        {
            Console.WriteLine(A);
            return;
        }
    }
}
