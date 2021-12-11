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
    public partial class Customer : Form
    {
        string Permission = Login.info.Permission;
        public Customer()
        {
            InitializeComponent();
     
        }
        
        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");


        private void ShowCust()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustDGV.DataSource = dt;         
            Con.Close();
        }

        private void Find()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl where CustName like '%"+FindTb.Text.Trim()+"%'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustDGV.DataSource = dt;
            Con.Close();
        }

        private void Reset()
        {
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
            CustGenCb.SelectedIndex = 0;
            Key = 0;
        }

        


        private void SaveBtn_Click(object sender, EventArgs e)
        {
           if(Permission == "Admin" || Permission == "Thu Ngân")
            {
               
                if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustAddTb.Text == "" || CustGenCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin !");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into CustomerTbl(CustName,CustAdd,CustPhone,CustDOB,CustGen)values(@CN,@CA,@CP,@CD,@CG)", Con);
                        cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                        cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                        cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã lưu  ");
                        Con.Close();

                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền này ");
            }
        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            if(Permission == "Admin" || Permission == "Thu Ngân")
            {
                if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustAddTb.Text == "" || CustGenCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Thiếu thông tin !");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Update CustomerTbl set CustName = @CN,CustAdd = @CA,CustPhone = @CP,CustDOB = @CD,CustGen = @CG where CustNum = @CKey", Con);
                        cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                        cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                        cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@CD", CusDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@CKey", Key);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã sửa ");
                        Con.Close();

                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }              
            }
            else
            {
                
                MessageBox.Show("Bạn không có quyền này !");
                
            }
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {

            if(Permission == "Admin" )
            {
                if (Key == 0)
                {
                    MessageBox.Show("Chọn một dòng để xóa !");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from CustomerTbl where CustNum = @CKey", Con);
                        cmd.Parameters.AddWithValue("@CKey", Key);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã xóa");
                        Con.Close();

                        Reset();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn ko có quyền này !");
            }
        }
        int Key = 0;
        private void CustDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.CustDGV.Rows[e.RowIndex];
                CustNameTb.Text = CustDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                CustAddTb.Text = CustDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                CustPhoneTb.Text = CustDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                CustGenCb.SelectedItem = CustDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
                CusDOB.Text = CustDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void guna2CustomGradientPanel21_Click(object sender, EventArgs e)
        {
           
                Dashboar obj = new Dashboar();
                obj.Show();
                this.Hide();
          
        }

        private void guna2CustomGradientPanel20_Click(object sender, EventArgs e)
        {
           
               if(Permission == "Admin" || Permission == "Quản Kho")
            {
                Medicne obj = new Medicne();
                obj.Show();
                this.Hide();
            }
               else
            {
                MessageBox.Show("Không thể truy cập !");
            }
           
        }

        private void guna2CustomGradientPanel18_Click(object sender, EventArgs e)
        {if (Permission == "Admin" )
            {
                Seller obj = new Seller();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Không thể truy cập !");
            }
        }

        private void guna2CustomGradientPanel17_Click(object sender, EventArgs e)
        {

            if (Permission == "Admin" || Permission == "Quản Kho")
            {
                Manufacturer obj = new Manufacturer();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Không thể truy cập !");
            }

        }

        private void guna2PictureBox34_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
         private void Customer_Load(object sender, EventArgs e)
         {
            CusDOB.Value = DateTime.Now;
            SaveBtn.Enabled = false;
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            guna2GradientButton2.Enabled = false;
            CustGenCb.SelectedIndex = 0;
           
         }

        private void CustNameTb_TextChanged(object sender, EventArgs e)
        {
           if(CustNameTb.Text == "")
            {
                SaveBtn.Enabled = false;
                EditBtn.Enabled = false;
                DeleteBtn.Enabled = false;
                guna2GradientButton2.Enabled = false;
            }
           else
            {
                SaveBtn.Enabled = true;
                EditBtn.Enabled = true;
                DeleteBtn.Enabled = true;
                guna2GradientButton2.Enabled = true;
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void FindTb_TextChanged(object sender, EventArgs e)
        {
            Find();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
            if(Permission == "Admin" || Permission == "Thu Ngân")              
            ShowCust();
            else
            {
                MessageBox.Show("Bạn không thể xem !");
            }
        }
    }
}
