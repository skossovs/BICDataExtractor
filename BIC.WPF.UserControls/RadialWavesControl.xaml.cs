using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BIC.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for RadialWavesControl.xaml
    /// </summary>
    public partial class RadialWavesControl : UserControl
    {
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register
                    ("Mode", typeof(int), typeof(RadialWavesControl), new FrameworkPropertyMetadata(0, OnModePropertyValueChanged));

        public RadialWavesControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private static void OnModePropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public int Mode
        {
            get { return (int)this.GetValue(ModeProperty); }
            set { this.SetValue(ModeProperty, value); }
        }
    }
}
