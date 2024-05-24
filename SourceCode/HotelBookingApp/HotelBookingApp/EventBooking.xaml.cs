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
    public partial class EventBooking : ContentPage
    {
        public class Event
        {
            public string eventName { get; set; }
            public string image { get; set; }
        }

        SqlConnection sqlConnection;

        public EventBooking()
        {
            InitializeComponent();

            string srvrdbname = "BookingApp";
            string srvrname = "192.168.2.50";
            string srvruser = "AtulKumar01";
            string srvrpassword = "Gupta@3939";

            string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);

            ObservableCollection<Event> events = new ObservableCollection<Event>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.EventDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                events.Add(new Event
                {
                    eventName = reader["EventName"].ToString(),
                    image = reader["Photo"].ToString(),

                });

            }
            reader.Close();
            sqlConnection.Close();

            listView.ItemsSource = events;
        }

        private void bt_continue(object sender, EventArgs e)
        {
            //sqlConnection.Open();
            Button button = (Button)sender;
            Event selectedEvent = (Event)button.BindingContext;
            //string selectedCompanyName = selectedRide.companyName;

            Navigation.PushAsync(new SelectedEvent(selectedEvent.eventName, selectedEvent.image));
            //sqlConnection.Close();

        }

        private void bt_search(object sender, EventArgs e)
        {
            sqlConnection.Open();
            List<Event> eventServices = new List<Event>();
            string queryString = "Select * from BookingApp.dbo.EventDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                eventServices.Add(new Event
                {
                    eventName = reader["EventName"].ToString(),
                    image = reader["Photo"].ToString(),
                });

            }

            reader.Close();
            sqlConnection.Close();
            var counter = 0;
            foreach (var item in eventServices)
            {
                if (searched_comp.Text == item.eventName)
                {
                    Navigation.PushAsync(new SelectedEvent(item.eventName, item.image));
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
