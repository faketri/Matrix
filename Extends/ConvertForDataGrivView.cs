using System;
using System.Windows.Forms;

namespace Main.Extends
{
    public static class DataGridViewExtends
    {
        public static void MatrixConvert(this DataGridView dataGridView, double[,] matrix) {

            dataGridView.RowCount = matrix.GetLength(0);
            dataGridView.ColumnCount = matrix.GetLength(1);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }

        public static void RandomFill(this DataGridView dataGridView)
        {
            if (dataGridView.RowCount > 0)
            {
                var rand = new Random();

                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                    {
                        dataGridView.Rows[i].Cells[j].Value = rand.Next();
                    }
                }
            }
        }

        public static void RandomFill(this DataGridView dataGridView, int MaxValue)
        {
            if (dataGridView.RowCount > 0)
            {
                var rand = new Random();

                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                    {
                        dataGridView.Rows[i].Cells[j].Value = rand.Next(MaxValue);
                    }
                }
            }
        }

        public static void Clear(this DataGridView dataGridView) {
            if (dataGridView.RowCount > 0)
            {
                dataGridView.RowCount = 1;
                dataGridView.ColumnCount = 1;

                dataGridView.Rows[0].Cells[0].Value = null;
            }
            else
            {
                dataGridView.RowCount = 1;
                dataGridView.ColumnCount = 1;
            }
        }
    }
}
