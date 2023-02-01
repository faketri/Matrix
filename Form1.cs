using System;
using System.Windows.Forms;
using Main.Extends;
using Main.Models;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBoxRows1.TextChanged += (sender, args) => SetRows(textBoxRows1, dataGridView1);
            textBoxRows2.TextChanged += (sender, args) => SetRows(textBoxRows2, dataGridView2);

            textBoxColumn1.TextChanged += (sender, args) => SetColumn(textBoxColumn1, dataGridView1);
            textBoxColumn2.TextChanged += (sender, args) => SetColumn(textBoxColumn2, dataGridView2);

            checkBox1Random.CheckedChanged += (sender, args) => { if (checkBox1Random.Checked) dataGridView1.RandomFill(30); };
            checkBox2Random.CheckedChanged += (sender, args) => { if (checkBox2Random.Checked) dataGridView2.RandomFill(30); };
        }

        private void SetRows(TextBox textBox, DataGridView data) {

            int.TryParse(textBox.Text, out int RowsCount);
            data.RowCount = RowsCount > 0 ? RowsCount : 1;
        }
        private void SetColumn(TextBox textBox, DataGridView data)
        {

            int.TryParse(textBox.Text, out int ColumnCount);
            data.ColumnCount = ColumnCount > 0 ? ColumnCount : 1;
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonAddition.Checked == true)
                {
                    dataGridViewResult.MatrixConvert(
                        (new Matrix(dataGridView1) + new Matrix(dataGridView2))
                            .GetMatrix()
                        );
                }
                if (radioButtonDivision.Checked == true)
                {
                    // TODO DIVISION 
                }
                if (radioButtonMultiply.Checked == true)
                {
                    dataGridViewResult.MatrixConvert(
                        (new Matrix(dataGridView1) * new Matrix(dataGridView2))
                            .GetMatrix()
                        );
                }
                if (radioButtonSubtraction.Checked == true)
                {
                    dataGridViewResult.MatrixConvert(
                         (new Matrix(dataGridView1) - new Matrix(dataGridView2))
                             .GetMatrix()
                         );
                }
                if (radioButtonTransportation.Checked == true)
                {
                    dataGridViewResult.MatrixConvert(
                            new Matrix(dataGridView1)
                            .Transport()
                            .GetMatrix()
                        );
                }
                if (radioButtonDeterminant.Checked == true)
                {
                    dataGridViewResult.Clear();
                    dataGridViewResult.Rows[0].Cells[0].Value = new Matrix(dataGridView1).FindDeterminant();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

      /*
        private void DivisionMatrix()
        {
            try { 
                int i, j, k;
                NewArray = new double[RowsMatrix2, ColumsMatrix2];
                double[,] TempArray = new double[RowsMatrix2, ColumsMatrix2];

                for (i = 0; i < RowsMatrix2; i++)
                {
                    for (j = 0; j < ColumsMatrix2; j++)
                    {
                        TempArray[i, j] = Matrix2[i, j];
                    }
                }

                for (int x = 0; x < RowsMatrix1; x++)
                {
                    for (int y = 0; y < ColumsMatrix1; y++)
                    {
                        Matrix2[x, y] = TempArray[y, x];
                    }
                }

                double[,] MartrixDivRes = new double[RowsMatrix1, ColumsMatrix1];
                MatrixResult = new double[RowsMatrix1, ColumsMatrix1];
                resultDet = FindDeterminant(Matrix2, ColumsMatrix2);
                FindAlgDop(Matrix2, RowsMatrix2, NewArray);

                for (i = 0; i < RowsMatrix1; i++)
                {
                    for (j = 0; j < ColumsMatrix1; j++)
                    {
                        MartrixDivRes[i, j] = (1 / -resultDet) * NewArray[i, j];
                    }
                }

                for (i = 0; i < RowsMatrix1; i++)
                {
                    for (j = 0; j < ColumsMatrix1; j++)
                    {
                        for (k = 0; k < ColumsMatrix1; k++)
                        {
                            MatrixResult[i, j] += Matrix1[i, k] * MartrixDivRes[k, j];
                        }
                    }
                }
                ShowResultMatrix(MatrixResult);
            }
            catch (Exception ex) { MessageBox.Show("Неверный ввод данных в поле Строки и/или столбцы", "Критическая ошибка!", MessageBoxButtons.OK); };
        }
      */

        private void radioButtonDeterminant_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDeterminant.Checked == true)
            {
                groupBoxMatrix2.Enabled = false;
                dataGridView2.Enabled = false;
            }
            else 
            { 
                groupBoxMatrix2.Enabled = true;
                dataGridView2.Enabled = false;
            }
        }
    }
}