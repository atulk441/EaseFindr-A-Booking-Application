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
    public partial class HotelBooking : ContentPage
    {
        public class Hotel
        {
            public string hotelName { get; set; }
            public string image { get; set; }
        }

        SqlConnection sqlConnection;
        public HotelBooking()
        {
            InitializeComponent();
            string srvrdbname = "BookingApp";
            string srvrname = "192.168.2.50";
            string srvruser = "AtulKumar01";
            string srvrpassword = "Gupta@3939";
            string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);

            ObservableCollection<Hotel> taxis = new ObservableCollection<Hotel>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.HotelDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                taxis.Add(new Hotel
                {
                    hotelName = reader["HotelName"].ToString(),
                    image = reader["Photo"].ToString(),

                });

            }
            reader.Close();
            sqlConnection.Close();

            listView.ItemsSource = taxis;

        }
        private void bt_continue(object sender, EventArgs e)
        {
            //sqlConnection.Open();
            Button button = (Button)sender;
            Hotel selected = (Hotel)button.BindingContext;
            //string selectedCompanyName = selectedRide.companyName;

            Navigation.PushAsync(new SelectedHotel(selected.hotelName, selected.image));
            //sqlConnection.Close();

        }

        private void bt_search(object sender, EventArgs e)
        {
            sqlConnection.Open();
            List<Hotel> taxiServices = new List<Hotel>();
            string queryString = "Select * from BookingApp.dbo.HotelDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                taxiServices.Add(new Hotel
                {
                    hotelName = reader["HotelName"].ToString(),
                    image = reader["Photo"].ToString(),
                });

            }

            reader.Close();
            sqlConnection.Close();
            var counter = 0;
            foreach (var item in taxiServices)
            {
                if (searched_comp.Text == item.hotelName)
                {
                    Navigation.PushAsync(new SelectedHotel(item.hotelName, item.image));
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
