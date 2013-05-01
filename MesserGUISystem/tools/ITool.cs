using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserGUISystem.tools {
    interface ITool {
        MesserGUISystem.utils.Globals.Tools type { get; }

        void lmb(System.Windows.Point point);
        void lmbBegin(System.Windows.Point point);
        void lmbEnd(System.Windows.Point point);

        System.Windows.Input.Cursor getCursor();
    }
}
