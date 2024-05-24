using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelBookingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeScreen : ContentPage
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        private void bt_next(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void bt_about(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutInfo());
        }
    }
}