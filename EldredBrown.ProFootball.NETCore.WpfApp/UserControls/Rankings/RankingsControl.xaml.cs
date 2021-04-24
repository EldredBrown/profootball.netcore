using System.Windows.Controls;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls.Rankings
{
    /// <summary>
    /// Interaction logic for RankingsControl.xaml
    /// </summary>
    public partial class RankingsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RankingsControl"/> class.
        /// </summary>
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
