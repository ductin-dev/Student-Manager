using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSSQLDemo
{
    public partial class Form3 : Form
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }
        public string UpdateId { get; set; }
        public int status { get; set; } = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private List<Student> stdList;
        private List<Classes> clsList;

        public Form3(List<Student> std,List<Classes> classes)
        {
            stdList = std;
            clsList = classes;
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (Classes i in classes)
            {
                comboBox1.Items.Add("[" + i.Id + "] " + i.Code);
            }
            comboBox1.SelectedIndex = 0;
            foreach (Student i in std)
            {
                comboBox2.Items.Add("[" + i.Id + "] " + i.Name);
            }
            comboBox2.SelectedIndex = 0;
   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
                status = 1;
                Name = textBox1.Text;
                Code = textBox2.Text;
                String[] temp0 = comboBox2.SelectedItem.ToString().Split(']');
                UpdateId = temp0[0].Replace("[", "");
                String[] temp = comboBox1.SelectedItem.ToString().Split(']');
                Id = temp[0].Replace("[", "");
                this.Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            status = 0;
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
                textBox1.Text = stdList[comboBox2.SelectedIndex].Stdcode;
                textBox2.Text = stdList[comboBox2.SelectedIndex].Name;
                int temp= stdList[comboBox2.SelectedIndex].Clscode;
            foreach (Classes i in clsList)
            {
                if (i.Id == temp)
                {
                    comboBox1.SelectedIndex = clsList.IndexOf(i);
                    break;
                }

            }
        }
    }
}
