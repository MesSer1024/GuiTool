using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MesserGUISystem.commands;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MesserGUISystem.utils {
    public static class Globals {

        public enum Tools {
            None,
            Rectangle,
            Move,
            Select,
            Ellipse,
            Text,
        }

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

        public static Bounds getBounds(UIElement ele) {
            var foo = ele as Shape;
            var pos = VisualTreeHelper.GetOffset(ele);
            Assert.True(foo);

            return new Bounds(pos.X, pos.Y, foo.Width, foo.Height);
        }
    }
}
