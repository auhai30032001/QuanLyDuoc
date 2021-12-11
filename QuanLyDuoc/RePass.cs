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
    public partial class RePass : Form
    {
        string Permission = Login.info.Permission;
        public RePass()
        {
            InitializeComponent();
          
            LoginName.Text = Login.User;
           
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

       
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       /* private void ShowAcount1()
        {
            Con.Open();
            string Query = "Select * from Acount where Email='"+LoginName.Text +"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AcountDGV.DataSource = dt;
            Con.Close();
        }*/

        private void ShowAcount2()
        {
            Con.Open();
            string Query = "Select SName,Password from SellerTbl  ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AcountDGV.DataSource = dt;
            Con.Close();
        }

        private void Reset()
        {
            REmailTb.Text = "";
            RePassTb.Text = "";
            ConfirmPassTb.Text = "";
            Key = 0;

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

       

   
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (RePassTb.Text == "" || REmailTb.Text == "" || ConfirmPassTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin !");
            }
          
            else
            {              
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from SellerTbl where SName= N'" + REmailTb.Text + "' and Password='" + RePassTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                       
                        SqlCommand cmd = new SqlCommand("Update SellerTbl set Password= @PA where SNum = ", Con);
                        cmd.Parameters.AddWithValue("@PA", ConfirmPassTb.Text);                      
                        
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã Sửa ");
                    Login obj = new Login();
                    obj.Show();
                    this.Hide();
                    Con.Close();
                        Reset();

                    }
                    else
                    {
                        MessageBox.Show("Sai mật khẩu cũ !");
                        
                    }
                Con.Close();
  
            }
        }
        int Key = 0;

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }

        

        

        private void RePass_Load(object sender, EventArgs e)
        {
            LoginName.Visible = false;
            REmailTb.Text = Login.User;
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if(Permission == "Admin")
            {
                ShowAcount2();
            }else
            {
                MessageBox.Show("Bạn không thể xem !");
            }
        }
    }
}



