using System.Configuration;
using System.Data;
using System.Windows;

namespace FitnessClientWPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            // Maak een instance van ReservationWindow
            var reservationWindow = new ReservationWindow();

            // Open MainWindow met de vereiste parameter
            var mainWindow = new MainWindow(reservationWindow);
            mainWindow.Show();
        }

    }

}
