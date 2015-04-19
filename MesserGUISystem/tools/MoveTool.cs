using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MesserGUISystem.logic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfCommon;
using WpfCommon.commands;
using MesserGUISystem.wpf_magic;
using MesserUI;


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
        private int _originalZIndex;

        public MoveTool() {
            type = Tools.Move;
            _limbo = false;
        }

        public override void lmbBegin(Point point) {
            var foo = Stage.HittestItems(point);
            Controller.handle(UserActions.MUIELEMENT_DESELECTED);
            resetMovedObject();

            if (Globals.isValidObject(foo)) {
                removeTemporaryObject();
                _currentlyDraggedObject = foo as UIElement;
                _timestampLmbDown = Environment.TickCount;
                _limbo = true;
                var bar = _currentlyDraggedObject as IManualMUIObject;

                _startPosition = new Point(bar.MUIBounds.X, bar.MUIBounds.Y);
                _lmbStartPosition = point;
                setMovedObject();

                Controller.handle(UserActions.MUIELEMENT_SELECTED_VALID, foo);
            }
            //MainWindow.Stage.Children.ea
            Logger.log("hitTestObject:" + foo);
        }

        private void resetMovedObject()
        {
            if (_currentlyDraggedObject != null)
            {
                Canvas.SetZIndex(_currentlyDraggedObject, _originalZIndex);
            }
            _originalZIndex = 0;
            _currentlyDraggedObject = null;
        }

        private void setMovedObject()
        {
            _originalZIndex = Canvas.GetZIndex(_currentlyDraggedObject);
            Canvas.SetZIndex(_currentlyDraggedObject, 10000);

            //_currentlyDraggedObject.InvalidateVisual();
            //_currentlyDraggedObject.InvalidateArrange();
            //_currentlyDraggedObject.InvalidateMeasure();
        }

        public override void lmb(Point point) {
            if (_currentlyDraggedObject == null) return;

            var bar = _currentlyDraggedObject as IManualMUIObject;
            if (_limbo)
            {
                if (timeout(DELAY_BEFORE_ACTION)) {
                    _limbo = false;

                    //create a copy...
                    _solidColorBrush = new SolidColorBrush();
                    _solidColorBrush.Color = Color.FromArgb(45, 35, 35, 35);
                    _tmpShapeCopy = cloneObject(_currentlyDraggedObject);
                    _tmpShapeCopy.Fill = _solidColorBrush;
                    _tmpShapeCopy.StrokeThickness = 1;
                    _tmpShapeCopy.Stroke = Brushes.LightGray;

                    var bounds = bar.MUIBounds;

                    _tmpShapeCopy.Width = bounds.Width;
                    _tmpShapeCopy.Height = bounds.Height;
                    Canvas.SetLeft(_tmpShapeCopy, bounds.X);
                    Canvas.SetTop(_tmpShapeCopy, bounds.Y);

                    Stage.addItem(_tmpShapeCopy);
                    var foo = Canvas.GetZIndex(_currentlyDraggedObject);
                    Canvas.SetZIndex(_tmpShapeCopy, Canvas.GetZIndex(_currentlyDraggedObject) - 1);

                    Controller.handle(UserActions.MOVE_ITEM_BEGIN, _currentlyDraggedObject as Visual);

                    lmb(point); //redo
                }
                return;
            } else {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) {
                    //only move in "biggest" axis
                    double deltaX = point.X - _lmbStartPosition.X;
                    double deltaY = point.Y - _lmbStartPosition.Y;
                    var bounds = bar.MUIBounds;
                    if (Globals.isBigger(deltaX, deltaY)) {
                        bounds.X = clamp(_startPosition.X + deltaX, WpfController.stageX, WpfController.stageWidth - _currentlyDraggedObject.DesiredSize.Width);
                        bounds.Y = _startPosition.Y;
                        bar.MUIBounds = bounds;
                    } else {
                        bounds.X = _startPosition.X;
                        bounds.Y = clamp(_startPosition.Y + deltaY, WpfController.stageY, WpfController.stageHeight - _currentlyDraggedObject.DesiredSize.Height);
                        bar.MUIBounds = bounds; 
                    }
                } else {
                    var bounds = bar.MUIBounds;
                    double deltaX = point.X - _lmbStartPosition.X;
                    double deltaY = point.Y - _lmbStartPosition.Y;
                    bounds.X = clamp(_startPosition.X + deltaX, WpfController.stageX, int.MaxValue);
                    bounds.Y = clamp(_startPosition.Y + deltaY, WpfController.stageY, int.MaxValue);

                    bar.MUIBounds = bounds;
                }
            }
        }

        Rect castToRect(MUIRectangle r)
        {
            return new Rect(r.X, r.Y, r.W, r.H);
        }

        public override void destroyed() {
            if (_currentlyDraggedObject != null && _tmpShapeCopy != null) {
                var b = Globals.getBounds(_tmpShapeCopy);
                var foo = _currentlyDraggedObject as IManualMUIObject;
                foo.MUIBounds = castToRect(b);
            }
            removeTemporaryObject();
            resetMovedObject();
        }

        private Shape cloneObject(UIElement used) {
            if (used is FillRectangle) {
                return new Rectangle();
            } else if (used is Ellipse) {
                return new Ellipse();
            } else if (used is LabelRectangle) {
                return new Rectangle();
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

            var bar = _currentlyDraggedObject as IManualMUIObject;
            Controller.handle(new MoveItemEndCommand(_currentlyDraggedObject, _startPosition, new Point(bar.MUIBounds.X, bar.MUIBounds.Y)));

            removeTemporaryObject();
            resetMovedObject();
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
