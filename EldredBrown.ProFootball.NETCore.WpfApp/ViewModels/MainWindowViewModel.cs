using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets this window's seasons collection.
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
                    throw new ArgumentNullException("Seasons");
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
        /// Gets or sets this window's selected season.
        /// </summary>
        private int _selectedSeason;
        public int SelectedSeason
        {
            get
            {
                return _selectedSeason;
            }
            set
            {
                if (value != _selectedSeason)
                {
                    _selectedSeason = value;
                    OnPropertyChanged("SelectedSeason");
                }
            }
        }

        /// <summary>
        /// Opens the GamePredictorWindow.
        /// </summary>
        private DelegateCommand _predictGameScoreCommand;
        public DelegateCommand PredictGameScoreCommand
        {
            get
            {
                if (_predictGameScoreCommand == null)
                {
                    _predictGameScoreCommand = new DelegateCommand(param => PredictGameScore());
                }
                return _predictGameScoreCommand;
            }
        }
        private void PredictGameScore()
        {
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
        /// Loads data into the Seasons control.
        /// </summary>
        private DelegateCommand _viewSeasonsCommand;
        public DelegateCommand ViewSeasonsCommand
        {
            get
            {
                if (_viewSeasonsCommand == null)
                {
                    _viewSeasonsCommand = new DelegateCommand(param => ViewSeasons());
                }
                return _viewSeasonsCommand;
            }
        }
        private void ViewSeasons()
        {
            Seasons = new ReadOnlyCollection<int>(new List<int> { 1920, 1921, 1922 });
            SelectedSeason = 1920;
        }
    }
}
