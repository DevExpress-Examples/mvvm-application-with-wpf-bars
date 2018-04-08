using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DX_MVVM.Helpers;
using DX_MVVM.Views;

namespace DX_MVVM {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
        }

        void PersonsView_Loaded(object sender, RoutedEventArgs e) {
            PersonsView personsView = (PersonsView)sender;
            manager.Bars["mainBar"].UnMerge();
            manager.Bars["mainBar"].Merge(personsView.ChildBar);
        }
        void PersonView_Loaded(object sender, RoutedEventArgs e) {
            PersonView personView = (PersonView)sender;
            manager.Bars["mainBar"].UnMerge();
            manager.Bars["mainBar"].Merge(personView.ChildBar);
        }
    }
}
