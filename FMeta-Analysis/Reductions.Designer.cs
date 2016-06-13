namespace FMeta_Analysis
{
    partial class Reductions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableColumn = new System.Windows.Forms.DataGridView();
            this.headerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tableColumn)).BeginInit();
            this.SuspendLayout();
            // 
            // tableColumn
            // 
            this.tableColumn.AllowUserToAddRows = false;
            this.tableColumn.AllowUserToDeleteRows = false;
            this.tableColumn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.tableColumn.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tableColumn.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.tableColumn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableColumn.CausesValidation = false;
            this.tableColumn.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.tableColumn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.headerName,
            this.nick});
            this.tableColumn.Location = new System.Drawing.Point(0, 0);
            this.tableColumn.MultiSelect = false;
            this.tableColumn.Name = "tableColumn";
            this.tableColumn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.tableColumn.Size = new System.Drawing.Size(231, 360);
            this.tableColumn.TabIndex = 0;
            this.tableColumn.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.tableColumn_CellBeginEdit);
            this.tableColumn.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableColumn_CellValueChanged);
            // 
            // headerName
            // 
            this.headerName.Frozen = true;
            this.headerName.HeaderText = "Заголовок столбца";
            this.headerName.Name = "headerName";
            this.headerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.headerName.Width = 111;
            // 
            // nick
            // 
            this.nick.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.nick.Frozen = true;
            this.nick.HeaderText = "Сокращение";
            this.nick.Name = "nick";
            this.nick.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nick.Width = 77;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(42, 362);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(145, 28);
            this.save.TabIndex = 1;
            this.save.Text = "Сохранить";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // Reductions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(231, 391);
            this.Controls.Add(this.save);
            this.Controls.Add(this.tableColumn);
            this.MaximumSize = new System.Drawing.Size(247, 430);
            this.MinimumSize = new System.Drawing.Size(247, 430);
            this.Name = "Reductions";
            this.Text = "Reductions";
            ((System.ComponentModel.ISupportInitialize)(this.tableColumn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tableColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn headerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn nick;
        private System.Windows.Forms.Button save;
    }
}