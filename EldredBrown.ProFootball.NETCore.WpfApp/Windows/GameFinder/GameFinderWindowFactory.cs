namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows.GameFinder
{
    public class GameFinderWindowFactory : IGameFinderWindowFactory
    {
        private readonly IGameFinderWindowViewModel _gameFinderWindowViewModel;

        public GameFinderWindowFactory(IGameFinderWindowViewModel gameFinderWindowViewModel)
        {
            _gameFinderWindowViewModel = gameFinderWindowViewModel;
        }

        /// <summary>
        /// Creates instances of the <see cref="GameFinderWindow"/> class.
        /// </summary>
        /// <returns>An instance of the <see cref="GameFinderWindow"/> class.</returns>
        public IGameFinderWindow CreateGameFinderWindow()
        {
            return new GameFinderWindow(_gameFinderWindowViewModel);
        }
    }
}
