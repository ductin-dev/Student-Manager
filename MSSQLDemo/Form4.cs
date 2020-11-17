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
    public partial class Form4 : Form
    {
        public string DeleteId { get; set; }
        public int status { get; set; } = 0;
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(List<Student> std)
        {
            InitializeComponent();
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
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
            String[] temp0 = comboBox2.SelectedItem.ToString().Split(']');
            DeleteId = temp0[0].Replace("[", "");
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
