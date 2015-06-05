using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using CMIETree.Extentions;
using Color = System.Drawing.Color;
using Form = System.Windows.Forms.Form;

namespace CMIETree
{
    public partial class TvForm : Form
    {
        protected UIApplication m_uiApplication;
        protected ArrayList m_types = new ArrayList();
        protected ArrayList m_familys = new ArrayList(); // FamilyString
        protected ArrayList m_familyNodes = new ArrayList(); // FamilyNodes
        protected ArrayList m_categories = new ArrayList(); // CategoryString
        protected ArrayList m_categoryNodes = new ArrayList(); // CategoryNodes
        protected List<Element> m_curObjs;

        protected List<ListViewItem> m_lvMatchItems;
        private int m_lvIndex;
        //protected List<List<Element>> m_parentTags;

        private List<TreeNode> m_foundNodes;
        private int m_retrieveIndex;

        private TreeViewData m_tvData;
        private string m_projectName;

        //public TvForm()
        //{
        //    InitializeComponent();
        //}

        // public TvForm(System.Object obj)
        //{
        //    InitializeComponent();

        //        // get in array form so we can call normal processing code.
        //    ArrayList objs = new ArrayList();
        //    objs.Add(obj);
        //    CommonInit(objs);
        //}

		public TvForm(TreeViewData tvData, UIApplication uiApplication)
		{
			InitializeComponent();
		    m_tvData = tvData;
            m_tvData.SetData();
            ArrayList objs = m_tvData.Objs;
		    m_uiApplication = uiApplication;
		    m_projectName = uiApplication.ActiveUIDocument.Document.ProjectInformation.Name;
			CommonInit(objs);
   		}

        //public TvForm(ElementSet elemSet)
        //{
        //    InitializeComponent();
        //    CommonInit(elemSet);
        //}

        protected void CommonInit(IEnumerable objs)
        {
            m_tvObjs.BeginUpdate();

            AddObjectsToTree2(objs);

            // if the tree isn't well populated, expand it and select the first item
            // so its not a pain for the user when there is only one relevant item in the tree
            if (m_tvObjs.Nodes.Count == 1)
            {
                m_tvObjs.Nodes[0].Expand();
                if (m_tvObjs.Nodes[0].Nodes.Count == 0)
                    m_tvObjs.SelectedNode = m_tvObjs.Nodes[0];
                else
                    m_tvObjs.SelectedNode = m_tvObjs.Nodes[0].Nodes[0];
            }

            m_tvObjs.EndUpdate();
        }
        
        /// <summary>
        /// 以Category为主节点
        /// </summary>
        /// <param name="objs"></param>
        protected void AddObjectsToTree1(IEnumerable objs)
        {
            m_tvObjs.Sorted = true;
            //m_parentTags= new List<List<Element>>();

            // initialize the tree control
            foreach (Object tmpObj in objs)
            {
                
                Element elem = tmpObj as Element;
                if (elem == null || elem.Category == null) //若category为空则也不继续进行，此处有待更佳的方式来处理。
                {
                    continue;
                }

                // hook this up to the correct spot in the tree based on the object's type
                TreeNode parentNode = GetExistingNodeForType(tmpObj.GetType()); //看是已有父节点
                if (parentNode == null) //如果没有父节点
                {
                    string nameStr;
                    nameStr = (elem.Category.Name == string.Empty) ? tmpObj.GetType().Name : elem.Category.Name; // category.name为空则取用type名。

                    parentNode = new TreeNode(nameStr);
                    //ParentTags.Add(elem);
                    //parentNode.Tag = ParentTags; //将对象绑定到tag上

                    List<Element> parentElementsCur = new List<Element>();
                    parentElementsCur.Add(elem);
                    //m_parentTags.Add(parentElementsCur); //父节点绑定到tag的集合
                    parentNode.Tag = parentElementsCur;

                    m_tvObjs.Nodes.Add(parentNode);
                    m_familyNodes.Add(parentNode);
                    m_types.Add(tmpObj.GetType());
                    m_categories.Add(elem.Category.Name);
                }
                //else //若果已有父节点，则父节点的tag需要更新
                //{
                //    List<Element> parentTagsCur = getExistingTagForType(tmpObj.GetType());
                //    parentTagsCur.Add(elem);
                //}

                // add the new node for this element
                TreeNode tmpNode = new TreeNode(ObjToLabelStr(tmpObj));
                List<Element> childTags = new List<Element>(); //尽管只有一个，但是为了和父节点的tag保持一致性还是存储在集合中。
                childTags.Add(elem);
                tmpNode.Tag = childTags;  //将此对象绑定到tag上
                parentNode.Nodes.Add(tmpNode);
            }
        }

        /// <summary>
        /// 以Family为主节点
        /// 第一层Node为projectName，第二层为Category，
        /// 第三层为Family，第四层为Id，同时第四层绑定Tag为Element。
        /// </summary>
        /// <param name="objs"></param>
        protected void AddObjectsToTree2(IEnumerable objs)
        {
            try
            {
                m_tvObjs.Sorted = true;
                //m_parentTags = new List<List<Element>>();

                // 第一层节点 项目信息
                string projectName = m_projectName;
                TreeNode projectNode = m_tvObjs.Nodes.Add(projectName);
                
                // initialize the tree control
                foreach (Object tmpObj in objs)
                {
                    Element elem = tmpObj as Element;

                    string familyStr = string.Empty;
                    string typeStr = string.Empty;
                    string familyTypeStr = string.Empty;
                    Space space = null;

                    ElementInfo.GetFamilyAndTypeInfo(elem, ref familyStr, ref typeStr, ref familyTypeStr, ref space);

                    if (elem == null || elem.Category == null || string.IsNullOrEmpty(familyStr)) //若Family为空则也不继续进行，此处有待更佳的方式来处理。
                    {
                        continue;
                    }

                    string categoryNameStr;
                    categoryNameStr = (string.IsNullOrEmpty(elem.Category.Name)) ? tmpObj.GetType().Name : elem.Category.Name; // category.name为空则取用type名。

                    // hook this up to the correct spot in the tree based on the object's type
                    //TreeNode parentNode = GetExistingNodeForType(tmpObj.GetType()); 
                    TreeNode categoryNode = GetExistingNodeForCategory(categoryNameStr);  //看是已有Category节点
                    if (categoryNode == null)  //如果没有Category节点
                    {
                        categoryNode = new TreeNode(categoryNameStr);
                        //ParentTags.Add(elem);
                        //parentNode.Tag = ParentTags; //将对象绑定到tag上

                        //List<Element> parentElementsCur = new List<Element>();
                        //parentElementsCur.Add(elem);
                        //m_parentTags.Add(parentElementsCur); //父节点绑定到tag的集合
                        //parentNode.Tag = parentElementsCur;

                        projectNode.Nodes.Add(categoryNode); // 在ProjectNode下添加CategoryNode节点
                        m_categoryNodes.Add(categoryNode);
                        m_categories.Add(categoryNameStr);
                    }

                    TreeNode familyNode = GetExistingNodeForFamily(familyStr);
                    if (familyNode == null) // 如果没有Family节点
                    {
                        familyNode = new TreeNode(familyStr);
                        //reeNode categoryNodeCur = GetExistingNodeForCategory(categoryNameStr);

                        categoryNode.Nodes.Add(familyNode); // 在制定CategoryNode下添加FamilyNode
                        m_familyNodes.Add(familyNode);
                        m_familys.Add(familyStr);
                    }


                    // add the new node for this element
                    TreeNode tmpNode = new TreeNode(ObjToLabelStr(tmpObj));
                    tmpNode.Tag = elem;  //将此对象绑定到tag上
                    familyNode.Nodes.Add(tmpNode); //加子节点
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// If we've already seen this type before, return the existing TreeNode object
        /// </summary>
        /// <param name="objType">System.Type we're looking to find</param>
        /// <returns>The existing TreeNode or NULL</returns>
        /// 
        protected TreeNode GetExistingNodeForType(System.Type objType)
        {
            int len = m_types.Count;
            for (int i = 0; i < len; i++)
            {
                if ((System.Type)m_types[i] == objType)
                    return (TreeNode)m_familyNodes[i];
            }

            return null;
        }

        /// <summary>
        /// If we've already seen this type before, return the existing TreeNode object
        /// </summary>
        /// <returns>The existing TreeNode or NULL</returns>
        /// 
        protected TreeNode GetExistingNodeForFamily(string familyName)
        {
            int len = m_familys.Count;
            for (int i = 0; i < len; i++)
            {
                if (string.Equals((string)m_familys[i], familyName))
                    return (TreeNode)m_familyNodes[i];
            }
            return null;
        }

        protected TreeNode GetExistingNodeForCategory(string categoryName)
        {
            int len = m_categories.Count;
            for (int i = 0; i < len; i++)
            {
                if (string.Equals((string)m_categories[i], categoryName))
                {
                    return (TreeNode) m_categoryNodes[i];
                }
            }
            return null;
        }

        //protected List<Element> getExistingTagForType(System.Type objType)
        //{
        //    int len = m_types.Count;
        //    for (int i = 0; i < len; i++)
        //    {
        //        if ((System.Type)m_types[i] == objType)
        //            return m_parentTags[i];
        //    }

        //    return null;
        //}

        //protected List<Element> getExistingTagForFamily(string familyName)
        //{
        //    int len = m_familys.Count;
        //    for (int i = 0; i < len; i++)
        //    {
        //        if (string.Equals((string)m_familys[i], familyName))
        //            return m_parentTags[i];
        //    }

        //    return null;
        //}

        public static string ObjToLabelStr(System.Object obj)
        {
            if (obj == null)
                return "< null >";

            Autodesk.Revit.DB.Element elem = obj as Autodesk.Revit.DB.Element;
            if (elem != null)
            {
                // TBD: Exceptions are thrown in certain cases when accessing the Name property. 
                // Eg. for the RoomTag object. So we will catch the exception and display the exception message
                // arj - 1/23/07
                try
                {
                    string nameStr = (elem.Name == string.Empty) ? "???" : elem.Name;		// use "???" if no name is set
                    return string.Format("< {0}  {1} >", nameStr, elem.Id.IntegerValue.ToString());
                }
                catch (System.InvalidOperationException ex)
                {
                    return string.Format("< {0}  {1} >", null, ex.Message);
                }
            }
            return string.Format("< {0} >", obj.GetType().Name);
        }
        

        #region events
        /// <summary>
        /// 节点被选中后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_tvObjs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //从节点的Tag中获取元素，并将数据填充到ListView
            //m_curObjs = e.Node.Tag as List<Element>;
            m_curObjs = new List<Element>();
            FormMethods.getNodeTags(e.Node, m_curObjs);
            FormMethods.FillListView(listView1, m_curObjs);
        }

        /// <summary>
        /// 节点选中前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_tvObjs_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (m_tvObjs.SelectedNode == null)
            {
                return;
            }
            //将当前选中节点的背景颜色换成指定颜色
            m_tvObjs.SelectedNode.BackColor = DefaultBackColor; // ButtonFace
        }

        /// <summary>
        /// ListView Item选中事件，将所选Element加入选择集，有一些莫名其妙的bug，未能通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItems = listView1.SelectedItems;
            if (selectedItems.Count <1)
            {
                return;
            }
            IList<Element> elements = new List<Element>();
            foreach (ListViewItem item in selectedItems)
            {
                Element elem = item.Tag as Element;
                if (elem == null)
                {
                    continue;
                }
                elements.Add(elem);
                //elements.Add(item.Tag as Element);
            }

            FormMethods.AddElementToSelection(m_uiApplication ,elements);
        }

        /// <summary>
        /// ListView双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var selItems = listView1.SelectedItems;
            //string name = string.Empty;
            List<Element> selectedElements = new List<Element>();
            foreach (ListViewItem item in selItems)
            {
                Element elem = item.Tag as Element;
                selectedElements.Add(elem);
                //name = elem.Name;
            }

            FormMethods.HandleSelectedElements(m_uiApplication,selectedElements);
            //MessageBox.Show("test\r\n" + name);
        }
        #endregion

        private void TvForm_Load(object sender, EventArgs e)
        {
            FormMethods.initializeListView(listView1);
        }

        private void cmbRetrieve_TextChanged(object sender, EventArgs e)
        {
            m_foundNodes = FormMethods.RetrieveNodes(m_tvObjs, cmbTvRetrieve.Text);
            m_retrieveIndex = 0;
            selectFoundnode();
        }

        /// <summary>
        /// 选中节点
        /// </summary>
        private void selectFoundnode()
        {
            if (m_foundNodes.Count>0)
            {
                m_tvObjs.SelectedNode = m_foundNodes[m_retrieveIndex];  //选中节点
                m_foundNodes[m_retrieveIndex].BackColor = System.Drawing.Color.OliveDrab; //设定选中节点背景颜色
                if (m_foundNodes[m_retrieveIndex].Parent == null)
                {
                    m_foundNodes[m_retrieveIndex].Expand(); //展开节点
                }
                else
                {
                    m_foundNodes[m_retrieveIndex].Parent.Expand(); //展开父节点
                }
            }
        }
        
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            addCmbItems(cmbTvRetrieve);

            if (m_foundNodes == null)
            {
                return;
            }

            if (m_retrieveIndex < m_foundNodes.Count -1) //选中下一个匹配目标
            {
                m_retrieveIndex ++;
            }
            else
            {
                m_retrieveIndex = 0; //溢出归零
            }
            selectFoundnode();
        }

        /// <summary>
        /// 添加搜索项到cmb的item中，若超过8个则删除之前的item
        /// </summary>
        private void addCmbItems(System.Windows.Forms.ComboBox cmb)
        {
            if (!string.IsNullOrEmpty(cmb.Text))
            {
                if (!cmb.Items.Contains(cmb.Text)) 
                {
                    cmb.Items.Add(cmb.Text);
                }

                if (cmb.Items.Count > 10)  //过10删除首项
                {
                    cmb.Items.RemoveAt(0);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbLvRetrieve_TextChanged(object sender, EventArgs e)
        {
            m_lvMatchItems = FormMethods.RetrieveLvItems(listView1, cmbLvRetrieve.Text);
            m_lvIndex = 0;
            selectLvItem();
        }

        private void btnLvRetrieve_Click(object sender, EventArgs e)
        {
            addCmbItems(cmbLvRetrieve);

            if (m_lvIndex < m_lvMatchItems.Count - 1) //选中下一个匹配目标
            {
                m_lvIndex++;
            }
            else
            {
                m_lvIndex = 0; //溢出归零
            }
            selectLvItem();
        }

        private void selectLvItem()
        {
            if (listView1.Items.Count >0)
            {
                foreach (ListViewItem lvitem in listView1.Items)
                {
                    lvitem.BackColor = Color.White; // 将背景色换成白色
                }
            }
            
            if (m_lvMatchItems.Count>0)
            {
                m_lvMatchItems[m_lvIndex].Selected = true; // 选中
                m_lvMatchItems[m_lvIndex].Focused = true; // 设置焦点
                m_lvMatchItems[m_lvIndex].EnsureVisible(); // 保持可见
                m_lvMatchItems[m_lvIndex].BackColor = System.Drawing.Color.OliveDrab; //设定选中item背景颜色
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("帮助文档构建中……", "帮助信息");
        }
        
    }
}
