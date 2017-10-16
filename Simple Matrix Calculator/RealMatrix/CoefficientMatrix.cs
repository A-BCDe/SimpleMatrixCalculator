using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    class CoefficientMatrix : Matrix
    {
        public CoefficientMatrix(List<RowVector> rowVectors, bool transpose = false)
            :base(transpose? rowVectors.Count : rowVectors[0].Row,
                 transpose ? rowVectors[0].Row : rowVectors.Count)
        {
            Lock = false;
            if(transpose)
            {
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        mat[i, j] = rowVectors[i][j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        mat[i, j] = rowVectors[j][i];
                    }
                }
            }
            Lock = true;
        }
        public CoefficientMatrix(List<ColumnVector> colVectors, bool transpose = false)
            : base(transpose ? colVectors[0].Col : colVectors.Count,
                  transpose ? colVectors.Count : colVectors[0].Col)
        {
            if (transpose)
            {
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        mat[i, j] = colVectors[j][i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        mat[i, j] = colVectors[i][j];
                    }
                }
            }
        }
    }
}
