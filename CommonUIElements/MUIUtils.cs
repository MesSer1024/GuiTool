using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserUI {
    public static class MUIUtils {
        public static MUIElement createInstanceFromType(MUITypes type, MUIRectangle bounds) {
            MUIElement val = null;

            switch (type) {
                case MUITypes.Fill:
                    val = new MUIFill();
                    break;
                case MUITypes.Text:
                    val = new MUIText();
                    break;
                default:
                    throw new NotImplementedException("Unvalid type: " + type);
                    break;
            }            

            val.Bounds = bounds;
            return val;
        }

        public static MUIElement createInstanceFromType(MUITypes type) {
            return createInstanceFromType(type, new MUIRectangle(0, 0, 250, 250));
        }
    }
}
