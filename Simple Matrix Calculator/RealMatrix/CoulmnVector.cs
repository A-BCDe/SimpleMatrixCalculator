using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    public class ColumnVector : Matrix
    {
        public ColumnVector(int col)
            : base(1, col)
        {

        }
        public ColumnVector(double[,] vector)
            : base(vector)
        {
            if (vector.GetLength(0) != 1)
            {
                throw new Exception("Wrong vector input");
            }
        }
        public ColumnVector(double[] vector)
            : base(1, vector.Length)
        {
            for (int i = 0; i < Col; i++)
            {
                mat[i, 0] = vector[i];
            }
        }
        public ColumnVector(int col, SpecialMatrix specialMatrix)
            : base(1, col, specialMatrix)
        {

        }

        public double this[int col]
        {
            get
            {
                return mat[0, col];
            }
            set
            {
                mat[0, col] = value;
            }
        }
        public new RowVector Transpose
        {
            get
            {
                return new RowVector(this);
            }
        }
    }
}
