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
        /// <summary>
        /// Initializes a new instance of the <see cref="GamesWindow"/> class.
        /// </summary>
        public GamesWindow()
        {
            InitializeComponent();

            DataContext = new GamesWindowViewModel();
        }

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
