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

namespace MesserGUISystem.wpf_magic
{
    class FillRectangle : Shape, IManualMUIObject
    {
        private SolidColorBrush _brush;
        private Pen _pen;
        private Rect _bounds;

        public FillRectangle(Rect bounds)
        {
            _brush = Brushes.Red;
            _pen = null; //no border
            //_pen = new Pen(Brushes.Yellow, 1.0);
            _bounds = bounds;
        }

        /// <summary>
        /// This will only affect the visuals of an element, for instance adding effects, or drawing more items on something...
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(_brush, _pen, _bounds);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                var geo = new RectangleGeometry(new Rect(_bounds.X, _bounds.Y, _bounds.Width, _bounds.Height));
                return geo;
            }
        }

        public Rect MUIBounds { 
            get { return _bounds; }
            set { 
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
