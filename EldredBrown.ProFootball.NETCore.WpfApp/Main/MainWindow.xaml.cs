using System.Windows;
using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows.Games;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IGamesWindowFactory _gamesWindowFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="gamesWindowFactory">The <see cref="IWindowFactory"/> object that will create instances of the 
        /// <see cref="GameWindow"/> for this class.</param>
        public MainWindow(IGamesWindowFactory gamesWindowFactory)
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

            _gamesWindowFactory = gamesWindowFactory;
        }

        private void SeasonsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeamSeasonsControl.Refresh();
            SeasonStandingsControl.Refresh();
            RankingsControl.Refresh();
        }

        private void ShowGamesButton_Click(object sender, RoutedEventArgs e)
        {
            var gamesWindow = _gamesWindowFactory.CreateGamesWindow();
            gamesWindow.ShowDialog();

            TeamSeasonsControl.Refresh();
        }
    }
}
