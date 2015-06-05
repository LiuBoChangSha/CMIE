using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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
    public partial class TreeForm : Form
    {
        #region 字段
        //private DataSet dataSet;
        //private DataTable dataTable;

        private UIApplication m_uiApplication;
        private TreeViewData m_treeViewData;
        private List<string> m_categoryList;
        private List<TreeNode> m_categoryNodes; 
        private List<List<TreeNode>> m_familyNodes;
        #endregion

        public TreeForm(UIApplication uiApplication, TreeViewData treeViewData, List<string> categoryList)
        {
            m_uiApplication = uiApplication;
            m_treeViewData = treeViewData;
            m_categoryList = categoryList;
            InitializeComponent();
        }

        private void TreeForm_Load(object sender, EventArgs e)
        {
            fillTreeView();
        }

        /// <summary>
        /// 从文件中获取
        /// </summary>
        private void getData()
        {
            
        }

        /// <summary>
        /// 重置树
        /// </summary>
        private void initialiseTree()
        {
            treeView1.Nodes.Clear();
            //string[] columnStr = null;
            //DataView dv = new DataView();
        }

        /// <summary>
        /// 给父节点加子节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns>创建的节点</returns>
        private TreeNode addChildNode(TreeNode parentNode, string childNodeText)
        {
            TreeNode childNode = new TreeNode(childNodeText);
            parentNode.Nodes.Add(childNode);
            return childNode;
        }

        /// <summary>
        /// 给节点添加兄弟节点，须有父节点
        /// </summary>
        /// <returns>创建的节点</returns>
        private TreeNode addBrotherNode(TreeNode existingNode, string brotherNodeText)
        {
            TreeNode node = new TreeNode(brotherNodeText);
            existingNode.Parent.Nodes.Add(node);
            return node;
        }

        /// <summary>
        /// 填充树表
        /// </summary>
        private void fillTreeView()
        {
            treeView1.Nodes.Clear();
            // 项目节点
            string projectName = m_uiApplication.ActiveUIDocument.Document.ProjectInformation.Name;
            TreeNode projectNode = treeView1.Nodes.Add(projectName);
            // 文档节点
            string docName = m_uiApplication.ActiveUIDocument.Document.Title;
            TreeNode docNode = addChildNode(projectNode, docName);

            // 类别节点
            m_categoryNodes = new List<TreeNode>();
            foreach (string category in m_categoryList)
            {
                m_categoryNodes.Add(addChildNode(docNode, category));
            }

            // 族节点
            m_familyNodes = new List<List<TreeNode>>();
            foreach (TreeNode categoryNode in m_categoryNodes)
            {
                List<TreeNode> thisFamilyList = new List<TreeNode>();
                m_familyNodes.Add(thisFamilyList);
            }
            findFamilies();
        }

        /// <summary>
        /// 检索相应的族并创建族节点，这里创建的是族，而非实例
        /// </summary>
        private void findFamilies()
        {
            ElementClassFilter famFilter = new ElementClassFilter(typeof (Family));
            FilteredElementCollector collector = new FilteredElementCollector(m_uiApplication.ActiveUIDocument.Document);
            FilteredElementIterator iterator = collector.WherePasses(famFilter).GetElementIterator();
            iterator.Reset();
            for (; iterator.MoveNext();)
            {
                Family family = iterator.Current as Family;
                if (null != family)
                {
                    foreach (object symbol in family.Symbols)
                    {
                        FamilySymbol familyType = symbol as FamilySymbol;
                        
                        if (null != familyType && null != familyType.Category)
                        {
                            // add symbols of beams and braces to lists 
                            string categoryName = familyType.Category.Name;
                            //if (m_categoryList.Contains(categoryName))
                            if (m_categoryList.Contains(categoryName))
                            {
                                int index = m_categoryList.IndexOf(categoryName);
                                m_familyNodes[index].Add(addChildNode(m_categoryNodes[index], familyType.Name));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 检索项目中的实例并且创建它所属的族节点
        /// </summary>
        private void findInstancesNodes()
        {
            //创建迭代器
            FilteredElementIterator iterator = new FilteredElementCollector(
                m_uiApplication.ActiveUIDocument.Document).GetElementIterator();

            iterator.Reset(); // 初始化迭代
            for (; iterator.MoveNext(); )
            {
                // add wall to list
                Element element = iterator.Current;
                if (null != element)
                {
                    if (m_categoryList.Contains(element.Category.Name))
                    {
                        //element.
                    }
                }
            }
        }

        /// <summary>
        /// 用数据填充表格
        /// </summary>
        private void fillDgvTreeInfo()
        {
            
        }

 
    }
}
