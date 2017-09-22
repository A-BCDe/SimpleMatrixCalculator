using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    class CoefficientMatrix : Matrix
    {
        public CoefficientMatrix(List<RowVector> rowVectors)
            :base(rowVectors.Count, rowVectors[0].Col)
        {
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    mat[i, j] = rowVectors[i][j];
                }
            }
        }
        public CoefficientMatrix(List<ColumnVector> colVectors) // basically uses transpose
            : base(colVectors[0].Col, colVectors.Count)
        {
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    mat[i, j] = colVectors[i][j];
                }
            }
        }

    }
}
