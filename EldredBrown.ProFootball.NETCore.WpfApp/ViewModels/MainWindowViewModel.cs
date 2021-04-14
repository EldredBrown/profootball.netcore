using System;
using System.Collections.ObjectModel;
using System.Linq;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using EldredBrown.ProFootball.WpfApp;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISeasonRepository _seasonRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="seasonRepository">
        /// The <see cref="ISeasonRepository"/> object by which season data will be accessed.
        /// </param>
        public MainWindowViewModel(ISeasonRepository seasonRepository = null)
        {
            _seasonRepository = seasonRepository ??
                App.ServiceProvider.GetService(typeof(ISeasonRepository)) as ISeasonRepository;
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
        }
    }
}
