using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;
using EldredBrown.ProFootball.NETCore.WpfApp.Windows.GameFinder;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IGameFinderWindowViewModel _gameFinderWindowViewModel;

        public WindowFactory(IGameFinderWindowViewModel gameFinderWindowViewModel)
        {
            _gameFinderWindowViewModel = gameFinderWindowViewModel;
        }

        public IGameFinderWindow CreateGameFinderWindow()
        {
            return new GameFinderWindow(_gameFinderWindowViewModel);
        }
    }
}
