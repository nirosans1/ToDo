using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo.Services;
using ToDo.Utility;
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace ToDo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MobileCenter.Start(typeof(Analytics), typeof(Crashes));

            bool useMock = false;

            if (useMock)
                ServiceLocator.Instance.Add<IAzureService, MockService>();
            else
                ServiceLocator.Instance.Add<IAzureService, AzureService>();

            MainPage = new NavigationPage(new Pages.ToDoPage())
            {
                BarBackgroundColor = (Color)Current.Resources["primaryBlue"],
                BarTextColor = Color.White
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
