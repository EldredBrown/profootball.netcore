using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.WpfApp;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class TeamSeasonsControlViewModel : ViewModelBase
    {
        private readonly ITeamSeasonRepository _teamSeasonRepository;
        private readonly ITeamSeasonScheduleProfileRepository _teamSeasonScheduleProfileRepository;
        private readonly ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private readonly ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;

        public TeamSeasonsControlViewModel()
        {
            _teamSeasonRepository =
                App.ServiceProvider.GetService(typeof(ITeamSeasonRepository)) as ITeamSeasonRepository;
            _teamSeasonScheduleProfileRepository =
                App.ServiceProvider.GetService(typeof(ITeamSeasonScheduleProfileRepository))
                as ITeamSeasonScheduleProfileRepository;
            _teamSeasonScheduleTotalsRepository =
                App.ServiceProvider.GetService(typeof(ITeamSeasonScheduleTotalsRepository))
                as ITeamSeasonScheduleTotalsRepository;
            _teamSeasonScheduleAveragesRepository =
                App.ServiceProvider.GetService(typeof(ITeamSeasonScheduleAveragesRepository))
                as ITeamSeasonScheduleAveragesRepository;
        }

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
                if (_viewTeamsCommand is null)
                {
                    _viewTeamsCommand = new DelegateCommand(param => ViewTeams());
                }
                return _viewTeamsCommand;
            }
        }
        private void ViewTeams()
        {
            var teamSeasons = _teamSeasonRepository.GetTeamSeasonsBySeason(WpfGlobals.SelectedSeason);
            Teams = new ReadOnlyCollection<TeamSeason>(teamSeasons.ToList());
        }

        /// <summary>
        /// Views the team schedule profile, totals, and averages for the selected team.
        /// </summary>
        private DelegateCommand _viewTeamScheduleCommand;
        public DelegateCommand ViewTeamScheduleCommand
        {
            get
            {
                if (_viewTeamScheduleCommand is null)
                {
                    _viewTeamScheduleCommand = new DelegateCommand(param => ViewTeamSchedule());
                }
                return _viewTeamScheduleCommand;
            }
        }
        private void ViewTeamSchedule()
        {
            if (SelectedTeam is null)
            {
                return;
            }

            var teamName = SelectedTeam.TeamName;
            var seasonYear = SelectedTeam.SeasonYear;

            var teamSeasonOpponentProfiles = 
                _teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfile(teamName, seasonYear);
            TeamSeasonScheduleProfile = 
                new ReadOnlyCollection<TeamSeasonOpponentProfile>(teamSeasonOpponentProfiles.ToList());

            var teamSeasonScheduleTotals =
                _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(teamName, seasonYear);
            TeamSeasonScheduleTotals =
                new ReadOnlyCollection<TeamSeasonScheduleTotals>(
                    new List<TeamSeasonScheduleTotals>
                    {
                        teamSeasonScheduleTotals
                    });

            var teamSeasonScheduleAverages =
                _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(teamName, seasonYear);
            TeamSeasonScheduleAverages =
                new ReadOnlyCollection<TeamSeasonScheduleAverages>(
                    new List<TeamSeasonScheduleAverages>
                    {
                        teamSeasonScheduleAverages
                    });
        }
    }
}
