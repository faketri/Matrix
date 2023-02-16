using System;
using System.Threading.Tasks;
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

            checkBox1Random.CheckedChanged += (sender, args) => { if (checkBox1Random.Checked) Task.Factory.StartNew(() => { dataGridView1.RandomFill(30); }); };
            checkBox2Random.CheckedChanged += (sender, args) => { if (checkBox2Random.Checked) Task.Factory.StartNew(() => { dataGridView2.RandomFill(30); }); };
        }

        private void SetRows(TextBox textBox, DataGridView data) {

            int.TryParse(textBox.Text, out int RowsCount);
            data.RowCount = RowsCount > 0 && RowsCount <= 30 ? RowsCount : 1;
        }
        private void SetColumn(TextBox textBox, DataGridView data){

            int.TryParse(textBox.Text, out int ColumnCount);
            data.ColumnCount = ColumnCount > 0 && ColumnCount <= 30 ? ColumnCount : 1;
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (radioButtonAddition.Checked == true)
                        dataGridViewResult.MatrixConvert(
                            (new Matrix(dataGridView1) + new Matrix(dataGridView2))
                                .GetMatrix()
                            );

                    if (radioButtonDivision.Checked == true)
                        dataGridViewResult.MatrixConvert(
                            (new Matrix(dataGridView1) / new Matrix(dataGridView2))
                            .GetMatrix()
                        );

                    if (radioButtonMultiply.Checked == true)
                        dataGridViewResult.MatrixConvert(
                            (new Matrix(dataGridView1) * new Matrix(dataGridView2))
                                .GetMatrix()
                            );

                    if (radioButtonSubtraction.Checked == true)
                        dataGridViewResult.MatrixConvert(
                             (new Matrix(dataGridView1) - new Matrix(dataGridView2))
                                 .GetMatrix()
                             );

                    if (radioButtonTransportation.Checked == true)
                        dataGridViewResult.MatrixConvert(
                                new Matrix(dataGridView1)
                                .Transport()
                                .GetMatrix()
                            );

                    if (radioButtonDeterminant.Checked == true)
                    {
                        dataGridViewResult.Clear();
                        dataGridViewResult.Rows[0].Cells[0].Value = new Matrix(dataGridView1).FindDeterminant();
                    }
                    if (rbBackWard.Checked == true)
                        dataGridViewResult.MatrixConvert(new Matrix(dataGridView1).Inverse().GetMatrix());
                }
                catch (Exception ex) { MessageBox.Show("Неверный ввод данных в поле Строки и/или столбцы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            });
        }

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