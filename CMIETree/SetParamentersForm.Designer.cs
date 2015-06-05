namespace CMIETree
{
    partial class SetParamentersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetParamentersForm));
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.cmbRetrieve = new System.Windows.Forms.ComboBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectReverse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvCategories.Location = new System.Drawing.Point(12, 37);
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.RowHeadersWidth = 20;
            this.dgvCategories.RowTemplate.Height = 23;
            this.dgvCategories.Size = new System.Drawing.Size(392, 266);
            this.dgvCategories.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "选取";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "参数(Category)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "中机国际工程设计研究院有限责任公司";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(240, 344);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(330, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(154, 312);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(65, 23);
            this.btnRetrieve.TabIndex = 12;
            this.btnRetrieve.Text = "检 索";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // cmbRetrieve
            // 
            this.cmbRetrieve.FormattingEnabled = true;
            this.cmbRetrieve.Location = new System.Drawing.Point(12, 315);
            this.cmbRetrieve.Name = "cmbRetrieve";
            this.cmbRetrieve.Size = new System.Drawing.Size(125, 20);
            this.cmbRetrieve.TabIndex = 11;
            this.cmbRetrieve.TextChanged += new System.EventHandler(this.cmbRetrieve_TextChanged);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 8);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(62, 23);
            this.btnSelectAll.TabIndex = 13;
            this.btnSelectAll.Text = "全 选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectReverse
            // 
            this.btnSelectReverse.Location = new System.Drawing.Point(80, 8);
            this.btnSelectReverse.Name = "btnSelectReverse";
            this.btnSelectReverse.Size = new System.Drawing.Size(62, 23);
            this.btnSelectReverse.TabIndex = 14;
            this.btnSelectReverse.Text = "反 选";
            this.btnSelectReverse.UseVisualStyleBackColor = true;
            this.btnSelectReverse.Click += new System.EventHandler(this.btnSelectReverse_Click);
            // 
            // SetParamentersForm
            // 
            this.AcceptButton = this.btnRetrieve;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(414, 381);
            this.Controls.Add(this.btnSelectReverse);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.cmbRetrieve);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCategories);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetParamentersForm";
            this.Text = "选取参数";
            this.Load += new System.EventHandler(this.SetParamentersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.ComboBox cmbRetrieve;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectReverse;
    }
}