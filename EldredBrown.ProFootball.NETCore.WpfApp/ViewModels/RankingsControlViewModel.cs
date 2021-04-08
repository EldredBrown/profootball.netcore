using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class RankingsControlViewModel : ViewModelBase
    {
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
            TotalRankings = new ReadOnlyCollection<TeamSeason>(
                new List<TeamSeason>
                {
                    new TeamSeason { TeamName = "Team 1" },
                    new TeamSeason { TeamName = "Team 2" },
                    new TeamSeason { TeamName = "Team 3" }
                });

            OffensiveRankings = new ReadOnlyCollection<TeamSeason>(
                new List<TeamSeason>
                {
                    new TeamSeason { TeamName = "Team 1" },
                    new TeamSeason { TeamName = "Team 2" },
                    new TeamSeason { TeamName = "Team 3" }
                });

            DefensiveRankings = new ReadOnlyCollection<TeamSeason>(
                new List<TeamSeason>
                {
                    new TeamSeason { TeamName = "Team 1" },
                    new TeamSeason { TeamName = "Team 2" },
                    new TeamSeason { TeamName = "Team 3" }
                });
        }
    }
}
