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
    public class Info
    {
        public Info(int SNum,string SName, DateTime SDOB, string SPhone, string SAdd, string SGen, string Permission,string Password)
        {
            this.SNum = SNum;
            this.SName = SName;
            this.SDOB = SDOB;
            this.SPhone = SPhone;
            this.SGen = SGen;
            this.SAdd = SAdd;
            this.Password = Password;
            this.Permission = Permission;
        }
        public int SNum { get; set; }
        public DateTime SDOB { get; set; }
        public string SPhone { get; set; }
        public string Permission { get; set; }

        public string SAdd { get; set; }
        public string SName { get; set; }
        public string SGen { get; set; }
        public string Password { get; set; }
  

    }
        //Form
        public partial class Login : Form
    {
        
        public static Info info;
        public Login()
        {
            InitializeComponent();
        }

        
        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

       

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
        string id;
      

        public void LoginBtn_Click_1(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from SellerTbl where SName= N'" + UNameTb.Text + "' and Password='" + PassTb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                   // public Info(int SNum, string SName, string SDOB, string SPhone, string SAdd, string SGen, string Password, string Permission)
                    info = new Info(Convert.ToInt32(dt.Rows[0][0].ToString()) ,dt.Rows[0][1].ToString(), Convert.ToDateTime( dt.Rows[0][2]), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString(), dt.Rows[0][7].ToString());
                    User = UNameTb.Text;
                    Dashboar obj = new Dashboar();
                    obj.Show();
                    this.Hide();
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

       

      
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            SignUp obj = new SignUp();
            obj.Show();
            this.Hide();
        }
    }
}
