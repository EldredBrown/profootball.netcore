using EldredBrown.ProFootball.NETCore.WpfApp.Properties;

namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public class GameFinderWindowViewModel : ViewModelBase, IGameFinderWindowViewModel
    {
        /// <summary>
        /// Gets or sets this <see cref="GameFinderWindowViewModel"/> object's GuestName value.
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
        /// Gets or sets this <see cref="GameFinderWindowViewModel"/> object's HostName value.
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
        /// Handles the WindowLoaded event.
        /// </summary>
        private DelegateCommand _windowLoadedCommand;
        public DelegateCommand WindowLoadedCommand
        {
            get
            {
                if (_windowLoadedCommand is null)
                {
                    _windowLoadedCommand = new DelegateCommand(param => WindowLoaded());
                }
                return _windowLoadedCommand;
            }
        }
        private void WindowLoaded()
        {
            MoveFocusTo("GuestName");
        }

        public (bool, string) ValidateDataEntry()
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

        private void MoveFocusTo(string focusedProperty)
        {
            OnMoveFocus(focusedProperty);
        }
    }
}
