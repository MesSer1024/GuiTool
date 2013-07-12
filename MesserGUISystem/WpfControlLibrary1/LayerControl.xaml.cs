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
using MesserGUISystem.logic;

namespace MesserControlsLibrary {
    /// <summary>
    /// Interaction logic for LayerRowControl.xaml
    /// </summary>
    public partial class LayerRowControl : UserControl {
        private Brush _defaultBrush;
        private MUILayer _layer;

        public MUILayer Layer {
            get { return _layer; }
        }

        public static readonly DependencyProperty LayerNameProperty = DependencyProperty.Register("LayerName", typeof(string), typeof(LayerRowControl), new PropertyMetadata("New Layer"));
        public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(LayerRowControl), new PropertyMetadata(true));

        public LayerRowControl() {
            InitializeComponent();
            ContentRoot.DataContext = this;
            _defaultBrush = ContentRoot.Background;
            _layer = new MUILayer();
            items.ItemsSource = _layer.Items;            
        }

        public string LayerName {
            get { return (string)GetValue(LayerNameProperty); }
            set {
                SetValue(LayerNameProperty, value);
                _layer.Name = value;
            }
        }

        public bool Checked {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        private void ContentRoot_MouseEnter(object sender, MouseEventArgs e) {
            ContentRoot.Background = new SolidColorBrush(Colors.Silver);
        }

        private void ContentRoot_MouseLeave(object sender, MouseEventArgs e) {
            ContentRoot.Background = _defaultBrush;
        }

        private void ContentRoot_MouseUp(object sender, MouseButtonEventArgs e) {
            //if(items.Visibility == Visibility.Collapsed) {
            //    expand();
            //} else {
            //    items.Visibility = Visibility.Collapsed;
            //}
        }

        private void onExpandClick(object sender, RoutedEventArgs e)
        {
            if (items.Visibility == Visibility.Collapsed)
            {
                expand();
            }
            else
            {
                items.Visibility = Visibility.Collapsed;
            }
        }

        public void expand() {
            items.Visibility = Visibility.Visible;
        }

        public void addItem(MUIElement item) {
            if(Assert.Validate(item, "Null Object!")) {
                _layer.add(item);
                items.Items.Refresh();
            }
        }

        public void removeItem(MUIElement item) {
            _layer.remove(item);
        }

        public bool containsItem(MUIElement item) {
            return _layer.Items.Contains(item);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Controller.handle(UserActions.CREATE_ITEM_SHOW_OVERLAY_SCREEN, this);
        }

        private void items_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var mVisual = _layer.Items[items.SelectedIndex];

            Controller.handle(UserActions.MUIELEMENT_DESELECTED);
            Controller.handle(UserActions.MUIELEMENT_SELECTED_VALID, WPFBridgeDatabase.Instance.getWpfElement(mVisual.IdKey));
        }

        private void onCheckboxState(object sender, RoutedEventArgs e) {
            Controller.handle(UserActions.MUIELEMENT_DESELECTED);
            Controller.handle(UserActions.MUILAYER_VISIBILITY_CHANGED, this);
        }
    }
}
