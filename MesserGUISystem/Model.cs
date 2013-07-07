using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using WpfCommon;

namespace MesserGUISystem {
    class Model {
        public static bool hasValidItem { get; set; }

        private static UIElement _selectedItem;
        public static UIElement SelectedItem {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                hasValidItem = Globals.isValidObject(value);
            }
        }
    }
}
