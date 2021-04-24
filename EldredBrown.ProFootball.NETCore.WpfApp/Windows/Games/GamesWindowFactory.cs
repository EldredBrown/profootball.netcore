namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows.Games
{
    public class GamesWindowFactory : IGamesWindowFactory
    {
        private readonly IGamesWindowViewModel _gamesWindowViewModel;

        public GamesWindowFactory(IGamesWindowViewModel gamesWindowViewModel)
        {
            _gamesWindowViewModel = gamesWindowViewModel;
        }

        /// <summary>
        /// Creates instances of the <see cref="GamesWindow"/> class.
        /// </summary>
        /// <returns>An instance of the <see cref="GamesWindow"/> class.</returns>
        public IGamesWindow CreateGamesWindow()
        {
            return new GamesWindow(_gamesWindowViewModel);
        }
    }
}
