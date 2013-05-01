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

namespace MesserGUISystem.tools {
    class MoveTool : ToolBase {
        private const int DELAY_BEFORE_ACTION = 85;
        private Point _startPosition;
        private Point _lmbStartPosition;
        private int _timestampLmbDown;
        private UIElement _selectedObject;
        private bool _limbo;

        public MoveTool() {
            type = utils.Globals.Tools.Move;
            _limbo = false;
        }

        public override void lmbBegin(Point point) {
            var foo = MainWindow.Stage.InputHitTest(point);
            if (foo is UIElement) {
                _selectedObject = foo as UIElement;
                _timestampLmbDown = Environment.TickCount;
                _limbo = true;
                var asdf = VisualTreeHelper.GetOffset(_selectedObject);
                _startPosition = new Point(asdf.X, asdf.Y);
                _lmbStartPosition = point;
            } else {
                Controller.handle(Controller.UserActions.OBJECT_CLICKED, null);
            }

            //MainWindow.Stage.Children.ea
            Logger.log("hitTestObject:" + foo);
        }

        public override void lmb(Point point) {
            if (_selectedObject == null) {
                return;
            }

            if (_limbo) {
                if (timeout(DELAY_BEFORE_ACTION)) {
                    _limbo = false;
                    lmb(point); //redo
                }
                return;
            } else {
                //Canvas.SetZIndex(_selectedObject, int.MinValue);
                
                double deltaX = point.X - _lmbStartPosition.X;
                double deltaY = point.Y - _lmbStartPosition.Y;
                Canvas.SetLeft(_selectedObject, clamp(_startPosition.X + deltaX, Controller.stageX, Controller.stageWidth - _selectedObject.DesiredSize.Width));
                //Canvas.SetLeft(_selectedObject, clamp(_startPosition.X + deltaX, Controller.stageX, Controller.stageWidth));
                Canvas.SetTop(_selectedObject, clamp(_startPosition.Y + deltaY, Controller.stageY, Controller.stageHeight - _selectedObject.DesiredSize.Height));
                //Canvas.SetTop(_selectedObject, 150);
                //MainWindow.Stage.Children.
            }
        }

        private double clamp(dynamic value, dynamic min, dynamic max) {
            return Math.Max(Math.Min(max, value), min);
        }

        public override void lmbEnd(Point point) {
            if (timeout(DELAY_BEFORE_ACTION)) {
                _limbo = false;
                Controller.handle(Controller.UserActions.OBJECT_CLICKED, _selectedObject);
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
