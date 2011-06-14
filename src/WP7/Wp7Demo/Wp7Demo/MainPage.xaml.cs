using System;
using System.Windows;
using DemoLib;
using Microsoft.Phone.Controls;

namespace Wp7Demo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = TinyIoC.TinyIoCContainer.Current.Resolve<MainViewModel>();
        }

        public MainViewModel ViewModel
        {
            get { return ((MainViewModel)DataContext); }
        }

        private void UpdateLocation(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateLocationCommand.Execute(null);
        }
    }
}