using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDuoc
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");
        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void Reset()
        {
            Acount.Text = "";
            PasswordTb.Text = "";
           
        }

       

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (Acount.Text == "" || PasswordTb.Text == "" )
            {
                MessageBox.Show("Yêu cầu nhập đầy đủ thông tin !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Acount(Email,Password)values(@EM,@PW)", Con);
                    cmd.Parameters.AddWithValue("@EM", Acount.Text);
                    cmd.Parameters.AddWithValue("@PW", PasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đăng kí thành công ");
                    Con.Close();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
