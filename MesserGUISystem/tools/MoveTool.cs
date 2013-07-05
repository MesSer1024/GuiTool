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
using MesserGUISystem.commands;

namespace MesserGUISystem.tools {
    class MoveTool : ToolBase {
        private const int DELAY_BEFORE_ACTION = 85;
        private Point _startPosition;
        private Point _lmbStartPosition;
        private int _timestampLmbDown;
        private UIElement _currentlyDraggedObject;
        private bool _limbo;
        private SolidColorBrush _solidColorBrush;
        private Shape _tmpShapeCopy; 

        public MoveTool() {
            type = utils.Globals.Tools.Move;
            _limbo = false;
        }

        public override void lmbBegin(Point point) {
            var foo = Stage.HittestItems(point);
            if (utils.Globals.isValidObject(foo)) {
                removeTemporaryObject();
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
            if (_currentlyDraggedObject == null) return;

            if (_limbo) {
                if (timeout(DELAY_BEFORE_ACTION)) {
                    _limbo = false;

                    //create a copy...
                    _solidColorBrush = new SolidColorBrush();
                    _solidColorBrush.Color = Color.FromArgb(45, 35, 35, 35);
                    _tmpShapeCopy = cloneObject(_currentlyDraggedObject);
                    _tmpShapeCopy.Fill = _solidColorBrush;
                    _tmpShapeCopy.StrokeThickness = 1;
                    _tmpShapeCopy.Stroke = Brushes.LightGray;
                    var size = VisualTreeHelper.GetContentBounds(_currentlyDraggedObject);
                    var offset = VisualTreeHelper.GetOffset(_currentlyDraggedObject);

                    _tmpShapeCopy.Width = size.Width;
                    _tmpShapeCopy.Height = size.Height;
                    Canvas.SetLeft(_tmpShapeCopy, offset.X);
                    Canvas.SetTop(_tmpShapeCopy, offset.Y);

                    Stage.addItem(_tmpShapeCopy);
                    var foo = Canvas.GetZIndex(_currentlyDraggedObject);
                    Canvas.SetZIndex(_tmpShapeCopy, Canvas.GetZIndex(_currentlyDraggedObject) - 1);

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

        private Shape cloneObject(UIElement used) {
            if (used is Rectangle) {
                return new Rectangle();
            } else if (used is Ellipse) {
                return new Ellipse();
            } else {
                Logger.error(String.Format("unknown type: {0}", used));
                throw new Exception("Unknown type");
            }
        }

        private double clamp(dynamic value, dynamic min, dynamic max) {
            return Math.Max(Math.Min(max, value), min);
        }

        public override void lmbEnd(Point point) {
            if (_currentlyDraggedObject == null) return;
            if (timeout(DELAY_BEFORE_ACTION)) {
                _limbo = false;
                var asdf = VisualTreeHelper.GetOffset(_currentlyDraggedObject);
                Controller.handle(new MoveItemEndCommand(_currentlyDraggedObject, _startPosition, new Point(asdf.X, asdf.Y)));

                removeTemporaryObject();
                _currentlyDraggedObject = null;
            }
        }

        private void removeTemporaryObject()
        {
            if (_tmpShapeCopy != null)
            {
                Stage.removeItem(_tmpShapeCopy);
                _tmpShapeCopy = null;
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
