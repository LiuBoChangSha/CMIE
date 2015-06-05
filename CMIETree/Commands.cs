using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;

/**************************************************************************
  Written By Bo Liu                                      
  October 2014                                                          
  http://www.cmie.cn 
**************************************************************************/

namespace CMIETree
{
    ///// <summary>
    ///// 显示窗口
    ///// </summary>
    //[Transaction(TransactionMode.Manual)]
    //[Regeneration(RegenerationOption.Manual)]
    //public class CmdShowDialog : IExternalCommand
    //{

    //    public Result Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
    //    {
    //        try
    //        {
    //            //Document doc = commandData.Application.ActiveUIDocument.Document;
    //            //Transaction trans = new Transaction(doc, "Select Categories");
    //            //trans.Start();
    //            System.Windows.Forms.Form paraForm = new SetParamentersForm(commandData, message, elements);
    //            DialogResult result = paraForm.ShowDialog(); // 显示模态窗口
    //            //trans.Commit();
    //            if (result != DialogResult.OK)
    //            {
    //                return Result.Cancelled;
    //            }
                

    //            return Result.Succeeded;
    //        }
    //        catch (Exception ex)
    //        {
    //            TaskDialog.Show("错误", ex.ToString());
    //            return Result.Failed;
    //        }

    //    }
    //}

    /// <summary>
    /// TreeView Command, 执行TreeView命令
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CmdTreeView: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            try
            {
                UIApplication uiApplication = commandData.Application;

                SetParamentersForm paraForm = new SetParamentersForm(commandData, message, elements);

                DialogResult result = paraForm.ShowDialog();
                List<BuiltInCategory> categoryList = paraForm.SelectedCategories;
                if (result != DialogResult.OK)
                {
                    return Result.Cancelled;
                }
                //paraForm.Close();
                Transaction Trans = new Transaction(uiApplication.ActiveUIDocument.Document, "CMIE TreeView");
                Trans.Start();
                TreeViewData treeViewData = new TreeViewData(commandData, message, elements, categoryList);
                TvForm tvForm = new TvForm(treeViewData, uiApplication);
                uiApplication.ActiveUIDocument.Document.Regenerate();
                tvForm.Show(new RevitOwnerWindowHandler()); //显示非模态窗口

                Trans.Commit();
                //Transaction Trans = new Transaction(uiApplication.ActiveUIDocument.Document, "CMIE TreeView");
                //Trans.Start();
                //        TreeViewData treeViewData = new TreeViewData(commandData, message, elements);
                //        TvForm tvForm = new TvForm(treeViewData);
                //        tvForm.Show(new RevitOwnerWindowHandler()); //显示非模态窗口

                        //Trans.Commit();
                        //tvForm.ShowDialog(); //显示模态窗口


                    //}
                    
                
                
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                TaskDialog.Show("错误", message);
                return Result.Failed;
            }
        }
    }



}
