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

namespace MSSQLDemo
{
    public partial class Form1 : Form
    {
        private SqlConnection conn = null;
        private static string ConnectString = "database=University; Server=(local); User id=fpt; password=fpt";

        private List<Student> students= new List<Student>();
        private List<Classes> classes = new List<Classes>();

        public Form1()
        {
            InitializeComponent();
        }

        private void GetAllData()
        {
            //Clear
            students.Clear();
            classes.Clear();

            //Init
            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand command1 = new SqlCommand();
            command1.CommandType = CommandType.Text;
            command1.Connection = conn;

            //Get Student
            command1.CommandText = "Select * From Student";
            SqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                int id = reader1.GetInt32(0);
                String stdcode = reader1.GetString(1);
                String name = reader1.GetString(2);
                int clscode = reader1.GetInt32(3);
                students.Add(new Student(id, stdcode, name, clscode));
            }
            conn.Close();

            //Get Classes
            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand command2 = new SqlCommand();
            command2.CommandType = CommandType.Text;
            command2.Connection = conn;
            command2.CommandText = "Select * From Class";
            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                int id = reader2.GetInt32(0);
                String code = reader2.GetString(1);
                String name = reader2.GetString(2);
                String date = reader2.GetDateTime(3).ToString();
                classes.Add(new Classes(id, code, name,date));
            }
            conn.Close();
        }

        private void GetStudentByClass(int clscode)
        {
            //Clear
            students.Clear();

            //Init
            if (conn.State!=ConnectionState.Open) conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = conn;

            //Get Student
            command.CommandText = "Select * From Student where clscode="+clscode;
            SqlDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                int id = reader1.GetInt32(0);
                String stdcode = reader1.GetString(1);
                String name = reader1.GetString(2);
                int clscode1 = reader1.GetInt32(3);
                students.Add(new Student(id, stdcode, name, clscode1));
            }
            conn.Close();
        }

        private void DeleteStudentById(int id)
        {
            //Init
            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = conn;

            //Get Student
            command.CommandText = "delete From Student where id=" + id;
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void AddStudent(Student a)
        {
            //Init
            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = conn;

            //Get Student
            command.CommandText = "insert into Student (stdcode,name,clscode)" +
                " values ('"+a.Stdcode+"','"+a.Name+"','"+a.Clscode+"')";
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void UpdateStudent(Student a)
        {
            //Init
            if (conn.State != ConnectionState.Open) conn.Open();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = conn;

            //Get Student
            command.CommandText = "update Student set" +
                " stdcode='" + a.Stdcode + "'," +
                " name='" + a.Name + "'," +
                " clscode=" + a.Clscode +" " +
                " WHERE id="+a.Id;
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //Info and Connect
                label3.Text = "Pedding...";
                label3.ForeColor = Color.Orange;
                conn=new SqlConnection(ConnectString);

                //Get All Data
                GetAllData();
                listBox2.Items.Clear();
                foreach (Classes i in classes)
                {
                    listBox2.Items.Add("["+i.Id+"] "+i.Code+" - "+i.Name);
                }
                listBox2.SelectedIndex = 0;

                //Show List Student By Class
                GetStudentByClass(classes.First().Id);
                dataGridView1.DataSource = null;
                DataTable table = Student.ConvertListToStudent(students);
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].HeaderText = "Id";
                dataGridView1.Columns[1].HeaderText = "Student Code";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Class Id";

                //Info
                label3.Text = "Connected !";
                label3.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                label3.Text = "Error, check your connection !";
                label3.ForeColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Info
                label3.Text = "Pedding...";
                label3.ForeColor = Color.Orange;
                if (conn != null)
                {
                    conn = null;
                }

                //Remove
                listBox2.Items.Clear();
                dataGridView1.DataSource = null;

                //Info
                label3.Text = "Not yet connected ";
                label3.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                label3.Text = "Error, details: " + ex.ToString();
                label3.ForeColor = Color.Red;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Show List Student By Class
            String[] temp = listBox2.SelectedItem.ToString().Split(']');
            GetStudentByClass(int.Parse(temp[0].Replace("[","")));
            dataGridView1.DataSource = null;
            DataTable table = Student.ConvertListToStudent(students);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].HeaderText = "Id";
            dataGridView1.Columns[1].HeaderText = "Student Code";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Class Id";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //Info and Connect
                label3.Text = "Quering...";
                label3.ForeColor = Color.Orange;
                if (conn == null) throw new Exception();

                //Delete
                Form4 frm4 = new Form4(students);
                frm4.ShowDialog();

                if (frm4.status == 0)
                {
                    //Canceled
                    label3.Text = "Connected !";
                    label3.ForeColor = Color.Green;
                    return;
                }
                DeleteStudentById(int.Parse(frm4.DeleteId));

                //UpdateView
                button4_Click(sender, e);
            }
            catch (Exception ex)
            {
                //Error
                label3.Text = "Error, Connect Again !";
                label3.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Info and Connect
                label3.Text = "Quering...";
                label3.ForeColor = Color.Orange;
                if (conn == null) throw new Exception();

                //Add
                Form2 frm2 = new Form2(classes);
                frm2.ShowDialog();

                if (frm2.status == 0)
                {
                    //Canceled
                    label3.Text = "Connected !";
                    label3.ForeColor = Color.Green;
                    return;
                }
                AddStudent(new Student(0, frm2.Code, frm2.Name, int.Parse(frm2.Id)));

                //UpdateView
                button4_Click(sender, e);
            }
            catch (Exception ex)
            {
                //Error
                label3.Text = "Error, Connect Again !";
                label3.ForeColor = Color.Red;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //Info and Connect
                label3.Text = "Quering...";
                label3.ForeColor = Color.Orange;
                if (conn == null) throw new Exception();

                //Add
                Form3 frm3 = new Form3(students,classes);
                frm3.ShowDialog();

                if (frm3.status == 0)
                {
                    //Canceled
                    label3.Text = "Connected !";
                    label3.ForeColor = Color.Green;
                    return;
                }
                UpdateStudent(new Student(int.Parse(frm3.UpdateId), frm3.Code, frm3.Name, int.Parse(frm3.Id)));

                //UpdateView
                button4_Click(sender, e);
            }
            catch (Exception ex)
            {
                //Error
                label3.Text = "Error, Connect Again !";
                label3.ForeColor = Color.Red;
            }
        }
    }
}
