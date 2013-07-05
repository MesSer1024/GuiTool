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
using System.Text.RegularExpressions;

namespace MesserControlsLibrary {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl {
        public delegate void PropertyWindowDelegate(object sender);
        public delegate void ConceptPressedDelegate(object sender, KeyEventArgs e);
        public event ConceptPressedDelegate OnConceptButtonPressedInTextbox;
        public event PropertyWindowDelegate OnMouseFocusLostFromTextbox;
        
        public UserControl1() {
            InitializeComponent();
            PropertyWindowRoot.DataContext = this;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            e.Handled = !IsNumerics(e.Text);
        }

        private void positionX_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter || e.Key == Key.Escape) {
                if (OnConceptButtonPressedInTextbox != null) {
                    OnConceptButtonPressedInTextbox.Invoke(sender, e);
                }
                //updateModel();
            }
        }

        private void positionX_LostMouseCapture(object sender, MouseEventArgs e) {
            if (OnMouseFocusLostFromTextbox != null) {
                OnMouseFocusLostFromTextbox.Invoke(sender);
                //e.Handled = true;
            }
        }

        private static bool IsNumerics(string text) {
            Regex regex = new Regex("[0-9,.]+");
            if (regex.IsMatch(text)) {
                double r = 0;
                if (Double.TryParse(text, out r)) {
                    return true;
                }
            }
            return false;
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

        public double PositionX_text {
            get { return Convert.ToDouble(this.positionX.Text); }
        }

        public double PositionY_text {
            get { return Convert.ToDouble(this.positionY.Text); }
        }

        public double SizeX_text {
            get { return Convert.ToDouble(this.sizeX.Text); }
        }

        public double SizeY_text {
            get { return Convert.ToDouble(this.sizeY.Text); }
        }
    }
}
