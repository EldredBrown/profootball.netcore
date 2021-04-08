using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class RankingsControlViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets this control's total rankings collection.
        /// </summary>
        private ReadOnlyCollection<TotalRanking> _totalRankings;
        public ReadOnlyCollection<TotalRanking> TotalRankings
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
        private ReadOnlyCollection<OffensiveRanking> _offensiveRankings;
        public ReadOnlyCollection<OffensiveRanking> OffensiveRankings
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
        private ReadOnlyCollection<DefensiveRanking> _defensiveRankings;
        public ReadOnlyCollection<DefensiveRanking> DefensiveRankings
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
                if (_viewRankingsCommand == null)
                {
                    _viewRankingsCommand = new DelegateCommand(param => ViewRankings());
                }
                return _viewRankingsCommand;
            }
        }
        private void ViewRankings()
        {
            TotalRankings = new ReadOnlyCollection<TotalRanking>(
                new List<TotalRanking>
                {
                    new TotalRanking { TeamName = "Team 1" },
                    new TotalRanking { TeamName = "Team 2" },
                    new TotalRanking { TeamName = "Team 3" }
                });

            OffensiveRankings = new ReadOnlyCollection<OffensiveRanking>(
                new List<OffensiveRanking>
                {
                    new OffensiveRanking { TeamName = "Team 1" },
                    new OffensiveRanking { TeamName = "Team 2" },
                    new OffensiveRanking { TeamName = "Team 3" }
                });

            DefensiveRankings = new ReadOnlyCollection<DefensiveRanking>(
                new List<DefensiveRanking>
                {
                    new DefensiveRanking { TeamName = "Team 1" },
                    new DefensiveRanking { TeamName = "Team 2" },
                    new DefensiveRanking { TeamName = "Team 3" }
                });
        }
    }
}
