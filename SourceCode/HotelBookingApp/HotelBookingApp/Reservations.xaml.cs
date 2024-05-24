using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelBookingApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Reservations : ContentPage
	{
        public class MyTableList
        {
            public string service { get; set; }

            public string name { get; set; }
            public string category { get; set; }
        }
        SqlConnection sqlConnection;

        public Reservations ()
		{
			InitializeComponent ();

            string srvrdbname = "StudentRegistration";
            string srvrname = "192.168.2.50";
            string srvruser = "AtulKumar01";
            string srvrpassword = "Gupta@3939";

            string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);

            List<MyTableList> myTableList = new List<MyTableList>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.Reservations";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                myTableList.Add(new MyTableList
                {
                    service = reader["Service"].ToString(),
                    name = reader["Name"].ToString(),
                    category = reader["Category"].ToString(),
                });


            }
            reader.Close();
            sqlConnection.Close();
            listView.ItemsSource = myTableList;


        }


        private void OnButtonClickedX(object sender, EventArgs e)
        {
            sqlConnection.Open();
            Button button = (Button)sender;
            MyTableList selectedCourse = (MyTableList)button.BindingContext;

            string deleting = selectedCourse.name;
            using (SqlCommand command = new SqlCommand($"DELETE FROM BookingApp.dbo.Reservations WHERE Name='{deleting}'", sqlConnection))
            {
                command.ExecuteNonQuery();

            }

            sqlConnection.Close();

            List<MyTableList> myTableList = new List<MyTableList>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.Reservations";
            SqlCommand command1 = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command1.ExecuteReader();
            while (reader.Read())
            {
                myTableList.Add(new MyTableList
                {
                    service = reader["Service"].ToString(),
                    name = reader["Name"].ToString(),
                    category = reader["Category"].ToString(),
                });


            }
            reader.Close();
            sqlConnection.Close();
            listView.ItemsSource = myTableList;

        }
        private void bt_back(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


    }
}