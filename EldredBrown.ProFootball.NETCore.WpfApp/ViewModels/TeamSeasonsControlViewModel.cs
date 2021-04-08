using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class TeamSeasonsControlViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets this control's teams collection.
        /// </summary>
        private ReadOnlyCollection<TeamSeason> _teams;
        public ReadOnlyCollection<TeamSeason> Teams
        {
            get
            {
                return _teams;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("Teams");
                }
                else if (value != _teams)
                {
                    _teams = value;
                    OnPropertyChanged("Teams");
                    RequestUpdate = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets this control's selected team.
        /// </summary>
        private TeamSeason _selectedTeam;
        public TeamSeason SelectedTeam
        {
            get
            {
                return _selectedTeam;
            }
            set
            {
                if (value != _selectedTeam)
                {
                    _selectedTeam = value;
                    OnPropertyChanged("SelectedTeam");
                }
            }
        }

        /// <summary>
        /// Gets this control's team schedule profile collection.
        /// </summary>
        private ReadOnlyCollection<TeamSeasonOpponentProfile> _teamSeasonScheduleProfile;
        public ReadOnlyCollection<TeamSeasonOpponentProfile> TeamSeasonScheduleProfile
        {
            get
            {
                return _teamSeasonScheduleProfile;
            }
            private set
            {
                if (value != _teamSeasonScheduleProfile)
                {
                    _teamSeasonScheduleProfile = value;
                    OnPropertyChanged("TeamSeasonScheduleProfile");
                }
            }
        }

        /// <summary>
        /// Gets this controls's collection of team schedule totals.
        /// </summary>
        private ReadOnlyCollection<TeamSeasonScheduleTotals> _teamSeasonScheduleTotals;
        public ReadOnlyCollection<TeamSeasonScheduleTotals> TeamSeasonScheduleTotals
        {
            get
            {
                return _teamSeasonScheduleTotals;
            }
            private set
            {
                if (value != _teamSeasonScheduleTotals)
                {
                    _teamSeasonScheduleTotals = value;
                    OnPropertyChanged("TeamSeasonScheduleTotals");
                }
            }
        }

        /// <summary>
        /// Gets this control's collection of team schedule averages.
        /// </summary>
        private ReadOnlyCollection<TeamSeasonScheduleAverages> _teamSeasonScheduleAverages;
        public ReadOnlyCollection<TeamSeasonScheduleAverages> TeamSeasonScheduleAverages
        {
            get
            {
                return _teamSeasonScheduleAverages;
            }
            private set
            {
                if (value != _teamSeasonScheduleAverages)
                {
                    _teamSeasonScheduleAverages = value;
                    OnPropertyChanged("TeamSeasonScheduleAverages");
                }
            }
        }

        /// <summary>
        /// Loads the DataModel's Teams table.
        /// </summary>
        private DelegateCommand _viewTeamsCommand;
        public DelegateCommand ViewTeamsCommand
        {
            get
            {
                if (_viewTeamsCommand == null)
                {
                    _viewTeamsCommand = new DelegateCommand(param => ViewTeams());
                }
                return _viewTeamsCommand;
            }
        }
        private void ViewTeams()
        {
            Teams = new ReadOnlyCollection<TeamSeason>(
                new List<TeamSeason>
                {
                    new TeamSeason { TeamName = "Team 1" },
                    new TeamSeason { TeamName = "Team 2" },
                    new TeamSeason { TeamName = "Team 3" }
                });
        }

        /// <summary>
        /// Views the team schedule profile, totals, and averages for the selected team.
        /// </summary>
        private DelegateCommand _viewTeamScheduleCommand;
        public DelegateCommand ViewTeamScheduleCommand
        {
            get
            {
                if (_viewTeamScheduleCommand == null)
                {
                    _viewTeamScheduleCommand = new DelegateCommand(param => ViewTeamSchedule());
                }
                return _viewTeamScheduleCommand;
            }
        }
        private void ViewTeamSchedule()
        {
            TeamSeasonScheduleProfile = new ReadOnlyCollection<TeamSeasonOpponentProfile>(
                new List<TeamSeasonOpponentProfile>
                {
                    new TeamSeasonOpponentProfile { Opponent = "Opponent 1" },
                    new TeamSeasonOpponentProfile { Opponent = "Opponent 2" },
                    new TeamSeasonOpponentProfile { Opponent = "Opponent 3" }
                });

            TeamSeasonScheduleTotals = new ReadOnlyCollection<TeamSeasonScheduleTotals>(
                new List<TeamSeasonScheduleTotals>
                {
                    new TeamSeasonScheduleTotals { Games = 3 }
                });

            TeamSeasonScheduleAverages = new ReadOnlyCollection<TeamSeasonScheduleAverages>(
                new List<TeamSeasonScheduleAverages>
                {
                    new TeamSeasonScheduleAverages { PointsFor = 7d, PointsAgainst = 7d }
                });
        }
    }
}
