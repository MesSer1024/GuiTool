using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using MesserUI;

namespace WpfCommon {
    public interface IManualMUIObject {
        Rect  MUIBounds { get; set; }
        Shape ShapeWPF { get; }
    }
}
