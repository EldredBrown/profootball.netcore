﻿using System.Windows.Controls;

namespace EldredBrown.ProFootball.NETCore.WpfApp.UserControls.Rankings
{
    /// <summary>
    /// Interaction logic for RankingsControl.xaml
    /// </summary>
    public partial class RankingsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RankingsControl"/> class.
        /// </summary>
        public RankingsControl()
        {
            InitializeComponent();

            DataContext =
                App.ServiceProvider.GetService(typeof(IRankingsControlViewModel)) as IRankingsControlViewModel;
        }
    }
}
