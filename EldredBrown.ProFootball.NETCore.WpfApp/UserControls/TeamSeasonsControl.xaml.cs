using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for TeamSeasonsControl.xaml
    /// </summary>
    public partial class TeamSeasonsControl : UserControl
    {
        public TeamSeasonsControl()
        {
            InitializeComponent();

            DataContext = new TeamSeasonsControlViewModel();
        }

        /// <summary>
        /// Refreshes the view of this TeamsControl object
        /// </summary>
        public void Refresh()
        {
            (DataContext as TeamSeasonsControlViewModel).ViewTeamsCommand.Execute(null);
        }
    }
}
