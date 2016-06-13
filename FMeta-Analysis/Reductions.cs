using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FMeta_Analysis
{
    /// <summary>
    /// TODO: Проверить работу, немного подправить визуальную часть
    /// TODO: Надеяться, что с R получится быстро все сделать :D
    /// </summary>

    public partial class Reductions : Form
    {
        int currentNRow, currentNCol;
        bool Load = false;

        public Reductions()
        {
            InitializeComponent();
            var l1 = Globals.ThisAddIn.getColCountWithData();
            MessageShower.Debug(l1.ToString());
            
        }

        private void save_Click(object sender, EventArgs e)
        {
            
        }

        private void tableColumn_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tableColumn_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            currentNCol = tableColumn.SelectedCells[0].ColumnIndex;
            currentNRow = tableColumn.SelectedCells[0].RowIndex;
            //MessageShower.Debug(currentNRow + " " + currentNCol);
        }

        private void tableUpdate()
        {
            Load = false;
            list.Clear();
            tableColumn.Rows.Clear();
            list = Globals.ThisAddIn.core.getListCol();
            foreach (var item in list)
            {
                tableColumn.Rows.Add(item.Name, item.Nick);
            }
            Load = true;    
        }
    }
}
