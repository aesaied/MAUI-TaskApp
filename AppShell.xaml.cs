using TaskApp.Pages;

namespace TaskApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

           
           Routing.RegisterRoute(nameof(ManageTask),typeof(ManageTask));
       

        }
    }
}
