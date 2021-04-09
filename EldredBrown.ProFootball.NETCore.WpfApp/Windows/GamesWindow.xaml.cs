using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows
{
    /// <summary>
    /// Interaction logic for GamesWindow.xaml
    /// </summary>
    public partial class GamesWindow : Window
    {
        public GamesWindow()
        {
            InitializeComponent();

            DataContext = new GamesWindowViewModel();
        }

        /// <summary>
        /// Handles the SelectionChanged event for the GamesDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesDataGrid.SelectedItem == CollectionView.NewItemPlaceholder)
            {
                // Prepare to add a new game.
                (DataContext as GamesWindowViewModel).SelectedGame = null;
            }
        }
    }
}
