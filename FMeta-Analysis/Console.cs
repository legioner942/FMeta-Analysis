using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMeta_Analysis
{
    public partial class Console : Form
    {
        Core core;

        public Console()
        {
            InitializeComponent();
            core = new Core();
            logTextBox.Text = "";
        }

        private void inpCons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string inp = inpCons.Text;

            if (inp == "test") Globals.ThisAddIn.setCell("23", 4, 3);
            else if (inp == "test2")
            {
                MessageBox.Show(Globals.ThisAddIn.getCell(4, 3));
            }
            else if (inp=="test3")
            {
                List<string> buf = null;
                buf = Globals.ThisAddIn.getRow(1, 3);
                foreach (var item in buf)
                {
                    logTextBox.Text += item;
                }
            }
            else if (inp=="test4")
            {
                Globals.ThisAddIn.clearRow(1);
            }
            else if (inp == "test6")
            {
                Globals.ThisAddIn.clearCol(3);
            }
            else
            {
                logTextBox.Text += inp + " result: ";

                try
                {
                    core.Compiler(inp);
                    logTextBox.Text += "Succesfull" + "\r\n";
                }
                catch (Exception ex)
                {
                    logTextBox.Text += ex.Message + "\r\n";
                }
            }
        }

        public void setStatus(bool flag)
        {
            string status = "";
            status = flag ? "Init" : "No init";
            toolStripStatusLabel1.Text = status;
            toolStripStatusLabel1.ForeColor = flag ? Color.Green : Color.Red;
        }
    }
}
