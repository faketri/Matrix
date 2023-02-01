using System;

namespace Main.Models
{
    internal class Matrix
    {
        private double[,] matrix;

        public int ColumnCount { get; private set; }

        public int RowsCount { get; private set; }

        public double this[int i, int j]
        {
            get => matrix[i, j];
            
            set => matrix[i, j] = value;
        }

        public Matrix(double[,] matrix) { 
            this.matrix = matrix;

            ColumnCount = matrix.GetLength(0);
            RowsCount = matrix.GetLength(1);
        }

        public Matrix(System.Windows.Forms.DataGridView datagrid)
        {
            try
            {
                this.matrix = new double[datagrid.RowCount, datagrid.ColumnCount];

                for (int i = 0; i < datagrid.RowCount; i++)
                    for (int j = 0; j < datagrid.ColumnCount; j++)
                        matrix[i, j] = System.Convert.ToDouble(datagrid.Rows[i].Cells[j].Value);

                RowsCount = matrix.GetLength(0);
                ColumnCount = matrix.GetLength(1);
            }
            catch { throw new ArgumentException("Неверные данные"); }
        }

        public double[,] GetMatrix() => matrix;

        public Matrix Transport()
        {
            try
            {
                var TransponseMatrix = new double[RowsCount, ColumnCount];

                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        TransponseMatrix[i, j] = matrix[j, i];

                return new Matrix(TransponseMatrix);
            }
            catch { throw new ArgumentException("Неверный ввод данных в поле Строки и/или столбцы"); }
        }

        public static Matrix operator +(Matrix Matrix, Matrix Matrix1) 
        {
            try
            {
                var Addition = new double[Matrix.RowsCount, Matrix.ColumnCount];

                for (int i = 0; i < Matrix.RowsCount; i++)
                {
                    for (int j = 0; j < Matrix.ColumnCount; j++)
                    {
                        Addition[i, j] = Matrix[i, j] + Matrix1[i, j];
                    }
                }
                return new Matrix(Addition); 
            }
            catch { throw new Exception("Неверные данные"); }
        }

        public static Matrix operator -(Matrix Matrix, Matrix Matrix1)
        {
            try { 
                var Subtraction = new double[Matrix.RowsCount, Matrix.ColumnCount];

                for (int i = 0; i < Matrix.RowsCount; i++)
                {
                    for (int j = 0; j < Matrix.ColumnCount; j++)
                    {
                        Subtraction[i, j] = Matrix[i, j] - Matrix1[i, j];
                    }
                }
                return new Matrix(Subtraction);

            }catch { throw new Exception("Неверные данные"); }
        }

        public static Matrix operator *(Matrix Matrix, Matrix Matrix1)
        {
            try
            {
                double[,] MatrixResult;

                int RowsCount = Matrix.RowsCount;
                int ColumnsCount = Matrix.ColumnCount;

                if (Matrix.ColumnCount > Matrix1.ColumnCount)
                    ++RowsCount;

                else if (Matrix.ColumnCount < Matrix1.ColumnCount)
                    ++ColumnsCount;
                
                MatrixResult = new double[RowsCount, ColumnsCount];

                for (int i = 0; i < Matrix.RowsCount; i++)
                {
                    for (int j = 0; j < Matrix1.ColumnCount; j++)
                    {
                        for (int k = 0; k < Matrix.ColumnCount; k++)
                        {
                            MatrixResult[i, j] += Matrix[i, k] * Matrix1[k, j];
                        }
                    }
                }
                return new Matrix(MatrixResult);
            }
            catch { throw new ArgumentException("Неверный ввод данных в поле Строки и/или столбцы"); }

        }

        public static Matrix operator /(Matrix matrix, Matrix matrix1)
        { return new Matrix(new double[2, 2]); }
    }
}
