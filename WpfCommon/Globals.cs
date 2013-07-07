using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using MesserUI;
using WpfCommon;
using System.Windows.Documents;
using MesserGUISystem.logic;

namespace WpfCommon {
    public static class Globals {
        public static bool anyEquals(Object value, params Object[] checks) {
            for (int i = 0; i < checks.Length; i++) {
                if (value.Equals(checks[i])) {
                    return true;
                }
            }
            return false;
        }

        public static bool isValidObject(object e) {
            //return e != null && e != Stage.Area && e is UIElement;
            return e != null && WPFBridgeDatabase.Instance.isMUIElement(e as UIElement);
        }

        public static bool isBigger(dynamic value, dynamic trueIfSmaller) {
            return Math.Abs(value) > Math.Abs(trueIfSmaller);
        }

        public static void updateUi(Action fn) {
            Stage.Area.Dispatcher.Invoke(fn);
        }

        public static MUIRectangle getBounds(UIElement ele) {
            var pos = VisualTreeHelper.GetOffset(ele);
            var foo = ele as FrameworkElement;
            Assert.NotNull(foo);
            return new MUIRectangle(pos.X, pos.Y, foo.Width, foo.Height);
        }

        public static MUIPoint getCenter(MUIRectangle r) {
            return new MUIPoint(r.X + r.Width / 2, r.Y + r.Height / 2);
        }

        public static Point getCenter(Rect r) {
            return new Point(r.X + r.Width / 2, r.Y + r.Height / 2);
        }

        public static MUITypes getMUIType(UIElement ele) {
            if (ele is Rectangle) {
                return MUITypes.Fill;
            } else if (ele is Ellipse) {
                return MUITypes.Fill;
            } else if (ele is TextBox || ele is Label || ele is IManualMUIObject) {
                return MUITypes.Text;
            }

            Assert.Fail(ele, "Unknown element type");
            return MUITypes.None;
        }

        public static dynamic clamp(dynamic value, dynamic min, dynamic max) {
            return Math.Max(min, Math.Min(value, max));
        }

        public static void clearAdornedElements(UIElement item) {
            if (item != null) {
                var myAdornerLayer = AdornerLayer.GetAdornerLayer(item);
                if (myAdornerLayer != null) {
                    var adorners = myAdornerLayer.GetAdorners(item);
                    if (adorners != null) {
                        foreach (var adorner in adorners) {
                            myAdornerLayer.Remove(adorner);
                        }
                    }
                }
            }

        }

        public static MUIElement getMUIElement(UIElement ele) {
            return WPFBridgeDatabase.Instance.getMUIElement(ele);
        }
    }
}
