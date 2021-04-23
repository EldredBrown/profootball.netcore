namespace EldredBrown.ProFootball.NETCore.WpfApp.Windows
{
    public interface IGameFinderWindow
    {
        object DataContext { get; set; }

        bool? ShowDialog();
    }
}