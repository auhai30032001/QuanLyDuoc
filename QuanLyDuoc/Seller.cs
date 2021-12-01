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
    public partial class Seller : Form
    {
        public Seller()
        {
            InitializeComponent();
            ShowSeller();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

        private void ShowSeller()
        {
            Con.Open();
            string Query = "Select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellDGV.DataSource = dt;
            Con.Close();
        }

        private void Reset()
        {
            SNameTb.Text = "";
            SAddTb.Text = "";
            
            SPhoneTb.Text = "";
            SGenCb.SelectedIndex = 0;

            Key = 0;
        }
       

       
        private void guna2CustomGradientPanel19_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
            this.Hide();
        }

       
        int Key = 0;
       

      

        
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SAddTb.Text == ""  || SGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SellerTbl(SName,SAdd,SPhone,SGen,SDOB)values(@SN,@SA,@SP,@SG,@SD)", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu ");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SAddTb.Text == ""  || SGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Chọn một dòng để sửa!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update SellerTbl set SName = @SN,SAdd = @SA ,SPhone = @SP,SGen = @SG,SDOB = @SD where SNum = @SKey", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã sửa ");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from SellerTbl where SNum = @SKey", Con);                 
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa ");
                    Con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SellDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.SellDGV.Rows[e.RowIndex];
                SNameTb.Text = SellDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                SAddTb.Text = SellDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                SPhoneTb.Text = SellDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                SDOB.Text = SellDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                SGenCb.SelectedItem = SellDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
                
            }
            if (SNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(SellDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void guna2CustomGradientPanel21_Click_1(object sender, EventArgs e)
        {
            Dashboar obj = new Dashboar();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel20_Click_1(object sender, EventArgs e)
        {
            Medicne obj = new Medicne();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel19_Click_1(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel17_Click_1(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel18_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox34_Click_1(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
