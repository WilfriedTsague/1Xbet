using _1Xbet.Services;

namespace _1Xbet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Dbcontext.ConfigDB();
            MainPage = new AppShell();
        }
    }
}
