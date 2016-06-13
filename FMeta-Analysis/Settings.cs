using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FMeta_Analysis
{
    public partial class Settings : Form
    {
        private static Color deffColor = Color.Black;
        private static Color defcColor = Color.Red;
        private static int defInterval = 5;
        private static string defSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FMeta";

        public Color fColor, cColor;
        public int interval;

        private void foreColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                foreColor.BackColor = colorDialog1.Color;
            }
        }

        private void commandColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                commandColor.BackColor = colorDialog1.Color;
            }
        }

        private void accept_Click(object sender, EventArgs e)
        {
            fColor = foreColor.BackColor;
            cColor = commandColor.BackColor;
            interval = (int)autosaveInterval.Value;
            Close();
        }

        private void restoreDef_Click(object sender, EventArgs e)
        {
            foreColor.BackColor = deffColor;
            commandColor.BackColor = defcColor;
            autosaveInterval.Value = defInterval;
            savePath.Text = defSavePath;
        }

        private void savePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!dialog.SelectedPath.Equals(""))
                { 
                    FormState.Default.SaverFolder = dialog.SelectedPath;
                    savePath.Text = dialog.SelectedPath.ToString();
                }
                else MessageShower.ShowError("Невозможно установить такой путь!");
            }
        }

        public Settings()
        {
            InitializeComponent();

            autosaveInterval.Value = FormState.Default.timeAutosave;
            foreColor.BackColor = FormState.Default.foreColor;
            commandColor.BackColor = FormState.Default.commandColor;
            savePath.Text = FormState.Default.SaverFolder;
        }
    }
}
