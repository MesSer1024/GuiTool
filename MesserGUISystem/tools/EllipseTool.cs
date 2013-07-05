using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using MesserGUISystem.utils;
using System.Windows.Input;

namespace MesserGUISystem.tools {
    class EllipseTool : ToolBase {
        private Point _startPosition;
        private Point _current;
        private Ellipse _ellipse;
        private SolidColorBrush _solidColorBrush;

        public EllipseTool()
            : base(utils.Globals.Tools.Ellipse) {

        }

        public override void lmbBegin(Point point) {
            if (_ellipse != null)
                return;
            _startPosition = point;
            _current = point;
            _ellipse = new Ellipse();
            Stage.addItem(_ellipse);
            _solidColorBrush = new SolidColorBrush();
            _solidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            _ellipse.Fill = _solidColorBrush;
            _ellipse.StrokeThickness = 1;
            _ellipse.Stroke = Brushes.Black;
        }

        public override void lmb(Point point) {
            if (_ellipse == null) { 
                return; 
            }

            var delta = point - _startPosition;
            _ellipse.Width = Math.Abs(delta.X);
            _ellipse.Height = Math.Abs(delta.Y);


            _current.X = Math.Min(point.X, _startPosition.X);
            _current.Y = Math.Min(point.Y, _startPosition.Y);
            Canvas.SetLeft(_ellipse, _current.X);
            Canvas.SetTop(_ellipse, _current.Y);
        }

        public override void lmbEnd(Point point) {
            Logger.log("drew an ellipse : " + _ellipse.RenderSize.ToString());
        }

        public override System.Windows.Input.Cursor getCursor() {
            return Cursors.Pen;
        }
    }
}
