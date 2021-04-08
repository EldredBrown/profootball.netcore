using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for RankingsControl.xaml
    /// </summary>
    public partial class RankingsControl : UserControl
    {
        public RankingsControl()
        {
            InitializeComponent();

            DataContext = new RankingsControlViewModel();
        }

        /// <summary>
        /// Refreshes the view of this RankingsControl object
        /// </summary>
        public void Refresh()
        {
            (DataContext as RankingsControlViewModel).ViewRankingsCommand.Execute(null);
        }
    }
}
