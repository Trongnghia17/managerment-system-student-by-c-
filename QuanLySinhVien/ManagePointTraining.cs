using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Notice.Frame.Types;

namespace QuanLySinhVien
{
    public partial class ManagePointTraining : Form
    {
        PointTrainingClass PointTraining = new PointTrainingClass();
        public ManagePointTraining()
        {
            InitializeComponent();
        }

        private void ManagePointTraining_Load(object sender, EventArgs e)
        {

            // to show score data on datagridview
            showPointTraining();
        }
        public void showPointTraining()
        {
            DataGridView_score.DataSource = PointTraining.getList(new MySqlCommand("SELECT pointtraining.StudentId,student.StdFirstName,student.StdLastName,pointtraining.PointTraining,pointtraining.Description FROM student INNER JOIN pointtraining ON pointtraining.StudentId=student.StdId"));
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            textBox_description.Clear();
            textBox_search.Clear();
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Cần dữ liệu điểm rèn luyện", "Không được để trống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
                double scor = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_description.Text;

                if (PointTraining.updatePointTraining(stdId, scor, desc))
                {
                    showPointTraining();
                    button_clear.PerformClick();
                    MessageBox.Show("Đã hoàn thành chỉnh sửa điểm rèn luyện", "Chỉnh sửa điểm rèn luyện", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Điểm rèn luyện không được chỉnh sửa", "Chỉnh sửa điểm rèn luyện", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "")
            {
                MessageBox.Show("Lỗi - chúng tôi cần id sinh viên", "Xóa điểm rèn luyện", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_stdId.Text);
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa điểm rèn luyện này không", "Xóa điểm rèn luyện", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (PointTraining.deletePointTraining(id))
                    {
                        showPointTraining();
                        MessageBox.Show("Đã xóa điểm rèn luyện", "Xóa điểm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                }

            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = PointTraining.getList(new MySqlCommand("SELECT pointtraining.StudentId, student.StdFirstName, student.StdLastName, pointtraining.PointTraining, pointtraining.Description FROM student INNER JOIN pointtraining ON pointtraining.StudentId = student.StdId WHERE CONCAT(student.StdFirstName, student.StdLastName)LIKE '%" + textBox_search.Text + "%'"));
        }

        private void DataGridView_score_Click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
           
            textBox_score.Text = DataGridView_score.CurrentRow.Cells[3].Value.ToString();
            textBox_description.Text = DataGridView_score.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
