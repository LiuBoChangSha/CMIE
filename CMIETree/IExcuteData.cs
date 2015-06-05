/**************************************************************************
  Written By Bo Liu                                       
  October 2014                                                       
  http://www.cmie.cn 
**************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace CMIETree
{
    public interface IExcuteData
    {
        void SetData();
        void Start();
    }

    /// <summary>
    /// 定义ExcuteData的抽象类
    /// </summary>
     public abstract class  ExcuteData
    {
        protected ExternalCommandData m_commandData;
        protected string m_message;
        protected ElementSet m_elementSet;

         /// <summary>
         /// 构造函数，获取用户文档。
         /// </summary>
         /// <param name="uiApplication">用户ui应用</param>
        protected ExcuteData(ExternalCommandData commandData, string msg, ElementSet elementSet)
         {
             m_commandData = commandData;
             m_message = msg;
             m_elementSet = elementSet;
         }

         /// <summary>
         /// 设置Data，一般从WinForm中在用户点击确定时调用。
         /// </summary>
         public abstract void SetData();

         /// <summary>
         /// 主函数，实现功能，一般在Command类Excute方法中调用。
         /// </summary>
         public abstract void Start();
    }
}
