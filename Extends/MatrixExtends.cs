using System;
using Main.Models;

namespace Main.Extends
{
    internal static class MatrixExtends
    {
        public static int FindDeterminant(this Matrix Matrix)
        {
            try
            {
                //останавливаем рекурсию, если матрица состоит из одного элемента
                if (Matrix.ColumnCount == 1)
                    return (int)Matrix[0, 0];


                else
                {
                    double det = 0;

                    for (int i = 0; i < Matrix.ColumnCount; i++)
                        det += Math.Pow(-1, i) * Matrix[0, i] * Matrix.GetMinor(0, i).FindDeterminant();
         
                    return (int)det;
                }
            }
            catch { throw new ArgumentException("Возможно вы ввели недопустимый символ"); }
        }
        private static Matrix GetMinor(this Matrix Matrix, int x, int y)
        {
            try
            {
                int xCount = 0;
                int yCount = 0;

                Matrix Minor = new Matrix(Matrix.RowsCount - 1,Matrix.ColumnCount - 1);

                for (int i = 0; i < Matrix.ColumnCount; i++)
                {
                    if (i != x)
                    {
                        yCount = 0;
                        for (int j = 0; j < Matrix.ColumnCount; j++)
                        {
                            if (j != y)
                            {
                                Minor[xCount, yCount] = Matrix[i, j];
                                yCount++;
                            }
                        }
                        xCount++;
                    }
                }
                return Minor;
            }
            catch (Exception e) { throw new ArgumentException(e.Message); }
        }

        private static Matrix FindAlgDop(this Matrix matrix)
        {
            try
            {
                Matrix NewArray = new Matrix(matrix.RowsCount, matrix.ColumnCount);

                double det = matrix.FindDeterminant();

                if (det > 0) // для знака алгебраического дополнения
                    det = -1;
                else
                    det = 1;

                for (int j = 0; j < matrix.RowsCount; j++)
                {
                    for (int i = 0; i < matrix.RowsCount; i++)
                    {
                        // получаем алгебраическое дополнение
                        Matrix minor = matrix.GetMinor(j, i);

                        if ((i + j) % 2 == 0)
                            NewArray[j, i] = -det * minor.FindDeterminant();
                        else
                            NewArray[j, i] = det * minor.FindDeterminant();
                    }
                }
                return NewArray;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public static Matrix Inverse(this Matrix matrix)
        {
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
                {
                    double temp1 = (1.0 / -resultDet);
                    var temp = newMat[i, j];
                    mat[i, j] += temp1 * temp;
                }

            return mat;
        }
    }
}
