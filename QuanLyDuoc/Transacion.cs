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
    public partial class Transacion : Form
    {
        string Permission = Login.info.Permission;
        public Transacion()
        {
            InitializeComponent();
            ShowSeller();
            ShowBill();
            SNameLbl.Text = Login.User;
            GetCustomer();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

        private void GetCustName()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl where CustNum= '" + CustIDCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }


        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustNum from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(Rdr);
            CustIDCb.ValueMember = "CustNum";
            CustIDCb.DataSource = dt;
            Con.Close();
        }

        private void ShowSeller()
        {
            Con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MedicinesDGV.DataSource = dt;
            Con.Close();
        }

        private void ShowBill()
        {
            Con.Open();
            string Query = "Select * from BillTbl where SName = '" + SNameLbl.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GiaodichDGV.DataSource = dt;
            Con.Close();
        }

        

        private void UpdateQuaty()
        {
            try
            {
                int newQty = Stock - Convert.ToInt32(MedQtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedQty = @MQ  where MedNum = @MKey", Con);
                cmd.Parameters.AddWithValue("@MQ", newQty);
                cmd.Parameters.AddWithValue("@MKey", Key);
                cmd.ExecuteNonQuery();
                Con.Close();
                ShowSeller();
                //Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void insertBill()
        {
            if (CustNameTb.Text == "")
            {
                MessageBox.Show("Thiếu thông tin !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(SName, CustNum, CustName,BDate,BAmount)values(@SN,@CN,@CNA,@BD,@BA)", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameLbl.Text);
                    cmd.Parameters.AddWithValue("@CN", CustIDCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CNA", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@BA", GridTotal);
                    cmd.ExecuteNonQuery();

                    Con.Close();
                    ShowBill();
                    // Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2GradientButton1_Click_1(object sender, EventArgs e)
        {
           
                insertBill();
        }

        private void MedicinesDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.MedicinesDGV.Rows[e.RowIndex];
                MedNameTb.Text = MedicinesDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                Stock = Convert.ToInt32(MedicinesDGV.Rows[e.RowIndex].Cells[3].Value.ToString());
                MedPriceTb.Text = MedicinesDGV.Rows[e.RowIndex].Cells[4].Value.ToString();

            }
            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MedicinesDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void CustIDCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
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

        private void guna2CustomGradientPanel19_Click(object sender, EventArgs e)
        {
            if (Permission == "Admin" || Permission == "Quản Kho")
            {
                Customer obj = new Customer();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Không thể truy cập !");
            }
        }

        private void guna2CustomGradientPanel18_Click(object sender, EventArgs e)
        {
            if (Permission == "Admin" || Permission == "Quản Kho")
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

      

        private void FindTb_TextChanged(object sender, EventArgs e)
        {
            Find();
        }

        private void MedQtyTb_TextChanged(object sender, EventArgs e)
        {
            if(MedQtyTb.Text == "")
            {
                guna2GradientButton1.Enabled = false;
                AddBillBtn.Enabled = false;
            }
            else
            {
                guna2GradientButton1.Enabled = true;
                AddBillBtn.Enabled = true;
            }
        }

        private void Transacion_Load(object sender, EventArgs e)
        {
            CustIDCb.SelectedIndex = 0;
            guna2GradientButton1.Enabled = false;
            AddBillBtn.Enabled = false;
            guna2GradientButton1.Enabled = false;
            if(Permission == "Admin" || Permission == "Thu Ngân")
            {
                guna2GradientButton1.Enabled = true;
            }
            else
            {
                guna2GradientButton1.Enabled = false;
            }
        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Find()
        {
            Con.Open();
            string Query = "Select * from MedicineTbl where MedName like '%" + FindTb.Text.Trim() + "%'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MedicinesDGV.DataSource = dt;
            Con.Close();
        }

        int n = 0, GridTotal = 0;


        int Key = 0, Stock;

        private void AddBillBtn_Click(object sender, EventArgs e)
        {
            if(Permission == "Admin" || Permission == "Thu Ngân")
            {
                if (MedQtyTb.Text == "" || Convert.ToInt32(MedQtyTb.Text) > Stock)
                {
                    MessageBox.Show("Vui lòng nhập lại số lượng !");
                }
                else
                {
                    int total = Convert.ToInt32(MedQtyTb.Text) * Convert.ToInt32(MedPriceTb.Text);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(BillDGV);
                    newRow.Cells[0].Value = n + 1;
                    newRow.Cells[1].Value = MedNameTb.Text;
                    newRow.Cells[2].Value = MedQtyTb.Text;
                    newRow.Cells[3].Value = MedPriceTb.Text;
                    newRow.Cells[4].Value = total;
                    BillDGV.Rows.Add(newRow);
                    //BillDGV.Rows.Remove(newRow);
                    GridTotal += total;
                    BillLbl.Text = "Thành tiền " + GridTotal;
                    n++;
                    UpdateQuaty();

                }
            }
            else
            {
                MessageBox.Show("Bạn không có quyền này !");
            }
        }
    }
}
