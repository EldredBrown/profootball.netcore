using System.Windows;
using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

        private void SeasonsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ShowGamesButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
