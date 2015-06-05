using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMIETree.Collectors
{
    /// <summary>
    ///  This is the base class of data collector objects.  We'll just establish
    ///  the protocol for derived classes.  This class is abstract and cannot be
    ///  instantiated except by derived classes.  A Snoop.Collector class is passed
    ///  an object and manually fills up an ArrayList of Snoop.Data objects.  The list
    ///  is then displayed in a Form (or can be sent elsewhere if need be).  For the most
    ///  part, the Collector is just the initial catch point.  CollectorExts are responsible
    ///  for knowing about certain types of objects.
    /// </summary>

    public class Collector
    {
        // Define an event so that we can broadcast out to any Collector Extension
        // objects to fill in data that we don't know about.  The Event is static
        // to the base class, so all instances of all derived classes will inherit
        // the ability to be extended.
        public delegate void CollectorExt(object sender, CollectorEventArgs e);
        public static event CollectorExt OnCollectorExt;

        protected ArrayList m_dataObjs = new ArrayList();

        public
        Collector()
        {
        }

        public ArrayList
        Data()
        {
            return m_dataObjs;
        }

        // Apparently, you can't call the Event from outside the actual class that defines it.
        // So, we'll simply wrap it.  Now all derived classes can broadcast the event.
        protected void
        FireEvent_CollectExt(object objToSnoop)
        {
            if (OnCollectorExt != null)
                OnCollectorExt(this, new CollectorEventArgs(objToSnoop));
        }
    }
}
