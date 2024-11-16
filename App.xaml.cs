using TaskApp.Helpers;
using TaskApp.Pages;

namespace TaskApp
{
    public partial class App : Application
    {
        Page tempPage = null;
        Page offlinePage = null;
        public App(AuthHelper helper)
        {
            InitializeComponent();

            Connectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
         
            tempPage = new LoginPage(helper);
            offlinePage = new OfflinePage();
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet) {
                MainPage = tempPage;
            }
            else
            {
                MainPage = offlinePage;
            }
           
        }

        private void Current_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
               Application.Current.MainPage = tempPage;
            }
            else
            {
                if (Application.Current.MainPage != offlinePage)
                {
                    tempPage = Application.Current.MainPage;
                }
                Application.Current.MainPage = offlinePage;
            }
        }
    }
}
