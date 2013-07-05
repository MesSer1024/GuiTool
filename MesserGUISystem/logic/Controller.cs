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
    public class Controller {
        private MainWindow _view;
        private Canvas _stage;
        private ITool _currentTool;
        private static Controller _instance;

        public static Controller Instance {
            get { return Controller._instance; }
        }
        private Controller(){}

        private static List<IObserver> _observers = new List<IObserver>();

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

        public enum UserActions
        {
            MOVE_TOOL,
            RESIZE_TOOL,
            RECTANGLE_TOOL,
            ELLIPSE_TOOL,
            LMB_DOWN,
            LMB_RELEASE,
            LMB_DOWN_MOUSE_MOVED,
            OBJECT_CLICKED,
            MOVE_ITEM_BEGIN,
            MOVE_ITEM_END,
            USER_UNDO,
            USER_REDO,
            USER_RESIZE_ITEM,
            USER_PRESS_ESCAPE_TEXTBOX,
            USER_REFRESH_PROPERTIES,
        }

        static void onKeyDown(object sender, KeyEventArgs e) {
            Logger.log("key pressed:" + e.Key.ToString());
            if (Globals.anyEquals(e.Key, Key.Oem5, Key.OemTilde)) {
                Console.Write(Logger.flush());
            } else {
                Button btn = null;
                switch (e.Key) {
                    case Key.Z:
                        if(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        {
                            handle(UserActions.USER_UNDO);
                        }
                        break;
                    case Key.Y:
                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        {
                            handle(UserActions.USER_REDO);
                        }
                        break;
                    case Key.V:
                        btn = _instance._view.moveTool;
                        break;
                    case Key.Q:
                        btn = _instance._view.resizeTool;
                        break;
                    case Key.R:
                        btn = _instance._view.createRectangle;
                        break;
                    case Key.E:
                        btn = _instance._view.createEllipse;
                        break;
                }
                if (btn != null) {
                    btn.Focus();
                    btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
        }

        public static void handle(MesserGUISystem.commands.ICommand cmd) {
            handle(cmd.Action, cmd);
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
                case UserActions.LMB_DOWN:
                    _instance.lmbDown(e as MouseEventArgs);
                    break;
                case UserActions.LMB_RELEASE:
                    _instance.lmbRelease(e as MouseEventArgs);
                    break;
                case UserActions.LMB_DOWN_MOUSE_MOVED:
                    _instance.lmbDownMoved(e as MouseEventArgs);
                    break;
                case UserActions.OBJECT_CLICKED:
                    _instance.onObjectSelected(e as UIElement);
                    break;
                case UserActions.USER_PRESS_ESCAPE_TEXTBOX:
                    {
                        var textBox = e as TextBox;
                        FrameworkElement parent = (FrameworkElement)textBox.Parent;
                        while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
                        {
                            parent = (FrameworkElement)parent.Parent;
                        }

                        DependencyObject scope = FocusManager.GetFocusScope(textBox);
                        FocusManager.SetFocusedElement(scope, parent as IInputElement);
                    }
                    break;
                default:
                    //Logger.error("Unhandled action:" + action);
                    break;
            }
            foreach (var i in _observers) {
                i.onMessage(action, e);
            }
        }

        public void addObserver(IObserver o) {
            _observers.Add(o);
        }

        internal void removeObserver(IObserver o)
        {
            _observers.Remove(o);
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

        private void lmbDown(MouseEventArgs e) {
            _stage.CaptureMouse();
            _currentTool.lmbBegin(e.GetPosition(_stage));
        }

        private void lmbRelease(MouseEventArgs e) {
            _stage.ReleaseMouseCapture();
            _currentTool.lmbEnd(e.GetPosition(_stage));
        }

        private void lmbDownMoved(MouseEventArgs e) {
            _currentTool.lmb(e.GetPosition(_stage));
        }

        private void onObjectSelected(UIElement element) {
            //element.AllowDrop = true;
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
