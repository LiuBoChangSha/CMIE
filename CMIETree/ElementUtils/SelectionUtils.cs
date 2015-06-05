using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CMIETree
{
    /// <summary>
    /// 选择集工具箱
    /// </summary>
    public class SelectionUtils
    {
        public SelectionUtils()
        {
            
        }

        public static List<Element> GetElementsFromSelection(UIDocument activeUiDocument, ICollection<int> catIds)
        {
            List<Element> list = new List<Element>();
            foreach (Element element in activeUiDocument.Selection.Elements)
            {
                if (((element != null) && (element.Category != null)) && catIds.Contains(element.Category.Id.IntegerValue))
                {
                    list.Add(element);
                }
            }
            return list;
        }

        public static void SelectElements(UIDocument uiDocument, ElementSet elementSet)
        {
            try
            {
                //uiDocument.Selection.Elements.Clear();
                SelElementSet sel = SelElementSet.Create();
                //SelElementSet sel = uiDocument.Selection.Elements;
                //sel.Clear();
                foreach (Element element in elementSet)
                {
                    try
                    {
                        if (element != null)
                        {
                            sel.Add(element);
                        }
                    }
                    catch
                    {
                    }
                }
                uiDocument.Selection.Elements = sel;
                
            }
            catch (Exception exception)
            {
                TaskDialog.Show(" SelectElements(Autodesk.Revit.UI.UIDocument doc, ElementSet elementSet)  ", exception.ToString());
            }
        }

        public static void SelectElements(UIDocument doc, IList<Element> elements, bool clearSelection = true)
        {
            try
            {
                if (clearSelection)
                {
                    doc.Selection.Elements.Clear();
                }
                SelElementSet set = SelElementSet.Create();
                foreach (Element element in elements)
                {
                    try
                    {
                        if (element != null)
                        {
                            set.Add(element);
                        }
                    }
                    catch
                    {
                    }
                }
                doc.Selection.Elements = set;
            }
            catch (Exception exception)
            {
                TaskDialog.Show(" SelectElements(Autodesk.Revit.UI.UIDocument doc, IList<Element> elements)  ", exception.ToString());
            }
        }
    }
}
