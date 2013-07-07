using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WpfCommon;

namespace MesserGUISystem.tools {
    class ToolBase : ITool {
        public ToolBase(Tools toolType = Tools.None) {
            type = toolType;
        }
        public Tools type { protected set; get; }


        public virtual void lmb(System.Windows.Point point) {
            Logger.log("unhandled lmb");
        }

        public virtual void lmbBegin(System.Windows.Point point) {
            Logger.log("unhandled lmbBegin");
        }

        public virtual void lmbEnd(System.Windows.Point point) {
            Logger.log("unhandled lmbEnd");
        }

        public virtual void destroyed() {
            Logger.log("unhandled destroyed");
        }

        public virtual System.Windows.Input.Cursor getCursor() {
            return Cursors.ArrowCD;
        }
    }
}
