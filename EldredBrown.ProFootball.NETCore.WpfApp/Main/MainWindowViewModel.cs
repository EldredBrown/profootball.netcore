using System;
using System.Collections.ObjectModel;
using System.Linq;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.NETCore.Services;
using EldredBrown.ProFootball.NETCore.Services.GamePredictorService;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;
using EldredBrown.ProFootball.WpfApp;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Main
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IWeeklyUpdateService _weeklyUpdateService;
        private readonly IGamePredictorService _gamePredictorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="seasonRepository">
        /// The <see cref="ISeasonRepository"/> object by which season data will be accessed.
        /// </param>
        /// <param name="weeklyUpdateService">
        /// The <see cref="IWeeklyUpdateService"/> object that will run the weekly update.
        /// </param>
        /// <param name="gamePredictorService">
        /// The <see cref="IGamePredictorService"/> object that will predict a game score.
        /// </param>
        public MainWindowViewModel(
            ISeasonRepository seasonRepository = null,
            IWeeklyUpdateService weeklyUpdateService = null,
            IGamePredictorService gamePredictorService = null)
        {
            _seasonRepository = seasonRepository ??
                App.ServiceProvider.GetService(typeof(ISeasonRepository)) as ISeasonRepository;
            _weeklyUpdateService = weeklyUpdateService ??
                App.ServiceProvider.GetService(typeof(IWeeklyUpdateService)) as IWeeklyUpdateService;
            _gamePredictorService = gamePredictorService ??
                App.ServiceProvider.GetService(typeof(IGamePredictorService)) as IGamePredictorService;
        }

        /// <summary>
        /// Gets or sets the seasons collection for this <see cref="MainWindowViewModel"/> object.
        /// </summary>
        private ReadOnlyCollection<int> _seasons;
        public ReadOnlyCollection<int> Seasons
        {
            get
            {
                return _seasons;
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException($"{GetType()}.{nameof(Seasons)}");
                }
                else if (value != _seasons)
                {
                    _seasons = value;
                    OnPropertyChanged("Seasons");
                    RequestUpdate = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected season for this <see cref="MainWindowViewModel"/> object.
        /// </summary>
        public int SelectedSeason
        {
            get
            {
                return WpfGlobals.SelectedSeason;
            }
            set
            {
                if (value != WpfGlobals.SelectedSeason)
                {
                    WpfGlobals.SelectedSeason = value;
                    OnPropertyChanged("SelectedSeason");
                }
            }
        }

        /// <summary>
        /// Opens the game predictor window.
        /// </summary>
        private DelegateCommand _predictGameScoreCommand;
        public DelegateCommand PredictGameScoreCommand
        {
            get
            {
                if (_predictGameScoreCommand is null)
                {
                    _predictGameScoreCommand = new DelegateCommand(param => PredictGameScore());
                }
                return _predictGameScoreCommand;
            }
        }
        private void PredictGameScore()
        {
            _gamePredictorService.PredictGameScore();
        }

        /// <summary>
        /// Runs a weekly update.
        /// </summary>
        private DelegateCommand _weeklyUpdateCommand;
        public DelegateCommand WeeklyUpdateCommand
        {
            get
            {
                if (_weeklyUpdateCommand is null)
                {
                    _weeklyUpdateCommand = new DelegateCommand(param => RunWeeklyUpdate());
                }
                return _weeklyUpdateCommand;
            }
        }
        private void RunWeeklyUpdate()
        {
            _weeklyUpdateService.RunWeeklyUpdate(WpfGlobals.SelectedSeason);
        }

        /// <summary>
        /// Loads data into the Seasons control.
        /// </summary>
        private DelegateCommand _viewSeasonsCommand;
        public DelegateCommand ViewSeasonsCommand
        {
            get
            {
                if (_viewSeasonsCommand is null)
                {
                    _viewSeasonsCommand = new DelegateCommand(param => ViewSeasons());
                }
                return _viewSeasonsCommand;
            }
        }
        private void ViewSeasons()
        {
            var seasons = _seasonRepository.GetSeasons().Select(s => s.Year);
            Seasons = new ReadOnlyCollection<int>(seasons.ToList());
            SelectedSeason = Seasons.First();
        }
    }
}
