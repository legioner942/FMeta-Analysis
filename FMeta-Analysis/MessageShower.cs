using System.Windows.Forms;

namespace FMeta_Analysis
{
    public static class MessageShower
    {
        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        public static DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public static void Debug(string message)
        {
            MessageBox.Show(message);
        }
    }
}
