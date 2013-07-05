using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Shapes;
using System.Timers;
using MesserGUISystem.commands;

namespace MesserGUISystem.logic {
    class PropertyWindow : IObserver {

        private UIElement _selectedItem;
        private MainWindow _stage;
        private Timer _timer;

        public PropertyWindow(MainWindow stage) {
            _stage = stage;
            _stage._myPropertyWindow.Visibility = Visibility.Hidden;
            _stage.positionX.PreviewKeyDown += positionX_PreviewKeyDown;
            _stage.positionY.PreviewKeyDown += positionX_PreviewKeyDown;
            _stage.SizeX.PreviewKeyDown += positionX_PreviewKeyDown;
            _stage.SizeY.PreviewKeyDown += positionX_PreviewKeyDown;

            _stage.positionX.LostMouseCapture += onLostFocus;
            _stage.positionY.LostMouseCapture += onLostFocus;
            _stage.SizeX.LostMouseCapture += onLostFocus;
            _stage.SizeY.LostMouseCapture += onLostFocus;

            _timer = new Timer(10);
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e) {
            updateView();
        }

        void onLostFocus(object sender, EventArgs e) {
            updateModel();
        }

        void positionX_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                updateModel();
            }
        }

        private static bool isNumeric(string text) {
            Regex regex = new Regex("[0-9]+");
            return regex.IsMatch(text);
        }

        private void updateModel() {            
            try {
                var x = Convert.ToDouble(_stage.positionX.Text);
                var y = Convert.ToDouble(_stage.positionY.Text);
                var w = Convert.ToDouble(_stage.SizeX.Text);
                var h = Convert.ToDouble(_stage.SizeY.Text);

                var foo = _selectedItem as Shape;
                var pos = VisualTreeHelper.GetOffset(foo);
                var original = new Bounds(pos.X, pos.Y, foo.Width, foo.Height);
                var target = new Bounds(x, y, w, h);

                Controller.handle(new ResizeItemCommand(foo, original, target));

            } catch (Exception e) {
                return;
            }
        }

        private void updateView() {
            if (utils.Globals.isValidObject(_selectedItem)) {
                updateUi(() => {
                    _stage._myPropertyWindow.Visibility = Visibility.Visible;

                    var foo = VisualTreeHelper.GetOffset(_selectedItem);
                    _stage.SizeX.Text = _selectedItem.RenderSize.Width.ToString();
                    _stage.SizeY.Text = _selectedItem.RenderSize.Height.ToString();
                    _stage.positionX.Text = foo.X.ToString();
                    _stage.positionY.Text = foo.Y.ToString();
                });
            } else {
                updateUi(() => { _stage._myPropertyWindow.Visibility = Visibility.Hidden; });
            }
        }

        public void updateUi(Action a) {
            _stage.Dispatcher.Invoke(a);
        }

        public void onMessage(Controller.UserActions action, object data) {
            switch (action) {
                case Controller.UserActions.OBJECT_CLICKED:
                    _selectedItem = data as UIElement;
                    break;
                case Controller.UserActions.MOVE_ITEM_BEGIN:
                    if (utils.Globals.isValidObject(_selectedItem)) {
                        _timer.Start();
                    }
                    break;
                case Controller.UserActions.MOVE_ITEM_END:
                    _timer.Stop();
                    break;
                case Controller.UserActions.USER_REFRESH_PROPERTIES:
                    updateView();
                    break;
            }
        }
    }
}
