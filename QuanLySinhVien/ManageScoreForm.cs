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

namespace QuanLySinhVien
{
    public partial class ManageScoreForm : Form
    {
        CourseClass course = new CourseClass();
        ScoreClass score = new ScoreClass();
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            //populate the combobox with courses name
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
            // to show score data on datagridview
            showScore();
        }
        public void showScore()
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId,student.StdFirstName,student.StdLastName,score.CourseName,score.Score,score.Description FROM student INNER JOIN score ON score.StudentId=student.StdId"));
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
                MessageBox.Show("Cần dữ liệu điểm", "Không được để trống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
                string cName = comboBox_course.Text;
                double scor = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_description.Text;

                if (score.updateScore(stdId, cName, scor, desc))
                {
                    showScore();
                    button_clear.PerformClick();
                    MessageBox.Show("Đã hoàn thành chỉnh sửa điểm", "Chỉnh sửa điểm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Điểm không được chỉnh sửa", "Chỉnh sửa điểm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "")
            {
                MessageBox.Show("Lỗi - chúng tôi cần id sinh viên", "Xóa điểm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_stdId.Text);
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa điểm này không", "Xóa điểm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(id))
                    {
                        showScore();
                        MessageBox.Show("Đã xóa điểm", "Xóa điểm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                }

            }
        }
     

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_score.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId, student.StdFirstName, student.StdLastName, score.CourseName, score.Score, score.Description FROM student INNER JOIN score ON score.StudentId = student.StdId WHERE CONCAT(student.StdFirstName, student.StdLastName, score.CourseName)LIKE '%" + textBox_search.Text + "%'"));
        }

        private void DataGridView_score_Click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();
            comboBox_course.Text = DataGridView_score.CurrentRow.Cells[3].Value.ToString();
            textBox_score.Text = DataGridView_score.CurrentRow.Cells[4].Value.ToString();
            textBox_description.Text = DataGridView_score.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
