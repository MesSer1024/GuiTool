using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

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
            return e != null && e != MainWindow.Stage && e != MainWindow.Stage && e is UIElement;
        }

        public static bool isBigger(dynamic value, dynamic trueIfSmaller) {
            return Math.Abs(value) > Math.Abs(trueIfSmaller);
        }

        public static MainWindow Stage { get; set; }
    }
}
