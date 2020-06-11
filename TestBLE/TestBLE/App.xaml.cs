using System;
using TestBLE.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestBLE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            TabbedPage tabbedPage = new TabbedPage();
            NavigationPage navigationPage = new NavigationPage(new MainPage());
            tabbedPage.Children.Add(navigationPage);
            tabbedPage.Children.Add(new PowerView());
            tabbedPage.Children.Add(new DeviceView());

            MainPage = tabbedPage;
           // MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
