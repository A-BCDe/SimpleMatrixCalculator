using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Matrix_Calculator.Number;

namespace Simple_Matrix_Calculator.ComplexValueMatrix
{
    class ComplexColumnVector : ComplexMatrix
    {
        public ComplexColumnVector(int col)
            : base(1, col)
        {

        }
        public ComplexColumnVector(Complex[,] vector)
            : base(vector)
        {
            if (vector.GetLength(0) != 1)
            {
                throw new Exception("Wrong vector input");
            }
        }
        public ComplexColumnVector(double[] vector)
            : base(1, vector.Length)
        {
            for (int i = 0; i < Col; i++)
            {
                mat[i, 0] = vector[i];
            }
        }
        public ComplexColumnVector(int col, ComplexSpecialMatrix specialMatrix)
            : base(1, col, specialMatrix)
        {

        }

        public Complex this[int col]
        {
            get
            {
                return mat[0, col];
            }
            set
            {
                if (Lock)
                {
                    throw new Exception("Cannot change locked ComplexColumnVector!");
                }
                mat[0, col] = value;
            }
        }
        public new ComplexRowVector Transpose
        {
            get
            {
                return new ComplexRowVector(this);
            }
        }
    }
}
