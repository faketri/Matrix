using System;
using System.Drawing;
using System.Windows.Forms;
using Main.Extends;
using Main.Models;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); ;

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
                    DivisionMatrix();
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
                    var resultDet = FindDeterminant(Matrix2, ColumsMatrix2);
                    dataGridViewResult.RowCount = 1;
                    dataGridViewResult.ColumnCount = 1;
                    dataGridViewResult.Rows[0].Cells[0].Value = resultDet;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private int FindDeterminant(double[,] Matrix2, int size)
        {
            try { 
                //останавливаем рекурсию, если матрица состоит из одного элемента
                if (size == 1)
                {
                    return (int)Matrix2[0, 0];
                }
                else
                {
                    double det = 0;
                    double[,] Minor = new double[size - 1, size - 1];
                    for (int i = 0; i < size; i++)
                    {
                        GetMinor(Matrix2, Minor, 0, i, size);
                        //рекурсия
                        det += Math.Pow(-1, i) * Matrix2[0, i] * FindDeterminant(Minor, size - 1); //приведение в тип (int)
                    }
                    return (int)det;
                }
            } catch { 
                MessageBox.Show("Возможно вы ввели недопустимый символ", "Error", MessageBoxButtons.OK);
                return 0;
            }
        }
        private int GetMinor(double[,] Matrix2, double[,] newArray, int x, int y, int size)
        {
            int xCount = 0;
            int yCount = 0;
            int i, j;
            for (i = 0; i < size; i++)
            {
                if (i != x)
                {
                    yCount = 0;
                    for (j = 0; j < size; j++)
                    {
                        if (j != y)
                        {
                            newArray[xCount, yCount] = Matrix2[i, j];
                            yCount++;
                        }
                    }
                    xCount++;
                }
            }
            return 0;
        }
      
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
        private void FindAlgDop(double[,] Matrix2, int size, double[,] NewArray)
        {
            try
            {
                double det = FindDeterminant(Matrix2, size);
                if (det > 0) // для знака алгебраического дополнения
                    det = -1;
                else
                    det = 1;
                double[,] minor = new double[size - 1, size - 1];

                for (int j = 0; j < size; j++)
                {
                    for (int i = 0; i < size; i++)
                    {
                        // получаем алгебраическое дополнение
                        GetMinor(Matrix2, minor, j, i, size);
                        if ((i + j) % 2 == 0)
                            NewArray[j, i] = -det * FindDeterminant(minor, size - 1);
                        else
                            NewArray[j, i] = det * FindDeterminant(minor, size - 1);
                    }
                }
            }
            catch (Exception e) { }
        }


        private void radioButtonDeterminant_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDeterminant.Checked == true)
            {
                groupBoxMatrix1.Enabled = false;
            }
            else 
            { 
                groupBoxMatrix1.Enabled = true;
            }
        }
    }
}