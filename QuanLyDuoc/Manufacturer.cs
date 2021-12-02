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
    public partial class Manufacturer : Form
    {
        string IdAcount = "", Email = "", Pass = "", Permission = "";
        public Manufacturer()
        {
            InitializeComponent();
            
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

        public Manufacturer(string IdAcount, string Email, string Pass, string Permission)
        {
            InitializeComponent();

            this.IdAcount = IdAcount;
            this.Email = Email;
            this.Pass = Pass;
            this.Permission = Permission;

        }
        private void ShowManufacturer()
        {
            Con.Open();
            string Query = "Select * from ManufacturerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ManufacturerDGV.DataSource = dt;
           // Manufacturer obj = new Manufacturer(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString());
            Con.Close();
        }

        private void Reset()
        {
            ManNameTb.Text = "";
            ManPhoneTb.Text = "";
            ManAddTb.Text = "";
            Key = 0;
        }
        

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
           if(Permission == "Admin")
            {
                if (ManAddTb.Text == "" || ManPhoneTb.Text == "" || ManNameTb.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin !");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into ManufacturerTbl(ManName,ManAdd,ManPhone,ManJDate)values(@MN,@MA,@MP,@MJD)", Con);
                        cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                        cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                        cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@MJD", ManJDate.Value.Date);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã lưu ");
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

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
           if(Permission == "Admin")
            {
                if (ManAddTb.Text == "" || ManPhoneTb.Text == "" || ManNameTb.Text == "")
                {
                    MessageBox.Show("Thiếu thông tin !");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Update ManufacturerTbl set ManName = @MN, ManAdd= @MA,ManPhone = @MP,ManJDate = @MJD where ManId = @MKey", Con);
                        cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                        cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                        cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@MJD", ManJDate.Value.Date);
                        cmd.Parameters.AddWithValue("@MKey", Key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã Sửa ");
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

        private void Find()
        {         
                Con.Open();
                string Query = "Select * from ManufacturerTbl where ManName like '%" + FindTb.Text.Trim() + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ManufacturerDGV.DataSource = dt;
                Con.Close();         

        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            if(Permission == "Admin")
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
                        SqlCommand cmd = new SqlCommand("Delete from ManufacturerTbl where ManId = @MKey", Con);
                        cmd.Parameters.AddWithValue("@MKey", Key);

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
                MessageBox.Show("Bạn không có quyền này !");
            }
        }

        int Key = 0;
        private void ManufacturerDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.ManufacturerDGV.Rows[e.RowIndex];
                ManNameTb.Text = ManufacturerDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                ManAddTb.Text = ManufacturerDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                ManPhoneTb.Text = ManufacturerDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                ManJDate.Text = ManufacturerDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            if (ManNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ManufacturerDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
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

        private void guna2CustomGradientPanel18_Click_1(object sender, EventArgs e)
        {
            Seller obj = new Seller();
            obj.Show();
            this.Hide();

        }

        private void guna2PictureBox34_Click_1(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();

        }

        private void guna2PictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FindTb_TextChanged(object sender, EventArgs e)
        {
            Find();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            ShowManufacturer();
        }

        private void ManNameTb_TextChanged(object sender, EventArgs e)
        {
            if (ManNameTb.Text == "")
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

        private void Manufacturer_Load(object sender, EventArgs e)
        {
            SaveBtn.Enabled = false;
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
            guna2GradientButton2.Enabled = false;
        }

        private void EditBtn_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
