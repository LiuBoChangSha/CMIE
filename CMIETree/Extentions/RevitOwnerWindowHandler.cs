namespace CMIETree
{
    using System;
    using System.Windows.Forms;
    using Autodesk.Windows;

    public class RevitOwnerWindowHandler : IWin32Window
    {
        private IntPtr _hwnd = ComponentManager.ApplicationWindow;

        public IntPtr Handle
        {
            get
            {
                return this._hwnd;
            }
        }
    }
}

