using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserUI {
    public static class MUIUtils {
        public static MUIBase createInstanceFromType(MUITypes type, MUIRectangle bounds) {
            MUIBase val = null;

            switch (type) {
                case MUITypes.Fill:
                    val = new MUIFill();
                    break;
                case MUITypes.Text:
                    val = new MUIText();
                    break;
            }
            Fatal.True(val, String.Format("Could not create instance from type {0}", type));
            val.Bounds = bounds;
            return val;
        }
    }
}
