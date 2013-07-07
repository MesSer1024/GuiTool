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
using MesserControlsLibrary;
using MesserUI;
using MesserGUISystem.utils;

namespace MesserGUISystem.logic {
    class PropertyWindow : IObserver {

        private UIElement _selectedItem;
        private PropertyWindowControl _content;
        private Timer _timer;

        ~PropertyWindow() {
            Controller.Instance.removeObserver(this);
        }

        public PropertyWindow(PropertyWindowControl content) {
            _content = content;
            _content.OnConceptButtonPressedInTextbox += new PropertyWindowControl.ConceptPressedDelegate(onConceptButtonPressedInTextbox);
            _content.OnMouseFocusLostFromTextbox += new PropertyWindowControl.PropertyWindowDelegate(onMouseFocusLostFromTextbox);
            _content.Visibility = Visibility.Hidden;

            _timer = new Timer(25);
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            Controller.Instance.addObserver(this);
        }

        void onConceptButtonPressedInTextbox(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.Escape:
                    Controller.handle(UserActions.USER_PRESS_ESCAPE_TEXTBOX, sender);
                    break;
                case Key.Enter:
                    updateModelWithViewValues();
                    break;
            }
        }

        void onMouseFocusLostFromTextbox(object sender) {
            updateModelWithViewValues();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e) {
            updateView();
        }

        private void updateModelWithViewValues() {            
            try {
                var original = utils.Globals.getBounds(_selectedItem);
                var target = new MUIRectangle(_content.PositionX_text, _content.PositionY_text, _content.SizeX_text, _content.SizeY_text);

                Controller.handle(new ResizeItemCommand(_selectedItem as Shape, original, target));

            } catch (Exception e) {
                return;
            }
        }

        private void updateView() {
            if (utils.Globals.isValidObject(_selectedItem)) {
                utils.Globals.updateUi(() => {
                    _content.Visibility = Visibility.Visible;

                    var foo = VisualTreeHelper.GetOffset(_selectedItem);
                    _content.PositionX = foo.X;
                    _content.PositionY = foo.Y;
                    _content.SizeX = _selectedItem.RenderSize.Width;
                    _content.SizeY = _selectedItem.RenderSize.Height;
                });
            } else {
                utils.Globals.updateUi(() => { _content.Visibility = Visibility.Hidden; });
            }
        }

        public void onMessage(UserActions action, object data) {
            switch (action) {
                case UserActions.OBJECT_CLICKED:
                    _selectedItem = data as UIElement;
                    updateView();
                    break;
                case UserActions.MOVE_ITEM_BEGIN:
                    if (utils.Globals.isValidObject(_selectedItem)) {
                        _timer.Start();
                    }
                    break;
                case UserActions.MOVE_ITEM_END:
                    _timer.Stop();
                    break;
                case UserActions.USER_REFRESH_PROPERTIES:
                    updateView();
                    break;
            }
        }
    }
}
