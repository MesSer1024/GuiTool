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
    /// Interaction logic for LayersControl.xaml
    /// </summary>
    public partial class LayersControl : UserControl {
        private List<LayerRowControl> _layers;
        public List<LayerRowControl> Layers { get { return _layers; } }

        public LayersControl() {
            InitializeComponent();
            ContentRoot.DataContext = this;
        }

        protected override void OnInitialized(EventArgs e) {
            base.OnInitialized(e);
            _layers = new List<LayerRowControl>();
            layers.ItemsSource = _layers;
        }

        public void CreateLayer(string name="New Layer") {
            var foo = new LayerRowControl();
            foo.LayerName = name;
            //foo.Checked = visible;
            _layers.Add(foo);
        }

        public void setSelectedLayer(LayerRowControl control) {
            Assert.NotNull(control);
            layers.SelectedItem = control;
            layers.Focus();
        }

        public void setSelectedLayer(int i) {
            Assert.True(i < layers.Items.Count && i >= 0, String.Format("setSelectedLayer out of range was: {0}", i));
            layers.SelectedIndex = i;
            layers.Focus();
        }

        public LayerRowControl getSelectedlayer() {
            return _layers[layers.SelectedIndex];
        }
    }
}
