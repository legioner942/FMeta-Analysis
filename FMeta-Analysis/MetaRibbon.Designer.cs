namespace FMeta_Analysis
{
    partial class MetaRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MetaRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.MetaTab = this.Factory.CreateRibbonTab();
            this.mainControlGroup = this.Factory.CreateRibbonGroup();
            this.CallConsole = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.MetaTab.SuspendLayout();
            this.mainControlGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // MetaTab
            // 
            this.MetaTab.Groups.Add(this.mainControlGroup);
            this.MetaTab.Label = "Мета-анализ";
            this.MetaTab.Name = "MetaTab";
            // 
            // mainControlGroup
            // 
            this.mainControlGroup.Items.Add(this.CallConsole);
            this.mainControlGroup.Label = "Основные команды";
            this.mainControlGroup.Name = "mainControlGroup";
            // 
            // CallConsole
            // 
            this.CallConsole.Label = "Вызов консоли";
            this.CallConsole.Name = "CallConsole";
            this.CallConsole.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CallConsole_Click);
            // 
            // MetaRibbon
            // 
            this.Name = "MetaRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.MetaTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.MetaRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.MetaTab.ResumeLayout(false);
            this.MetaTab.PerformLayout();
            this.mainControlGroup.ResumeLayout(false);
            this.mainControlGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab MetaTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup mainControlGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CallConsole;
    }

    partial class ThisRibbonCollection
    {
        internal MetaRibbon MetaRibbon
        {
            get { return this.GetRibbon<MetaRibbon>(); }
        }
    }
}
