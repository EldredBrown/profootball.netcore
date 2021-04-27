using System.Windows.Controls;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls.SeasonStandings
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

            DataContext = App.ServiceProvider.GetService(typeof(ISeasonStandingsControlViewModel))
                as ISeasonStandingsControlViewModel;
        }
    }
}
