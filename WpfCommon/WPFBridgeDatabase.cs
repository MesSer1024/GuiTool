using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserUI;
using WpfCommon;
using System.Windows;
using WpfCommon.commands;

namespace MesserGUISystem.logic {
    public class WPFBridgeDatabase : IObserver {

        private static WPFBridgeDatabase _instance;
        public static WPFBridgeDatabase Instance {
            get {
                return _instance; 
            }
        }

        public static void initialize() {
            _instance = new WPFBridgeDatabase();
        }

        ~WPFBridgeDatabase() {
            Controller.Instance.removeObserver(this);
        }

        private Dictionary<uint, MUIElement> _muiItems;
        private Dictionary<uint, UIElement> _wpfItems;
        private Dictionary<UIElement, MUIElement> _bridge;

        private WPFBridgeDatabase() {
            if (_instance != null) {
                throw new Exception("Can only create one instance of ItemDatabase");
            }
            Controller.Instance.addObserver(this);
            _muiItems = new Dictionary<uint, MUIElement>();
            _wpfItems = new Dictionary<uint, UIElement>();
            _bridge = new Dictionary<UIElement, MUIElement>();
        }

        private static uint getIdentifier(UIElement ele) {
            var values = _instance._wpfItems.Values.ToArray();
            for (var i = 0; i < values.Length; ++i) {
                var item = values[i];
                if (item == ele) {
                    return _instance._muiItems.Values.ToArray()[i].IdKey;
                }
            }

            throw new Exception("The requested UIElement does not have any matching MUIElement, maybe you should do isMuiElement before?" + ele);
        }

        public bool isMUIElement(UIElement ele) {
            if (ele != null) {
                if (_bridge.ContainsKey(ele)) {
                    return _bridge[ele] != null;
                }
            }
            return false;
        }

        public MUIElement getMUIElement(UIElement ele) {
            Fatal.NotNull(ele);
            var id = getIdentifier(ele);
            return _muiItems[id];
        }

        public void onMessage(UserActions action, object data) {
            switch (action) {
                case UserActions.COMMAND_CREATE_MUI_ITEM:
                    var cmd = data as CreateMUIElementCommand;
                    _muiItems.Add(cmd.MUIElement.IdKey, cmd.MUIElement);
                    _wpfItems.Add(cmd.MUIElement.IdKey, cmd.WpfElement);
                    _bridge.Add(cmd.WpfElement, cmd.MUIElement);
                    break;
            }
        }

        public UIElement getWpfElement(uint identifier) {
            return _wpfItems[identifier];
        }
    }
}
