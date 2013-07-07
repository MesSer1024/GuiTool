using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MesserUI;

namespace MesserGUISystem.utils {
    public enum Tools {
        None,
        Rectangle,
        Move,
        Select,
        Ellipse,
        Text,
    }

    public enum UserActions {
        MOVE_TOOL,
        RESIZE_TOOL,
        RECTANGLE_TOOL,
        ELLIPSE_TOOL,
        LMB_DOWN,
        LMB_RELEASE,
        LMB_DOWN_MOUSE_MOVED,
        OBJECT_CLICKED,
        MOVE_ITEM_BEGIN,
        MOVE_ITEM_END,
        USER_UNDO,
        USER_REDO,
        USER_RESIZE_ITEM,
        USER_PRESS_ESCAPE_TEXTBOX,
        USER_REFRESH_PROPERTIES,
        USER_CREATE_ITEM,
    }

    public static class Globals {
        public static bool anyEquals(Object value, params Object[] checks) {
            for (int i = 0; i < checks.Length; i++) {
                if (value.Equals(checks[i])) {
                    return true;
                }
            }
            return false;
        }

        public static bool isValidObject(object e) {
            return e != null && e != Stage.Area && e is UIElement;
        }

        public static bool isBigger(dynamic value, dynamic trueIfSmaller) {
            return Math.Abs(value) > Math.Abs(trueIfSmaller);
        }

        public static void updateUi(Action fn) {
            Stage.Area.Dispatcher.Invoke(fn);
        }

        public static MUIRectangle getBounds(UIElement ele) {
            var foo = ele as Shape;
            var pos = VisualTreeHelper.GetOffset(ele);
            Assert.True(foo);

            return new MUIRectangle(pos.X, pos.Y, foo.Width, foo.Height);
        }

        public static MUIPoint getCenter(MUIRectangle r) {
            return new MUIPoint(r.X + r.Width / 2, r.Y + r.Height / 2);
        }

        public static MUITypes getMUIType(UIElement ele) {
            if (ele is Rectangle) {
                return MUITypes.Fill;
            } else if (ele is Ellipse) {
                return MUITypes.Fill;
            }

            Assert.Fail(ele, "Unknown element type");
            return MUITypes.None;
        }

        public static dynamic clamp(dynamic value, dynamic min, dynamic max) {
            return Math.Max(min, Math.Min(value, max));
        }
    }
}
