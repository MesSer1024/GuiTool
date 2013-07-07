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

namespace MesserControlsLibrary {
    /// <summary>
    /// Interaction logic for LayerRowControl.xaml
    /// </summary>
    public partial class LayerRowControl : UserControl {
        private Brush _defaultBrush;
        private List<MUIBase> _items;

        public static readonly DependencyProperty LayerNameProperty = DependencyProperty.Register("LayerName", typeof(string), typeof(LayerRowControl), new PropertyMetadata("New Layer"));
        public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(LayerRowControl), new PropertyMetadata(true));

        public LayerRowControl() {
            InitializeComponent();
            ContentRoot.DataContext = this;
            _defaultBrush = ContentRoot.Background;
            _items = new List<MUIBase>();
            items.ItemsSource = _items;
        }

        public string LayerName {
            get { return (string)GetValue(LayerNameProperty); }
            set { SetValue(LayerNameProperty, value); }
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
            if(items.Visibility == Visibility.Collapsed) {
                expand();
            } else {
                items.Visibility = Visibility.Collapsed;
            }
        }

        public void expand() {
            items.Visibility = Visibility.Visible;
        }

        public void addItem(MUIBase item) {
            if(Assert.Validate(item, "Null Object!")) {
                _items.Add(item);
                items.Items.Refresh();
            }
        }

        public void removeItem(MUIBase item) {
            _items.Remove(item);
        }
    }
}
