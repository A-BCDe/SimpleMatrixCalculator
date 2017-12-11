using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Matrix_Calculator.RealMatrix
{
    public class Matrix
    {
        public readonly int Row;
        public readonly int Col;
        protected double[,] mat;
        private bool _lock;
        public bool Lock
        {
            get
            {
                return _lock;
            }
            set
            {
                detMade = false;
                trMade = false;
                LUMade = false;
                TMade = false;
                PMade = false;
                IMade = false;
                RREFMade = false;
                REFMade = false;
                RankMade = false;
                GMade = false;
                _lock = value;
            }
        } // choose whether the matrix is changable or not


        private double det;
        private bool detMade;
        private double tr;
        private bool trMade;

        private Matrix L;
        private Matrix U;
        private bool LUMade;
        private Matrix P;
        private bool PMade;

        private Matrix T;
        private bool TMade;
        private Matrix I;
        private bool IMade;

        private Matrix ReducedRowEchelonForm;
        private bool RREFMade;
        private Matrix RowEchelonForm;
        private bool REFMade;
        private int rank;
        private bool RankMade;

        private Matrix G;
        private bool GMade;

        public int Rank
        {
            get
            {
                if (RankMade)
                {
                    return rank;
                }
                else return MakeRank();
            }
        }

        private int[] permutation;
        private int permutationDeterminant = 1;

        public Matrix(int row, int col)
        {
            Row = row;
            Col = col;
            mat = new double[row, col];
            Lock = false;
        }
        public Matrix(double[,] matrix)
        {
            mat = matrix;
            Row = mat.GetLength(0);
            Col = mat.GetLength(1);
            Lock = true;
        }
        public Matrix(int row, int col, SpecialMatrix specialMatrix)
        {
            Row = row;
            Col = col;
            mat = new double[row, col];
            switch (specialMatrix)
            {
                case SpecialMatrix.Zero:
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            mat[i, j] = 0;
                        }
                    }
                    break;

                case SpecialMatrix.One:
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            mat[i, j] = 1;
                        }
                    }
                    break;

                case SpecialMatrix.Identity:
                    if (row != col)
                    {
                        throw NotSquareException;
                    }
                    else
                    {
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                mat[i, j] = 0;
                            }
                        }
                        for (int i = 0; i < row; i++)
                        {
                            mat[i, i] = 1;
                        }
                    }
                    break;

                case SpecialMatrix.Random:
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            mat[i, j] = rand.NextDouble();
                        }
                    }
                    break;

                default:
                    throw ShouldNotHappenException;
            }
            Lock = true;
        }

        public Matrix Transpose
        {
            get
            {
                if (Lock == true && TMade == true)
                {
                    return T;
                }
                T = new Matrix(Col, Row);
                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Col; j++)
                    {
                        T[j, i] = mat[i, j];
                    }
                }
                TMade = true;
                T.Lock = true;
                return T;
            }
        }
        /*
        public void MakeLUSimple()
        {
            if (IsSquare())
            {
                L = new Matrix(Row, Col, SpecialMatrix.Identity);
                U = mat.Clone() as double[,];
                L.Lock = false;
                U.Lock = false;

                permutation = new int[Row];
                for (int i = 0; i < Row; i++)
                {
                    permutation[i] = i;
                }
                int selectedCol = 0;

                
            }
        }
        */

        private Matrix Rref()
        {
            if (RREFMade)
            {
                return ReducedRowEchelonForm;
            }
            ReducedRowEchelonForm = mat;
            ReducedRowEchelonForm.Lock = false;
            int row = 0;

            for (int j = 0; j < Col && row < Row; j++)
            {
                for (int i = row; i < Row; i++)
                {
                    if(ReducedRowEchelonForm[i, j] != 0)
                    {
                        RowOperation_Swap(i, row, ref ReducedRowEchelonForm);
                        RowOperation_Divide(row, ReducedRowEchelonForm[row, j], ref ReducedRowEchelonForm);
                        for(int k = 0; k < Row; k++)
                        {
                            if (k == row || ReducedRowEchelonForm[k, j] == 0)
                            {
                                continue;
                            }
                            RowOperation_Add(k, row, -ReducedRowEchelonForm[k, j], ref ReducedRowEchelonForm);
                        }
                        break;
                    }
                }
                row++;
            }
            ReducedRowEchelonForm.Lock = true;
            rank = row; // problem
            RankMade = true;
            RREFMade = true;
            return ReducedRowEchelonForm;
        }
        public Matrix RREF()
        {
            return this.Rref();
        }
        public Matrix RREF(ColumnVector b)
        {
            Matrix A = mat;
            A.ColumnMerge(b);
            return A.Rref();
        }

        private Matrix Ref()
        {
            if (REFMade)
            {
                return RowEchelonForm;
            }
            RowEchelonForm = mat;
            RowEchelonForm.Lock = false;
            int row = 0;

            for (int j = 0; j < Col && row < Row; j++)
            {
                for (int i = row; i < Row; i++)
                {
                    if (RowEchelonForm[i, j] != 0)
                    {
                        RowOperation_Swap(i, row, ref RowEchelonForm);
                        RowOperation_Divide(row, RowEchelonForm[row, j], ref RowEchelonForm);
                        for (int k = row + 1; k < Row; k++)
                        {
                            if (RowEchelonForm[k, j] == 0)
                            {
                                continue;
                            }
                            RowOperation_Add(k, row, -RowEchelonForm[k, j], ref RowEchelonForm);
                        }
                    }
                }
                row++;
            }
            RowEchelonForm.Lock = true;
            rank = row; // problem
            RankMade = true;
            REFMade = true;
            return RowEchelonForm;
        }
        public Matrix REF()
        {
            return this.Ref();
        }
        public Matrix REF(ColumnVector b)
        {
            Matrix A = mat;
            A.ColumnMerge(b);
            return A.Ref();
        }

        private int MakeRank()
        {
            throw new NotImplementedException();
        }

        public void MakeLU()
        {
            if (IsSquare())
            {
                // Do not run this if LU is made!

                L = new Matrix(Row, Col, SpecialMatrix.Identity);
                U = mat;
                L.Lock = false;
                U.Lock = false;

                permutation = new int[Row];
                for (int i = 0; i < Row; i++)
                {
                    permutation[i] = i;
                }
                int selectedCol = 0;

                for (int i = 0; i < Col - 1; i++)
                {
                    double tmp = 0;
                    int inttmp;
                    for (int j = i; j < Row; j++)
                    {
                        if (abs(U[j, i]) > tmp)
                        {
                            tmp = abs(U[j, i]);
                            selectedCol = j;
                        }
                    }

                    ///
                    /// need to change in some way
                    ///

                    if (tmp == 0)
                    {
                        //Console.WriteLine(U);
                        //Console.WriteLine();
                        for (int j = 0; j < Row; j++)
                        {
                            //Console.Write(permutation[j] + " ");
                        }
                        //Console.WriteLine();
                        throw SingularMatrixException;
                    }

                    inttmp = permutation[i];
                    permutation[i] = permutation[selectedCol];
                    permutation[selectedCol] = inttmp;

                    for (int j = 0; j < i; j++)
                    {
                        tmp = L[i, j];
                        L[i, j] = L[selectedCol, j];
                        L[selectedCol, j] = tmp;
                    }

                    if (i != selectedCol) permutationDeterminant *= -1;

                    for (int j = 0; j < Col; j++)
                    {
                        tmp = U[i, j];
                        U[i, j] = U[selectedCol, j];
                        U[selectedCol, j] = tmp;
                    }

                    for (int j = i + 1; j < Row; j++)
                    {
                        L[j, i] = U[j, i] / U[i, i];
                        for (int k = i; k < Col; k++)
                        {
                            U[j, k] = U[j, k] - L[j, i] * U[i, k];
                        }
                    }
                }
                L.Lock = true;
                U.Lock = true;
                LUMade = true;
            }
            else
            {
                throw NotSquareException;
            }
        }

        public Matrix PermutationMatrix
        {
            get
            {
                if (Lock == true && PMade == true)
                {
                    return P;
                }
                if (!LUMade)
                {
                    MakeLU();
                }
                P = new Matrix(Row, Col, SpecialMatrix.Zero);
                P.Lock = false;
                for (int i = 0; i < Row; i++)
                {
                    P[permutation[i], i] = 1;
                }
                PMade = true;
                P.Lock = true;
                return P;
            }
        }

        public Matrix Inverse
        {
            get
            {
                if (Lock == true && IMade == true)
                {
                    return I;
                }
                if (!LUMade)
                {
                    MakeLU();
                }
                I = new Matrix(Row, Col);

                Parallel.For(0, Row, i =>
                {
                    ColumnVector rowVector = new ColumnVector(Row);
                    rowVector[i] = 1;
                    I.SetCol(SolveWith(rowVector), i);
                });
                IMade = true;
                I.Lock = true;
                return I;
            }
        }

        public double Det
        {
            get
            {
                if (Lock == true && detMade == true)
                {
                    return det;
                }
                if (!LUMade)
                {
                    MakeLU();
                }
                det = permutationDeterminant;
                Parallel.For(0, Row, i =>
                {
                    det *= U[i, i];
                });
                detMade = true;
                return det;
            }
        }
        public double Tr
        {
            get
            {
                if (Lock == true && trMade == true)
                {
                    return tr;
                }
                if (IsSquare())
                {
                    tr = 0;
                    for (int i = 0; i < Row; i++)
                    {
                        tr += mat[i, i];
                    }
                    trMade = true;
                    return tr;
                }
                else
                {
                    throw NotSquareException;
                }
            }
        }
        public Matrix LMatrix
        {
            get
            {
                if (Lock == true && LUMade == true)
                {
                    return L;
                }
                if (!LUMade)
                {
                    MakeLU();
                }
                return L;
            }
        }
        public Matrix UMatrix
        {
            get
            {
                if (Lock == true && LUMade == true)
                {
                    return U;
                }
                if (!LUMade)
                {
                    MakeLU();
                }
                return U;
            }
        }

        public Matrix ConvolutionMatrix(Matrix kernel)
        {
            //Assuming kernels have even rows and columns
            if (kernel.Row % 2 == 0 || kernel.Col % 2 == 0)
            {
                return null;
            }
            if (this.Row < kernel.Row || this.Col < kernel.Col)
            {
                return null;
            }
            Matrix A = new Matrix(this.Row - kernel.Row + 1, this.Col - kernel.Col + 1);
            Parallel.For(0, A.Row, i =>
            {
                for (int j = 0; j < A.Col; j++)
                {
                    for (int k = 0; k < kernel.Row; k++)
                    {
                        for (int l = 0; l < kernel.Col; l++)
                        {
                            A[i, j] += mat[i + k, j + l] * kernel[k, l];
                        }
                    }
                }
            });
            return A;
        }

        public Matrix GrammMatrix
        {
            get
            {
                if (GMade)
                {
                    return G;
                }
                else
                {
                    G = new Matrix(Col, Col);
                    for(int i = 0; i < Col; i++)
                    {
                        double sum;
                        for (int j = 0; j < i; j++)
                        {
                            sum = 0;
                            for(int k = 0; k < Row; k++)
                            {
                                sum += mat[k, i] * mat[k, j];
                            }
                            G[i, j] = G[j, i] = sum;
                        }
                        for(int k = 0; k < Row; k++)
                        {
                            G[i, i] += mat[k, i] * mat[k, i];
                        }
                    }
                    GMade = true;
                    return G;
                }
            }
        }

        public bool IsSquare()
        {
            return (Row == Col);
        }
        public static bool IsSquare(Matrix A)
        {
            return (A.Row == A.Col);
        }
        public static bool IsSameSize(Matrix A, Matrix B)
        {
            return ((A.Row == B.Row) && (A.Col == B.Col));
        }
        public static bool IsMultipliable(Matrix A, Matrix B)
        {
            return (A.Col == B.Row);
        }
        public bool IsFullRowRank()
        {
            return Rank == Col;
        }
        public bool IsFullColRank()
        {
            return Rank == Row;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    s += String.Format("{0,5:0.0000}", mat[i, j]) + " ";
                }
                s += "\r\n";
            }
            return s;
        }
        public Matrix Clone()
        {
            return new Matrix(mat.Clone() as double[,]);
        }

        public double this[int row, int col]
        {
            get
            {
                return mat[row, col];
            }
            set
            {
                if (Lock)
                {
                    throw new Exception("Cannot change locked matrix!");
                }
                mat[row, col] = value;
            }
        }

        public ColumnVector ToColumnVector()
        {
            if (Col == 1)
            {
                return new ColumnVector(mat.Clone() as double[,]);
            }
            else return null;
        }
        public RowVector ToRowVector()
        {
            if (Row == 1)
            {
                return new RowVector(mat.Clone() as double[,]);
            }
            else return null;
        }


        public Matrix RowMerge(Matrix A, Matrix B)
        {
            return A.Clone().RowMerge(B);
        }
        public Matrix ColumnMerge(Matrix A, Matrix B)
        {
            return A.Clone().ColumnMerge(B);
        }

        #region Operator

        public static implicit operator Matrix(double[,] matrix)
        {
            return new Matrix((double[,])matrix.Clone());
        }
        public static implicit operator double[,] (Matrix matrix)
        {
            return (double[,])matrix.mat.Clone();
        }
        public static Matrix operator +(Matrix A, Matrix B)
        {
            return Add(A, B);
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            return Subtract(A, B);
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            return SimpleMultiply(A, B);
        }
        public static Matrix operator *(double a, Matrix B)
        {
            return Multiply(a, B);
        }
        public static Matrix operator *(Matrix A, double b)
        {
            return Multiply(b, A);
        }
        public static Matrix operator /(Matrix A, double b)
        {
            return Divide(b, A);
        }
        public static Matrix operator -(Matrix A)
        {
            return Negative(A);
        }

        #endregion

        #region Exceptions

        private Exception ShouldNotHappenException = new Exception("Should not happen");
        private Exception NotSquareException = new Exception("Row and Col are different");
        private Exception SingularMatrixException = new Exception("Singular matrix");
        private Exception DifferentSizeException = new Exception("Size is Different");
        private Exception NotMultiplableException = new Exception("Not multiplable");

        private static Exception StaticDifferentSizeException = new Exception("Size is Different");
        private static Exception StaticNotMultiplableException = new Exception("Not multiplable");

        #endregion

        #region inline functions

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double abs(double a)
        {
            return a > 0 ? a : -a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetCol(ColumnVector v, int k)
        {
            for (int i = 0; i < Row; i++)
            {
                this.mat[i, k] = v[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ColumnVector SolveForth(Matrix A, ColumnVector b)
        {
            int row = A.Row;
            ColumnVector B = new ColumnVector(row);
            b.Lock = false;
            for (int i = 0; i < row; i++)
            {
                B[i] = b[i];
                for (int j = 0; j < i; j++)
                {
                    B[i] -= A[i, j] * B[j];
                }
                B[i] /= A[i, i];
            }
            b.Lock = true;
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ColumnVector SolveBack(Matrix A, ColumnVector b)
        {
            int row = A.Row;
            ColumnVector B = new ColumnVector(row);
            b.Lock = false;

            for (int i = row - 1; i > -1; i--)
            {
                B[i] = b[i];
                for (int j = row - 1; j > i; j--)
                {
                    B[i] -= A[i, j] * B[j];
                }
                B[i] /= A[i, i];
            }
            b.Lock = true;
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ColumnVector SolveWith(ColumnVector v)
        {
            if (Row != Col)
            {
                throw NotSquareException;
            }
            if (Row != v.Row)
            {
                throw new Exception("Wrong number of results in solution vector!");
            }

            ColumnVector b = new ColumnVector(Row);
            b.Lock = false;
            for (int i = 0; i < Row; i++)
            {
                b[i] = v[permutation[i]];
            }
            b.Lock = true;

            return SolveBack(U, SolveForth(L, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Matrix Add(Matrix A, Matrix B)
        {
            if (IsSameSize(A, B))
            {
                Matrix C = new Matrix(A.Row, A.Col);
                C.Lock = false;
                for (int i = 0; i < A.Row; i++)
                {
                    for (int j = 0; j < B.Col; j++)
                    {
                        C[i, j] = A[i, j] + B[i, j];
                    }
                }
                C.Lock = true;
                return C;
            }
            else
            {
                throw StaticDifferentSizeException;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Matrix Subtract(Matrix A, Matrix B)
        {
            if (IsSameSize(A, B))
            {
                Matrix C = new Matrix(A.Row, A.Col);
                C.Lock = false;
                for (int i = 0; i < A.Row; i++)
                {
                    for (int j = 0; j < B.Col; j++)
                    {
                        C[i, j] = A[i, j] - B[i, j];
                    }
                }
                C.Lock = true;
                return C;
            }
            else
            {
                throw StaticDifferentSizeException;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Matrix SimpleMultiply(Matrix A, Matrix B)
        {
            if (IsMultipliable(A, B))
            {
                Matrix C = new Matrix(A.Row, B.Col, SpecialMatrix.Zero);
                C.Lock = false;
                /*
                for(int i = 0; i < A.Row; i++)
                {
                    for(int j = 0; j < B.Col; j++)
                    {
                        for(int k = 0; k < A.Col; k++)
                        {
                            C[i, j] += A[i, k] * B[k, j];
                        }
                    }
                }
                */
                Parallel.For(0, A.Row, i =>
                {
                    int j, k;
                    for (j = 0; j < B.Col; j++)
                    {
                        for (k = 0; k < A.Col; k++)
                        {
                            C[i, j] += A[i, k] * B[k, j];
                        }
                    }
                });
                C.Lock = true;
                return C;
            }
            else
            {
                throw StaticNotMultiplableException;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Matrix Multiply(double a, Matrix B)
        {
            B.Lock = false;
            Parallel.For(0, B.Row, i =>
            {
                for (int j = 0; j < B.Col; j++)
                {
                    B[i, j] *= a;
                }
            });
            B.Lock = true;
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Matrix Divide(double a, Matrix B)
        {
            B.Lock = false;
            Parallel.For(0, B.Row, i =>
              {
                  for (int j = 0; j < B.Col; j++)
                  {
                      B[i, j] /= a;
                  }
              });
            B.Lock = true;
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Matrix Negative(Matrix A)
        {
            Matrix B = A;
            B.Lock = false;
            Parallel.For(0, A.Row, i =>
            {
                for (int j = 0; j < A.Col; j++)
                {
                    B[i, j] = -A[i, j];
                }
            });
            B.Lock = true;
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void RowOperation_Swap(int a, int b, ref Matrix A) // swap row a and b
        {
            if (a == b)
            {
                return;
            }
            for (int i = 0; i < A.Col; i++)
            {
                double tmp = A[a, i];
                A[a, i] = A[b, i];
                A[b, i] = tmp;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void RowOperation_Add(int a, int b, double c, ref Matrix A) // Add row b to row a
        {
            if (a == b)
            {
                throw new Exception("a should not be b");
            }
            for (int i = 0; i < A.Col; i++)
            {
                A[a, i] += A[b, i] * c;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void RowOperation_Multiply(int a, double b, ref Matrix A) // multiply row a with b
        {
            for (int i = 0; i < A.Col; i++)
            {
                A[a, i] *= b;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void RowOperation_Divide(int a, double b, ref Matrix A) // divide row a with b
        {
            for (int i = 0; i < A.Col; i++)
            {
                A[a, i] /= b;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix RowMerge(Matrix B)
        {
            if (this.Col != B.Col)
            {
                return null;
            }
            Matrix A = new Matrix(Row + B.Row, Col);
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    A[i, j] = mat[i, j];
                }
            }
            for (int i = 0; i < B.Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    A[i + Row, j] = B[i, j];
                }
            }
            A.Lock = true;
            return A;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix ColumnMerge(Matrix B)
        {
            if (this.Row != B.Row)
            {
                return null;
            }
            Matrix A = new Matrix(Row, Col + B.Col);
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    A[i, j] = mat[i, j];
                }
            }
            for (int i = 0; i < B.Row; i++)
            {
                for (int j = 0; j < B.Col; j++)
                {
                    A[i, j + Col] = B[i, j];
                }
            }
            A.Lock = true;
            return A;
        }

        #endregion
    }

    public enum SpecialMatrix
    {
        Zero,
        One,
        Identity,
        Random
    }
}
