using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

namespace MesserGUISystem.utils {
    static class Stage {
        public static Canvas Area { get; set; }
        private static int _counter = 0;

        public static void addItem(UIElement o) {
            Area.Children.Add(o);
            Canvas.SetZIndex(o, _counter++);
        }

        public static void removeItem(UIElement o) {
            Area.Children.Remove(o);
        }

        internal static object HittestItems(Point point) {
            return Area.InputHitTest(point);
        }
    }
}
