using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
/**************************************************************************
  Written By Bo Liu                                       
  October 2014                                                          
  http://www.cmie.cn 
**************************************************************************/
using Autodesk.Revit.UI.Selection;
using CMIETree.Extentions;

namespace CMIETree
{
    /// <summary>
    /// 存储TreeView的数据并填充TreeView
    /// </summary>
    public class TreeViewData : ExcuteData
    {
        #region fields
        
        /// <summary>
        /// 所有检索到的Element。
        /// </summary>
        private ArrayList m_objs;

        private UIApplication m_uiApplication;

        private List<BuiltInCategory> m_categories; 
        
        //private List<Category> m_categoryList;
        //private List<Family> m_familyList; 
        #endregion

        public ArrayList Objs
        {
            get { return m_objs; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uiApplication">用户Ui程序</param>
        public TreeViewData(ExternalCommandData cmdData, string message, ElementSet elementSet, List<BuiltInCategory> categories)
            : base(cmdData, message, elementSet)
        {
            m_uiApplication = cmdData.Application;
            m_categories = categories;
        }


        public override void SetData()
        {
            // iterate over the collection and put them in an ArrayList so we can pass on
            // to our Form
            Autodesk.Revit.DB.Document doc = m_uiApplication.ActiveUIDocument.Document;
            FilteredElementCollector elemTypeCtor = (new FilteredElementCollector(doc)).WhereElementIsElementType();
            FilteredElementCollector notElemTypeCtor = (new FilteredElementCollector(doc)).WhereElementIsNotElementType();
            FilteredElementCollector allElementCtor = elemTypeCtor.UnionWith(notElemTypeCtor);
            FilteredElementCollector filteredcCollector = elemTypeCtor;
            ICollection<Element> founds = notElemTypeCtor.ToElements();

            //List<BuiltInCategory> elemCategories = CategoryUtils.GetBuiltInCategories(founds as List<Element>);
            List<Category> elemCategories = new List<Category>();
            foreach (Element element in founds)
            {
                if (element.Category == null)
                {
                    elemCategories.Add(null);
                }
                else
                {
                    elemCategories.Add(element.Category);
                }
            }

            List<Element> seleted = new List<Element>();
            List<Element> foundElements = founds as List<Element>;
            for (int i = 0; i < elemCategories.Count; i++)
            {
                if (foundElements[i].Category ==null)
                {
                    continue;
                }


                BuiltInCategory CategoryCur = (BuiltInCategory) elemCategories[i].Id.IntegerValue;
                if (m_categories.Contains(CategoryCur))
                {
                    seleted.Add(foundElements[i]);
                }
            }

            m_objs = new ArrayList();
            foreach (Element element in seleted)
            {
                m_objs.Add(element);
            }
        }

        public override void Start()
        {
            
        }


        }
}
