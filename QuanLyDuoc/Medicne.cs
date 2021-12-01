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
    public partial class Medicne : Form
    {
        public Medicne()
        {
            InitializeComponent();
            ShowMedicine();
            GetManufacturer();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

           
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");


        private void ShowMedicine()
        {
            Con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            MedNameTb.Text = "";
            MedPriceTb.Text = "";
            MedManTb.Text = "";
            MedQtyTb.Text = "";
            MedTypeCb.SelectedIndex = 0;
            Key = 0;
        }

        private void GetManName()
        {
            Con.Open();
            string Query = "Select  * from ManufacturerTbl where ManId= '" + MedManCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                MedManTb.Text = dr["ManName"].ToString();
            }
            Con.Close();
        }


        private void GetManufacturer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select ManId from ManufacturerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ManId", typeof(int));
            dt.Load(Rdr);
            MedManCb.ValueMember = "ManId";
            MedManCb.DataSource = dt;
            Con.Close();
        }



       

        

      
        

       

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            if (MedNameTb.Text == "" || MedPriceTb.Text == "" || MedQtyTb.Text == "" || MedTypeCb.SelectedIndex == -1 || MedManTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MedicineTbl(MedName,MedType,MedQty,MedPrice,MedManId,MedManufact)values(@MN,@MT,@MQ,@MP,@MMI,@MM)", Con);
                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCb.SelectedValue.ToString()); ;
                    cmd.Parameters.AddWithValue("@MM", MedManTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    ShowMedicine();
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
            if (MedNameTb.Text == "" || MedPriceTb.Text == "" || MedQtyTb.Text == "" || MedTypeCb.SelectedIndex == -1 || MedManTb.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedName = @MN ,MedType = @MT ,MedQty = @MQ ,MedPrice =  @MP ,MedManId =@MMI ,MedManufact = @MM where MedNum = @MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCb.SelectedValue.ToString()); ;
                    cmd.Parameters.AddWithValue("@MM", MedManTb.Text);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    ShowMedicine();
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
                    SqlCommand cmd = new SqlCommand("Delete from MedicineTbl where MedNum = @MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa");
                    Con.Close();
                    ShowMedicine();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key = 0;
        private void MedDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.MedDGV.Rows[e.RowIndex];
                MedNameTb.Text = MedDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                MedTypeCb.SelectedItem = MedDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
                MedQtyTb.Text = MedDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
                MedPriceTb.Text = MedDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
                MedManCb.SelectedItem = MedDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
                MedManTb.Text = MedDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MedDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

       



       

        private void guna2CustomGradientPanel21_Click_1(object sender, EventArgs e)
        {
            Dashboar obj = new Dashboar();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel19_Click(object sender, EventArgs e)
        {
            Medicne obj = new Medicne();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel18_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel17_Click(object sender, EventArgs e)
        {
            Seller obj = new Seller();
            obj.Show();
            this.Hide();
        }

        private void guna2PictureBox34_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MedManCb_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetManName();
        }
    }
}
