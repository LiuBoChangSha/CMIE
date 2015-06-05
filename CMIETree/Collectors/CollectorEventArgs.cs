using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMIETree.Collectors
{
    /// <summary>
    /// Simple class to package up EventArgs for any Snoop Collector Extensions.
    /// </summary>

    public class CollectorEventArgs : System.EventArgs
    {
        private object m_objToSnoop;

        public
        CollectorEventArgs(object objToSnoop)
        {
            m_objToSnoop = objToSnoop;
        }

        public object ObjToSnoop
        {
            get { return m_objToSnoop; }
        }
    }
}
