using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    public class ColumnVector : Matrix // basically, Col = 1
    {
        private double length;
        private bool lengthMade;
        private double lengthSquare;
        private bool lengthSquareMade;

        private ColumnVector unit;
        private bool unitMade;

        private Matrix H;
        private bool HMade;

        public ColumnVector(int row)
            : base(row, 1)
        {
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
        }
        public ColumnVector(double[,] vector)
            : base(vector)
        {
            if(vector.GetLength(1) != 1)
            {
                throw new Exception("Wrong vector input");
            }
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
        }
        public ColumnVector(double[] vector)
            : base(vector.Length, 1)
        {
            for(int i = 0; i < Row; i++)
            {
                mat[i, 0] = vector[i];
            }
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
        }
        public ColumnVector(int row, SpecialMatrix specialMatrix)
            : base(row, 1, specialMatrix)
        {
            HMade = false;
            lengthMade = false;
            lengthSquareMade = false;
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

        public new RowVector Transpose
        {
            get
            {
                RowVector vector = new RowVector(this.Row);
                for(int i = 0; i < Row; i++)
                {
                    vector[i] = mat[i, 0];
                }
                vector.Lock = true;
                return vector;
            }
        }

        public new ColumnVector Clone()
        {
            return new ColumnVector(mat.Clone() as double[,]);
        }

        public double LengthSquare
        {
            get
            {
                if(lengthSquareMade)
                {
                    return lengthSquare;
                }
                lengthSquare = 0;
                for(int i = 0; i < Row; i++)
                {
                    lengthSquare += mat[i, 0] * mat[i, 0];
                }
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
                return length = Math.Sqrt(LengthSquare);
            }
        }
        public ColumnVector Unit
        {
            get
            {
                if (unitMade)
                {
                    return unit;
                }
                unitMade = true;
                return unit = (ToColumnVector() / Length).ToColumnVector();
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
                return H = new Matrix(this.Row, this.Row, SpecialMatrix.Identity) - 2 * this.Transpose.GrammMatrix / this.LengthSquare;
            }
        }
    }
}
