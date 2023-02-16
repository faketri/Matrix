using System;
using Main.Models;

namespace Main.Extends
{
    internal static class MatrixExtends
    {

        /// <summary>
        /// Finding the Determinant of a Matrix
        /// </summary>
        /// <param name="Matrix"></param>
        /// <returns>Determinant</returns>
        public static int FindDeterminant(this Matrix Matrix){
            if (Matrix.ColumnCount == 1)
                return (int)Matrix[0, 0];

            double det = 0;

            for (int i = 0; i < Matrix.ColumnCount; i++)
                det += Math.Pow(-1, i) * Matrix[0, i] * Matrix.GetMinor(0, i).FindDeterminant();
         
            return (int)det;

        }

        /// <summary>
        /// A minor in linear algebra is the determinant of some smaller 
        /// square matrix B cut out from a given matrix A by removing 
        /// one or more of its rows and columns.
        /// </summary>
        /// <param name="Matrix"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static Matrix GetMinor(this Matrix Matrix, int x, int y){
            int xCount = 0;
            int yCount = 0;

            Matrix Minor = new Matrix(Matrix.RowsCount - 1, Matrix.ColumnCount - 1);

            for (int i = 0; i < Matrix.ColumnCount; i++)
                if (i != x)
                {
                    yCount = 0;
                    for (int j = 0; j < Matrix.ColumnCount; j++)
                    {
                        if (j != y)
                            Minor[xCount, yCount++] = Matrix[i, j];
                    }
                    xCount++;
                }

            return Minor;
        }

        /// <summary>
        /// The algebraic complement of the element a[i,j] defined by Determinant 
        /// is called its minor, the application with the sign (-1)^(i+j)
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private static Matrix FindAlgDop(this Matrix matrix){
            Matrix algDop = new Matrix(matrix.RowsCount, matrix.ColumnCount);

            double det = matrix.FindDeterminant();

            if (det > 0)
                det = -1;
            else
                det = 1;

            for (int j = 0; j < matrix.RowsCount; j++)
                for (int i = 0; i < matrix.RowsCount; i++) { 

                    Matrix minor = matrix.GetMinor(j, i);
                    algDop[j, i] = (i + j) % 2 == 0 ? -det * minor.FindDeterminant() : det * minor.FindDeterminant();
                }

            return algDop;
        }

        /// <summary>
        /// Finding the inverse matrix for matrix. А^-1
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Matrix Inverse(this Matrix matrix){
            if (matrix.RowsCount != matrix.ColumnCount)
                throw new ArgumentException("Обратная матрица существует только для квадратных, невырожденных, матриц.");

            Matrix newMat = matrix.Transport();
            int resultDet = newMat.FindDeterminant();

            if (resultDet == 0)
                throw new ArgumentException("определитель матрицы равен НУЛЮ – обратной матрицы НЕ СУЩЕСТВУЕТ.");

            newMat = newMat.FindAlgDop();

            Matrix mat = new Matrix(newMat.RowsCount, newMat.ColumnCount);

            for (int i = 0; i < newMat.RowsCount; i++)
                for (int j = 0; j < newMat.ColumnCount; j++)
                    mat[i, j] += (1.0 / (double)resultDet) * newMat[i, j];

            return mat;
        }
    }
}
