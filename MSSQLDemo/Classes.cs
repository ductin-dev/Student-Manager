using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLDemo
{
    public class Classes
    {
        public int Id { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }
        public String Date { get; set; }

        public Classes(int id, string code, string name, string date)
        {
            Id = id;
            Code = code;
            Name = name;
            Date = date;
        }

        public static DataTable ConvertListToClasses(List<Classes> list)
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
                temp[1] = array.Code;
                temp[2] = array.Name;
                temp[3] = array.Date;
                table.Rows.Add(temp);
            }

            return table;
        }
    }
}
