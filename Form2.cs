using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BetaMartAPP
{
    public partial class Form2 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        Form1 f;

        public Form2(Form1 f)
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\evan\Documents\BetaMartDatabase\dbbetamart.accdb;";
            this.f = f;
        }

        private void Clear()
        {
            txtIdBarang.Clear();
            txtNamaBarang.Clear();
            txtHarga.Clear();
            txtKategori.Clear();
            txtStock.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtIdBarang.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtIdBarang.Text == "" || txtNamaBarang.Text == "" || txtHarga.Text == "" || txtKategori.Text == "" || txtStock.Text == "")
                {
                    MessageBox.Show("REQUIRED MISSING FIELD!", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into Barang (id_barang,nama_barang,harga,kategori,stock) values('" + txtIdBarang.Text + "','" + txtNamaBarang.Text + "','" + txtHarga.Text + "','" + txtKategori.Text + "','" + txtStock.Text + "')";

                command.ExecuteNonQuery();
                MessageBox.Show("NEW DATA HAS BEEN SUCCESSFULLY SAVED.", "MASSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f.LoadRecords();
                Clear();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtIdBarang.Text == "" || txtNamaBarang.Text == "" || txtHarga.Text == "" || txtKategori.Text == "" || txtStock.Text == "")
                {
                    MessageBox.Show("REQUIRED MISSING FIELD!", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("WANT TO UPDATE THIS RECORD?", "MESSAGE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    connection.Open();
                    OleDbCommand command = connection.CreateCommand();
                    string query = "update Barang set nama_barang='" + txtNamaBarang.Text + "' ,harga='" + txtHarga.Text + "' ,kategori='" + txtKategori.Text + "' ,stock='" + txtStock.Text + "'WHERE id_barang='" + txtIdBarang.Text + "'";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("NEW DATA HAS BEEN SUCCESSFULLY UPDATED.", "MASSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadRecords();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}