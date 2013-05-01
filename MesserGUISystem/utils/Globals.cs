using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserGUISystem.utils {
    public static class Globals {

        public enum Tools {
            None,
            Rectangle,
            Move,
            Ellipse,
        }

        public static bool anyEquals(Object value, params Object[] checks) {
            for (int i = 0; i < checks.Length; i++) {
                if (value.Equals(checks[i])) {
                    return true;
                }
            }
            return false;
        }
    }
}
