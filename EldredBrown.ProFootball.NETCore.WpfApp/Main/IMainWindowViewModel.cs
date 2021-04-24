using System.Collections.ObjectModel;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Main
{
    public interface IMainWindowViewModel
    {
        ReadOnlyCollection<int> Seasons { get; set; }
        int SelectedSeason { get; set; }
        DelegateCommand PredictGameScoreCommand { get; }
        DelegateCommand ViewSeasonsCommand { get; }
        DelegateCommand WeeklyUpdateCommand { get; }
    }
}
