namespace CMIETree
{
    partial class TreeForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点5", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点1");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点2", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeForm));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dgvTreeInfo = new System.Windows.Forms.DataGridView();
            this.dataSet1 = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点3";
            treeNode1.Text = "节点3";
            treeNode2.Name = "节点4";
            treeNode2.Text = "节点4";
            treeNode3.Name = "节点6";
            treeNode3.Text = "节点6";
            treeNode4.Name = "节点7";
            treeNode4.Text = "节点7";
            treeNode5.Name = "节点5";
            treeNode5.Text = "节点5";
            treeNode6.Name = "节点0";
            treeNode6.Text = "节点0";
            treeNode7.Name = "节点0";
            treeNode7.Text = "节点0";
            treeNode8.Name = "节点1";
            treeNode8.Text = "节点1";
            treeNode9.Name = "节点1";
            treeNode9.Text = "节点1";
            treeNode10.Name = "节点2";
            treeNode10.Text = "节点2";
            treeNode11.Name = "节点2";
            treeNode11.Text = "节点2";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode8,
            treeNode11});
            this.treeView1.Size = new System.Drawing.Size(151, 220);
            this.treeView1.TabIndex = 0;
            // 
            // dgvTreeInfo
            // 
            this.dgvTreeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTreeInfo.Location = new System.Drawing.Point(169, 12);
            this.dgvTreeInfo.Name = "dgvTreeInfo";
            this.dgvTreeInfo.RowTemplate.Height = 23;
            this.dgvTreeInfo.Size = new System.Drawing.Size(468, 220);
            this.dgvTreeInfo.TabIndex = 1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // TreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 375);
            this.Controls.Add(this.dgvTreeInfo);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TreeForm";
            this.Text = "TreeForm";
            this.Load += new System.EventHandler(this.TreeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dgvTreeInfo;
        private System.Data.DataSet dataSet1;
    }
}