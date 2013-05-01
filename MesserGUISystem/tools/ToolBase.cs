using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserGUISystem.utils;
using System.Windows.Input;

namespace MesserGUISystem.tools {
    class ToolBase : ITool {
        public ToolBase() {
            type = utils.Globals.Tools.None;
        }
        public MesserGUISystem.utils.Globals.Tools type { protected set; get; }


        public virtual void lmb(System.Windows.Point point) {
            Logger.log("unhandled lmb");
        }

        public virtual void lmbBegin(System.Windows.Point point) {
            Logger.log("unhandled lmbBegin");
        }

        public virtual void lmbEnd(System.Windows.Point point) {
            Logger.log("unhandled lmbEnd");
        }

        public virtual System.Windows.Input.Cursor getCursor() {
            return Cursors.ArrowCD;
        }
    }
}
