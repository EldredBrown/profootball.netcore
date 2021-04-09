using System.Windows;
using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows;

namespace EldredBrown.ProFootball.NETCore.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }

        private void SeasonsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeamSeasonsControl.Refresh();
            SeasonStandingsControl.Refresh();
            RankingsControl.Refresh();
        }

        private void ShowGamesButton_Click(object sender, RoutedEventArgs e)
        {
            var gamesWindow = new GamesWindow();
            gamesWindow.ShowDialog();

            TeamSeasonsControl.Refresh();
        }
    }
}
