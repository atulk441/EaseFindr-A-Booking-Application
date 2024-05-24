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
    public partial class SelectedEvent : ContentPage
    {
        public class Event
        {
            public string eventName { get; set; }
            public string service1 { get; set; }
            public string service2 { get; set; }
            public string service3 { get; set; }
            public string service4 { get; set; }
            public string service5 { get; set; }
            public double kidsPrice { get; set; }
            public double adultsPrice { get; set; }
            public double seniorsPrice { get; set; }
            public int rating { get; set; }

        }
        SqlConnection sqlConnection;
        public SelectedEvent(string selectedEventName, string name)
        {
            InitializeComponent();
            comp_name.Text = selectedEventName;
            image_link.Source = name;
            string srvrdbname = "BookingApp";
            string srvrname = "192.168.2.50";
            string srvruser = "AtulKumar01";
            string srvrpassword = "Gupta@3939";

            string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
            List<Event> eventServices = new List<Event>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.EventDetails";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                eventServices.Add(new Event
                {
                    //id = Convert.ToInt32(reader["PK"]),
                    eventName = reader["EventName"].ToString(),
                    service1 = reader["Good_feature_1"].ToString(),
                    service2 = reader["Good_feature_2"].ToString(),
                    service3 = reader["Good_feature_3"].ToString(),
                    service4 = reader["Good_feature_4"].ToString(),
                    service5 = reader["Good_feature_5"].ToString(),
                    kidsPrice = Convert.ToDouble(reader["Price_Kids"].ToString()),
                    adultsPrice = Convert.ToDouble(reader["Price_Adults"].ToString()),
                    seniorsPrice = Convert.ToDouble(reader["Price_Seniors"].ToString()),
                    rating = Convert.ToInt32(reader["Rating"]),


                    //credit = Convert.ToInt32(reader["Credits"]),
                    //labs = reader["Labs"].ToString(),
                }); ;
            }
            reader.Close();
            sqlConnection.Close();
            foreach (var item in eventServices)
            {
                if (selectedEventName == item.eventName)
                {
                    service_1.Text = "1.) " + item.service1;
                    service_2.Text = "2.) " + item.service2;
                    service_3.Text = "3.) " + item.service3;
                    service_4.Text = "4.) " + item.service4;
                    service_5.Text = "5.) " + item.service5;
                    kids_price.Text = "$ " + item.kidsPrice;
                    adlt_price.Text = "$ " + item.adultsPrice;
                    senr_price.Text = "$ " + item.seniorsPrice;

                    if (item.rating == 1)
                    {
                        bt1.BackgroundColor = Color.Gold;
                        bt2.BackgroundColor = Color.LightGray;
                        bt3.BackgroundColor = Color.LightGray;
                        bt4.BackgroundColor = Color.LightGray;
                        bt5.BackgroundColor = Color.LightGray;
                        bt1.FontSize = 30;
                        bt2.FontSize = 20;
                        bt3.FontSize = 20;
                        bt4.FontSize = 20;
                        bt5.FontSize = 20;
                        bt1.Opacity = 1;
                        bt2.Opacity = .2;
                        bt3.Opacity = .2;
                        bt4.Opacity = .2;
                        bt5.Opacity = .2;
                    }
                    else if (item.rating == 2)
                    {
                        bt2.BackgroundColor = Color.Gold;
                        bt1.BackgroundColor = Color.LightGray;
                        bt3.BackgroundColor = Color.LightGray;
                        bt4.BackgroundColor = Color.LightGray;
                        bt5.BackgroundColor = Color.LightGray;
                        bt2.FontSize = 30;
                        bt1.FontSize = 20;
                        bt3.FontSize = 20;
                        bt4.FontSize = 20;
                        bt5.FontSize = 20;
                        bt2.Opacity = 1;
                        bt1.Opacity = .2;
                        bt3.Opacity = .2;
                        bt4.Opacity = .2;
                        bt5.Opacity = .2;
                    }
                    else if (item.rating == 3)
                    {
                        bt3.BackgroundColor = Color.Gold;
                        bt2.BackgroundColor = Color.LightGray;
                        bt1.BackgroundColor = Color.LightGray;
                        bt4.BackgroundColor = Color.LightGray;
                        bt5.BackgroundColor = Color.LightGray;
                        bt3.FontSize = 30;
                        bt2.FontSize = 20;
                        bt1.FontSize = 20;
                        bt4.FontSize = 20;
                        bt5.FontSize = 20;
                        bt3.Opacity = 1;
                        bt2.Opacity = .2;
                        bt1.Opacity = .2;
                        bt4.Opacity = .2;
                        bt5.Opacity = .2;
                    }
                    else if (item.rating == 4)
                    {
                        bt4.BackgroundColor = Color.Gold;
                        bt2.BackgroundColor = Color.LightGray;
                        bt3.BackgroundColor = Color.LightGray;
                        bt1.BackgroundColor = Color.LightGray;
                        bt5.BackgroundColor = Color.LightGray;
                        bt4.FontSize = 30;
                        bt2.FontSize = 20;
                        bt3.FontSize = 20;
                        bt1.FontSize = 20;
                        bt5.FontSize = 20;
                        bt4.Opacity = 1;
                        bt2.Opacity = .2;
                        bt3.Opacity = .2;
                        bt1.Opacity = .2;
                        bt5.Opacity = .2;

                    }
                    else if (item.rating == 5)
                    {
                        bt5.BackgroundColor = Color.Gold;
                        bt2.BackgroundColor = Color.LightGray;
                        bt3.BackgroundColor = Color.LightGray;
                        bt4.BackgroundColor = Color.LightGray;
                        bt1.BackgroundColor = Color.LightGray;
                        bt5.FontSize = 30;
                        bt2.FontSize = 20;
                        bt3.FontSize = 20;
                        bt4.FontSize = 20;
                        bt1.FontSize = 20;
                        bt5.Opacity = 1;
                        bt2.Opacity = .2;
                        bt3.Opacity = .2;
                        bt4.Opacity = .2;
                        bt1.Opacity = .2;
                    }
                    //totalCredits = Convert.ToInt32(item.credit);
                    //labStatus = item.labs.ToString();
                    break;
                }


            }
        }

        private void bt_rating(object sender, EventArgs e)
        {
            var num = 0;
            Button identifyClickedButton = (Button)sender;
            if (identifyClickedButton.Text == "1")
            {
                num = 1;
                bt1.BackgroundColor = Color.Gold;
                bt2.BackgroundColor = Color.LightGray;
                bt3.BackgroundColor = Color.LightGray;
                bt4.BackgroundColor = Color.LightGray;
                bt5.BackgroundColor = Color.LightGray;
                bt1.FontSize = 30;
                bt2.FontSize = 20;
                bt3.FontSize = 20;
                bt4.FontSize = 20;
                bt5.FontSize = 20;
                bt1.Opacity = 1;
                bt2.Opacity = .2;
                bt3.Opacity = .2;
                bt4.Opacity = .2;
                bt5.Opacity = .2;
            }
            else if (identifyClickedButton.Text == "2")
            {
                num = 2;
                bt2.BackgroundColor = Color.Gold;
                bt1.BackgroundColor = Color.LightGray;
                bt3.BackgroundColor = Color.LightGray;
                bt4.BackgroundColor = Color.LightGray;
                bt5.BackgroundColor = Color.LightGray;
                bt2.FontSize = 30;
                bt1.FontSize = 20;
                bt3.FontSize = 20;
                bt4.FontSize = 20;
                bt5.FontSize = 20;
                bt2.Opacity = 1;
                bt1.Opacity = .2;
                bt3.Opacity = .2;
                bt4.Opacity = .2;
                bt5.Opacity = .2;
            }
            else if (identifyClickedButton.Text == "3")
            {
                num = 3;
                bt3.BackgroundColor = Color.Gold;
                bt2.BackgroundColor = Color.LightGray;
                bt1.BackgroundColor = Color.LightGray;
                bt4.BackgroundColor = Color.LightGray;
                bt5.BackgroundColor = Color.LightGray;
                bt3.FontSize = 30;
                bt2.FontSize = 20;
                bt1.FontSize = 20;
                bt4.FontSize = 20;
                bt5.FontSize = 20;
                bt3.Opacity = 1;
                bt2.Opacity = .2;
                bt1.Opacity = .2;
                bt4.Opacity = .2;
                bt5.Opacity = .2;
            }
            else if (identifyClickedButton.Text == "4")
            {
                num = 4;
                bt4.BackgroundColor = Color.Gold;
                bt2.BackgroundColor = Color.LightGray;
                bt3.BackgroundColor = Color.LightGray;
                bt1.BackgroundColor = Color.LightGray;
                bt5.BackgroundColor = Color.LightGray;
                bt4.FontSize = 30;
                bt2.FontSize = 20;
                bt3.FontSize = 20;
                bt1.FontSize = 20;
                bt5.FontSize = 20;
                bt4.Opacity = 1;
                bt2.Opacity = .2;
                bt3.Opacity = .2;
                bt1.Opacity = .2;
                bt5.Opacity = .2;

            }
            else if (identifyClickedButton.Text == "5")
            {
                num = 5;
                bt5.BackgroundColor = Color.Gold;
                bt2.BackgroundColor = Color.LightGray;
                bt3.BackgroundColor = Color.LightGray;
                bt4.BackgroundColor = Color.LightGray;
                bt1.BackgroundColor = Color.LightGray;
                bt5.FontSize = 30;
                bt2.FontSize = 20;
                bt3.FontSize = 20;
                bt4.FontSize = 20;
                bt1.FontSize = 20;
                bt5.Opacity = 1;
                bt2.Opacity = .2;
                bt3.Opacity = .2;
                bt4.Opacity = .2;
                bt1.Opacity = .2;
            }
            sqlConnection.Open();


            string stringQuery = $"UPDATE BookingApp.dbo.EventDetails SET Rating='{num}' WHERE EventName='{comp_name.Text}'";
            using (SqlCommand command = new SqlCommand(stringQuery, sqlConnection))
            {

                command.ExecuteNonQuery();

            }
            sqlConnection.Close();




            //sqlConnection.Open();
            //using (SqlCommand command2 = new SqlCommand("INSERT INTO BookingApp.dbo.TaxiCompanyDetails VALUES(@Course, @Credit, @Semester, @Lab)", sqlConnection))
            //{
            //    command2.Parameters.Add(new SqlParameter("Course", courses.SelectedItem.ToString()));
            //    command2.Parameters.Add(new SqlParameter("Credit", totalCredits.ToString()));
            //    command2.Parameters.Add(new SqlParameter("Semester", select_sem.SelectedItem.ToString()));
            //    command2.Parameters.Add(new SqlParameter("Lab", labStatus.ToString()));

            //    command2.ExecuteNonQuery();
            //}
            //sqlConnection.Close();

        }

        private void bt_request(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookingScreen(comp_name.Text, "Kid", "Adult", "Senior", "Event"));

        }

        private void bt_back(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}