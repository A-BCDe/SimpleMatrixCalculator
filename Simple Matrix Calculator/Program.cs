﻿using System;
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
            /*
            Stopwatch stopwatch = new Stopwatch();
            Matrix I = new Matrix(new double[,]{
                { 1, 0, 1 },
                { 0, 1, 1 },
                { 1, 1, 2 }
            });
            //foo(I);
            stopwatch.Reset();
            stopwatch.Start();

            List<ColumnVector> samples;
            ColumnVector correspondingValues;
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
                    string[] split = str.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    n = int.Parse(split[0]);
                    m = int.Parse(split[1]);
                    samples = new List<ColumnVector>();
                    correspondingValues = new ColumnVector(n);
                    for (int i = 0; i < n; i++)
                    {
                        str = sr.ReadLine();
                        split = str.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                        ColumnVector sample = new ColumnVector(m);
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
                samples = new List<ColumnVector>(n);
                correspondingValues = new ColumnVector(n, SpecialMatrix.Random);

                for (int i = 0; i < n; i++)
                {
                    samples.Add(new ColumnVector(m, SpecialMatrix.Random));
                }
            }
            samplesMatrix = new CoefficientMatrix(samples, true);

            Console.WriteLine();
            Console.WriteLine(samplesMatrix);
            Console.WriteLine();

            ColumnVector result = ((samplesMatrix.Transpose * samplesMatrix).Inverse * samplesMatrix.Transpose * correspondingValues).ToColumnVector();
            Console.WriteLine(result);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
            Matrix A = new Matrix(new double[,]
            {
                { 1, -1, -1, 0, 0, 0 },
                { 0, 1, 0, -1, -1, 0 },
                { 0, 0, 1, 1, 0, -1 },
                { 0, 1, -2, 1, 0, 0 },
                { 0, 0, 0, 1, -2, 1 },
                { 0, 1, 0, 0, 2, 0 }
            });
            ColumnVector b = new ColumnVector(new double[]
            {
                0,
                0,
                0,
                0,
                0,
                7
            });
            Console.WriteLine((A.Inverse * b).ToColumnVector());
            Console.WriteLine(A.SolveWith(b));

            Matrix B = new Matrix(10, 10, SpecialMatrix.Random);
            Matrix kernel = 0.25 * new Matrix(new double[3, 3] {
                { 0, 1, 0 },
                { 1, 0, 1 },
                { 0, 1, 0 }
            });
            Matrix C = B.ConvolutionMatrix(kernel);
            Console.WriteLine();
            Console.WriteLine(C == null ? "null" : C.ToString());

            Matrix D = new Matrix(4, 4, SpecialMatrix.Random);
            Matrix E = new Matrix(5, 4, SpecialMatrix.Random);
            Matrix F = D.RowMerge(E);
            Matrix G = new Matrix(4, 5, SpecialMatrix.Random);
            Matrix H = D.ColumnMerge(G);
            Console.WriteLine(D);
            Console.WriteLine(E);
            Console.WriteLine(F == null ? "null" : F.ToString());
            Console.WriteLine(G);
            Console.WriteLine(H == null ? "null" : H.ToString());
            foo(I.RREF());
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine(I.Rank);
            stopwatch.Reset();
            stopwatch.Start();
            foo(I.REF());
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine(I.Rank);
            ColumnVector J = new ColumnVector(new double[]
            {
                -2, 2, 2
            });
            Console.WriteLine(J.HouseholderMatrix);
            */
            /*
            List<ColumnVector> input;
            ColumnVector output;

            int n, m;
            string FileName = "test.txt";
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
                string[] split = str.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                n = int.Parse(split[0]);
                m = int.Parse(split[1]);
                input = new List<ColumnVector>();
                output = new ColumnVector(n);
                for (int i = 0; i < n; i++)
                {
                    str = sr.ReadLine();
                    split = str.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    ColumnVector sample = new ColumnVector(m);
                    for (int j = 0; j < m; j++)
                    {
                        sample[j] = int.Parse(split[j]);
                    }
                    input.Add(sample);
                    output[i] = int.Parse(split[m]);
                }
            }

            // double alpha = 0.1;
            // ColumnVector theta_batch = new ColumnVector(n, SpecialMatrix.One);
            int hiddenLayer = 3;
            List<Matrix> WeightMatrix = new List<Matrix>(hiddenLayer + 1);
            WeightMatrix[0] = new Matrix(m, n + 1);
            for (int i = 1; i < hiddenLayer; i++)
            {
                WeightMatrix[i] = new Matrix(n + 1, n + 1);
            }
            WeightMatrix[hiddenLayer] = new Matrix(n + 1, 1);
            */
            /*
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            RowVector v = new RowVector(new double[]
            {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20
            });
            foo(v.HouseholderMatrix);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
            stopwatch.Start();
            ColumnVector u = new ColumnVector(new double[]
             {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20
             });
            foo(v.HouseholderMatrix);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            */
            Matrix A = new Matrix(new double[,]
            {
                {1,2,3 },
            });
            Matrix B = new Matrix(new double[,]
            {
                {4,3,2 },
            });
            Console.WriteLine(B - A);
        }
        private static void foo(Matrix A)
        {
            // Console.WriteLine(A);
            return;
        }
    }
}
