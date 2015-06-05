using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Form = System.Windows.Forms.Form;

/**************************************************************************
  Written By Bo Liu                                       
  October 2014                                                       
  http://www.cmie.cn 
**************************************************************************/

namespace CMIETree
{
    public partial class SetParamentersForm : Form
    {
        //private UIApplication m_uiApplication;
        private ExternalCommandData m_cmdData;
        private string m_message;
        private ElementSet m_elementSet;
        private List<DataGridViewRow> m_matchedRows = new List<DataGridViewRow>();
        private int m_retrieveIndex = 0;


        private List<BuiltInCategory> m_selectedCategories;

        public List<BuiltInCategory> SelectedCategories
        {
            get { return m_selectedCategories; }
        }

         
        

        public SetParamentersForm(ExternalCommandData cmdData, string msg, ElementSet elementSet)
        {
            m_cmdData = cmdData;
            m_message = msg;
            m_elementSet = elementSet;
            InitializeComponent();
        }

        /// <summary>
        /// 检索文档中所有的Category
        /// </summary>
        private Categories retrieveCategories()
        {
            Settings docSettings = m_cmdData.Application.ActiveUIDocument.Document.Settings;
            Categories categories = docSettings.Categories;
            return categories;
        }

        /// <summary>
        /// 获取文档中所有的类别并且填充dgv
        /// </summary>
        private void fillDgv()
        {
            Categories categories = retrieveCategories();
            if (categories.IsEmpty)
            {
                return;
            }

            CategoryNameMapIterator iterator = categories.ForwardIterator();
            iterator.Reset();
            int index = 0;
            for (; iterator.MoveNext(); )
            {
                Category category  = iterator.Current as Category;
                dgvCategories.Rows.Add();
                dgvCategories.Rows[index].Cells[0].Value = false;
                dgvCategories.Rows[index].Cells[1].Value = category.Name;
                dgvCategories.Rows[index].Cells[1].Tag = category; // 绑定Category到Tag
                index++;
            }
            
        }

        private void SetParamentersForm_Load(object sender, EventArgs e)
        {
           fillDgv();
        }

        /// <summary>
        /// 搜集所有用户选取的类别
        /// </summary>
        private void collectSelectedItems()
        {
            try
            {
                m_selectedCategories = new List<BuiltInCategory>();
                foreach (DataGridViewRow row in dgvCategories.Rows)
                {
                    //object obj = row.Cells[0].Value;
                    if (bool.Parse(row.Cells[0].EditedFormattedValue.ToString())) // 如果被勾选
                    {
                        Category category = row.Cells[1].Tag as Category;
                        m_selectedCategories.Add((BuiltInCategory)category.Id.IntegerValue);
                    }
                }
            }
            catch (Exception ex)
            {
                m_message += ex.ToString();
            }
        }

        #region Events

        private void btnOk_Click(object sender, EventArgs e)
        {
            collectSelectedItems();
            this.DialogResult = DialogResult.OK;
            this.Close();
            //CmdTreeView cmdTreeView = new CmdTreeView();
            //cmdTreeView.Execute(m_cmdData, ref m_message, m_elementSet);
            //this.Close();
            //MessageBox.Show(m_selectedCategories[0] + m_selectedCategories[1]);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void cmbRetrieve_TextChanged(object sender, EventArgs e)
        {
            string text = cmbRetrieve.Text;
            m_retrieveIndex = 0;
            m_matchedRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if (row.Cells[1].Value.ToString().Contains(text))
                {
                    m_matchedRows.Add(row);
                }
            }

            if (m_matchedRows.Count > 0)
            {
                selectRetrievedRow();
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            addCmbItems();

            if (m_matchedRows.Count ==0)
            {
                return;
            }

            if (m_retrieveIndex < m_matchedRows.Count - 1) //选中下一个匹配目标
            {
                m_retrieveIndex++;
            }
            else
            {
                m_retrieveIndex = 0; //溢出归零
            }

            selectRetrievedRow();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                row.Cells[0].Value = true;
            }
        }

        private void btnSelectReverse_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                bool reverse;
                if (bool.Parse(row.Cells[0].EditedFormattedValue.ToString()))
                {
                    reverse = false;
                }
                else
                {
                    reverse = true;
                }
                row.Cells[0].Value = reverse;
            }
        }
        #endregion

        private void selectRetrievedRow()
        {
            foreach (DataGridViewRow selRow in dgvCategories.SelectedRows)
            {
                selRow.Selected = false;
            }
            
            m_matchedRows[m_retrieveIndex].Selected = true;
            dgvCategories.FirstDisplayedCell = m_matchedRows[m_retrieveIndex].Cells[0]; // 显示为第一行
        }

        /// <summary>
        /// 添加搜索项到cmb的item中，若超过8个则删除之前的item
        /// </summary>
        private void addCmbItems()
        {
            if (!string.IsNullOrEmpty(cmbRetrieve.Text))
            {
                if (!cmbRetrieve.Items.Contains(cmbRetrieve.Text))
                {
                    cmbRetrieve.Items.Add(cmbRetrieve.Text);
                }

                if (cmbRetrieve.Items.Count > 8)  //过8删除首项
                {
                    cmbRetrieve.Items.RemoveAt(0);
                }
            }
        }

    }

    /// <summary>
    /// 包含类别信息的类
    /// </summary>
    public class CategoryMap
    {
        #region fields
        string m_categoryName = "";
        Category m_category = null;
        #endregion

        #region  Constructors
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="level">level</param>
        public CategoryMap(Category category)
        {
            m_category = category;
            m_categoryName = category.Name;
        }
        #endregion

        #region  properties
        /// <summary>
        /// FamilyInstanceName property
        /// </summary>
        public string CategoryName
        {
            get
            {
                return m_categoryName;
            }
        }

        /// <summary>
        /// FamilyInstance property
        /// </summary>
        public Category Category
        {
            get
            {
                return m_category;
            }
        }

        #endregion
    }

}
