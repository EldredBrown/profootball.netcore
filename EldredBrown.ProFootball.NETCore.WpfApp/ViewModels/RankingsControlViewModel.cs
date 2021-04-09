using System;
using System.Collections.ObjectModel;
using System.Linq;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.WpfApp;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class RankingsControlViewModel : ViewModelBase
    {
        private readonly ITeamSeasonRepository _teamSeasonRepository;

        public RankingsControlViewModel()
        {
            _teamSeasonRepository =
                App.ServiceProvider.GetService(typeof(ITeamSeasonRepository)) as ITeamSeasonRepository;
        }

        /// <summary>
        /// Gets or sets this control's total rankings collection.
        /// </summary>
        private ReadOnlyCollection<TeamSeason> _totalRankings;
        public ReadOnlyCollection<TeamSeason> TotalRankings
        {
            get
            {
                return _totalRankings;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("TotalRankings");
                }
                else if (value != _totalRankings)
                {
                    _totalRankings = value;
                    OnPropertyChanged("TotalRankings");
                }
            }
        }

        /// <summary>
        /// Gets or sets this control's offensive rankings collection.
        /// </summary>
        private ReadOnlyCollection<TeamSeason> _offensiveRankings;
        public ReadOnlyCollection<TeamSeason> OffensiveRankings
        {
            get
            {
                return _offensiveRankings;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("OffensiveRankings");
                }
                else if (value != _offensiveRankings)
                {
                    _offensiveRankings = value;
                    OnPropertyChanged("OffensiveRankings");
                }
            }
        }

        /// <summary>
        /// Gets or sets this control's defensive rankings collection.
        /// </summary>
        private ReadOnlyCollection<TeamSeason> _defensiveRankings;
        public ReadOnlyCollection<TeamSeason> DefensiveRankings
        {
            get
            {
                return _defensiveRankings;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("DefensiveRankings");
                }
                else if (value != _defensiveRankings)
                {
                    _defensiveRankings = value;
                    OnPropertyChanged("DefensiveRankings");
                }
            }
        }

        /// <summary>
        /// Loads the DataModel's Teams table.
        /// </summary>
        private DelegateCommand _viewRankingsCommand;
        public DelegateCommand ViewRankingsCommand
        {
            get
            {
                if (_viewRankingsCommand is null)
                {
                    _viewRankingsCommand = new DelegateCommand(param => ViewRankings());
                }
                return _viewRankingsCommand;
            }
        }
        private void ViewRankings()
        {
            var teamSeasons = _teamSeasonRepository.GetTeamSeasonsBySeason(WpfGlobals.SelectedSeason).ToList();
            TotalRankings = new ReadOnlyCollection<TeamSeason>(teamSeasons);
            OffensiveRankings = new ReadOnlyCollection<TeamSeason>(teamSeasons);
            DefensiveRankings = new ReadOnlyCollection<TeamSeason>(teamSeasons);
        }
    }
}
