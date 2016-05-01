using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace FMeta_Analysis
{
    public partial class ThisAddIn
    {
        
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //this.Application.ActiveWorkbook.ActiveSheet.Cells[1, 1] = 12;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {

        }

        public void setCell(string val, int colNam, int rowNum)
        {
            string pointer = ((char)(colNam + 64)).ToString() + rowNum;
            Excel.Range rng = this.Application.Range[pointer];

            rng.Value2 = val;
        }

        public void setRow(string[] str, int rowNum)
        {
            int len = str.Length;
            string ender;
            Excel.Range rng;

            ender = ((char)(len + 64)).ToString()+rowNum;

            //System.Windows.Forms.MessageBox.Show(ender);

            if (len > 1)
            {
                rng = this.Application.Range["A"+rowNum, ender];
                rng.Value = str;
            }
            else if (len == 1)
            {
                setCell(str[0], 1, rowNum);
            }
        }

        public List<string> getRow(int rowNum, int colCount)
        {
            List<string> str = new List<string>();

            Excel.Range rng;
            string ender;

            ender = ((char)(colCount + 64)).ToString() + rowNum;

            if (colCount == 0)
            {
                throw new Exception("Передан 0 в качестве числа столбцов");
            }

            if (rowNum == 0)
            {
                throw new Exception("Передан 0 в качестве номера строки");
            }

            rng = Application.Range["A" + rowNum, ender];

            foreach (var item in rng.Value)
            {
                //System.Windows.Forms.MessageBox.Show(item.GetType().ToString());
                str.Add(item);
            } 
            
            return str;
        }

        public string getCell(int colNam, int rowNum)
        {
            string pointer = ((char)(colNam + 64)).ToString() + rowNum;
            Excel.Range rng = this.Application.Range[pointer];

            return rng.Value2.ToString();
        }

        #region Код, автоматически созданный VSTO

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
