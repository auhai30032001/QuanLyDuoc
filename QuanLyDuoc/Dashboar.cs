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
    public partial class Dashboar : Form
    {
        public Dashboar()
        {
            InitializeComponent();
            CountMed();
            CountSell();
            CountCust();
            SumAmount();
            GetSeller();
            GetSBesteller();
            GetSBestCust();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

        private void CountMed()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from MedicineTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MedNum.Text = "Tổng loại:" + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void CountSell()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from SellerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellerLbl.Text = "Số nhân viên:" + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void CountCust()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from CustomerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustLbl.Text = "Số khách: " + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void SumAmount()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(BAmount) from BillTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellAmountLbl.Text = "Tổng thu :" + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void SumAmountBySeller()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(BAmount) from BillTbl where SName='" + getSellcb.SelectedValue.ToString() + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellByAmountLbl.Text = "Tổng thu :" + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void GetSeller()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select SName from SellerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName", typeof(string));
            dt.Load(Rdr);
            getSellcb.ValueMember = "SName";
            getSellcb.DataSource = dt;
            Con.Close();
        }

        private void GetSBesteller()
        {
            try
            {
                Con.Open();
                string innerQuery = "Select Max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(innerQuery, Con);
                sda1.Fill(dt1);
                string Query = "Select SName from BillTbl where BAmount='" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SellBest.Text = dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch (Exception ex)
            {
                Con.Close();
            }
        }

        private void GetSBestCust()
        {
            try
            {
                Con.Open();
                string innerQuery = "Select Max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(innerQuery, Con);
                sda1.Fill(dt1);
                string Query = "Select CustName from BillTbl where BAmount='" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                CustBest.Text = dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch (Exception ex)
            {
                Con.Close();
            }
        }

       
        private void guna2CustomGradientPanel19_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel18_Click(object sender, EventArgs e)
        {
            Seller obj = new Seller();
            obj.Show();
            this.Hide();
        }

        private void guna2CustomGradientPanel17_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Hide();
        }

        private void guna2PictureBox34_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

       

       

       

        private void guna2PictureBox37_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2PictureBox36_Click(object sender, EventArgs e)
        {
            Transacion obj = new Transacion();
            obj.Show();
        }

        private void getSellcb_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            SumAmountBySeller();
        }
    }
}
