using EldredBrown.ProFootball.NETCore.WpfApp.Windows.GameFinder;

namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows
{
    public interface IWindowFactory
    {
        IGameFinderWindow CreateGameFinderWindow();
    }
}
