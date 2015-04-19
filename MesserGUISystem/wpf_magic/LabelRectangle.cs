using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using WpfCommon;
using System.Globalization;

namespace MesserGUISystem.wpf_magic {
    public class LabelRectangle : Shape, IManualMUIObject {
        private SolidColorBrush _brush;
        private Pen _pen;
        private Rect _bounds;
        private FormattedText _text;

        public LabelRectangle(string s, Rect bounds) {
            _brush = new SolidColorBrush(Color.FromArgb(40, 110,110,110));
            _pen = new Pen(Brushes.White, 0.25);
            _bounds = bounds;
            var family = new Typeface(new FontFamily("arial"), FontStyles.Normal, FontWeights.SemiBold, FontStretches.SemiCondensed);
            _text = new FormattedText(s, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, family, 14, Brushes.Black);
        }

        protected override void OnRender(DrawingContext drawingContext) {
            //need to see if bounds are still valid...
            drawingContext.DrawRectangle(_brush, _pen, _bounds);
            drawingContext.DrawText(_text, Globals.getCenter(_bounds));
        }

        protected override Geometry DefiningGeometry {
            get {
                var geo = new RectangleGeometry(new Rect(_bounds.X, _bounds.Y, _bounds.Width, _bounds.Height));
                return geo; 
            }
        }

        public Rect MUIBounds
        {
            get { return _bounds; }
            set
            {
                _bounds = value;
                InvalidateVisual();
                InvalidateMeasure();
            }
        }

        public Shape ShapeWPF
        {
            get { return this; }
        }
    }
}
