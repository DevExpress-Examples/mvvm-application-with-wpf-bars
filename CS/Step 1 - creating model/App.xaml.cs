using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Core;

namespace DX_MVVM {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            MainWindow = new DXWindow() { Width = 525, Height = 350 };
            MainWindow.Content = new MainPage();
            MainWindow.Show();
        }
    }
}
