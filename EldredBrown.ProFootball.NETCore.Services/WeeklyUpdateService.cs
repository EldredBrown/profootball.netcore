using System;
using System.Linq;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
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
        private readonly ICalculator _calculator;

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
        /// <param name="calculator">The calculator service.</param>
        public WeeklyUpdateService(
            ISeasonRepository seasonRepository,
            IGameRepository gameRepository,
            ILeagueSeasonRepository leagueSeasonRepository,
            ILeagueSeasonTotalsRepository leagueSeasonTotalsRepository,
            ITeamSeasonRepository teamSeasonRepository,
            ITeamSeasonScheduleTotalsRepository teamSeasonScheduleTotalsRepository,
            ITeamSeasonScheduleAveragesRepository teamSeasonScheduleAveragesRepository,
            ISharedRepository sharedRepository,
            ICalculator calculator)
        {
            _seasonRepository = seasonRepository;
            _gameRepository = gameRepository;
            _leagueSeasonRepository = leagueSeasonRepository;
            _leagueSeasonTotalsRepository = leagueSeasonTotalsRepository;
            _teamSeasonRepository = teamSeasonRepository;
            _teamSeasonScheduleTotalsRepository = teamSeasonScheduleTotalsRepository;
            _teamSeasonScheduleAveragesRepository = teamSeasonScheduleAveragesRepository;
            _sharedRepository = sharedRepository;
            _calculator = calculator;
        }

        /// <summary>
        /// Runs a weekly update of the data store.
        /// </summary>
        public async Task RunWeeklyUpdate(int seasonId)
        {
            // TODO: 2019-12-06 - Reimplement this using ASP.NET Core.
            //// I experimented with farming this long running operation out to a separate thread to improve UI
            //// responsiveness, but I eventually concluded that I actually DON'T want the UI to be responsive while
            //// this update operation is running. An attempt to view data that's in the process of changing may
            //// cause the application to crash, and the data won't mean anything, anyway.
            //var dlgResult = _sharedService.ShowMessageBox(
            //    "This operation may make the UI unresponsive for a minute or two. Are you sure you want to continue?",
            //    WpfGlobals.Constants.AppName, MessageBoxButton.YesNo, MessageBoxImage.Question);

            //if (dlgResult == MessageBoxResult.No)
            //{
            //    return;
            //}

            // These hard-coded values are a bit of a hack at this time, but I intend to make them selectable by the
            // user in the future.
            var leagueName = "APFA";

            UpdateLeagueSeason(leagueName, seasonId);
            var srcWeekCount = await UpdateWeekCount(seasonId);

            await _sharedRepository.SaveChanges();

            if (srcWeekCount >= 3)
            {
                await UpdateRankings();
            }

            //_sharedService.ShowMessageBox("Weekly update completed.", WpfGlobals.Constants.AppName, MessageBoxButton.OK,
            //    MessageBoxImage.Information);
        }

        private void UpdateLeagueSeason(string leagueName, int seasonId)
        {
            var leagueSeason = _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonId);

            //try
            //{
            var leagueSeasonTotals = _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonId);
            //}
            //catch (ArgumentNullException ex)
            //{
            //    Log.Error($"ArgumentNullException caught in MainWindowService.UpdateLeagueSeason: {ex.Message}");

            //    _sharedService.ShowExceptionMessage(ex);

            //    return;
            //}

            if (leagueSeasonTotals.TotalGames == null)
            {
                return;
            }

            leagueSeason.TotalGames = leagueSeasonTotals.TotalGames.Value;
            leagueSeason.TotalPoints = leagueSeasonTotals.TotalPoints.Value;
            leagueSeason.AveragePoints = _calculator.Divide(
                leagueSeasonTotals.TotalPoints.Value, leagueSeasonTotals.TotalGames.Value);
        }

        private async Task UpdateRankings()
        {
            // Get the list of TeamSeason objects for the selected season.
            var teamSeasons = (await _teamSeasonRepository.GetTeamSeasons())
                .Where(ts => ts.SeasonId == _selectedSeason);

            // This looks like the place where I want to make maximum use of parallel threading.
            //Parallel.ForEach(teamSeasons, teamSeason => UpdateRankingsByTeamSeason(teamSeason));

            // Iterate through the list of TeamSeason objects.
            foreach (var teamSeason in teamSeasons)
            {
                await UpdateRankingsByTeamSeason(teamSeason);
            }

            // Save changes.
            await _sharedRepository.SaveChanges();
        }

        private async Task UpdateRankingsByTeamSeason(TeamSeason teamSeason)
        {
            try
            {
                var teamSeasonScheduleTotals =
                    await _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(
                        teamSeason.TeamName, teamSeason.SeasonId);

                var teamSeasonScheduleAverages =
                    await _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(
                        teamSeason.TeamName, teamSeason.SeasonId);

                lock (_dbLock)
                {
                    if (teamSeasonScheduleTotals != null && teamSeasonScheduleAverages != null &&
                        teamSeasonScheduleTotals.ScheduleGames != null)
                    {
                        teamSeason.OffensiveAverage = _calculator.Divide(
                            teamSeason.PointsFor, teamSeason.Games);
                        teamSeason.DefensiveAverage = _calculator.Divide(
                            teamSeason.PointsAgainst, teamSeason.Games);

                        teamSeason.OffensiveFactor = _calculator.Divide(
                            teamSeason.OffensiveAverage.Value, teamSeasonScheduleAverages.PointsAgainst.Value);

                        teamSeason.DefensiveFactor = _calculator.Divide(
                            teamSeason.DefensiveAverage.Value, teamSeasonScheduleAverages.PointsFor.Value);

                        var leagueSeason = 
                            _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(
                                teamSeason.LeagueName, teamSeason.SeasonId);

                        teamSeason.OffensiveIndex = (teamSeason.OffensiveAverage + teamSeason.OffensiveFactor *
                            leagueSeason.AveragePoints) / 2d;

                        teamSeason.DefensiveIndex = (teamSeason.DefensiveAverage + teamSeason.DefensiveFactor *
                            leagueSeason.AveragePoints) / 2d;

                        teamSeason.FinalPythagoreanWinningPercentage =
                            _calculator.CalculatePythagoreanWinningPercentage(teamSeason);
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

        private async Task<int> UpdateWeekCount(int seasonId)
        {
            var srcWeekCount = (await _gameRepository.GetGames())
                .Where(g => g.SeasonId == seasonId)
                .Select(g => g.Week)
                .Max();

            var destSeason = await _seasonRepository.GetSeason(seasonId);

            destSeason.NumOfWeeksCompleted = srcWeekCount;

            return srcWeekCount;
        }
    }
}
