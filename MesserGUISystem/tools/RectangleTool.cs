using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using MesserGUISystem.utils;
using System.Windows.Controls;
using System.Windows.Input;

namespace MesserGUISystem.tools {
    class RectangleTool : ToolBase {
        private Point _startPosition;
        private Point _current;
        private Rectangle _rectangle;
        private SolidColorBrush _solidColorBrush;

        public RectangleTool() {
            type = utils.Globals.Tools.Rectangle;
        }

        public override void lmbBegin(Point point) {
            _startPosition = point;
            _current = point;
            _rectangle = new Rectangle();
            MainWindow.Stage.Children.Add(_rectangle);
            _solidColorBrush = new SolidColorBrush();
            _solidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            _rectangle.Fill = _solidColorBrush;
            _rectangle.StrokeThickness = 1;
            _rectangle.Stroke = Brushes.Black;
        }

        public override void lmb(Point point) {
            var delta = point - _startPosition;
            _rectangle.Width = Math.Abs(delta.X);
            _rectangle.Height = Math.Abs(delta.Y);


            _current.X = Math.Min(point.X, _startPosition.X);
            _current.Y = Math.Min(point.Y, _startPosition.Y);
            Canvas.SetLeft(_rectangle, _current.X);
            Canvas.SetTop(_rectangle, _current.Y);
        }

        public override void lmbEnd(Point point) {
            Logger.log("drew a rectangle : " + _rectangle.RenderSize.ToString());
        }

        public override System.Windows.Input.Cursor getCursor() {
            return Cursors.Pen;
        }
    }
}
