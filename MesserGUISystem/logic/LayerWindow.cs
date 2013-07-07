using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserGUISystem.commands;
using MesserUI;
using System.Windows;

namespace MesserGUISystem.logic {
    class LayerWindow : IObserver {
        private MesserControlsLibrary.LayersControl _content;

        public LayerWindow(MesserControlsLibrary.LayersControl layersControl) {
            // TODO: Complete member initialization
            _content = layersControl;

            _content.CreateLayer("Layer1");
            _content.CreateLayer("Layer2");
            _content.setSelectedLayer(0);
            _content.getSelectedlayer().expand();

            Controller.Instance.addObserver(this);
        }

        ~LayerWindow() {
            Controller.Instance.removeObserver(this);
        }

        public void onMessage(utils.UserActions action, object data) {
            switch (action) {
                case utils.UserActions.USER_CREATE_ITEM:
                    var cmd = data as CreateMUIElementCommand;
                    Assert.NotNull(cmd, cmd.MUIElement);
                    addItemToLayer(cmd.MUIElement);
                    break;
            }
        }

        private void addItemToLayer(MUIBase item) {
            _content.getSelectedlayer().addItem(item);
            _content.getSelectedlayer().expand();
        }
    }
}
