using System;
using System.Threading.Tasks;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;

namespace EldredBrown.ProFootball.NETCore.Services
{
    public enum Direction
    {
        Down,
        Up
    }

    delegate int Operation(int x, int y);

    /// <summary>
    /// Service to handle the more complicated actions of adding, editing, or deleting games in the data store.
    /// </summary>
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamSeasonRepository _teamSeasonRepository;
        private readonly ISharedRepository _sharedRepository;
        private readonly ICalculator _calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="gameRepository">The repository by which game data will be accessed.</param>
        /// <param name="teamSeasonRepository">The repository by which team season data will be accessed.</param>
        /// <param name="sharedRepository">The repository by which data will be accessed.</param>
        /// <param name="calculator">The calcualator by which mathematical operations will be computed.</param>
        public GameService(
            IGameRepository gameRepository,
            ITeamSeasonRepository teamSeasonRepository,
            ISharedRepository sharedRepository,
            ICalculator calculator)
        {
            _gameRepository = gameRepository;
            _teamSeasonRepository = teamSeasonRepository;
            _sharedRepository = sharedRepository;
            _calculator = calculator;
        }

        /// <summary>
        /// Adds a <see cref="Game"/> entity to the data store.
        /// </summary>
        /// <param name="game">The <see cref="Game"/> entity to add to the data store.</param>
        public async Task AddGame(Game game)
        {
            DecideWinnerAndLoser(game);

            await _gameRepository.Add(game);

            await AddGameToTeams(game);

            await _sharedRepository.SaveChanges();
        }

        /// <summary>
        /// Edits a <see cref="Game"/> entity in the data store.
        /// </summary>
        /// <param name="oldGame">The <see cref="Game"/> entity containing data to remove from the data store.</param>
        /// <param name="newGame">The <see cref="Game"/> entity containing data to add to the data store.</param>
        public async Task EditGame(Game oldGame, Game newGame)
        {
            DecideWinnerAndLoser(newGame);

            var selectedGame = await _gameRepository.GetGame(newGame.ID);
            selectedGame.Week = newGame.Week;
            selectedGame.GuestName = newGame.GuestName;
            selectedGame.GuestScore = newGame.GuestScore;
            selectedGame.HostName = newGame.HostName;
            selectedGame.HostScore = newGame.HostScore;
            selectedGame.WinnerName = newGame.WinnerName;
            selectedGame.WinnerScore = newGame.WinnerScore;
            selectedGame.LoserName = newGame.LoserName;
            selectedGame.LoserScore = newGame.LoserScore;
            selectedGame.IsPlayoffGame = newGame.IsPlayoffGame;
            selectedGame.Notes = newGame.Notes;

            _gameRepository.Edit(selectedGame);

            await EditGameInTeams(oldGame, newGame);

            await _sharedRepository.SaveChanges();
        }

        /// <summary>
        /// Deletes a <see cref="Game"/> entity from the data store.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Game"/> entity to delete.</param>
        public async Task DeleteGame(int id)
        {
            var oldGame = await _gameRepository.GetGame(id);

            DecideWinnerAndLoser(oldGame);

            await DeleteGameFromTeams(oldGame);

            await _gameRepository.Delete(id);
            await _sharedRepository.SaveChanges();
        }

        private async Task AddGameToTeams(Game game)
        {
            await EditTeams(game, Direction.Up);
        }

        private void DecideWinnerAndLoser(Game game)
        {
            if (game.GuestScore > game.HostScore)
            {
                game.WinnerName = game.GuestName;
                game.WinnerScore = game.GuestScore;
                game.LoserName = game.HostName;
                game.LoserScore = game.HostScore;
            }
            else if (game.HostScore > game.GuestScore)
            {
                game.WinnerName = game.HostName;
                game.WinnerScore = game.HostScore;
                game.LoserName = game.GuestName;
                game.LoserScore = game.GuestScore;
            }
            else
            {
                game.WinnerName = null;
                game.LoserName = null;
            }
        }

        private async Task DeleteGameFromTeams(Game oldGame)
        {
            await EditTeams(oldGame, Direction.Down);
        }

        private async Task EditGameInTeams(Game oldGame, Game newGame)
        {
            await EditTeams(oldGame, Direction.Down);
            await EditTeams(newGame, Direction.Up);
        }

        private async Task EditScoringData(Game game, Operation operation)
        {
            await EditScoringDataByTeamSeason(game.GuestName, game.SeasonYear, operation, game.GuestScore, game.HostScore);
            await EditScoringDataByTeamSeason(game.HostName, game.SeasonYear, operation, game.HostScore, game.GuestScore);
        }

        private async Task EditScoringDataByTeamSeason(string teamName, int seasonYear, Operation operation,
            int teamScore, int opponentScore)
        {
            var teamSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(teamName, seasonYear);
            if (teamSeason != null)
            {
                teamSeason.PointsFor = operation(teamSeason.PointsFor, teamScore);
                teamSeason.PointsAgainst = operation(teamSeason.PointsAgainst, opponentScore);

                var pythPct = _calculator.CalculatePythagoreanWinningPercentage(teamSeason);
                if (pythPct == null)
                {
                    teamSeason.PythagoreanWins = 0;
                    teamSeason.PythagoreanLosses = 0;
                }
                else
                {
                    teamSeason.PythagoreanWins = _calculator.Multiply(pythPct.Value, teamSeason.Games);
                    teamSeason.PythagoreanLosses = _calculator.Multiply((1d - pythPct.Value), teamSeason.Games);
                }
            }
        }

        private async Task EditTeams(Game game, Direction direction)
        {
            Operation operation;

            // Decide whether the teams need to be edited up or down.
            // Up for new game, down then up for edited game, down for deleted game.
            switch (direction)
            {
                case Direction.Up:
                    operation = new Operation(_calculator.Add);
                    break;

                case Direction.Down:
                    operation = new Operation(_calculator.Subtract);
                    break;

                default:
                    throw new ArgumentException("direction");
            }

            //try
            //{
                await ProcessGame(game, operation);
            //}
            //catch (ObjectNotFoundException ex)
            //{
            //    Log.Error("ObjectNotFoundException in GamesService.EditTeams: " + ex.Message);
            //    _sharedService.ShowExceptionMessage(ex, "ObjectNotFoundException");
            //}
        }

        private async Task EditWinLossData(Game game, Operation operation)
        {
            var seasonYear = game.SeasonYear;

            var guestSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.GuestName, seasonYear);
            if (guestSeason != null)
            {
                guestSeason.Games = operation(guestSeason.Games, 1);
            }

            var hostSeason = await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.HostName, seasonYear);
            if (hostSeason != null)
            {
                hostSeason.Games = operation(hostSeason.Games, 1);
            }

            var winnerName = game.WinnerName;
            var loserName = game.LoserName;
            if (string.IsNullOrEmpty(winnerName) || string.IsNullOrEmpty(loserName))
            {
                // Game is a tie.
                if (guestSeason != null)
                {
                    guestSeason.Ties = operation(guestSeason.Ties, 1);
                }

                if (hostSeason != null)
                {
                    hostSeason.Ties = operation(hostSeason.Ties, 1);
                }
            }
            else
            {
                // Game is not a tie (has a winner and a loser).
                var winnerSeason = 
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.WinnerName, seasonYear);
                if (winnerSeason != null)
                {
                    winnerSeason.Wins = operation(winnerSeason.Wins, 1);
                }

                var loserSeason = 
                    await _teamSeasonRepository.GetTeamSeasonByTeamAndSeason(game.LoserName, seasonYear);
                if (loserSeason != null)
                {
                    loserSeason.Losses = operation(loserSeason.Losses, 1);
                }
            }

            // Calculate each team's season winning percentage.
            if (guestSeason != null)
            {
                guestSeason.WinningPercentage = _calculator.CalculateWinningPercentage(guestSeason);
            }

            if (hostSeason != null)
            {
                hostSeason.WinningPercentage = _calculator.CalculateWinningPercentage(hostSeason);
            }
        }

        private async Task ProcessGame(Game game, Operation operation)
        {
            await EditWinLossData(game, operation);
            await EditScoringData(game, operation);
        }
    }
}
