using Xamarin.Forms;

namespace DependencyServiceDemos
{
    public partial class App : Application
    {
        public static int screenHeight, screenWidth;
        public App()
        {
            InitializeComponent();

            MainPage =  new NavigationPage(new MainPage());
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
