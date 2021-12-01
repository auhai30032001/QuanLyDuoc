﻿using System;
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
        public RePass()
        {
            InitializeComponent();
            ShowAcount();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=.\MSSQL_EXP_2008R2;Initial Catalog=HealthCare;Integrated Security=True");

        private void ShowAcount()
        {
            Con.Open();
            string Query = "Select IdAcount, Email from Acount";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AcountDGV.DataSource = dt;
            Con.Close();
        }
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

       

        bool a = true;
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (RePassTb.Text == "" || REmailTb.Text == "" || ConfirmPassTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin !");
            }
          
            else
            {              
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Acount where Email='" + REmailTb.Text + "' and Password='" + RePassTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                       
                        SqlCommand cmd = new SqlCommand("Update Acount set Email = @EM, Password= @PA where IdAcount = @AKey", Con);
                        cmd.Parameters.AddWithValue("@EM", REmailTb.Text);
                        cmd.Parameters.AddWithValue("@PA", ConfirmPassTb.Text);
                        cmd.Parameters.AddWithValue("@AKey", Key);
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
        private void AcountDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.AcountDGV.Rows[e.RowIndex];
                REmailTb.Text = AcountDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
                
            }
            if (REmailTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AcountDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
    }
}



/*
Con.Open();
SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Acount where Email='" + UNameTb.Text + "' and Password='" + PassTb.Text + "'", Con);
DataTable dt = new DataTable();
sda.Fill(dt);
if (dt.Rows[0][0].ToString() == "1")
{
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
Con.Close();*/