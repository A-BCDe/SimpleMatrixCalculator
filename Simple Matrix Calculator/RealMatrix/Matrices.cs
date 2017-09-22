using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    class Matrices // list of matrices with same size
    {
        private List<Matrix> matrices;
        public int Row;
        public int Col;

        private Matrix expectedValue;

        public Matrices(int row, int col)
        {
            matrices = new List<Matrix>();
            Row = row;
            Col = col;
        }
        public Matrices(int row, int col, int size)
        {
            matrices = new List<Matrix>(size);
            Row = row;
            Col = col;
        }
        public Matrices(Matrix[] mats) // assuming all matrices are same size
        {
            Row = mats[0].Row;
            Col = mats[0].Col;
            Parallel.For(0, mats.Length, i =>
              {
                  matrices[i] = mats[i];
              });
        }
        public Matrices(List<Matrix> mats) // assuming all matrices are same size
        {
            Row = mats[0].Row;
            Col = mats[0].Col;
            matrices = mats;
        }

        public void AddMatrix(Matrix mat)
        {
            if (mat.Row != Row || mat.Col != Col)
            {
                throw new Exception("Wrong matrix size!");
            }
            matrices.Add(mat);
        }

        private Matrix Add()
        {
            Matrix mat = new Matrix(Row, Col, SpecialMatrix.Zero);
            for (int i = 0; i < matrices.Count; i++)
            {
                mat += matrices[i];
            }
            return mat;
        }

        public Matrix this[int index]
        {
            get
            {
                return matrices[index];
            }
            set
            {
                matrices[index] = value;
            }
        }

        public Matrices Transpose()
        {
            List<Matrix> mats = new List<Matrix>();
            for(int i = 0; i < matrices.Count; i++)
            {
                mats.Add(matrices[i].Transpose);
            }
            return new Matrices(mats);
        }

        #region Statistics

        public void ExpectedValue()
        {
            expectedValue = new Matrix(matrices[0].Row, matrices[0].Col, SpecialMatrix.Zero);
            for(int i = 0; i < matrices.Count; i++)
            {
                expectedValue += matrices[i];
            }
            expectedValue /= matrices.Count;
            return;
        }
        
        public Matrix E
        {
            get
            {
                ExpectedValue();
                return expectedValue;
            }
        }

        public void Variance()
        {
            
        }

#endregion
    }
}
