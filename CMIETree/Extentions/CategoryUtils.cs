using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.Revit.DB;

namespace CMIETree.Extentions
{
    /// <summary>
    /// 处理Category的工具类
    /// </summary>
    public class CategoryUtils
    {
        /// <summary>
        /// 获取Element的BuiltinCategory
        /// </summary>
        /// <param name="element">需要获取BuiltInCategory的Element</param>
        /// <returns>BuiltInCategory</returns>
        public static BuiltInCategory GetBuiltInCategory(Element element)
        {
            Category category = element.Category;
            if (category == null) //若Category为空
            {
                return BuiltInCategory.INVALID;
            }

            BuiltInCategory builtInCategory = (BuiltInCategory) category.Id.IntegerValue;
            return builtInCategory;
        }

        /// <summary>
        /// 获取Element的BuiltinCategory
        /// </summary>
        /// <param name="elements">需要获取BuiltInCategory的Elements</param>
        /// <returns>List of BuiltInCategory</returns>
        public static List<BuiltInCategory> GetBuiltInCategories(List<Element> elements )
        {
            List<BuiltInCategory> builtInCategories = new List<BuiltInCategory>();
            foreach (Element element in elements)
            {
                Category category = element.Category;
                if (category == null) //若Category为空
                {
                    builtInCategories.Add(BuiltInCategory.INVALID);
                }
                else
                {
                    BuiltInCategory builtInCategory = (BuiltInCategory)category.Id.IntegerValue;
                    builtInCategories.Add(builtInCategory);
                }
            }

            return builtInCategories;
        }
    }
}
