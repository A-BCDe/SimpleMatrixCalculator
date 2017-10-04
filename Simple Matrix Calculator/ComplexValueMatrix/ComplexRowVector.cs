using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Matrix_Calculator.Number;

namespace Simple_Matrix_Calculator.ComplexValueMatrix
{
    class ComplexRowVector : ComplexMatrix
    {
        public ComplexRowVector(int row)
            : base(row, 1)
        {

        }
        public ComplexRowVector(Complex[,] vector)
            : base(vector)
        {
            if (vector.GetLength(1) != 1)
            {
                throw new Exception("Wrong vector input");
            }
        }
        public ComplexRowVector(double[] vector)
            : base(vector.Length, 1)
        {
            for (int i = 0; i < Row; i++)
            {
                mat[i, 0] = vector[i];
            }
        }
        public ComplexRowVector(int row, ComplexSpecialMatrix specialMatrix)
            : base(row, 1, specialMatrix)
        {

        }

        public Complex this[int row]
        {
            get
            {
                return mat[row, 0];
            }
            set
            {
                if (Lock)
                {
                    throw new Exception("Cannot change locked ComplexRowVector!");
                }
                mat[row, 0] = value;
            }
        }

        public new ComplexColumnVector Transpose
        {
            get
            {
                return new ComplexColumnVector(this);
            }
        }
    }
}
