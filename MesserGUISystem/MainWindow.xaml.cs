using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MesserGUISystem.utils;
using MesserGUISystem.tools;
using MesserGUISystem.logic;
using System.Text.RegularExpressions;

namespace MesserGUISystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static List<object> _items = new List<object>();

        public static int getItemCount() {
            return _items.Count;
        }

        public static Rectangle getBoxByIndex(int index) {
            return _items[index] as Rectangle;
        }

        public static void addItem(object o) {
            _items.Add(o);
            Stage.Children.Add(o as UIElement);
        }

        public static void removeItem(object o) {
            if (_items.Remove(o)) {
                Stage.Children.Remove(o as UIElement);
            }
        }

        public static Canvas Stage;
        private static PropertyWindow _propertyWindow;
        private static HistoryManager _history;

        public MainWindow() {
            InitializeComponent();
            utils.Globals.Stage = this;
            Stage = stage;
            //Canvas.SetZIndex(stage, int.MinValue);
            Controller.initialize(this, stage);
            this.MouseMove += onMouseMove;            
            _propertyWindow = new PropertyWindow(this.properyWindow);
            _history = new HistoryManager();
        }

        private void moveTool_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(Controller.UserActions.MOVE_TOOL);
        }

        private void resizeTool_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(Controller.UserActions.RESIZE_TOOL);
        }

        private void createRectangle_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(Controller.UserActions.RECTANGLE_TOOL);
        }

        private void createEllipse_click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(Controller.UserActions.ELLIPSE_TOOL);
        }

        private void canvas_mouseDown(object sender, MouseButtonEventArgs e) {
            e.Handled = true;
            Logger.log("lmb press");
            if (e.ChangedButton == MouseButton.Left) {
                Controller.handle(Controller.UserActions.LMB_DOWN, e);
            }
        }

        private void canvas_mouseUp(object sender, MouseButtonEventArgs e) {
            e.Handled = true;
            Logger.log("lmb release");
            if (e.ChangedButton == MouseButton.Left) {
                Controller.handle(Controller.UserActions.LMB_RELEASE, e);
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e) {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed) {
                //var pos = e.GetPosition(stage);
                Controller.handle(Controller.UserActions.LMB_DOWN_MOUSE_MOVED, e);
            }
        }

        private static bool IsNumerics(string text) {
            Regex regex = new Regex("[0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumerics(e.Text);
            if (Keyboard.IsKeyDown(Key.Escape))
            {
                Controller.handle(Controller.UserActions.USER_PRESS_ESCAPE_TEXTBOX, sender);
                e.Handled = true;
            }
        }

        private void DockPanel_LostMouseCapture(object sender, MouseEventArgs e) {
            var foo = 1;
        }
    }
}
