using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using CMIETree.Extentions;

namespace CMIETree
{
    public static class FormMethods
    {
        //public FormMethods()
        //{
        //}

        /// <summary>
        /// 初始化ListView
        /// </summary>
        /// <param name="lvCur"></param>
        public static void initializeListView(ListView lvCur)
        {
            lvCur.View = System.Windows.Forms.View.Details; //Detail模式

            //添加表头,暂时4种 
            lvCur.Columns.Add("Category");
            lvCur.Columns.Add("Family");
            lvCur.Columns.Add("Type");
            lvCur.Columns.Add("Id");

            //调整列宽
            int columnWidth = lvCur.Width / 4;
            lvCur.Columns[0].Width = columnWidth;
            lvCur.Columns[1].Width = columnWidth;
            lvCur.Columns[2].Width = columnWidth;
            lvCur.Columns[3].Width = lvCur.Width - 3 * columnWidth;
        }

        /// <summary>
        /// 填充ListView
        /// </summary>
        /// <param name="curListView"></param>
        public static void FillListView(ListView lvCur, List<Element> elements)
        {
            lvCur.BeginUpdate(); //开始
            lvCur.Items.Clear(); // 清空节点

            //添加内容
            foreach (Element elemCur in elements)
            {
                ElementId instanceId = elemCur.AssemblyInstanceId;
                string categoryStr = elemCur.Category.Name;
                string idStr = elemCur.Id.IntegerValue.ToString();
                string familyStr = string.Empty;
                string typeStr = string.Empty;
                string familyTypeStr = string.Empty;
                Space space = null;

                ElementInfo.GetFamilyAndTypeInfo(elemCur, ref familyStr, ref typeStr, ref familyTypeStr, ref space);
                if (elemCur is FamilyInstance)
                {
                }
                else
                {
                    typeStr = elemCur.Name;
                }
                
                
                ListViewItem liCat = new ListViewItem(categoryStr);
                liCat.SubItems.Add(familyStr);
                liCat.SubItems.Add(typeStr);
                liCat.SubItems.Add(idStr);
                liCat.Tag = elemCur; //将Element绑到ListViewItem的tag上
                lvCur.Items.Add(liCat);
            }


            lvCur.EndUpdate(); //一次更新ListView
        }

        /// <summary>
        /// 已弃用，请使用ElementInfo.GetFamilyAndTypeInfo
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        private static string getTypeString(Element elem)
        {
            switch (elem.Category.Name)
            {
                case "墙":
                    Wall wall = elem as Wall;
                    return wall.WallType.Name;
                //case "视图":
                //    Autodesk.Revit.DB.View view = elem as Autodesk.Revit.DB.View;
                //    return view.ViewType.ToString();
                case "图纸":
                    ViewSheet viewSheet = elem as ViewSheet;
                    return viewSheet.ViewType.ToString();
            }
            return elem.GetType().Name;
        }

        /// <summary>
        /// 检索树形结构
        /// </summary>
        public static List<TreeNode> RetrieveNodes(TreeView tvCur, string text)
        {
            List<TreeNode> foundNodes = new List<TreeNode>();
            foreach (TreeNode node in tvCur.Nodes)
            {
                getNodes(foundNodes, node, text);
            }
            return foundNodes;
        }

        /// <summary>
        /// 获取所有nodes
        /// </summary>
        /// <param name="foundNodes"></param>
        /// <param name="node"></param>
        /// <param name="text"></param>
        private static void getNodes(List<TreeNode> foundNodes, TreeNode node, string text)
        {
            if (node.Text.Contains(text))
            {
                foundNodes.Add(node);
            }

            if (node.Nodes.Count >0)
            {
                foreach (TreeNode nodeCur in node.Nodes)
                {
                    getNodes(foundNodes, nodeCur, text); //迭代所有nodes
                }
            }
        }

        /// <summary>
        /// 迭代获取最低层Node的tag
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tagElements"></param>
        public static void getNodeTags(TreeNode node, List<Element> tagElements)
        {
            //int level = node.Level;
            //List<Element> tagElements = new List<Element>();
            if (node.Nodes.Count >0)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    getNodeTags(childNode, tagElements);
                }
            }
            else
            {
                tagElements.Add(node.Tag as Element);
            }
        }

        #region 处理ListView事件
        /// <summary>
        /// 将所选Element加入选择集
        /// </summary>
        /// <param name="elements"></param>
        public static void AddElementToSelection(UIApplication uiApplication , IList<Element> elements)
        {
            //Document doc = m_uiApplication.ActiveUIDocument.Document;
            //SubTransaction subTrans = new SubTransaction(doc);
            //subTrans.Start();
            SelectionUtils.SelectElements(uiApplication.ActiveUIDocument, elements);
            //doc.Regenerate();
            //subTrans.Commit();
        }

        /// <summary>
        /// 用于处理双击ListView事件时处理选中Element
        /// </summary>
        /// <param name="selectedElements"></param>
        public static void HandleSelectedElements(UIApplication uiApplication, List<Element> selectedElements)
        {
            if (selectedElements.Count == 0)
            {
                return;
            }

            if (!isElementsOfOneType(selectedElements)) //如果所选项中有多种Family
            {
                MessageBox.Show("所选对象中包含多种族，不能同时进行处理。", "查看对象");
                return;
            }

            int elementCount = selectedElements.Count;

            if (elementCount == 1) // 单个Element
            {
                implementOneElement(uiApplication ,selectedElements[0]);
            }
            else // 多个Element
            {
                string promptStr = string.Empty;
                foreach (Element elemCur in selectedElements)
                {
                    promptStr += (elemCur.Name + "\r\n");
                }
                MessageBox.Show("所选元素是\r\n" + promptStr, "查看属性");
            }
        }

        /// <summary>
        /// 检查选择集中的Element是同一种Family
        /// </summary>
        /// <param name="selectedElements"></param>
        /// <returns></returns>
        private static bool isElementsOfOneType(List<Element> selectedElements)
        {
            string familyStr = string.Empty;
            string typeStr = string.Empty;
            string familyTypeStr = string.Empty;
            Space space = null;
            List<string> familyNames = new List<string>();
            int familyCount = 0;
            foreach (Element elemCur in selectedElements)
            {
                ElementInfo.GetFamilyAndTypeInfo(elemCur, ref familyStr, ref typeStr, ref familyTypeStr, ref space);
                if (!familyNames.Contains(familyStr))
                {
                    familyNames.Add(familyStr);
                }
                familyCount = familyNames.Count;
                if (familyCount > 1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ListView双击时处理单个Element
        /// </summary>
        /// <param name="element">要处理的Element</param>
        private static void implementOneElement(UIApplication uiApplication, Element element)
        {
            switch (element.Category.Name)
            {
                 //若为图纸或者视图，则切换至视图
                case "图纸":
                case "视图":
                    Autodesk.Revit.DB.View view = element as Autodesk.Revit.DB.View;
                    uiApplication.ActiveUIDocument.ActiveView = view;
                    break;

                    //其他情况显示提示信息，暂不做开发
                default:
                    MessageBox.Show("所选元素是\r\n" + element.Name, "查看属性");
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 检索ListView中匹配的Items
        /// </summary>
        public static List<ListViewItem> RetrieveLvItems(ListView lv, string retrieve)
        {
            List<ListViewItem> matchItems = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lv.Items)
            {
                if (isitemMatched(lvItem, retrieve))
                {
                    matchItems.Add(lvItem);
                }
            }
            return matchItems;
        }

        /// <summary>
        /// 检索ListViewItem是否匹配检索字符串
        /// </summary>
        /// <param name="lvItem"></param>
        /// <param name="retrieve"></param>
        /// <returns></returns>
        private static bool isitemMatched(ListViewItem lvItem, string retrieve)
        {
            if (lvItem.Text.Contains(retrieve))
            {
                return true;
            }

            if (lvItem.SubItems.Count >0)
            {
                foreach (ListViewItem.ListViewSubItem subItem in lvItem.SubItems)
                {
                    if (subItem.Text.Contains(retrieve))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
