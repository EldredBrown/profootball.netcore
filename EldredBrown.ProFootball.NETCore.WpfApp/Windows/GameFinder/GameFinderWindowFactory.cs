using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows
{
    public class GameFinderWindowFactory : IGameFinderWindowFactory
    {
        private readonly IGameFinderWindowViewModel _viewModel;

        public GameFinderWindowFactory(IGameFinderWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public IGameFinderWindow Create()
        {
            return new GameFinderWindow(_viewModel);
        }
    }
}
