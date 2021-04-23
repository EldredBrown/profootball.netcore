using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using EldredBrown.ProFootball.NETCore.WpfApp.Properties;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows;
using EldredBrown.ProFootball.WpfApp;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class GamesWindowViewModel : ViewModelBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IGameService _gameService;
        private readonly IGameFinderWindowFactory _gameFinderWindowFactory;

        private string _filterGuestName;
        private string _filterHostName;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesWindowViewModel"/> class.
        /// </summary>
        /// <param name="gameRepository">The <see cref="IGameRepository"/> by which game data will be accessed.</param>
        /// <param name="seasonRepository">
        /// The <see cref="ISeasonRepository"/> by which season data will be accessed.
        /// </param>
        /// <param name="gameService">
        /// The <see cref="IGameService"/> object that will process game data in the data store.
        /// </param>
        /// <param name="gameFinderWindowFactory">
        /// The <see cref="IGameFinderWindowFactory"/> that will create instances of the
        /// <see cref="IGameFinderWindow"/> interface.
        /// </param>
        public GamesWindowViewModel(IGameRepository gameRepository = null, ISeasonRepository seasonRepository = null,
            IGameService gameService = null, IGameFinderWindowFactory gameFinderWindowFactory = null)
        {
            _gameRepository = gameRepository ??
                App.ServiceProvider.GetService(typeof(IGameRepository)) as IGameRepository;
            _seasonRepository = seasonRepository ??
                App.ServiceProvider.GetService(typeof(ISeasonRepository)) as ISeasonRepository;
            _gameService = gameService ??
                App.ServiceProvider.GetService(typeof(IGameService)) as IGameService;
            _gameFinderWindowFactory = gameFinderWindowFactory ??
                App.ServiceProvider.GetService(typeof(IGameFinderWindowFactory)) as IGameFinderWindowFactory;
        }

        #region Detail Properties

        /// <summary>
        /// Gets or sets the Week value for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private int _week;
        public int Week
        {
            get
            {
                return _week;
            }
            set
            {
                if (value != _week)
                {
                    _week = value;
                    OnPropertyChanged("Week");
                }
            }
        }

        /// <summary>
        /// Gets or sets the GuestName value for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private string _guestName;
        public string GuestName
        {
            get
            {
                return _guestName;
            }
            set
            {
                if (value != _guestName)
                {
                    _guestName = value;
                    OnPropertyChanged("GuestName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the GuestScore value for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private int _guestScore;
        public int GuestScore
        {
            get
            {
                return _guestScore;
            }
            set
            {
                if (value != _guestScore)
                {
                    _guestScore = value;
                    OnPropertyChanged("GuestScore");
                }
            }
        }

        /// <summary>
        /// Gets or sets the HostName value for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private string _hostName;
        public string HostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                if (value != _hostName)
                {
                    _hostName = value;
                    OnPropertyChanged("HostName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the HostScore value for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private int _hostScore;
        public int HostScore
        {
            get
            {
                return _hostScore;
            }
            set
            {
                if (value != _hostScore)
                {
                    _hostScore = value;
                    OnPropertyChanged("HostScore");
                }
            }
        }

        /// <summary>
        /// Gets or sets the IsPlayoff value for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private bool _isPlayoff;
        public bool IsPlayoff
        {
            get
            {
                return _isPlayoff;
            }
            set
            {
                if (value != _isPlayoff)
                {
                    _isPlayoff = value;
                    OnPropertyChanged("IsPlayoff");
                }
            }
        }

        /// <summary>
        /// Gets or sets the flag that indicates whether SelectedGame game can be a playoff game.
        /// </summary>
        private bool _isPlayoffEnabled;
        public bool IsPlayoffEnabled
        {
            get
            {
                return _isPlayoffEnabled;
            }
            set
            {
                if (value != _isPlayoffEnabled)
                {
                    _isPlayoffEnabled = value;
                    OnPropertyChanged("IsPlayoffEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Notes value for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private string _notes;
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (value != _notes)
                {
                    _notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the AddGame control
        /// </summary>
        private Visibility _addGameControlVisibility = Visibility.Visible;
        public Visibility AddGameControlVisibility
        {
            get
            {
                return _addGameControlVisibility;
            }
            set
            {
                if (value != _addGameControlVisibility)
                {
                    _addGameControlVisibility = value;
                    OnPropertyChanged("AddGameControlVisibility");
                }
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the EditGame control
        /// </summary>
        private Visibility _editGameControlVisibility = Visibility.Hidden;
        public Visibility EditGameControlVisibility
        {
            get
            {
                return _editGameControlVisibility;
            }
            set
            {
                if (value != _editGameControlVisibility)
                {
                    _editGameControlVisibility = value;
                    OnPropertyChanged("EditGameControlVisibility");
                }
            }
        }

        /// <summary>
        /// Gets or sets the visibility of the DeleteGame control
        /// </summary>
        private Visibility _deleteGameControlVisibility = Visibility.Hidden;
        public Visibility DeleteGameControlVisibility
        {
            get
            {
                return _deleteGameControlVisibility;
            }
            set
            {
                if (value != _deleteGameControlVisibility)
                {
                    _deleteGameControlVisibility = value;
                    OnPropertyChanged("DeleteGameControlVisibility");
                }
            }
        }

        #endregion

        #region Index Properties

        /// <summary>
        /// Gets or sets the Games collection for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private ReadOnlyCollection<Game> _games;
        public ReadOnlyCollection<Game> Games
        {
            get
            {
                return _games;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException($"{GetType()}.{nameof(Games)}");
                }
                else if (value != _games)
                {
                    _games = value;
                    OnPropertyChanged("Games");
                    RequestUpdate = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the SelectedGame for this <see cref="GamesWindowViewModel"/> object.
        /// </summary>
        private Game _selectedGame;
        public Game SelectedGame
        {
            get
            {
                return _selectedGame;
            }
            set
            {
                if (value != _selectedGame || _selectedGame is null)
                {
                    _selectedGame = value;
                    OnPropertyChanged("SelectedGame");

                    if (_selectedGame is null)
                    {
                        ClearDataEntryControls();
                    }
                    else
                    {
                        PopulateDataEntryControls();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value that indicates whether the Games collection is read-only.
        /// </summary>
        private bool _isGamesReadOnly;
        public bool IsGamesReadOnly
        {
            get
            {
                return _isGamesReadOnly;
            }
            set
            {
                if (value != _isGamesReadOnly)
                {
                    _isGamesReadOnly = value;
                    OnPropertyChanged("IsGamesReadOnly");
                }
            }
        }

        /// <summary>
        /// Gets or sets the value that indicates whether the ShowAllGames function is enabled.
        /// </summary>
        private bool _isShowAllGamesEnabled;
        public bool ShowAllGamesEnabled
        {
            get
            {
                return _isShowAllGamesEnabled;
            }
            set
            {
                if (value != _isShowAllGamesEnabled)
                {
                    _isShowAllGamesEnabled = value;
                    OnPropertyChanged("ShowAllGamesEnabled");
                }
            }
        }

        #endregion

        public bool FindGameFilterApplied { get; set; }

        /// <summary>
        /// Adds a new game to the data store.
        /// </summary>
        private DelegateCommand _addGameCommand;
        public DelegateCommand AddGameCommand
        {
            get
            {
                if (_addGameCommand is null)
                {
                    _addGameCommand = new DelegateCommand(param => AddGame());
                }
                return _addGameCommand;
            }
        }
        private void AddGame()
        {
            var (valid, message) = ValidateDataEntry();
            if (!valid)
            {
                MessageBox.Show(message, "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newGame = MapNewGameValues();
            _gameService.AddGame(newGame);

            LoadGamesForSelectedSeason();
            SelectedGame = null;
        }

        /// <summary>
        /// Deletes an existing game from the data store.
        /// </summary>
        private DelegateCommand _deleteGameCommand;
        public DelegateCommand DeleteGameCommand
        {
            get
            {
                if (_deleteGameCommand is null)
                {
                    _deleteGameCommand = new DelegateCommand(param => DeleteGame());
                }
                return _deleteGameCommand;
            }
        }
        private void DeleteGame()
        {
            var oldGame = MapOldGameValues();
            _gameService.DeleteGame(oldGame.ID);

            LoadGamesForSelectedSeason();
            SelectedGame = null;
        }

        /// <summary>
        /// Edits an existing game in the data store.
        /// </summary>
        private DelegateCommand _editGameCommand;
        public DelegateCommand EditGameCommand
        {
            get
            {
                if (_editGameCommand is null)
                {
                    _editGameCommand = new DelegateCommand(param => EditGame());
                }
                return _editGameCommand;
            }
        }
        private void EditGame()
        {
            var (valid, message) = ValidateDataEntry();
            if (!valid)
            {
                MessageBox.Show(message, "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var oldGame = MapOldGameValues();
            var newGame = MapNewGameValues();
            _gameService.EditGame(newGame, oldGame);

            if (FindGameFilterApplied)
            {
                ApplyFindGameFilter();
            }
            else
            {
                LoadGamesForSelectedSeason();
            }
        }

        /// <summary>
        /// Searches for a specified game in the data store.
        /// </summary>
        private DelegateCommand _findGameCommand;
        public DelegateCommand FindGameCommand
        {
            get
            {
                if (_findGameCommand is null)
                {
                    _findGameCommand = new DelegateCommand(param => FindGame());
                }
                return _findGameCommand;
            }
        }
        private void FindGame()
        {
            var gameFinderWindow = _gameFinderWindowFactory.Create();
            if (gameFinderWindow.ShowDialog() == false)
            {
                return;
            }

            var vm = gameFinderWindow.DataContext as IGameFinderWindowViewModel;
            _filterGuestName = vm.GuestName;
            _filterHostName = vm.HostName;

            ApplyFindGameFilter();
            IsGamesReadOnly = true;
            ShowAllGamesEnabled = true;

            if (Games.Count == 0)
            {
                SelectedGame = null;
            }

            AddGameControlVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// Shows all the games currently in the data store.
        /// </summary>
        private DelegateCommand _showAllGamesCommand;
        public DelegateCommand ShowAllGamesCommand
        {
            get
            {
                if (_showAllGamesCommand is null)
                {
                    _showAllGamesCommand = new DelegateCommand(param => ShowAllGames());
                }
                return _showAllGamesCommand;
            }
        }
        private void ShowAllGames()
        {
            ViewGames();
            FindGameFilterApplied = false;
            IsGamesReadOnly = false;
            ShowAllGamesEnabled = false;
            SelectedGame = null;
        }

        /// <summary>
        /// Loads all the Games for the selected season.
        /// </summary>
        private DelegateCommand _viewGamesCommand;
        public DelegateCommand ViewGamesCommand
        {
            get
            {
                if (_viewGamesCommand is null)
                {
                    _viewGamesCommand = new DelegateCommand(param => ViewGames());
                }
                return _viewGamesCommand;
            }
        }
        private void ViewGames()
        {
            LoadGamesForSelectedSeason();
            SelectedGame = null;

            MoveFocusTo("GuestName");

            var season = _seasonRepository.GetSeasonByYear(WpfGlobals.SelectedSeason);
            if (season is null)
            {
                return;
            }

            Week = season.NumOfWeeksCompleted;
        }

        private void ApplyFindGameFilter()
        {
            var games = _gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason)
                .Where(g => g.GuestName == _filterGuestName)
                .Where(g => g.HostName == _filterHostName);
            Games = new ReadOnlyCollection<Game>(games.ToList());

            FindGameFilterApplied = true;
        }

        private void ClearDataEntryControls()
        {
            GuestName = string.Empty;
            GuestScore = 0;
            HostName = string.Empty;
            HostScore = 0;
            IsPlayoff = false;
            Notes = string.Empty;

            AddGameControlVisibility = Visibility.Visible;
            EditGameControlVisibility = Visibility.Hidden;
            DeleteGameControlVisibility = Visibility.Hidden;

            MoveFocusTo("GuestName");
        }

        private void LoadGamesForSelectedSeason()
        {
            var games = _gameRepository.GetGamesBySeason(WpfGlobals.SelectedSeason);
            Games = new ReadOnlyCollection<Game>(games.ToList());
        }

        private Game MapNewGameValues()
        {
            return new Game
            {
                SeasonYear = (int)WpfGlobals.SelectedSeason,
                Week = this.Week,
                GuestName = this.GuestName,
                GuestScore = this.GuestScore,
                HostName = this.HostName,
                HostScore = this.HostScore,
                IsPlayoff = this.IsPlayoff,
                Notes = this.Notes
            };
        }

        private Game MapOldGameValues()
        {
            return new Game
            {
                ID = SelectedGame.ID,
                SeasonYear = WpfGlobals.SelectedSeason,
                Week = SelectedGame.Week,
                GuestName = SelectedGame.GuestName,
                GuestScore = SelectedGame.GuestScore,
                HostName = SelectedGame.HostName,
                HostScore = SelectedGame.HostScore,
                WinnerName = SelectedGame.WinnerName,
                WinnerScore = SelectedGame.WinnerScore,
                LoserName = SelectedGame.LoserName,
                LoserScore = SelectedGame.LoserScore,
                IsPlayoff = SelectedGame.IsPlayoff,
                Notes = SelectedGame.Notes
            };
        }

        private void MoveFocusTo(string focusedProperty)
        {
            OnMoveFocus(focusedProperty);
        }

        private void PopulateDataEntryControls()
        {
            var selectedGame = SelectedGame;
            Week = selectedGame.Week;
            GuestName = selectedGame.GuestName;
            GuestScore = selectedGame.GuestScore;
            HostName = selectedGame.HostName;
            HostScore = selectedGame.HostScore;
            IsPlayoff = selectedGame.IsPlayoff;
            Notes = selectedGame.Notes;

            AddGameControlVisibility = Visibility.Hidden;
            EditGameControlVisibility = Visibility.Visible;
            DeleteGameControlVisibility = Visibility.Visible;
        }

        private (bool, string) ValidateDataEntry()
        {
            if (string.IsNullOrWhiteSpace(GuestName))
            {
                MoveFocusTo("GuestName");
                return (false, Settings.Default.BothTeamsNeededErrorMessage);
            }
            else if (string.IsNullOrWhiteSpace(HostName))
            {
                MoveFocusTo("HostName");
                return (false, Settings.Default.BothTeamsNeededErrorMessage);
            }
            else if (GuestName == HostName)
            {
                MoveFocusTo("GuestName");
                return (false, Settings.Default.DifferentTeamsNeededErrorMessage);
            }

            return (true, null);
        }
    }
}
