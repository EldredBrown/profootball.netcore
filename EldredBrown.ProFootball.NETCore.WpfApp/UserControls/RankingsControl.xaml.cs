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
    /// Interaction logic for RankingsControl.xaml
    /// </summary>
    public partial class RankingsControl : UserControl
    {
        public RankingsControl()
        {
            InitializeComponent();

            DataContext = new RankingsControlViewModel();
        }
    }
}
