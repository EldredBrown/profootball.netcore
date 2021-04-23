namespace EldredBrown.ProFootball.NETCore.WpfApp.ViewModels
{
    public interface IGameFinderWindowViewModel
    {
        string GuestName { get; set; }
        string HostName { get; set; }
        DelegateCommand WindowLoadedCommand { get; }

        (bool, string) ValidateDataEntry();
    }
}