using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserUI
{
    public class Assert {
        public static bool Validate(object o, string errorDesc = "") {
            if (o == null) {
                throw new ValidateException("Assert.Validate failed: " + errorDesc);
                return false;
            }
            return true;
        }

        public static bool Validate(bool o, string errorDesc = "")
        {
            if (!o)
            {
                throw new ValidateException("Assert.Validate failed: " + errorDesc);
                return false;
            }
            return true;
        }

        public static void True(bool o, string errorDesc = "")
        {
            if (!o)
            {
                throw new AssertException("Assert.True (bool) failed: " + errorDesc);
            }
        }

        public static void True(object o, string errorDesc = "")
        {
            if (o == null)
            {
                throw new AssertException("Assert.True (NotNull) failed: " + errorDesc);
            }
        }

        public static void Fail(object o, string errorDesc = "") {
            throw new AssertException("Assert.Fail : " + errorDesc + ", object=" + o);
        }

        public static void NotNull(params object[] objects) {
            foreach (var i in objects) {
                Assert.True(i, "Null Item found");
            }
        }
    }
}
