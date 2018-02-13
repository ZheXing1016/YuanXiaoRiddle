using System;
using System.Collections.Generic;
using System.Text;

namespace CommonClass
{
    class getconnstr
    {
        private static string _connstr;
        public static string connstr
        {
            set { _connstr = value; }
            get { return _connstr; }
        }

        
    }
}
