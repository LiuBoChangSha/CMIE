using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestListView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listView1.Columns.Add("学生序号");
            this.listView1.Columns.Add("学生姓名");
            this.listView1.Columns.Add("学生学号");
            this.listView1.View = System.Windows.Forms.View.Details;

            ListViewItem li = new ListViewItem("A1");
            li.SubItems.Add("abc");
            li.SubItems.Add("cde");
            this.listView1.Items.Add(li);

            ListViewItem l2 = new ListViewItem("B1");
            l2.SubItems.Add("Bbc");
            l2.SubItems.Add("Bde");
            this.listView1.Items.Add(l2);
        }
    }
}
