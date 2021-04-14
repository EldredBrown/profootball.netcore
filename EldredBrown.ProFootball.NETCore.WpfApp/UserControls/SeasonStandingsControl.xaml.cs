using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for SeasonStandingsControl.xaml
    /// </summary>
    public partial class SeasonStandingsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeasonStandingsControl"/> class.
        /// </summary>
        public SeasonStandingsControl()
        {
            InitializeComponent();

            DataContext = new SeasonStandingsControlViewModel();
        }

        /// <summary>
        /// Refreshes this <see cref="StandingsControl"/> object.
        /// </summary>
        public void Refresh()
        {
            (DataContext as SeasonStandingsControlViewModel).ViewStandingsCommand.Execute(null);
        }
    }
}
