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
    public partial class AboutInfo : ContentPage
    {
        public AboutInfo()
        {
            InitializeComponent();
        }

        private void bt_back(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}