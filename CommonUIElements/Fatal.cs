using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserUI {
    class Fatal {
        public static void True(bool o, string errorDesc = "") {
            if (!o) {
                throw new FatalException("Fatal.True (bool) failed: " + errorDesc);
            }
        }

        public static void True(object o, string errorDesc = "") {
            if (o == null) {
                throw new FatalException("Fatal.True (NotNull) failed: " + errorDesc);
            }
        }
    }
}
