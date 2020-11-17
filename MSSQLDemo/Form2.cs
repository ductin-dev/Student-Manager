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
    public partial class Form2 : Form
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Id { get; set; }

        public int status { get; set; }=0;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(List<Classes> classes)
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (Classes i in classes)
            {
                comboBox1.Items.Add("["+i.Id+"] "+i.Code);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                status = 1;
                Name = textBox1.Text;
                Code = textBox2.Text;
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
    }
}
