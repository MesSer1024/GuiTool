using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserUI;
using System.Windows;
using WpfCommon;
using WpfCommon.commands;
using MesserControlsLibrary;

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

        public void onMessage(UserActions action, object data) {
            switch (action) {
                case UserActions.COMMAND_CREATE_MUI_ITEM: {
                        var cmd = data as CreateMUIElementCommand;
                        Assert.NotNull(cmd, cmd.MUIElement);
                        addItemToLayer(cmd.MUIElement);
                    }
                    break;
                case UserActions.MUIELEMENT_SELECTED_VALID: {
                        var muiElement = Globals.getMUIElement(Model.SelectedItem);
                        var newLayer = getLayerIndexForItem(muiElement);
                        _content.setSelectedLayer(newLayer);
                    }
                    break;
                case UserActions.CREATE_ITEM_SHOW_OVERLAY_SCREEN:
                    _content.setSelectedLayer(data as LayerRowControl);
                    break;
                case UserActions.COMMAND_CREATE_MUI_LAYER:
                    break;
            }
        }

        private int getLayerIndexForItem(MUIElement ele) {
            int ix = 0;
            foreach (var i in _content.Layers) {
                if (i.containsItem(ele)) {
                    return ix;
                }
                ix++;
            }
            Assert.Fail("Did not find element in any layer!");
            return -1;
        }

        private void addItemToLayer(MUIElement item) {
            _content.getSelectedlayer().addItem(item);
            _content.getSelectedlayer().expand();
        }

        public MUILayer getSelectedMUILayer() {
            return _content.getSelectedlayer().Layer;
        }
    }
}
