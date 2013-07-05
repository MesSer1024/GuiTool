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

namespace MesserControlsLibrary {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl {
        
        public UserControl1() {
            InitializeComponent();
            PropertyWindowRoot.DataContext = this;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {

        }

        public static readonly DependencyProperty PosPropertyX = DependencyProperty.Register("PositionX", typeof(double), typeof(UserControl1), new PropertyMetadata(0d));
        public static readonly DependencyProperty PosPropertyY = DependencyProperty.Register("PositionY", typeof(double), typeof(UserControl1), new PropertyMetadata(0d));
        public static readonly DependencyProperty SizePropertyX = DependencyProperty.Register("SizeX", typeof(double), typeof(UserControl1), new PropertyMetadata(0d));
        public static readonly DependencyProperty SizePropertyY = DependencyProperty.Register("SizeY", typeof(double), typeof(UserControl1), new PropertyMetadata(0d));

        public double PositionX {
            get { return (double)GetValue(PosPropertyX); }
            set { SetValue(PosPropertyX, value); }
        }

        public double PositionY {
            get { return (double)GetValue(PosPropertyY); }
            set { SetValue(PosPropertyY, value); }
        }

        public double SizeX {
            get { return (double)GetValue(SizePropertyX); }
            set { SetValue(SizePropertyX, value); }
        }

        public double SizeY {
            get { return (double)GetValue(SizePropertyY); }
            set { SetValue(SizePropertyY, value); }
        }
    }
}
