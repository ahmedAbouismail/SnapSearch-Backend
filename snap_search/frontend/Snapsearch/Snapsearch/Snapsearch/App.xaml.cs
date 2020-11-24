using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snapsearch
{
    public partial class App : Application
    {
        public App()
        {
            //setting for applying xamarin.shapes => it's not fully implemented yet.
            Device.SetFlags(new[] { "Shapes_Experimental", "Brush_Experimental" });
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
