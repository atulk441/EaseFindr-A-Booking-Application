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
    public partial class TaxiBooking : ContentPage
    {

        public class Taxi
        {
            public string companyName { get; set; }
            public string image { get; set; }
        }

        SqlConnection sqlConnection;


        public TaxiBooking()
        {
            InitializeComponent();

            string srvrdbname = "BookingApp";
            string srvrname = "192.168.2.50";
            string srvruser = "AtulKumar01";
            string srvrpassword = "Gupta@3939";

            string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);

            ObservableCollection<Taxi> taxis = new ObservableCollection<Taxi>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.TaxiCompanyDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                taxis.Add(new Taxi
                {
                    companyName = reader["CompanyName"].ToString(),
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
            Taxi selectedRide = (Taxi)button.BindingContext;
            //string selectedCompanyName = selectedRide.companyName;

            Navigation.PushAsync(new SelectedTaxi(selectedRide.companyName, selectedRide.image));
            //sqlConnection.Close();

        }

        private void bt_search(object sender, EventArgs e)
        {
            sqlConnection.Open();
            List<Taxi> taxiServices = new List<Taxi>();
            string queryString = "Select * from BookingApp.dbo.TaxiCompanyDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                taxiServices.Add(new Taxi
                {
                    companyName = reader["CompanyName"].ToString(),
                    image = reader["Photo"].ToString(),
                });

            }

            reader.Close();
            sqlConnection.Close();
            var counter = 0;
            foreach (var item in taxiServices)
            {
                if (searched_comp.Text == item.companyName)
                {
                    Navigation.PushAsync(new SelectedTaxi(item.companyName, item.image));
                    searched_comp.Text = "";
                    counter = 1; break;
                }
            }
            if (counter == 0)
            {
                DisplayAlert("Alert", $"Didn't find anything having name \"{searched_comp.Text}\"", "OK")
;            }
        }
    }
}




//ObservableCollection<Taxi> taxis = new ObservableCollection<Taxi>();
//taxis.Add(new Taxi { companyName = "Yellow Cab", image = "https://dailymedia.case.edu/wp-content/uploads/2018/12/03093619/safe-ride-feat-1024x439.jpg" });
//taxis.Add(new Taxi { companyName = "Red Line Taxi", image = "https://redline-cabs.co.uk/newsite/wp-content/uploads/2016/05/slider-new1.jpg" });
//taxis.Add(new Taxi { companyName = "Red Line Taxi", image = "https://redline-cabs.co.uk/newsite/wp-content/uploads/2016/05/slider-new1.jpg" });
//listView.ItemsSource = taxis;