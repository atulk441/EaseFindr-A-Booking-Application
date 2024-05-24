using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelBookingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FlightBooking : ContentPage
	{
        public class Flight
        {
            public string companyName { get; set; }
            public string image { get; set; }
        }

        SqlConnection sqlConnection;


        public FlightBooking ()
		{
			InitializeComponent ();
            string srvrdbname = "BookingApp";
            string srvrname = "192.168.2.50";
            string srvruser = "AtulKumar01";
            string srvrpassword = "Gupta@3939";

            string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);

            ObservableCollection<Flight> flights = new ObservableCollection<Flight>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.FlightCompanyDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                flights.Add(new Flight
                {
                    companyName = reader["CompanyName"].ToString(),
                    image = reader["Photo"].ToString(),

                });

            }

            reader.Close();
            sqlConnection.Close();

            listView.ItemsSource = flights;


        }
        private void bt_continue(object sender, EventArgs e)
        {
            //sqlConnection.Open();
            Button button = (Button)sender;
            Flight selectedFlight = (Flight)button.BindingContext;
            //string selectedCompanyName = selectedRide.companyName;

            Navigation.PushAsync(new SelectedFlight(selectedFlight.companyName, selectedFlight.image));
            //sqlConnection.Close();

        }

        private void bt_search(object sender, EventArgs e)
        {
            sqlConnection.Open();
            List<Flight> flightServices = new List<Flight>();
            string queryString = "Select * from BookingApp.dbo.FlightCompanyDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                flightServices.Add(new Flight
                {
                    companyName = reader["CompanyName"].ToString(),
                    image = reader["Photo"].ToString(),
                });

            }

            reader.Close();
            sqlConnection.Close();
            var counter = 0;
            foreach (var item in flightServices)
            {
                if (searched_comp.Text == item.companyName)
                {
                    Navigation.PushAsync(new SelectedFlight(item.companyName, item.image));
                    searched_comp.Text = "";
                    counter = 1; break;
                }
            }
            if (counter == 0)
            {
                DisplayAlert("Alert", $"Didn't find anything having name \"{searched_comp.Text}\"", "OK")
;
            }
        }
    }
}
