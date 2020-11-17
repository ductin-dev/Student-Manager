using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLDemo
{
    public class Student
    {
        public int Id { get; set; }
        public String Stdcode { get; set; }
        public String Name { get; set; }
        public int Clscode { get; set; }

        public Student(int id, string stdcode, string name, int clscode)
        {
            Id = id;
            Stdcode = stdcode;
            Name = name;
            Clscode = clscode;
        }

        public static DataTable ConvertListToStudent(List<Student> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 4;

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                string[] temp = new string[4];
                temp[0] = array.Id.ToString();
                temp[1] = array.Stdcode;
                temp[2] = array.Name;
                temp[3] = array.Clscode.ToString();
                table.Rows.Add(temp);
            }

            return table;
        }
    }
}
