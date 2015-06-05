using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CMIETree.Extentions
{
    public class ElementInfo
    {
        public static void GetFamilyAndTypeInfo(Autodesk.Revit.DB.Element element, ref string familyName, ref string typeName, ref string familyTypeName, ref Autodesk.Revit.DB.Mechanical.Space space)
        {
            if (element is FamilyInstance)
            {
                FamilyInstance instance = element as FamilyInstance;
                Autodesk.Revit.DB.Element objectTypeAsElement = GetObjectTypeAsElement(instance, null);
                familyName = instance.Symbol.Family.Name;
                typeName = objectTypeAsElement.Name;
                Parameter parameter = objectTypeAsElement.get_Parameter(BuiltInParameter.SYMBOL_FAMILY_AND_TYPE_NAMES_PARAM);
                if (parameter != null)
                {
                    familyTypeName = parameter.AsString();
                }
                try
                {
                    if (instance.Space != null)
                    {
                        space = instance.Space;
                    }
                }
                catch (Exception)
                {
                    //DebugUtilities.WriteLine(" aaaa  ", exception);
                    //TaskDialog.Show("GetFamilyAndTypeInfo", "GetFamilyAndTypeInfo(Autodesk.Revit.DB.Element element)" + exception.ToString());
                }
            }
            else
            {
                Parameter parameter2 = element.get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM);
                if (parameter2 != null)
                {
                    Parameter parameter3 = null;
                    ElementId elementId = parameter2.AsElementId();
                    Autodesk.Revit.DB.Element element3 = GetElement(element.Document, elementId);
                    if (element3 != null)
                    {
                        parameter3 = element3.get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME);
                        if (parameter3 != null)
                        {
                            familyName = parameter3.AsString();
                        }
                        parameter3 = element3.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_NAME);
                        if (parameter3 != null)
                        {
                            typeName = parameter3.AsString();
                        }
                        if (!string.IsNullOrEmpty(typeName) && !string.IsNullOrEmpty(familyName))
                        {
                            familyTypeName = familyName + " : " + typeName;
                        }
                    }
                    else
                    {
                        try
                        {
                            parameter3 = element.get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME);
                            if (parameter3 != null)
                            {
                                familyName = parameter3.AsString();
                            }
                        }
                        catch
                        {
                        }
                        try
                        {
                            parameter3 = element.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_NAME);
                            if (parameter3 != null)
                            {
                                typeName = parameter3.AsString();
                            }
                        }
                        catch
                        {
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(typeName) && !string.IsNullOrEmpty(familyName))
                            {
                                familyTypeName = familyName + " : " + typeName;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public static string GetFamilyAndTypeName(Autodesk.Revit.DB.Element element)
        {
            string familyName = string.Empty;
            string typeName = string.Empty;
            string familyTypeName = string.Empty;
            Autodesk.Revit.DB.Mechanical.Space space = null;
            try
            {
                GetFamilyAndTypeInfo(element, ref familyName, ref typeName, ref familyTypeName, ref space);
            }
            catch (Exception exception)
            {
                //DebugUtilities.WriteLine(" GetFamilyAndTypeName(Autodesk.Revit.DB.Element element)  ", exception);
                TaskDialog.Show("GetFamilyAndTypeName", "GetFamilyAndTypeName(Autodesk.Revit.DB.Element element)" + exception.ToString());
            }
            return familyTypeName;
        }

        public static Autodesk.Revit.DB.Element GetObjectTypeAsElement(Autodesk.Revit.DB.Element element, Autodesk.Revit.DB.Document doc = null)
        {
            try
            {
                ElementId typeId = element.GetTypeId();
                if (doc == null)
                {
                    return GetElement(element.Document, typeId);
                }
                return GetElement(element.Document, typeId);
            }
            catch
            {
            }
            return null;
        }

        public static Autodesk.Revit.DB.Element GetElement(Autodesk.Revit.DB.Document oDocument, ElementId elementId)
        {
            try
            {
                return oDocument.GetElement(elementId);
            }
            catch
            {
                return null;
            }
        }
    }
}
