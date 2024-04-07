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
    public partial class PointTrainingForm : Form
    {
        CourseClass course = new CourseClass();
        StudentClass student = new StudentClass();
        PointTrainingClass pointTraining = new PointTrainingClass();
        public PointTrainingForm()
        {
            InitializeComponent();
        }
        private void showPointTraining()
        {
            DataGridView_student.DataSource = pointTraining.getList(new MySqlCommand("SELECT pointtraining.StudentId,student.StdFirstName,student.StdLastName,pointtraining.PointTraining,pointtraining.Description FROM student INNER JOIN pointtraining ON pointtraining.StudentId=student.StdId"));
        }

        private void PointTraining_Load(object sender, EventArgs e)
        {

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Cần thêm điểm rèn luyện", "Không để trống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
           
                double pt = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_description.Text;
                if (!pointTraining.checkPointTraining(stdId))
                {

                    if (pointTraining.insertPointTraining(stdId, pt, desc))
                    {
                        showPointTraining();
                        button_clear.PerformClick();
                        MessageBox.Show("Thêm điểm rèn luyện mới thành công", "Thêm điểm rèn luyện mới", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Chưa thêm điểm rèn luyện mới", "Thêm điểm rèn luyện mới", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Điểm rèn luyện cho khóa học này đã tồn tại", "Thêm điểm rèn luyện", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            textBox_description.Clear();
        }

        private void button_sStudent_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT `StdId`,`StdFirstName`,`StdLastName` FROM `student`"));
        }

        private void button_sScore_Click(object sender, EventArgs e)
        {
            showPointTraining();
        }
    }
}
