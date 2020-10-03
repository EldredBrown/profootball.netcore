using System.Collections.Generic;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using NUnit.Framework;

namespace EldredBrown.ProFootball.NETCore.Services.Tests
{
    [TestFixture]
    public class WeeklyUpdateServiceTests
    {
        private ISeasonRepository _seasonRepository;
        private IGameRepository _gameRepository;
        private ILeagueSeasonRepository _leagueSeasonRepository;
        private ILeagueSeasonTotalsRepository _leagueSeasonTotalsRepository;
        private ITeamSeasonRepository _teamSeasonRepository;
        private ITeamSeasonScheduleTotalsRepository _teamSeasonScheduleTotalsRepository;
        private ITeamSeasonScheduleAveragesRepository _teamSeasonScheduleAveragesRepository;
        private ISharedRepository _sharedRepository;

        [SetUp]
        public void Setup()
        {
            _seasonRepository = A.Fake<ISeasonRepository>();
            _gameRepository = A.Fake<IGameRepository>();
            _leagueSeasonRepository = A.Fake<ILeagueSeasonRepository>();
            _leagueSeasonTotalsRepository = A.Fake<ILeagueSeasonTotalsRepository>();
            _teamSeasonRepository = A.Fake<ITeamSeasonRepository>();
            _teamSeasonScheduleTotalsRepository = A.Fake<ITeamSeasonScheduleTotalsRepository>();
            _teamSeasonScheduleAveragesRepository = A.Fake<ITeamSeasonScheduleAveragesRepository>();
            _sharedRepository = A.Fake<ISharedRepository>();
        }

        [Test]
        public async Task RunWeeklyUpdate_DoesNotUpdateLeagueSeasonWhenLeagueSeasonTotalGamesIsNull()
        {
            var leagueSeason = A.Fake<LeagueSeason>();
            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeason);

            var leagueSeasonTotals = new LeagueSeasonTotals { TotalGames = null };
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(A<string>.Ignored, A<int>.Ignored))
                .Returns(leagueSeasonTotals);

            int seasonYear = 1920;
            var games = new List<Game>{
                new Game {SeasonYear = seasonYear, Week = 0},
                new Game {SeasonYear = seasonYear, Week = 1},
                new Game {SeasonYear = seasonYear, Week = 2}
            };
            A.CallTo(() => _gameRepository.GetGames()).Returns(games);

            var service = new WeeklyUpdateService(
                _seasonRepository, _gameRepository, _leagueSeasonRepository, _leagueSeasonTotalsRepository,
                _teamSeasonRepository, _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository,
                _sharedRepository);

            await service.RunWeeklyUpdate(seasonYear);

            var leagueName = "APFA";

            A.CallTo(() => _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear))
                .MustHaveHappened();
            A.CallTo(() => _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear))
                .MustHaveHappened();

            A.CallTo(() => leagueSeason.UpdateGamesAndPoints(A<int>.Ignored, A<int>.Ignored)).MustNotHaveHappened();

            A.CallTo(() => _gameRepository.GetGames()).MustHaveHappened();
            A.CallTo(() => _seasonRepository.GetSeason(seasonYear)).MustHaveHappened();
            A.CallTo(() => _sharedRepository.SaveChanges()).MustHaveHappened();

            A.CallTo(() => _teamSeasonRepository.GetTeamSeasons()).MustNotHaveHappened();
        }

        //[Test]
        //public async Task RunWeeklyUpdate_()
        //{
        //    var service = new WeeklyUpdateService(
        //        _seasonRepository, _gameRepository, _leagueSeasonRepository, _leagueSeasonTotalsRepository,
        //        _teamSeasonRepository, _teamSeasonScheduleTotalsRepository, _teamSeasonScheduleAveragesRepository,
        //        _sharedRepository);

        //    int seasonYear = 1920;
        //    await service.RunWeeklyUpdate(seasonYear);


        //    var leagueName = "APFA";

        //    //UpdateLeagueSeason(leagueName, seasonYear);
        //    var leagueSeason = _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(leagueName, seasonYear);

        //    var leagueSeasonTotals = _leagueSeasonTotalsRepository.GetLeagueSeasonTotals(leagueName, seasonYear);
        //    if (leagueSeasonTotals.TotalGames == null)
        //    {
        //        return;
        //    }

        //    leagueSeason.UpdateGamesAndPoints(leagueSeasonTotals.TotalGames.Value,
        //        leagueSeasonTotals.TotalPoints.Value);

        //    //var srcWeekCount = await UpdateWeekCount(seasonYear);
        //    var srcWeekCount = (await _gameRepository.GetGames())
        //        .Where(g => g.SeasonYear == seasonYear)
        //        .Select(g => g.Week)
        //        .Max();

        //    var destSeason = await _seasonRepository.GetSeason(seasonYear);

        //    destSeason.NumOfWeeksCompleted = srcWeekCount;

        //    await _sharedRepository.SaveChanges();

        //    if (srcWeekCount >= 3)
        //    {
        //        //await UpdateRankings();
        //        var teamSeasons = (await _teamSeasonRepository.GetTeamSeasons())
        //            .Where(ts => ts.SeasonYear == _selectedSeason);

        //        foreach (var teamSeason in teamSeasons)
        //        {
        //            //await UpdateRankingsForTeamSeason(teamSeason);
        //            try
        //            {
        //                var teamSeasonScheduleTotals =
        //                    await _teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotals(
        //                        teamSeason.TeamName, teamSeason.SeasonYear);

        //                var teamSeasonScheduleAverages =
        //                    await _teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAverages(
        //                        teamSeason.TeamName, teamSeason.SeasonYear);

        //                lock (_dbLock)
        //                {
        //                    if (teamSeasonScheduleTotals != null && teamSeasonScheduleAverages != null &&
        //                        teamSeasonScheduleTotals.ScheduleGames != null)
        //                    {
        //                        var leagueSeason =
        //                            _leagueSeasonRepository.GetLeagueSeasonByLeagueAndSeason(
        //                                teamSeason.LeagueName, teamSeason.SeasonYear);

        //                        teamSeason.UpdateRankings(teamSeasonScheduleAverages.PointsFor,
        //                            teamSeasonScheduleAverages.PointsAgainst, leagueSeason.AveragePoints);
        //                    }
        //                }
        //            }
        //            catch (InvalidOperationException ex)
        //            {
        //                if (ex.Message == "Nullable object must have a value.")
        //                {
        //                    // Ignore exception.
        //                }
        //                else
        //                {
        //                    //_sharedService.ShowExceptionMessage(ex, $"InvalidOperationException: {teamSeason.TeamName}");
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                //_sharedService.ShowExceptionMessage(ex.InnerException, $"Exception: {teamSeason.TeamName}");
        //            }
        //        }

        //        await _sharedRepository.SaveChanges();
        //    }
        //    Assert.Pass();
        //}
    }
}