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

        public Core core;
        public Console console;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            core = new Core();
            console = new Console();
            if (FormState.Default.SaverFolder.Equals("")) FormState.Default.SaverFolder = 
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FMeta";
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            try
            {
                core.save_column_name_toFile("dump");
                FormState.Default.Init_status = false;
                FormState.Default.Log = "";
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }   
        }

        public void setCell(string val, int rowNum, string colNum)
        {
            //colNum = colNum.ToUpper();
            //string pointer = colNum + rowNum;
            //Excel.Range rng = this.Application.Range[pointer];
            //rng.Value2 = val;
            int col = 0;
            try
            {
                col = int.Parse(colNum);
                Application.Cells[rowNum, col] = val;
            }
            catch(FormatException)
            {
                Application.Cells[rowNum, colNum] = val;
            }
        }

        public void setRow(string[] str, int rowNum)
        {
            int len = str.Length;
            string ender;
            Excel.Range rng;

            ender = ((char)(len + 64)).ToString() + rowNum;
            if (len > 1)
            {
                rng = this.Application.Range["A" + rowNum, ender];
                rng.Value = str;
            }
            else if (len == 1)
            {
                setCell(str[0], rowNum, ((char)(len + 64)).ToString());
            }
        }

        public List<string> getRow(int rowNum, string colCount)
        {
            List<string> str = new List<string>();

            Excel.Range rng;
            string ender;

            ender = colCount + rowNum;

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

        public string getCell(int rowNum, string colNum)
        {
            colNum = colNum.ToUpper();
            string pointer = colNum + rowNum;
            Excel.Range rng = Application.Range[pointer];

            return rng.Value2.ToString()==null?"":rng.Value2.ToString();
        }

        public void clearRow(int rowNum, bool delete = false)
        {
            Excel.Range rng = Application.Rows[rowNum, missing];
            rng.Select();

            if (!delete) rng.Clear();            
            else rng.Delete(Excel.XlDirection.xlUp);

            rng = Application.Cells[1,1];
            rng.Select();
        }

        public void clearCol(string colNum, bool delete = false)
        {
            Excel.Range rng;
            try
            {
                int col = int.Parse(colNum);
                rng = Application.Columns[col];
            }
            catch(FormatException)
            {
                colNum = colNum.ToUpper();
                rng = Application.Columns[colNum];
            }
            
            rng.Select();

            if (!delete) rng.Clear();
            else rng.Delete(Excel.XlDirection.xlToLeft);

            rng = Application.Cells[1,1];
            rng.Select();
        }

        public int getRowCountWithData()
        {
            return Application.ActiveSheet.UsedRange.Rows.Count;
        }
        
        public int getColCountWithData()
        {
            return Application.ActiveSheet.UsedRange.Columns.Count;
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
