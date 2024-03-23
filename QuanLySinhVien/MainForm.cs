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
    public partial class MainForm : Form
    {   
        StudentClass student = new StudentClass();
        CourseClass course = new CourseClass();
        public MainForm()
        {
            InitializeComponent();
            customizeDesign();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            studentCount();
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseName";
        }
        private void studentCount()
        {
            label_totalStd.Text = "Tổng sô học sinh : " + student.totalStudent();
            label_maleStd.Text = "Nam : " + student.maleStudent();
            label_femaleStd.Text = "Nữ : " + student.femaleStudent();

        }
        private void customizeDesign()
        {
            panel_stdsubmenu.Visible = false;
            panel_courseSubmenu.Visible = false;
            panel_scoreSubmenu.Visible = false;

        }

        private void hideSubmenu()
        {
            if (panel_stdsubmenu.Visible == true)
                panel_stdsubmenu.Visible = false;
            if (panel_courseSubmenu.Visible == true)
                panel_courseSubmenu.Visible = false;
            if (panel_scoreSubmenu.Visible == true)
                panel_scoreSubmenu.Visible = false;
        }

        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }
        private void button_std_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_stdsubmenu);
        }

        private void button_registration_Click(object sender, EventArgs e)
        {
            openChildForm(new RegisterForm());
            hideSubmenu();
        }
        private void button_manageStd_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageStudentForm());
            hideSubmenu();
        }

     

        private void button_course_Click(object sender, EventArgs e)
        {   
            showSubmenu(panel_courseSubmenu);
        }
        private void button_newCourse_Click(object sender, EventArgs e)
        {
            openChildForm(new CourseForm());
            hideSubmenu();
        }
        private void button_manageCourse_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageCourseForm());
            hideSubmenu();

        }
        private void button_score_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_scoreSubmenu);
        }
        private void button_newScore_Click(object sender, EventArgs e)
        {   
            openChildForm(new ScoreForm());
            hideSubmenu();
        }

        private void button_manageScore_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageScoreForm());
            hideSubmenu();
        }

       
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void button_dashboard_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            panel_main.Controls.Add(panel_cover);
            studentCount();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            this.Hide();
            login.Show();
        }

        private void comboBox_course_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_cmale.Text = "Nam : " + student.exeCount("SELECT COUNT(*) FROM student INNER JOIN score ON score.StudentId = student.StdId WHERE score.CourseName = '" + comboBox_course.Text + "' AND student.Gender = 'Nam'");
            label_cfemale.Text = "Nữ : " + student.exeCount("SELECT COUNT(*) FROM student INNER JOIN score ON score.StudentId = student.StdId WHERE score.CourseName = '" + comboBox_course.Text + "' AND student.Gender = 'Nữ'");
        }
    }
}
