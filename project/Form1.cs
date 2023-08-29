using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace project
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            load();
        }

        //SqlConnection conn = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=emp;Integrated Security=True");
        SqlConnection conn = new SqlConnection("Data Source=shareefemployeedb.database.windows.net;Initial Catalog=employeedb;User ID=shareef;Password=password;Connect Timeout=30;Encrypt=True");
        SqlCommand cmd;
        SqlDataReader read;
        SqlDataAdapter drr;
        String empid;
        bool Mode = true;
        String sql;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            String name = t2.Text;
            String dept = t3.Text;
            String sal = t4.Text;

            if (Mode == true)
            {
                sql = "insert into empq(empname,empdept,empsalary) values(@empname,@empdept,@empsalary)";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                 
                cmd.Parameters.AddWithValue("@empname", name);  
                cmd.Parameters.AddWithValue("@empdept", dept);
                cmd.Parameters.AddWithValue("@empsalary", sal);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();

                t2.Clear();
                t3.Clear();
                t4.Clear();

                t2.Focus();
                button1.Text = "Save";

            }
            else
            {
                empid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update empq set empname='"+name+"', empdept='"+dept+"', empsalary='"+sal+"' where empid ='"+empid+"'";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.Parameters.AddWithValue("@empname", name);
                cmd.Parameters.AddWithValue("@empdept", dept);
                cmd.Parameters.AddWithValue("@empsalary", sal);
                MessageBox.Show("Record Updated");
                cmd.ExecuteNonQuery();

                
                t2.Clear();
                t3.Clear();
                t4.Clear();

                t2.Focus();
                button1.Text = "Save";
                Mode = true;
            }
            conn.Close();
        }
        public void load()
        {
            try
            {
                sql = "select * from empq";
                cmd=new SqlCommand(sql, conn);
                conn.Open();

                read = cmd.ExecuteReader();
                //drr=new SqlDataAdapter(sql, conn);
                dataGridView1.Rows.Clear();
                while(read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                conn.Close ();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void getID(string empid)
        {
            sql = "select * from empq where empid='"+empid+"'";
            cmd=new SqlCommand(sql, conn);
            conn.Open();
            read = cmd.ExecuteReader();
            while(read.Read())
            {
                
                t2.Text = read[1].ToString();
                t3.Text = read[2].ToString();
                t4.Text = read[3].ToString();
            }
            conn.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dataGridView1.Columns["Column5"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                empid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(empid);
                button1.Text = "Edit";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Column6"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                empid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from empq where empid='"+empid+"'";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Delete");
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t2.Clear();
            t3.Clear();
            t4.Clear();

            t2.Focus();
            button1.Text = "Save";
            Mode = true;
        }
    }
}
