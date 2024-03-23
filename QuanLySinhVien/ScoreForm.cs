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
    public partial class ScoreForm : Form
    {
        CourseClass course = new CourseClass();
        StudentClass student = new StudentClass();
        ScoreClass score = new ScoreClass();
        public ScoreForm()
        {
            InitializeComponent();
        }
        //create a function to show data on datagridview score
        private void showScoe()
        {
            DataGridView_student.DataSource = score.getList(new MySqlCommand("SELECT score.StudentId,student.StdFirstName,student.StdLastName,score.CourseName,score.Score,score.Description FROM student INNER JOIN score ON score.StudentId=student.StdId"));
        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
            // to show data on score datagridview

            //To Display the student list on Datagridview
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT `StdId`,`StdFirstName`,`StdLastName` FROM `student`"));
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Cần thêm điểm", "Không để trống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
                string cName = comboBox_course.Text;
                double scor = Convert.ToInt32(textBox_score.Text);
                string desc = textBox_description.Text;
                if (!score.checkScore(stdId, cName))
                {

                    if (score.insertScore(stdId, cName, scor, desc))
                    {
                        showScoe();
                        button_clear.PerformClick();
                        MessageBox.Show("Thêm điểm mới thành công", "Thêm điểm mới", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Chưa thêm điểm mới", "Thêm điểm mới", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Điểm cho khóa học này đã tồn tại", "Thêm điểm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            comboBox_course.SelectedIndex = 0;
            textBox_description.Clear();
        }

        private void button_sStudent_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.getList(new MySqlCommand("SELECT `StdId`,`StdFirstName`,`StdLastName` FROM `student`"));
        }

        private void button_sScore_Click(object sender, EventArgs e)
        {
            showScoe();
        }
    }
}
