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
    //Form
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SignUp obj = new SignUp();
            obj.Show();
            this.Hide();
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {

        }
        public static string User;

       
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        public void LoginBtn_Click_1(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Acount where Email= N'" + UNameTb.Text + "' and Password='" + PassTb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);            
               if(dt.Rows.Count > 0)
                {              
                    
                    User = UNameTb.Text;
                    Customer obj = new Customer(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString());             
                    //Dashboar obj = new Dashboar();
                    obj.Show();
                    this.Hide();
                   
                   // MessageBox.Show( dt.Rows[0][3].ToString());

                    Con.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu !");
                }
                Con.Close();
            }

            
        }

        private void guna2PictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SignUp obj = new SignUp();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            RePass obj = new RePass();
            obj.Show();
            this.Hide();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            SignUp obj = new SignUp();
            obj.Show();
            this.Hide();
        }
    }
}
