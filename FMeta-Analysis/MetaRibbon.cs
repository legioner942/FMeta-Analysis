using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace FMeta_Analysis
{
    public partial class MetaRibbon
    {
        private void MetaRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            
        }

        private void CallConsole_Click(object sender, RibbonControlEventArgs e)
        {
            Console f = new Console();
            f.Show();
        }
    }
}
