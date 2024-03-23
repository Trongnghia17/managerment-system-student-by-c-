using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class ManageStudentForm : Form
    {      
        StudentClass student = new StudentClass();
        public ManageStudentForm()
        {
            InitializeComponent();
        }

        private void ManageStudentForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentlist(new MySqlCommand("SELECT * FROM `student`"));
            DataGridView_student.RowTemplate.Height = 80;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_id.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            textBox_Fname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textBox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Nam")
                radioButton_male.Checked = true;

            textBox_phone.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            textBox_address.Text = DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox_student.Image = Image.FromStream(ms);
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();
            textBox_address.Clear();
            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;
        }
        bool verify()
        {
            if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") ||
                (textBox_phone.Text == "") || (textBox_address.Text == "") ||
                (pictureBox_student.Image == null))
            {
                return false;
            }
            else
                return true;
        }
        private void button_update_Click(object sender, EventArgs e)
        {
            // update student record
            int id = Convert.ToInt32(textBox_id.Text);
            string fname = textBox_Fname.Text;
            string lname = textBox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textBox_phone.Text;
            string address = textBox_address.Text;
            string gender = radioButton_male.Checked ? "Nam" : "Nữ";

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 18 || (this_year - born_year) > 100)
            {
                MessageBox.Show("Sinh viên từ tuổi 18 đến 100", "Ngày sinh không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    // to get photo from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    if (student.updateStudent(id, fname, lname, bdate, gender, phone, address, img))
                    {
                        showTable();
                        MessageBox.Show("Thông tin học sinh đã được sửa", "Sửa thông tin học sinh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không để trống", "Sửa thông tin sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchStudent(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            //remove the selected Student
            int id = Convert.ToInt32(textBox_id.Text);
            //Show a confirmation message before delete the student
            if (MessageBox.Show("Bạn muốn xóa thông tin sinh viên", "Xóa thông tin sinh viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (student.deleteStudent(id))
                {
                    showTable();
                    MessageBox.Show("Xóa thông tin sinh viên", "Xóa thông tin sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_clear.PerformClick();
                }
            }
        }
    }
}
