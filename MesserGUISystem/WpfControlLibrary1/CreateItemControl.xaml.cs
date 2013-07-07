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
using MesserUI;
using WpfCommon;

namespace MesserControlsLibrary {
    /// <summary>
    /// Interaction logic for CreateItemControl.xaml
    /// </summary>
    public partial class CreateItemControl : UserControl {
        List<MUITypes> _muiTypes;

        public CreateItemControl() {
            InitializeComponent();
            ContentRoot.DataContext = this;
            _muiTypes = new List<MUITypes>();

            foreach (var i in Enum.GetValues(typeof(MUITypes))) {
                if ((MUITypes)i != MUITypes.None && (MUITypes)i != MUITypes.Layer) {
                    _muiTypes.Add((MUITypes)i);
                }
            }

            this._availableItems.ItemsSource = _muiTypes;
            this._availableItems.SelectedItem = MUITypes.Fill;
        }

        private void onAbort(object sender, RoutedEventArgs e) {
            Controller.handle(UserActions.CREATE_ITEM_ABORT);
        }

        private void onOk(object sender, RoutedEventArgs e) {
            var item = this._availableItems.SelectedItem;
            if (item != null) {
                Controller.handle(UserActions.CREATE_ITEM_OBJECT_SELECTED, item);
            } else {
                throw new NotImplementedException("This should not happen since we have default selection...");
            }
        }
    }
}
