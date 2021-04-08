using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for SeasonStandingsControl.xaml
    /// </summary>
    public partial class SeasonStandingsControl : UserControl
    {
        public SeasonStandingsControl()
        {
            InitializeComponent();

            DataContext = new SeasonStandingsControlViewModel();
        }
    }
}
