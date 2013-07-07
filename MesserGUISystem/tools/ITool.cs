﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserGUISystem.utils;

namespace MesserGUISystem.tools {
    interface ITool {
        Tools type { get; }

        void lmb(System.Windows.Point point);
        void lmbBegin(System.Windows.Point point);
        void lmbEnd(System.Windows.Point point);
        void destroyed();

        System.Windows.Input.Cursor getCursor();
    }
}
