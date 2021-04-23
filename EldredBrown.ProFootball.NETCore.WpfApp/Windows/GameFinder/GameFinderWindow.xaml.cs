using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;
using System.Windows;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows
{
    /// <summary>
    /// Interaction logic for GameFinderWindow.xaml
    /// </summary>
    public partial class GameFinderWindow : Window, IGameFinderWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameFinderWindow"/> class.
        /// </summary>
        public GameFinderWindow(IGameFinderWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IGameFinderWindowViewModel;

            // The GuestName and HostName properties in the underlying ViewModel are not updated automatically when a press
            // of the Enter key clicks the OK button automatically, so we need to update these directly as follows: 
            viewModel.GuestName = GuestTextBox.Text;
            viewModel.HostName = HostTextBox.Text;

            var (valid, message) = viewModel.ValidateDataEntry();
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
