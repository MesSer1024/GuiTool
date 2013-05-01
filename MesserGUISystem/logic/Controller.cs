using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MesserGUISystem.tools;
using MesserGUISystem.utils;
using System.Windows.Media;

namespace MesserGUISystem.logic {
    class Controller {
        private MainWindow _view;
        private Canvas _stage;
        private ITool _currentTool;
        private static Controller _instance;
        private Controller(){}

        public static double stageX {
            get {return 0;}
        }

        public static double stageY {
            get { return 0; }
        }

        public static double stageWidth {
            get {
                return _instance._stage.ActualWidth;
            }
        }

        public static double stageHeight {
            get {
                return _instance._stage.ActualHeight;
            }
        }

        public enum UserActions {
            MOVE_TOOL,
            RESIZE_TOOL,
            RECTANGLE_TOOL,
            ELLIPSE_TOOL,
            CANVAS_LMB_DOWN,
            CANVAS_LMB_RELEASE,
            CANVAS_LMB_DOWN_MOVED,
            OBJECT_CLICKED,

        }

        static void onKeyDown(object sender, KeyEventArgs e) {
            Logger.log("key pressed:" + e.Key.ToString());
            if (Globals.anyEquals(e.Key, Key.Oem5, Key.OemTilde)) {
                Console.Write(Logger.flush());
            } else {
                switch (e.Key) {
                    case Key.V:
                        _instance._view.moveTool.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));                        
                        break;
                    case Key.Q:
                        _instance._view.resizeTool.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));                        
                        break;
                    case Key.R:
                        _instance._view.createRectangle.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));                        
                        break;
                    case Key.E:
                        _instance._view.createEllipse.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));                        
                        break;
                }
            }
        }

        public static void handle(UserActions action, Object e = null) {
            switch (action) {
                case UserActions.MOVE_TOOL:
                    _instance.moveToolClicked();
                    break;
                case UserActions.RESIZE_TOOL:
                    _instance.resizeToolClicked();
                    break;
                case UserActions.RECTANGLE_TOOL:
                    _instance.rectangleToolClicked();
                    break;
                case UserActions.ELLIPSE_TOOL:
                    _instance.ellipseToolClicked();
                    break;
                case UserActions.CANVAS_LMB_DOWN:
                    _instance.canvasLmbDown(e as MouseEventArgs);
                    break;
                case UserActions.CANVAS_LMB_RELEASE:
                    _instance.canvasLmbRelease(e as MouseEventArgs);
                    break;
                case UserActions.CANVAS_LMB_DOWN_MOVED:
                    _instance.canvasLmbDownMoved(e as MouseEventArgs);
                    break;
                case UserActions.OBJECT_CLICKED:
                    _instance.onObjectSelected(e as UIElement);
                    break;
                default:
                    Logger.error("Unhandled action:" + action);
                    break;
            }
        }

        private void moveToolClicked() {
            changeTool(new MoveTool());
        }

        private void resizeToolClicked() {
            changeTool(new ToolBase());
        }

        private void rectangleToolClicked() {
            changeTool(new RectangleTool());            
        }

        private void ellipseToolClicked() {
            changeTool(new EllipseTool());            
        }

        private void changeTool(ITool tool) {
            _currentTool = tool;
            Mouse.OverrideCursor = _currentTool.getCursor();
        }

        private void canvasLmbDown(MouseEventArgs e) {
            _currentTool.lmbBegin(e.GetPosition(_stage));
        }

        private void canvasLmbRelease(MouseEventArgs e) {
            _currentTool.lmbEnd(e.GetPosition(_stage));
        }

        private void canvasLmbDownMoved(MouseEventArgs e) {
            _currentTool.lmb(e.GetPosition(_stage));
        }

        private void onObjectSelected(UIElement element) {
            if (element == null) {
                //deselect stuff
            } else {

            }
        }


        internal static void initialize(MainWindow view, Canvas stage) {
            if (_instance != null)
                throw new Exception("Can only initialize Controller once");
            _instance = new Controller();
            _instance._view = view;
            _instance._stage = stage;
            _instance._view.KeyDown += new KeyEventHandler(onKeyDown);
            _instance._currentTool = new ToolBase();
        }
    }
}
