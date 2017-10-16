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
                    /*
                    samples[i][0] = 1;
                    samples[i][1] = i & 1431655765;
                    samples[i][2] = i / 2;
                    */
                }
            }
            samplesMatrix = new CoefficientMatrix(samples, true);

            /*
            foo(samplesMatrix);
            foo(null);
            foo(samplesMatrix.Transpose);
            foo(null);
            foo((samplesMatrix.Transpose * samplesMatrix));
            foo(null);
            foo((samplesMatrix.Transpose * samplesMatrix).Inverse);
            foo(null);
            foo((samplesMatrix.Transpose * samplesMatrix).Inverse * samplesMatrix.Transpose);
            */
            RowVector result = ((samplesMatrix.Transpose * samplesMatrix).Inverse * samplesMatrix.Transpose * correspondingValues).ToRowVector();
            Console.WriteLine(result);
        }
        private static void foo(Matrix A)
        {
            Console.WriteLine(A);
            return;
        }
    }
}
