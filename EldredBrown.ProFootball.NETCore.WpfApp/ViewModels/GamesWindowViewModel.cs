using System;
using System.Collections.ObjectModel;
using System.Windows;
using EldredBrown.ProFootball.NETCore.Data.Entities;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class GamesWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets/sets SelectedGame window's week value.
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
        /// Gets or sets SelectedGame window's guest value.
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
        /// Gets or sets SelectedGame window's guest score value.
        /// </summary>
        private double _guestScore;
        public double GuestScore
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
        /// Gets or sets SelectedGame window's host value.
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
        /// Gets or sets SelectedGame window's host score value.
        /// </summary>
        private double _hostScore;
        public double HostScore
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
        /// Gets or sets the value that indicates whether SelectedGame game is a playoff game.
        /// </summary>
        private bool _isPlayoffGame;
        public bool IsPlayoffGame
        {
            get
            {
                return _isPlayoffGame;
            }
            set
            {
                if (value != _isPlayoffGame)
                {
                    _isPlayoffGame = value;
                    OnPropertyChanged("IsPlayoffGame");
                }
            }
        }

        /// <summary>
        /// Gets or sets the value that indicates whether SelectedGame game can be a playoff game.
        /// </summary>
        private bool _isPlayoffGameEnabled;
        public bool IsPlayoffGameEnabled
        {
            get
            {
                return _isPlayoffGameEnabled;
            }
            set
            {
                if (value != _isPlayoffGameEnabled)
                {
                    _isPlayoffGameEnabled = value;
                    OnPropertyChanged("IsPlayoffGameEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the notes entered for SelectedGame game, if any.
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
        /// Gets the visibility of the AddEntity control
        /// </summary>
        private Visibility _addGameControlVisibility;
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
        /// Gets the visibility of the EditEntity control.
        /// </summary>
        private Visibility _editGameControlVisibility;
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
        /// Gets the visibility of the RemoveEntity control.
        /// </summary>
        private Visibility _deleteGameControlVisibility;
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

        /// <summary>
        /// Gets SelectedGame window's games collection. 
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
                    throw new ArgumentNullException("Games");
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
        /// Gets or sets SelectedGame window's selected game
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
                if (value != _selectedGame)
                {
                    _selectedGame = value;
                    OnPropertyChanged("SelectedGame");
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
        public bool IsShowAllGamesEnabled
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
                    OnPropertyChanged("IsShowAllGamesEnabled");
                }
            }
        }

        /// <summary>
        /// Views the Games database table.
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
        }

        /// <summary>
        /// Adds a new game to the database.
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
        }

        /// <summary>
        /// Edits an existing game in the database.
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
        }

        /// <summary>
        /// Removes an existing game from the database.
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
        }

        /// <summary>
        /// Searches for a specified game in the database.
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
        }

        /// <summary>
        /// Shows all the games currently in the database.
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
        }
    }
}
