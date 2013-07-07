using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace WpfCommon {
    public static class Stage {
        public static Canvas Area { get; set; }
        private static int _counter = 0;

        //public static bool containsItem(UIElement o) {
        //    return Area.Children.Contains(o);
        //}

        public static void addItem(UIElement o) {
            Area.Children.Add(o);
            Canvas.SetZIndex(o, _counter++);
        }

        public static void removeItem(UIElement o) {
            Area.Children.Remove(o);
        }

        public static object HittestItems(Point point) {
            return Area.InputHitTest(point);
        }
    }
}
