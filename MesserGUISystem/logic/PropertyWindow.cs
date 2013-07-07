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
using MesserControlsLibrary;
using MesserUI;
using WpfCommon;
using System.Windows.Documents;
using MesserGUISystem.wpf_magic;
using WpfCommon.commands;

namespace MesserGUISystem.logic {
    class PropertyWindow : IObserver {

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
                var original = Globals.getBounds(Model.SelectedItem);
                var target = new MUIRectangle(_content.PositionX_text, _content.PositionY_text, _content.SizeX_text, _content.SizeY_text);

                Controller.handle(new ResizeItemCommand(Model.SelectedItem as Shape, original, target));

            } catch (Exception e) {
                return;
            }
        }

        private void updateView() {
            //#TODO: Is this neccessary since we have that on all valid objects
            if (Globals.isValidObject(Model.SelectedItem)) {
                var item = Model.SelectedItem;
                Globals.updateUi(() => {
                    _content.Visibility = Visibility.Visible;

                    var foo = VisualTreeHelper.GetOffset(item);
                    _content.PositionX = foo.X;
                    _content.PositionY = foo.Y;
                    _content.SizeX = item.RenderSize.Width;
                    _content.SizeY = item.RenderSize.Height;
                });
            } else {
                Globals.updateUi(() => { _content.Visibility = Visibility.Hidden; });
            }
        }

        public void onMessage(UserActions action, object data) {
            switch (action) {
                case UserActions.MUIELEMENT_DESELECTED:
                    updateView();
                    break;
                case UserActions.MUIELEMENT_SELECTED_VALID:
                    updateView();
                    break;
                case UserActions.MOVE_ITEM_BEGIN:
                    if (Globals.isValidObject(Model.SelectedItem)) {
                        _timer.Start();
                    }
                    break;
                case UserActions.COMMAND_MOVE_ITEM_END:
                    _timer.Stop();
                    break;
                case UserActions.USER_REFRESH_PROPERTIES:
                    updateView();
                    break;
            }
        }
    }
}
