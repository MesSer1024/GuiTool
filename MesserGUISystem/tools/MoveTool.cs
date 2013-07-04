using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MesserGUISystem.utils;
using MesserGUISystem.logic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MesserGUISystem.tools {
    class MoveTool : ToolBase {
        private const int DELAY_BEFORE_ACTION = 85;
        private Point _startPosition;
        private Point _lmbStartPosition;
        private int _timestampLmbDown;
        private UIElement _currentlyDraggedObject;
        private bool _limbo;
        private SolidColorBrush _solidColorBrush;
        private Rectangle _tmpRectangle; 

        public MoveTool() {
            type = utils.Globals.Tools.Move;
            _limbo = false;
        }

        public override void lmbBegin(Point point) {
            var foo = MainWindow.Stage.InputHitTest(point);
            if (utils.Globals.isValidObject(foo)) {
                _currentlyDraggedObject = foo as UIElement;
                _timestampLmbDown = Environment.TickCount;
                _limbo = true;
                var asdf = VisualTreeHelper.GetOffset(_currentlyDraggedObject);
                _startPosition = new Point(asdf.X, asdf.Y);
                _lmbStartPosition = point;
            }

            Controller.handle(Controller.UserActions.OBJECT_CLICKED, foo as UIElement);
            //MainWindow.Stage.Children.ea
            Logger.log("hitTestObject:" + foo);
        }

        public override void lmb(Point point) {
            if (_currentlyDraggedObject == null) {
                return;
            }

            if (_limbo) {
                if (timeout(DELAY_BEFORE_ACTION)) {
                    _limbo = false;

                    //create a copy...
                    _solidColorBrush = new SolidColorBrush();
                    _solidColorBrush.Color = Color.FromArgb(45, 35, 35, 35);
                    _tmpRectangle = new Rectangle();
                    _tmpRectangle.Fill = _solidColorBrush;
                    _tmpRectangle.StrokeThickness = 1;
                    _tmpRectangle.Stroke = Brushes.LightGray;
                    var size = VisualTreeHelper.GetContentBounds(_currentlyDraggedObject);
                    var offset = VisualTreeHelper.GetOffset(_currentlyDraggedObject);

                    _tmpRectangle.Width = size.Width;
                    _tmpRectangle.Height = size.Height;
                    Canvas.SetLeft(_tmpRectangle, offset.X);
                    Canvas.SetTop(_tmpRectangle, offset.Y);

                    MainWindow.addItem(_tmpRectangle);
                    Canvas.SetZIndex(_tmpRectangle, int.MinValue);

                    Controller.handle(Controller.UserActions.MOVE_ITEM_BEGIN, _currentlyDraggedObject as UIElement);

                    lmb(point); //redo
                }
                return;
            } else {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) {
                    //only move in "biggest" axis
                    double deltaX = point.X - _lmbStartPosition.X;
                    double deltaY = point.Y - _lmbStartPosition.Y;
                    if (utils.Globals.isBigger(deltaX, deltaY)) {
                        Canvas.SetLeft(_currentlyDraggedObject, clamp(_startPosition.X + deltaX, Controller.stageX, Controller.stageWidth - _currentlyDraggedObject.DesiredSize.Width));
                        Canvas.SetTop(_currentlyDraggedObject, _startPosition.Y);
                    } else {
                        Canvas.SetLeft(_currentlyDraggedObject, _startPosition.X);
                        Canvas.SetTop(_currentlyDraggedObject, clamp(_startPosition.Y + deltaY, Controller.stageY, Controller.stageHeight - _currentlyDraggedObject.DesiredSize.Height));
                    }
                } else {
                    double deltaX = point.X - _lmbStartPosition.X;
                    double deltaY = point.Y - _lmbStartPosition.Y;
                    Canvas.SetLeft(_currentlyDraggedObject, clamp(_startPosition.X + deltaX, Controller.stageX, int.MaxValue));
                    Canvas.SetTop(_currentlyDraggedObject, clamp(_startPosition.Y + deltaY, Controller.stageY, int.MaxValue));
                }
            }
        }

        private double clamp(dynamic value, dynamic min, dynamic max) {
            return Math.Max(Math.Min(max, value), min);
        }

        public override void lmbEnd(Point point) {
            if (timeout(DELAY_BEFORE_ACTION)) {
                _limbo = false;
                if (_tmpRectangle != null) {
                    MainWindow.removeItem(_tmpRectangle);
                    _tmpRectangle = null;
                }
                Controller.handle(Controller.UserActions.MOVE_ITEM_END, _currentlyDraggedObject as UIElement);

                _currentlyDraggedObject = null;
            }
        }

        private bool timeout(int timeout) {
            return Environment.TickCount - _timestampLmbDown >= timeout;
        }

        public override System.Windows.Input.Cursor getCursor() {
            return Cursors.Arrow;
        }

    }
}
