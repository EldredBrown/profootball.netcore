using System.Collections.ObjectModel;
using System.Linq;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.WpfApp;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class SeasonStandingsControlViewModel : ViewModelBase
    {
        private readonly ISeasonStandingsRepository _seasonStandingsRepository;

        public SeasonStandingsControlViewModel()
        {
            _seasonStandingsRepository =
                App.ServiceProvider.GetService(typeof(ISeasonStandingsRepository)) as ISeasonStandingsRepository;
        }

        /// <summary>
        /// Gets or sets this control's standings collection.
        /// </summary>
        private ReadOnlyCollection<SeasonTeamStanding> _standings;
        public ReadOnlyCollection<SeasonTeamStanding> Standings
        {
            get
            {
                return _standings;
            }
            set
            {
                if (value != _standings)
                {
                    _standings = value;
                    OnPropertyChanged("Standings");
                }
            }
        }

        /// <summary>
        /// Loads the control.
        /// </summary>
        private DelegateCommand _viewStandingsCommand;
        public DelegateCommand ViewStandingsCommand
        {
            get
            {
                if (_viewStandingsCommand is null)
                {
                    _viewStandingsCommand = new DelegateCommand(param => ViewStandings());
                }
                return _viewStandingsCommand;
            }
        }
        private void ViewStandings()
        {
            var seasonStandings = _seasonStandingsRepository.GetSeasonStandings(WpfGlobals.SelectedSeason);
            Standings = new ReadOnlyCollection<SeasonTeamStanding>(seasonStandings.ToList());
        }
    }
}
