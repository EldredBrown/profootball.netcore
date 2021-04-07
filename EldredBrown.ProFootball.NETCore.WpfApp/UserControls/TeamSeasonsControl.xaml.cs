using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EldredBrown.ProFootball.NETCore.WpfApp.ViewModels;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls
{
    /// <summary>
    /// Interaction logic for TeamSeasonsControl.xaml
    /// </summary>
    public partial class TeamSeasonsControl : UserControl
    {
        public TeamSeasonsControl()
        {
            InitializeComponent();

            DataContext = new TeamSeasonsControlViewModel();
        }
    }
}
