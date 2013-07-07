using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserUI {
    class MesserUIExceptions : Exception {
        public MesserUIExceptions(string s)
            : base(s) {

        }
    }
    class AssertException : MesserUIExceptions {
        public AssertException(string s)
            : base(s) {

        }
    }

    class FatalException : MesserUIExceptions {
        public FatalException(string s)
            : base(s) {

        }
    }

    class ValidateException : MesserUIExceptions {
        public ValidateException(string s)
            : base(s) {

        }

    }
}
