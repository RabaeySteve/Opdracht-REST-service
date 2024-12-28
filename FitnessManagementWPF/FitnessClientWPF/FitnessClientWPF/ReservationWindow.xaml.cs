using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace FitnessClientWPF {
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window {
        public ReservationWindow() {
            InitializeComponent();
        }
        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        //    this.Visibility = Visibility.Collapsed; // om te verbergen
        //    e.Cancel = true; // om te voorkomen dat de WPF app het window toch nog vernietigt
        //}
        public void NoButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }
        public void YesButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }
    }
}
