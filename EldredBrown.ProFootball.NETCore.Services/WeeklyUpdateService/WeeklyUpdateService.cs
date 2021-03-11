﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Decorators;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    /// <summary>
    /// A service to run a weekly update of the pro football data store.
    /// </summary>
    public class WeeklyUpdateService : IWeeklyUpdateService
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILeagueSeasonRepository _leagueSeasonRepository;
        private readonly ILeagueSeasonTotalsRepository _leagueSeasonTotalsRepository;
        private readonly ITeamSeasonRepository _teamSeasonRepository;
        private readonly ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private readonly ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;
        private readonly ISharedRepository _sharedRepository;

        private object _dbLock = new object();

        private int _selectedSeason = 1920;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklyUpdateService"/> class.
        /// </summary>
        /// <param name="seasonRepository">The repository by which WeekCount data will be accessed.</param>
        /// <param name="gameRepository">The repository by which Game data will be accessed.</param>
        /// <param name="leagueSeasonRepository">The repository by which LeagueSeason data will be accessed.</param>
        /// <param name="leagueSeasonTotalsRepository">The repository by which LeagueSeasonTotals data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which TeamSeason data will be accessed.</param>
        /// <param name="teamSeasonScheduleTotalsRepository">The repository by which TeamSeasonScheduleTotals data will be accessed.</param>
        /// <param name="teamSeasonScheduleAveragesRepository">The repository by which TeamSeasonScheduleAverages data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which data will be accessed.</param>
        public WeeklyUpdateService(
            ISeasonRepository seasonRepository,
            IGameRepository gameRepository,
            ILeagueSeasonRepository leagueSeasonRepository,
            ILeagueSeasonTotalsRepository leagueSeasonTotalsRepository,
            ITeamSeasonRepository teamSeasonRepository,
            ITeamSeasonScheduleTotalsRepository teamSeasonScheduleTotalsRepository,
            ITeamSeasonScheduleAveragesRepository teamSeasonScheduleAveragesRepository,
            ISharedRepository sharedRepository)
        {
            _seasonRepository = seasonRepository;
            _gameRepository = gameRepository;
            _leagueSeasonRepository = leagueSeasonRepository;
            _leagueSeasonTotalsRepository = leagueSeasonTotalsRepository;
            _teamSeasonRepository = teamSeasonRepository;
            _teamSeasonScheduleTotalsRepository = teamSeasonScheduleTotalsRepository;
            _teamSeasonScheduleAveragesRepository = teamSeasonScheduleAveragesRepository;
            _sharedRepository = sharedRepository;
        }

        /// <summary>
        /// Runs a weekly update of the data store.
        /// </summary>
        public async Task RunWeeklyUpdate(int seasonYear)
        {
            // These hard-coded values are a bit of a hack at this time, but I intend to make them selectable by the
            // user in the future.
            var leagueName = "APFA";

            UpdateLeagueSeason(leagueName, seasonYear);
            var srcWeekCount = await UpdateWeekCount(seasonYear);

            await _sharedRepository.SaveChanges();

            if (srcWeekCount >= 3)
            {
                await UpdateRankings();
            }
        }

        private void UpdateLeagueSeason(string leagueName, int seasonYear)
        {
            var leagueSeasonTotals = _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear);
            if (leagueSeasonTotals.TotalGames is null)
            {
                return;
            }

            var leagueSeason = _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear);
            LeagueSeasonDecorator leagueSeasonDecorator = null;
            if (!(leagueSeason is null))
            {
                leagueSeasonDecorator = new LeagueSeasonDecorator(leagueSeason);
            }
            leagueSeasonDecorator.UpdateGamesAndPoints(leagueSeasonTotals.TotalGames.Value,
                leagueSeasonTotals.TotalPoints.Value);
        }

        private async Task<int> UpdateWeekCount(int seasonYear)
        {
            var srcWeekCount = (await _gameRepository.GetGames())
                .Where(g => g.SeasonYear == seasonYear)
                .Select(g => g.Week)
                .Max();

            var destSeason = await _seasonRepository.GetSeason(seasonYear);

            destSeason.NumOfWeeksCompleted = srcWeekCount;

            return srcWeekCount;
        }

        private async Task UpdateRankings()
        {
            var teamSeasons = (await _teamSeasonRepository.GetTeamSeasons())
                .Where(ts => ts.SeasonYear == _selectedSeason);

            foreach (var teamSeason in teamSeasons)
            {
                var teamSeasonDecorator = new TeamSeasonDecorator(teamSeason);
                await UpdateRankingsForTeamSeason(teamSeasonDecorator);
            }

            await _sharedRepository.SaveChanges();
        }

        private async Task UpdateRankingsForTeamSeason(TeamSeasonDecorator teamSeasonDecorator)
        {
            try
            {
                var teamSeasonScheduleTotals =
                    await _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(
                        teamSeasonDecorator.TeamName, teamSeasonDecorator.SeasonYear);

                var teamSeasonScheduleAverages =
                    await _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(
                        teamSeasonDecorator.TeamName, teamSeasonDecorator.SeasonYear);

                lock (_dbLock)
                {
                    if (!(teamSeasonScheduleTotals is null) && !(teamSeasonScheduleAverages is null) &&
                        !(teamSeasonScheduleTotals.ScheduleGames is null))
                    {
                        var leagueSeason =
                            _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(
                                teamSeasonDecorator.LeagueName, teamSeasonDecorator.SeasonYear);

                        teamSeasonDecorator.UpdateRankings(teamSeasonScheduleAverages.PointsFor.Value,
                            teamSeasonScheduleAverages.PointsAgainst.Value, leagueSeason.AveragePoints.Value);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Nullable object must have a value.")
                {
                    // Ignore exception.
                }
                else
                {
                    //_sharedService.ShowExceptionMessage(ex, $"InvalidOperationException: {teamSeason.TeamName}");
                }
            }
            catch (Exception)
            {
                //_sharedService.ShowExceptionMessage(ex.InnerException, $"Exception: {teamSeason.TeamName}");
            }
        }
    }
}