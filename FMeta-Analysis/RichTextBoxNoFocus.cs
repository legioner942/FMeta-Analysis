using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMeta_Analysis
{
    public class RichTextBoxNoFocus : RichTextBox
    {
        public RichTextBoxNoFocus()
        {
            ReadOnly = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0x7) base.WndProc(ref m);
        }
    };
}
