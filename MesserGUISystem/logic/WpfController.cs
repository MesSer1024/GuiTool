using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MesserGUISystem.tools;
using System.Windows.Media;
using WpfCommon;
using MesserControlsLibrary;
using MesserUI;
using WpfCommon.commands;
using MesserGUISystem.wpf_magic;
using System.Windows.Shapes;
using System.Windows.Documents;

namespace MesserGUISystem.logic {
    public class WpfController : IObserver {
        private MainWindow _view;
        private Canvas _stage;
        private ITool _currentTool;
        private static WpfController _instance;

        public static WpfController Instance {
            get { return WpfController._instance; }
        }

        private WpfController() {
            Controller.Instance.addObserver(this);
        }

        ~WpfController() {
            Controller.Instance.removeObserver(this);
        }

        internal static void initialize(MainWindow view, Canvas stage) {
            if (_instance != null)
                throw new Exception("Can only initialize WpfController once");
            _instance = new WpfController();
            _instance._view = view;
            _instance._stage = stage;
            _instance._view.KeyDown += new KeyEventHandler(onKeyDown);
            _instance._currentTool = new ToolBase();
        }

        public static double stageX {
            get { return 0; }
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

        static void onKeyDown(object sender, KeyEventArgs e) {
            if (Globals.anyEquals(e.Key, Key.Oem5, Key.OemTilde)) {
                Console.Write(Logger.flush());
            } else {
                Button btn = null;
                switch (e.Key) {
                    case Key.Z:
                        if(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        {
                            Controller.handle(UserActions.USER_UNDO);
                        }
                        break;
                    case Key.Y:
                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        {
                            Controller.handle(UserActions.USER_REDO);
                        }
                        break;
                    case Key.V:
                        btn = _instance._view.moveTool;
                        break;
                }
                if (btn != null) {
                    btn.Focus();
                    btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
        }

        public void onMessage(UserActions action, object data) {
            switch (action) {
                case UserActions.MUIELEMENT_DESELECTED:
                    Globals.clearAdornedElements(Model.SelectedItem);
                    Model.SelectedItem = null;
                    break;
                case UserActions.MUIELEMENT_SELECTED_VALID:
                    Model.SelectedItem = data as UIElement;
                    AdornerLayer.GetAdornerLayer(Model.SelectedItem).Add(new SelectionAdorner(Model.SelectedItem));
                    break;
                case UserActions.MOVE_TOOL:
                    _instance.moveToolClicked();
                    break;
                case UserActions.LMB_DOWN:
                    _instance.lmbDown(data as MouseEventArgs);
                    break;
                case UserActions.LMB_RELEASE:
                    _instance.lmbRelease(data as MouseEventArgs);
                    break;
                case UserActions.LMB_DOWN_MOUSE_MOVED:
                    _instance.lmbDownMoved(data as MouseEventArgs);
                    break;
                case UserActions.USER_PRESS_ESCAPE_TEXTBOX: {
                        var textBox = data as TextBox;
                        FrameworkElement parent = (FrameworkElement)textBox.Parent;
                        while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable) {
                            parent = (FrameworkElement)parent.Parent;
                        }

                        DependencyObject scope = FocusManager.GetFocusScope(textBox);
                        FocusManager.SetFocusedElement(scope, parent as IInputElement);
                        Controller.handle(UserActions.USER_REFRESH_PROPERTIES);
                    }
                    break;
                case UserActions.CREATE_ITEM_SHOW_OVERLAY_SCREEN: {                    
                        var foo = new CreateItemControl();
                        _view.overlay.Children.Add(foo);
                        _view.overlay.Visibility = Visibility.Visible;
                        Canvas.SetLeft(foo, 200);
                        Canvas.SetTop(foo, 200);
                    }
                    break;
                case UserActions.CREATE_ITEM_ABORT:
                    _view.overlay.Children.Clear(); //#TODO: If that command is ever triggered somewhere else, all "overlay" items will be cleared...
                    break;
                case UserActions.CREATE_ITEM_OBJECT_SELECTED: {
                        Assert.NotNull((MUITypes)data);
                        _view.overlay.Children.Clear();
                        var wpfElem = createWpfItemFromType((MUITypes)data);   
                        var muiLayer = MainWindow._layerWindow.getSelectedMUILayer();
                        Controller.handle(new CreateMUIElementCommand(wpfElem, muiLayer));
                    }
                    break;
                case UserActions.MUILAYER_VISIBILITY_CHANGED:                    
                    var layer = data as LayerRowControl;                    
                    bool visible = layer.Checked;
                    foreach (var ele in layer.Layer.Items) {
                        var wpfElement = WPFBridgeDatabase.Instance.getWpfElement(ele.IdKey);
                        wpfElement.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
                    }
                    break;
            }
        }

        public static UIElement createWpfItemFromType(MUITypes muiType) {
            UIElement ele = null;

            switch (muiType) {
                case MUITypes.Fill: {
                        var r = new Rectangle();
                        r.Width = 250;
                        r.Height = 250;

                        r.Fill = Brushes.Black;

                        ele = r;
                    }
                    break;
                case MUITypes.Text: {
                        var foo = new LabelRectangle("foobar", new Rect(0, 0, 250, 250));
                        ele = foo;
                    }
                    break;
            }
            Assert.NotNull(ele);
            return ele;
        }

        private void moveToolClicked() {
            changeTool(new MoveTool());
        }

        private void changeTool(ITool tool) {
            if (_currentTool != null) {
                _currentTool.destroyed();
            }
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
    }
}
