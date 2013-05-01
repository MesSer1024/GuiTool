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

namespace MesserGUISystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static Canvas Stage;

        public MainWindow() {
            InitializeComponent();
            Stage = stage;
            //Canvas.SetZIndex(stage, int.MinValue);
            Controller.initialize(this, stage);
            this.MouseMove += onMouseMove;
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
                Controller.handle(Controller.UserActions.CANVAS_LMB_DOWN, e);
            }
        }

        private void canvas_mouseUp(object sender, MouseButtonEventArgs e) {
            e.Handled = true;
            Logger.log("lmb release");
            if (e.ChangedButton == MouseButton.Left) {
                Controller.handle(Controller.UserActions.CANVAS_LMB_RELEASE, e);
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e) {
            e.Handled = true;
            if (e.LeftButton == MouseButtonState.Pressed) {
                Controller.handle(Controller.UserActions.CANVAS_LMB_DOWN_MOVED, e);
            }
        }
    }
}
