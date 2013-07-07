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
using MesserUI;

namespace MesserGUISystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private static PropertyWindow _propertyWindow;
        private static HistoryManager _history;
        private LayerWindow _layerWindow;

        public MainWindow() {
            InitializeComponent();
            Stage.Area = this.stage;
            //Canvas.SetZIndex(stage, int.MinValue);
            Controller.initialize(this, stage);
            this.MouseMove += onMouseMove;            
            _propertyWindow = new PropertyWindow(this.properyWindow);
            _history = new HistoryManager();
            _layerWindow = new LayerWindow(this.layersWindow);
            //layersWindow.getSelectedlayer().addItem(new MUIFill());
        }

        private void moveTool_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(UserActions.MOVE_TOOL);
        }

        private void resizeTool_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(UserActions.RESIZE_TOOL);
        }

        private void createRectangle_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(UserActions.RECTANGLE_TOOL);
        }

        private void createEllipse_click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            Controller.handle(UserActions.ELLIPSE_TOOL);
        }

        private void canvas_mouseDown(object sender, MouseButtonEventArgs e) {
            e.Handled = true;
            Logger.log("lmb press");
            if (e.ChangedButton == MouseButton.Left) {
                Controller.handle(UserActions.LMB_DOWN, e);
            }
        }

        private void canvas_mouseUp(object sender, MouseButtonEventArgs e) {
            e.Handled = true;
            Logger.log("lmb release");
            if (e.ChangedButton == MouseButton.Left) {
                Controller.handle(UserActions.LMB_RELEASE, e);
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e) {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed) {
                //var pos = e.GetPosition(stage);
                Controller.handle(UserActions.LMB_DOWN_MOUSE_MOVED, e);
            }
        }

        private static bool IsNumerics(string text) {
            Regex regex = new Regex("[0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        private void DockPanel_LostMouseCapture(object sender, MouseEventArgs e) {
            //var foo = 1;
        }
    }
}
