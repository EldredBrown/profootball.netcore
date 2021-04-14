using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for TeamSeasonsControl.xaml
    /// </summary>
    public partial class TeamSeasonsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSeasonsControl"/> class.
        /// </summary>
        public TeamSeasonsControl()
        {
            InitializeComponent();

            DataContext = new TeamSeasonsControlViewModel();
        }

        /// <summary>
        /// Refreshes this <see cref="TeamsControl"/> object.
        /// </summary>
        public void Refresh()
        {
            (DataContext as TeamSeasonsControlViewModel).ViewTeamsCommand.Execute(null);
        }
    }
}
