namespace CMIETree
{
    partial class TvForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TvForm));
            this.m_tvObjs = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cmbTvRetrieve = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblCompany = new System.Windows.Forms.Label();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.btnLvRetrieve = new System.Windows.Forms.Button();
            this.cmbLvRetrieve = new System.Windows.Forms.ComboBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_tvObjs
            // 
            this.m_tvObjs.Location = new System.Drawing.Point(10, 48);
            this.m_tvObjs.Name = "m_tvObjs";
            this.m_tvObjs.Size = new System.Drawing.Size(217, 360);
            this.m_tvObjs.TabIndex = 2;
            this.m_tvObjs.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_tvObjs_BeforeSelect);
            this.m_tvObjs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_tvObjs_AfterSelect);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(240, 48);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(544, 360);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // cmbTvRetrieve
            // 
            this.cmbTvRetrieve.FormattingEnabled = true;
            this.cmbTvRetrieve.Location = new System.Drawing.Point(10, 12);
            this.cmbTvRetrieve.Name = "cmbTvRetrieve";
            this.cmbTvRetrieve.Size = new System.Drawing.Size(179, 20);
            this.cmbTvRetrieve.TabIndex = 5;
            this.cmbTvRetrieve.TextChanged += new System.EventHandler(this.cmbRetrieve_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(628, 453);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(547, 453);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(8, 459);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(209, 12);
            this.lblCompany.TabIndex = 7;
            this.lblCompany.Text = "中机国际工程设计研究院有限责任公司";
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(195, 10);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(65, 23);
            this.btnRetrieve.TabIndex = 10;
            this.btnRetrieve.Text = "检索树表";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // btnLvRetrieve
            // 
            this.btnLvRetrieve.Location = new System.Drawing.Point(718, 11);
            this.btnLvRetrieve.Name = "btnLvRetrieve";
            this.btnLvRetrieve.Size = new System.Drawing.Size(65, 23);
            this.btnLvRetrieve.TabIndex = 12;
            this.btnLvRetrieve.Text = "检索表格";
            this.btnLvRetrieve.UseVisualStyleBackColor = true;
            this.btnLvRetrieve.Click += new System.EventHandler(this.btnLvRetrieve_Click);
            // 
            // cmbLvRetrieve
            // 
            this.cmbLvRetrieve.FormattingEnabled = true;
            this.cmbLvRetrieve.Location = new System.Drawing.Point(533, 13);
            this.cmbLvRetrieve.Name = "cmbLvRetrieve";
            this.cmbLvRetrieve.Size = new System.Drawing.Size(179, 20);
            this.cmbLvRetrieve.TabIndex = 11;
            this.cmbLvRetrieve.TextChanged += new System.EventHandler(this.cmbLvRetrieve_TextChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHelp.Location = new System.Drawing.Point(709, 453);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 13;
            this.btnHelp.Text = "帮助";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // TvForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(796, 488);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnLvRetrieve);
            this.Controls.Add(this.cmbLvRetrieve);
            this.Controls.Add(this.btnRetrieve);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.cmbTvRetrieve);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.m_tvObjs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TvForm";
            this.Text = "CMIE树表";
            this.Load += new System.EventHandler(this.TvForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView m_tvObjs;
        protected System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox cmbTvRetrieve;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.Button btnLvRetrieve;
        private System.Windows.Forms.ComboBox cmbLvRetrieve;
        private System.Windows.Forms.Button btnHelp;
    }
}