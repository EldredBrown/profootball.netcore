using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;
using System.Windows;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows
{
    /// <summary>
    /// Interaction logic for GameFinderWindow.xaml
    /// </summary>
    public partial class GameFinderWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameFinderWindow"/> class.
        /// </summary>
        public GameFinderWindow()
        {
            InitializeComponent();

            DataContext = new GameFinderWindowViewModel();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as GameFinderWindowViewModel;

            // The GuestName and HostName properties in the underlying ViewModel are not updated automatically when a press
            // of the Enter key clicks the OK button automatically, so we need to update these directly as follows: 
            vm.GuestName = GuestTextBox.Text;
            vm.HostName = HostTextBox.Text;

            var (valid, message) = vm.ValidateDataEntry();
            if (!valid)
            {
                MessageBox.Show(message, "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Error);
                GuestTextBox.Focus();
                return;
            }

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
