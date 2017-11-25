using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    public class RowVector : Matrix
    {
        private double length;
        private bool lengthMade;
        private double lengthSquare;
        private bool lengthSquareMade;

        private RowVector unit;
        private bool unitMade;

        private Matrix H;
        private bool HMade;

        public RowVector(int col)
            : base(1, col)
        {
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
        }
        public RowVector(double[,] vector)
            : base(vector)
        {
            if (vector.GetLength(0) != 1)
            {
                throw new Exception("Wrong vector input");
            }
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
        }
        public RowVector(double[] vector)
            : base(1, vector.Length)
        {
            for (int i = 0; i < Col; i++)
            {
                mat[0, i] = vector[i];
            }
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
        }
        public RowVector(int col, SpecialMatrix specialMatrix)
            : base(1, col, specialMatrix)
        {
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
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
        public new ColumnVector Transpose
        {
            get
            {
                ColumnVector vector = new ColumnVector(this.Row);
                for (int i = 0; i < Col; i++)
                {
                    vector[i] = mat[i, 0];
                }
                vector.Lock = true;
                return vector;
            }
        }

        public new RowVector Clone()
        {
            return new RowVector(mat.Clone() as double[,]);
        }

        public double LengthSquare
        {
            get
            {
                if (lengthSquareMade)
                {
                    return lengthSquare;
                }
                lengthSquare = 0;
                for (int i = 0; i < Col; i++)
                {
                    lengthSquare += mat[i, 0] * mat[i, 0];
                }
                lengthSquareMade = true;
                return lengthSquare;
            }
        }
        public double Length
        {
            get
            {
                if (lengthMade)
                {
                    return length;
                }
                lengthMade = true;
                return length = Math.Sqrt(LengthSquare);
            }
        }
        public RowVector Unit
        {
            get
            {
                if (unitMade)
                {
                    return unit;
                }
                unitMade = true;
                return unit = (ToRowVector() / Length).ToRowVector();
            }
        }

        public Matrix HouseholderMatrix
        {
            get
            {
                if (HMade)
                {
                    return H;
                }
                HMade = true;
                return H = new Matrix(this.Col, this.Col, SpecialMatrix.Identity) - 2 * this.Transpose * this.Clone() / this.LengthSquare;
            }
        }
    }
}
