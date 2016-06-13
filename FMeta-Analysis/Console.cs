using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace FMeta_Analysis
{

    public partial class Console : Form
    {
        string lastEnter = "";

        public Console()
        {
            InitializeComponent();
            statusLabel.Text = "";
            logTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            timerStart();
        }

        private List<string> getListFile()
        {
            List<string> list = new List<string>();
            string path = FormState.Default.SaverFolder;
            string[] buf = Directory.GetFiles(path, "*.json");

            foreach (var item in buf)
            {
                list.Add(item.Remove(item.Length-5).Remove(0, item.LastIndexOf('\\')+1));
            }

            return list;
        }

        private void inpCons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string inp = lastEnter = inpCons.Text;

                if (inp == "test") Globals.ThisAddIn.setCell("23", 4, "3");
                else if (inp == "test2")
                {
                    MessageBox.Show(Globals.ThisAddIn.getCell(4, "C"));
                }
                else if (inp == "test22")
                {
                    string[] test = { "1", "2" };
                    Globals.ThisAddIn.setRow(test, 2);
                }
                else if (inp == "test3")
                {
                    List<string> buf = null;
                    buf = Globals.ThisAddIn.getRow(1, "C");
                    foreach (var item in buf)
                    {
                        logTextBox.Text += item;
                    }
                }
                else if (inp == "test4")
                {
                    Globals.ThisAddIn.clearRow(1);
                }
                else if (inp == "test6")
                {
                    Globals.ThisAddIn.clearCol("1");
                }
                else if (inp == "test7")
                {
                    MessageBox.Show(Globals.ThisAddIn.Application.Path);
                }
                else if (inp == "test8")
                {
                    Globals.ThisAddIn.core.save_column_name_toFile("test");
                }
                else if (inp == "test9")
                {
                    Globals.ThisAddIn.core.load_from_file("test");
                }
                else if (inp == "test10")
                {
                    int a = Globals.ThisAddIn.getRowCountWithData();
                    MessageShower.Debug(a.ToString());
                }
                else if (inp == "test11")
                {
                    int a = Globals.ThisAddIn.getColCountWithData();
                    MessageShower.Debug(a.ToString());
                }
                else if (inp == "clear")
                {
                    logTextBox.Clear();
                }
                else if (inp == "list")
                {
                    List<string> list = getListFile();
                    logTextBox.Text += "Список сохраненных файлов настроек:";
                    foreach (var item in list)
                    {
                        logTextBox.Text += "\r\n" + item;
                    }
                    logTextBox.Text += "\r\n";
                }
                else if (!inp.Equals(string.Empty))
                {
                    logTextBox.Text += inp + " result: ";

                    try
                    {
                        Globals.ThisAddIn.core.Compiler(inp);
                        logTextBox.Text += "Succesfull" + "\r\n";
                    }
                    catch (Exception ex)
                    {
                        logTextBox.Text += ex.Message + "\r\n";
                    }  
                }
                inpCons.Clear();
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                inpCons.Text = lastEnter;
                inpCons.SelectionStart = logTextBox.Text.Length;
            }
            else return;
        }

        public void setStatus(bool flag)
        {
            string status = "";
            status = flag ? "выполнена" : "не выполнена";
            statusLabel.Text = status;
            statusLabel.BackColor = flag ? Color.Green : Color.Red;
        }

        private void Console_Load(object sender, EventArgs e)
        {
            logTextBox.Text = FormState.Default.Log;
            setStatus(FormState.Default.Init_status);
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormState.Default.Log = logTextBox.Text;
            FormState.Default.Init_status = statusLabel.Text == "Init complete" ? true : false;
        }

        private void logTextBox_TextChanged(object sender, EventArgs e)
        {
            var currentSelStart = logTextBox.SelectionStart;
            var currentSelLength = logTextBox.SelectionLength;
            string buf = "";

            logTextBox.Select(currentSelStart, logTextBox.Text.Length - currentSelStart);
            logTextBox.SelectionColor = FormState.Default.foreColor;
            buf = logTextBox.SelectedText;

            foreach (var item in Core.reduction)
            {
                var matches = Regex.Matches(buf, @"\b" + @item + @"\b");
                foreach (var match in matches.Cast<Match>())
                {
                    logTextBox.Select(match.Index + currentSelStart, match.Length);
                    logTextBox.SelectionColor = FormState.Default.commandColor;
                }
            }

            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }
        
        private void inpCons_TextChanged(object sender, EventArgs e)
        {
            var currentSelStart = inpCons.SelectionStart;
            var currentSelLength = inpCons.SelectionLength;

            inpCons.SelectAll();
            inpCons.SelectionColor = FormState.Default.foreColor;

            foreach (var item in Core.reduction)
            {
                var matches = Regex.Matches(inpCons.Text, @"\b"+@item+@"\b");
                foreach (var match in matches.Cast<Match>())
                {
                    inpCons.Select(match.Index, match.Length);
                    inpCons.SelectionColor = FormState.Default.commandColor;
                }
            }

            inpCons.Select(currentSelStart, currentSelLength);
            inpCons.SelectionColor = FormState.Default.foreColor;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FMeta. Все права сохранены. Сделано legioner942", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "FMeta.chm", HelpNavigator.TableOfContents);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timerStart()
        {
            if (FormState.Default.timeAutosave == 0)
            {
                Autosave.Stop();
                autoSaveStat.Text = "";
                return;
            }
            Autosave.Interval = FormState.Default.timeAutosave * 60000;
            //Autosave.Interval = 10000;
            Autosave.Start();
        }

        private void Autosave_Tick(object sender, EventArgs e)
        {
            Globals.ThisAddIn.core.save_column_name_toFile("dump");            
            autoSaveStat.Text = "Последнее автосохранение: " + DateTime.Now.ToString("HH:mm:ss");
            timerStart();
        }

        private void настройкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            if (set.ShowDialog() == DialogResult.OK)
            {
                FormState.Default.commandColor = set.cColor;
                FormState.Default.foreColor = set.fColor;
                FormState.Default.timeAutosave = set.interval;

                timerStart();
            }
        }

        private void именаИСокращенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reductions red = new Reductions();
            red.Show();
        }
    }
}
