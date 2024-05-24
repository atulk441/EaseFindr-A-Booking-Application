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
	public partial class ReservationServices : ContentPage
	{
		public ReservationServices()
		{
			InitializeComponent ();
		}

		private void bt_reserve_taxi(object sender, EventArgs e)
		{
			Navigation.PushAsync(new TaxiBooking());
        }

        private void bt_reserve_hotel(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HotelBooking());
        }

        private void bt_reserve_flight(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FlightBooking());
        }

        private void bt_reserve_event(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EventBooking());
        }

        private void bt_my_orders(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Reservations());
        }

    }
}