using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    public class RowVector : Matrix // basically, Col = 1
    {
        public RowVector(int row)
            : base(row, 1)
        {

        }
        public RowVector(double[,] vector)
            : base(vector)
        {
            if(vector.GetLength(1) != 1)
            {
                throw new Exception("Wrong vector input");
            }
        }
        public RowVector(double[] vector)
            : base(vector.Length, 1)
        {
            for(int i = 0; i < Row; i++)
            {
                mat[i, 0] = vector[i];
            }
        }
        public RowVector(int row, SpecialMatrix specialMatrix)
            : base(row, 1, specialMatrix)
        {

        }

        public double this[int row]
        {
            get
            {
                return mat[row, 0];
            }
            set
            {
                mat[row, 0] = value;
            }
        }

        public new ColumnVector Transpose
        {
            get
            {
                return new ColumnVector(this);
            }
        }
    }
}
