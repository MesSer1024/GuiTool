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
using MesserGUISystem.commands;
using MesserGUISystem.logic;

namespace MesserGUISystem.tools {
    class RectangleTool : ToolBase {
        private Point _startPosition;
        private Point _current;
        private Rectangle _shape;
        private SolidColorBrush _solidColorBrush;

        public RectangleTool()
            : base(Tools.Rectangle) {
        }

        public override void lmbBegin(Point point) {
            if (_shape != null)
                return;
            _startPosition = point;
            _current = point;
            _shape = new Rectangle();
            Stage.addItem(_shape);
            _solidColorBrush = new SolidColorBrush();
            _solidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
            _shape.Fill = _solidColorBrush;
            _shape.StrokeThickness = 1;
            _shape.Stroke = Brushes.Black;
        }

        public override void lmb(Point point) {
            if (_shape == null)
                return;

            var delta = point - _startPosition;
            _shape.Width = Math.Abs(delta.X);
            _shape.Height = Math.Abs(delta.Y);


            _current.X = Math.Min(point.X, _startPosition.X);
            _current.Y = Math.Min(point.Y, _startPosition.Y);
            Canvas.SetLeft(_shape, _current.X);
            Canvas.SetTop(_shape, _current.Y);
        }

        public override void lmbEnd(Point point) {
            if (_shape == null)
                return;
            Logger.log("drew a rectangle : " + _shape.RenderSize.ToString());
        }

        public override System.Windows.Input.Cursor getCursor() {
            return Cursors.Pen;
        }

        public override void destroyed() {
            if (_shape != null) {
                Controller.handle(new CreateMUIElementCommand(_shape));
            }
        }
    }
}
