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
        private readonly IGameRepository _gameRepository;
        private readonly IWeekCountRepository _weekCountRepository;
        private readonly ISeasonLeagueRepository _seasonLeagueRepository;
        private readonly ISeasonLeagueTotalsRepository _seasonLeagueTotalsRepository;
        private readonly ISeasonTeamRepository _seasonTeamRepository;
        private readonly ISeasonTeamScheduleTotalsRepository _seasonTeamScheduleTotalsRepository;
        private readonly ISeasonTeamScheduleAveragesRepository _seasonTeamScheduleAveragesRepository;
        private readonly ISharedRepository _sharedRepository;
        private readonly ICalculator _calculator;

        private object _dbLock = new object();

        private int _selectedSeason = 1920;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklyUpdateService"/> class.
        /// </summary>
        /// <param name="gameRepository">The repository by which Game data will be accessed.</param>
        /// <param name="weekCountRepository">The repository by which WeekCount data will be accessed.</param>
        /// <param name="seasonLeagueRepository">The repository by which SeasonLeague data will be accessed.</param>
        /// <param name="seasonLeagueTotalsRepository">The repository by which SeasonLeagueTotals data will be accessed.</param>
        /// <param name="seasonTeamRepository">The repository by which SeasonTeam data will be accessed.</param>
        /// <param name="seasonTeamScheduleTotalsRepository">The repository by which SeasonTeamScheduleTotals data will be accessed.</param>
        /// <param name="seasonTeamScheduleAveragesRepository">The repository by which SeasonTeamScheduleAverages data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which data will be accessed.</param>
        /// <param name="calculator">The calculator service.</param>
        public WeeklyUpdateService(
            IGameRepository gameRepository,
            IWeekCountRepository weekCountRepository,
            ISeasonLeagueRepository seasonLeagueRepository,
            ISeasonLeagueTotalsRepository seasonLeagueTotalsRepository,
            ISeasonTeamRepository seasonTeamRepository,
            ISeasonTeamScheduleTotalsRepository seasonTeamScheduleTotalsRepository,
            ISeasonTeamScheduleAveragesRepository seasonTeamScheduleAveragesRepository,
            ISharedRepository sharedRepository,
            ICalculator calculator)
        {
            _gameRepository = gameRepository;
            _weekCountRepository = weekCountRepository;
            _seasonLeagueRepository = seasonLeagueRepository;
            _seasonLeagueTotalsRepository = seasonLeagueTotalsRepository;
            _seasonTeamRepository = seasonTeamRepository;
            _seasonTeamScheduleTotalsRepository = seasonTeamScheduleTotalsRepository;
            _seasonTeamScheduleAveragesRepository = seasonTeamScheduleAveragesRepository;
            _sharedRepository = sharedRepository;
            _calculator = calculator;
        }

        /// <summary>
        /// Runs a weekly update of the data store.
        /// </summary>
        public async Task RunWeeklyUpdate()
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
            var seasonId = 1920;

            UpdateSeasonLeague(seasonId, leagueName);
            var srcWeekCount = await UpdateWeekCount(seasonId);

            await _sharedRepository.SaveChanges();

            if (srcWeekCount >= 3)
            {
                await UpdateRankings();
            }

            //_sharedService.ShowMessageBox("Weekly update completed.", WpfGlobals.Constants.AppName, MessageBoxButton.OK,
            //    MessageBoxImage.Information);
        }

        private async Task UpdateRankings()
        {
            // Get the list of TeamSeason objects for the selected season.
            var seasonTeams = (await _seasonTeamRepository.GetSeasonTeams())
                .Where(st => st.SeasonId == _selectedSeason);

            // This looks like the place where I want to make maximum use of parallel threading.
            //Parallel.ForEach(teamSeasons, seasonTeam => UpdateRankingsByTeamSeason(seasonTeam));

            // Iterate through the list of TeamSeason objects.
            foreach (var seasonTeam in seasonTeams)
            {
                UpdateRankingsBySeasonTeam(seasonTeam);
            }

            // Save changes.
            await _sharedRepository.SaveChanges();
        }

        private void UpdateRankingsBySeasonTeam(SeasonTeam seasonTeam)
        {
            try
            {
                lock (_dbLock)
                {
                    // Get needed stored procedure results.
                    var seasonTeamScheduleTotals = 
                        _seasonTeamScheduleTotalsRepository.GetSeasonTeamScheduleTotals(
                            seasonTeam.SeasonId, seasonTeam.TeamName);

                    var seasonTeamScheduleAverages = 
                        _seasonTeamScheduleAveragesRepository.GetSeasonTeamScheduleAverages(
                            seasonTeam.SeasonId, seasonTeam.TeamName);

                    // Calculate new rankings.
                    if (seasonTeamScheduleTotals != null && seasonTeamScheduleAverages != null &&
                        seasonTeamScheduleTotals.ScheduleGames != null)
                    {
                        seasonTeam.OffensiveAverage = _calculator.Divide(
                            (decimal)seasonTeam.PointsFor, (decimal)seasonTeam.Games);
                        seasonTeam.DefensiveAverage = _calculator.Divide(
                            (decimal)seasonTeam.PointsAgainst, (decimal)seasonTeam.Games);

                        seasonTeam.OffensiveFactor = _calculator.Divide(
                            seasonTeam.OffensiveAverage.Value, seasonTeamScheduleAverages.PointsAgainst.Value);

                        seasonTeam.DefensiveFactor = _calculator.Divide(
                            seasonTeam.DefensiveAverage.Value, seasonTeamScheduleAverages.PointsFor.Value);

                        var seasonLeague = _seasonLeagueRepository.GetSeasonLeagues()
                            .FirstOrDefault(st => 
                                st.SeasonId == seasonTeam.SeasonId && st.LeagueName == seasonTeam.LeagueName);

                        seasonTeam.OffensiveIndex = (seasonTeam.OffensiveAverage + seasonTeam.OffensiveFactor *
                            seasonLeague.AveragePoints) / 2m;

                        seasonTeam.DefensiveIndex = (seasonTeam.DefensiveAverage + seasonTeam.DefensiveFactor *
                            seasonLeague.AveragePoints) / 2m;

                        seasonTeam.FinalPythagoreanWinningPercentage =
                            _calculator.CalculatePythagoreanWinningPercentage(seasonTeam);
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
            catch (Exception ex)
            {
                //_sharedService.ShowExceptionMessage(ex.InnerException, $"Exception: {teamSeason.TeamName}");
            }
        }

        private void UpdateSeasonLeague(int seasonId, string leagueName)
        {
            var seasonLeague = _seasonLeagueRepository.GetSeasonLeagues()
                .FirstOrDefault(sl => sl.SeasonId == seasonId && sl.LeagueName == leagueName);

            //try
            //{
            var seasonLeagueTotals = _seasonLeagueTotalsRepository.GetSeasonLeagueTotals(seasonId, leagueName);
            //}
            //catch (ArgumentNullException ex)
            //{
            //    Log.Error($"ArgumentNullException caught in MainWindowService.UpdateLeagueSeason: {ex.Message}");

            //    _sharedService.ShowExceptionMessage(ex);

            //    return;
            //}

            if (seasonLeagueTotals.TotalGames == null)
            {
                return;
            }

            seasonLeague.TotalGames = seasonLeagueTotals.TotalGames.Value;
            seasonLeague.TotalPoints = seasonLeagueTotals.TotalPoints.Value;
            seasonLeague.AveragePoints = 
                (decimal)seasonLeagueTotals.TotalPoints.Value / (decimal)seasonLeagueTotals.TotalGames.Value;
        }

        private async Task<int> UpdateWeekCount(int seasonId)
        {
            var srcWeekCount = (await _gameRepository.GetGames())
                .Where(g => g.SeasonId == seasonId)
                .Select(g => g.Week)
                .Max();

            var destWeekCount = _weekCountRepository.GetWeekCount(seasonId);

            destWeekCount.Count = srcWeekCount;

            return srcWeekCount;
        }
    }
}
