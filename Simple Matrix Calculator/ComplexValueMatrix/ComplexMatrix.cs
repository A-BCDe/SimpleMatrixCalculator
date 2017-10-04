using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Matrix_Calculator.Number;
using Simple_Matrix_Calculator.RealMatrix;
using System.Runtime.CompilerServices;

namespace Simple_Matrix_Calculator.ComplexValueMatrix
{
    class ComplexMatrix
    {
        public readonly int Row;
        public readonly int Col;
        protected Complex[,] mat;
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
                _lock = value;
            }
        } // choose whether the matrix is changable or not

        private Complex det;
        private bool detMade;
        private Complex tr;
        private bool trMade;

        private ComplexMatrix L;
        private ComplexMatrix U;
        private bool LUMade;
        private ComplexMatrix P;
        private bool PMade;

        private ComplexMatrix T;
        private bool TMade;
        private ComplexMatrix I;
        private bool IMade;



        private int[] permutation;
        private int permutationDeterminant = 1;

        public ComplexMatrix(int row, int col)
        {
            Row = row;
            Col = col;
            mat = new Complex[row, col];
            Lock = false;
        }
        public ComplexMatrix(Complex[,] matrix)
        {
            mat = matrix;
            Row = mat.GetLength(0);
            Col = mat.GetLength(1);
            Lock = true;
        }
        public ComplexMatrix(int row, int col, ComplexSpecialMatrix specialMatrix)
        {
            Row = row;
            Col = col;
            mat = new Complex[row, col];
            switch (specialMatrix)
            {
                case ComplexSpecialMatrix.Zero:
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            mat[i, j] = 0;
                        }
                    }
                    break;

                case ComplexSpecialMatrix.One:
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            mat[i, j] = 1;
                        }
                    }
                    break;

                case ComplexSpecialMatrix.Identity:
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

                case ComplexSpecialMatrix.Random:
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            mat[i, j] = new Complex(rand.NextDouble(), rand.NextDouble());
                        }
                    }
                    break;

                default:
                    throw ShouldNotHappenException;
            }
            Lock = true;
        }

        public ComplexMatrix Transpose
        {
            get
            {
                if (Lock == true && TMade == true)
                {
                    return T;
                }
                T = new ComplexMatrix(Col, Row);
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

        public void MakeLU()
        {
            if (IsSquare())
            {
                // Do not run this if LU is made!

                L = new ComplexMatrix(Row, Col, ComplexSpecialMatrix.Identity);
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
                        if (Abs(U[j, i]) > tmp)
                        {
                            tmp = Abs(U[j, i]);
                            selectedCol = j;
                        }
                    }
                    /*
                    if (tmp == 0)
                    {
                        Console.WriteLine(U);
                        Console.WriteLine();
                        for(int j = 0; j < Row; j++)
                        {
                            Console.Write(permutation[j] + " ");
                        }
                        Console.WriteLine();
                        throw SingularMatrixException;
                    }
                    */
                    inttmp = permutation[i];
                    permutation[i] = permutation[selectedCol];
                    permutation[selectedCol] = inttmp;

                    for (int j = 0; j < i; j++)
                    {
                        Complex tmp1 = L[i, j];
                        L[i, j] = L[selectedCol, j];
                        L[selectedCol, j] = tmp1;
                    }

                    if (i != selectedCol) permutationDeterminant *= -1;

                    for (int j = 0; j < Col; j++)
                    {
                        Complex tmp1 = U[i, j];
                        U[i, j] = U[selectedCol, j];
                        U[selectedCol, j] = tmp1;
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

        public ComplexMatrix PermutationMatrix
        {
            get
            {
                if (Lock == true && PMade == true)
                {
                    return P;
                }
                P = new ComplexMatrix(Row, Col, ComplexSpecialMatrix.Zero);
                for (int i = 0; i < Row; i++)
                {
                    P[permutation[i], i] = 1;
                }
                PMade = true;
                P.Lock = true;
                return P;
            }
        }

        public ComplexMatrix Inverse
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
                I = new ComplexMatrix(Row, Col);

                Parallel.For(0, Row, i =>
                {
                    ComplexRowVector rowVector = new ComplexRowVector(Row);
                    rowVector[i] = 1;
                    I.SetCol(SolveWith(rowVector), i);
                });
                IMade = true;
                I.Lock = true;
                return I;
            }
        }

        public Complex Det
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
        public Complex Tr
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
        public ComplexMatrix LMatrix
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
        public ComplexMatrix UMatrix
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

        public bool IsSquare()
        {
            return (Row == Col);
        }
        public static bool IsSquare(ComplexMatrix A)
        {
            return (A.Row == A.Col);
        }
        public static bool IsSameSize(ComplexMatrix A, ComplexMatrix B)
        {
            return ((A.Row == B.Row) && (A.Col == B.Col));
        }
        public static bool IsMultipliable(ComplexMatrix A, ComplexMatrix B)
        {
            return (A.Col == B.Row);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    s += String.Format("{0}", mat[i, j]) + " ";
                }
                s += "\r\n";
            }
            return s;
        }

        public Complex this[int row, int col]
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

        #region Operator

        public static implicit operator ComplexMatrix(Matrix matrix)
        {
            ComplexMatrix A = new ComplexMatrix(matrix.Row, matrix.Col);
            A.Lock = false;
            for (int i = 0; i < matrix.Row; i++)
            {
                for(int j = 0; j < matrix.Col; j++)
                {
                    A[i, j] = matrix[i, j];
                }
            }
            A.Lock = true;
            return A;
        }
        public static implicit operator ComplexMatrix(Complex[,] complexMatrix)
        {
            return new ComplexMatrix((Complex[,])complexMatrix.Clone());
        }
        public static implicit operator Complex[,] (ComplexMatrix complexMatrix)
        {
            return (Complex[,])complexMatrix.mat.Clone();
        }
        public static ComplexMatrix operator +(ComplexMatrix A, ComplexMatrix B)
        {
            return Add(A, B);
        }
        public static ComplexMatrix operator -(ComplexMatrix A, ComplexMatrix B)
        {
            return Subtract(A, B);
        }
        public static ComplexMatrix operator *(ComplexMatrix A, ComplexMatrix B)
        {
            return SimpleMultiply(A, B);
        }
        public static ComplexMatrix operator *(Complex a, ComplexMatrix B)
        {
            return Multiply(a, B);
        }
        public static ComplexMatrix operator *(ComplexMatrix A, Complex b)
        {
            return Multiply(b, A);
        }
        public static ComplexMatrix operator /(ComplexMatrix A, Complex b)
        {
            return Divide(b, A);
        }
        public static ComplexMatrix operator -(ComplexMatrix A)
        {
            return Negative(A);
        }
        public static ComplexMatrix operator ~(ComplexMatrix A) // Conjugate Transpose
        {
            return ConjugateTranspose(A);
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
        private double Abs(Complex a)
        {
            return a.Re * a.Re + a.Im * a.Im;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetCol(ComplexRowVector v, int k)
        {
            for (int i = 0; i < Row; i++)
            {
                this.mat[i, k] = v[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexRowVector SolveForth(ComplexMatrix A, ComplexRowVector b)
        {
            int row = A.Row;
            ComplexRowVector B = new ComplexRowVector(row);

            for (int i = 0; i < row; i++)
            {
                B[i] = b[i];
                for (int j = 0; j < i; j++)
                {
                    B[i] -= A[i, j] * B[j];
                }
                B[i] /= A[i, i];
            }
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexRowVector SolveBack(ComplexMatrix A, ComplexRowVector b)
        {
            int row = A.Row;
            ComplexRowVector B = new ComplexRowVector(row);

            for (int i = row - 1; i > -1; i--)
            {
                B[i] = b[i];
                for (int j = row - 1; j > i; j--)
                {
                    B[i] -= A[i, j] * B[j];
                }
                B[i] /= A[i, i];
            }
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ComplexRowVector SolveWith(ComplexRowVector v)
        {
            if (Row != Col)
            {
                throw NotSquareException;
            }
            if (Row != v.Row)
            {
                throw new Exception("Wrong number of results in solution vector!");
            }

            ComplexRowVector b = new ComplexRowVector(Row);
            for (int i = 0; i < Row; i++)
            {
                b[i] = v[permutation[i]];
            }

            return SolveBack(U, SolveForth(L, b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexMatrix Add(ComplexMatrix A, ComplexMatrix B)
        {
            if (IsSameSize(A, B))
            {
                ComplexMatrix C = new ComplexMatrix(A.Row, A.Col);
                for (int i = 0; i < A.Row; i++)
                {
                    for (int j = 0; j < B.Row; j++)
                    {
                        C[i, j] = A[i, j] + B[i, j];
                    }
                }
                return C;
            }
            else
            {
                throw StaticDifferentSizeException;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexMatrix Subtract(ComplexMatrix A, ComplexMatrix B)
        {
            if (IsSameSize(A, B))
            {
                ComplexMatrix C = new ComplexMatrix(A.Row, A.Col);
                for (int i = 0; i < A.Row; i++)
                {
                    for (int j = 0; j < B.Row; j++)
                    {
                        C[i, j] = A[i, j] - B[i, j];
                    }
                }
                return C;
            }
            else
            {
                throw StaticDifferentSizeException;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexMatrix SimpleMultiply(ComplexMatrix A, ComplexMatrix B)
        {
            if (IsMultipliable(A, B))
            {
                ComplexMatrix C = new ComplexMatrix(A.Row, B.Col, ComplexSpecialMatrix.Zero);
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
        private static ComplexMatrix Multiply(Complex a, ComplexMatrix B)
        {
            Parallel.For(0, B.Row, i =>
            {
                for (int j = 0; j < B.Col; j++)
                {
                    B[i, j] *= a;
                }
            });
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexMatrix Divide(Complex a, ComplexMatrix B)
        {
            Parallel.For(0, B.Row, i =>
            {
                for (int j = 0; j < B.Col; j++)
                {
                    B[i, j] /= a;
                }
            });
            return B;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexMatrix Negative(ComplexMatrix A)
        {
            Parallel.For(0, A.Row, i =>
            {
                for (int j = 0; j < A.Col; j++)
                {
                    A[i, j] = -A[i, j];
                }
            });
            return A;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ComplexMatrix ConjugateTranspose(ComplexMatrix A)
        {
            ComplexMatrix B = new ComplexMatrix(A.Col, A.Row);
            Parallel.For(0, A.Row, i =>
            {
                for (int j = 0; j < A.Col; j++)
                {
                    B[j, i] = A[i, j];
                }
            });
            B.Lock = true;
            return B;
        }
    }
    #endregion

    public enum ComplexSpecialMatrix
    {
        Zero, One, Identity, Random
    }
}
