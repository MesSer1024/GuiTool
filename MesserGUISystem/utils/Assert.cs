using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MesserGUISystem.utils
{
    class Assert
    {
        public static bool Validate(bool o, string errorDesc = "")
        {
            if (!o)
            {
                Logger.error(errorDesc);
            }
            return o;
        }

        internal static void True(bool o, string errorDesc = "")
        {
            if (!o)
            {
                Logger.error(errorDesc);
            }
        }

        internal static void True(object o, string errorDesc = "")
        {
            if (o == null)
            {
                Logger.error(errorDesc);
            }
        }
    }
}
