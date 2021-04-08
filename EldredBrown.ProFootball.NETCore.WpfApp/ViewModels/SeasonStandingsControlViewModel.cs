using System.Collections.Generic;
using System.Collections.ObjectModel;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class SeasonStandingsControlViewModel : ViewModelBase
    {
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
            Standings = new ReadOnlyCollection<SeasonTeamStanding>(
                new List<SeasonTeamStanding>
                {
                    new SeasonTeamStanding { Team = "Team 1", Wins = 2, Losses = 0 },
                    new SeasonTeamStanding { Team = "Team 2", Wins = 1, Losses = 1 },
                    new SeasonTeamStanding { Team = "Team 3", Wins = 0, Losses = 2 }
                });
        }
    }
}
