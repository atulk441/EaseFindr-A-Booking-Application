using NodaTime;
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
    public partial class BookingScreen : ContentPage
    {
        public class Person
        {
           public string emailID { get; set; }
            public string name { get; set; }

        }
        static string service = "";
        SqlConnection sqlConnection;
        public BookingScreen(string companyName, string option1, string option2, string option3, string service1)
        {
            InitializeComponent();
            comp_name.Text = companyName;
            service = service1;
            string srvrdbname = "BookingApp";
            string srvrname = "192.168.2.50";
            string srvruser = "AtulKumar01";
            string srvrpassword = "Gupta@3939";

            string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
            List<Person> persons = new List<Person>();
            sqlConnection.Open();
            string queryString = "Select * from BookingApp.dbo.CurrentUser";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                persons.Add(new Person
                {
                    emailID = reader["EmailID"].ToString(),
                    name = reader["Name"].ToString(),  
                }); ;
            }

            reader.Close();
            sqlConnection.Close();

            Person currentUser = persons[persons.Count - 1];

            name.Text= currentUser.name;
            email.Text = currentUser.emailID;

            options.Items.Add(option1);
            options.Items.Add(option2);
            options.Items.Add(option3);
                
        }

        private void bt_next(object sender, EventArgs e)
        {
            if (phone.Text != "")
            {
                bool phoneValid = false;
                bool dateValid = false;
                bool dateTimeValid = false;
                string customerContact = phone.Text;

                int statusNumber = 0;
                for (int i = 0; i < customerContact.Length; i++)
                {
                    if (((int)customerContact[i] >= 48 && (int)customerContact[i] <= 57))
                    {
                        statusNumber += 1;
                    }
                }

                if (statusNumber == customerContact.Length && customerContact.Length == 10)
                {
                    phoneValid = true;
                }
                else if (customerContact.Length < 10 || customerContact.Length > 10)
                {

                    table.Text = "Invalid Message: Phone Number must have 10 digits";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }
                else
                {
                    table.Text = "Invalid Message: Invalid Phone Number";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }


                DateTime bookingDate = datePicker.Date;
                string date = "";
                for (int i = 0; i < bookingDate.ToString().Length && bookingDate.ToString()[i] != ' '; i++)
                {
                    date += bookingDate.ToString()[i];
                }

                int mm1 = 0, dd1 = 0, yy1 = 0;
                String m1 = "", d1 = "", y1 = "";
                int counter1 = 0;
                for (int i = 0; i < date.Length; i++)
                {

                    if (counter1 == 0 && date[i] != '/')
                    {
                        m1 += date[i];
                        mm1 = Int32.Parse(m1);

                    }

                    else if (counter1 == 0 && date[i] == '/')
                    {
                        counter1 = 1;
                        continue;
                    }
                    if (counter1 == 1 && date[i] != '/')
                    {
                        d1 += date[i];
                        dd1 = Int32.Parse(d1);

                    }
                    else if (counter1 == 1 && date[i] == '/')
                    {
                        counter1 = 2;
                        continue;
                    }

                    if (counter1 == 2)
                    {
                        y1 += date[i];
                        yy1 = Int32.Parse(y1);
                    }
                }

                LocalDate date1 = new LocalDate(yy1, mm1, dd1);

                DateTime currentDate = DateTime.Now;
                string currentDateS = currentDate.ToString("MM/dd/yyyy");
                int mm2 = 0, dd2 = 0, yy2 = 0;
                String m2 = "", d2 = "", y2 = "";
                int counter2 = 0;
                for (int i = 0; i < currentDateS.Length; i++)
                {
                    if (counter2 == 0 && currentDateS[i] != '/')
                    {
                        m2 += currentDateS[i];
                        mm2 = Int32.Parse(m2);

                    }
                    else if (counter2 == 0 && currentDateS[i] == '/')
                    {
                        if (m2.Length == 2)
                        {
                            counter2 = 1;
                        }
                        continue;
                    }
                    if (counter2 == 1 && currentDateS[i] != '/')
                    {
                        d2 += currentDateS[i];
                        dd2 = Int32.Parse(d2);

                    }
                    else if (counter2 == 1 && currentDateS[i] == '/')
                    {
                        if (d2.Length == 2)
                        {
                            counter2 = 2;
                        }
                        continue;
                    }

                    if (counter2 == 2)
                    {
                        y2 += currentDateS[i];
                        yy2 = Int32.Parse(y2);
                    }
                }


                TimeSpan bookingTime = timePicker.Time;
                DateTime selectedTime = new DateTime(yy1, mm1, dd1, bookingTime.Hours, bookingTime.Minutes, bookingTime.Seconds);
                DateTime currentTime = DateTime.Now;
                if (currentTime < selectedTime)
                {
                    dateTimeValid = true;
                }

                else
                {
                    table.Text = "Invalid Message: Invalid Date/Time selected (order in past time can't be made)";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;
                }


                if(options.SelectedItem==null)
                {
                    table.Text = "Invalid Message: Please select at least one of the categories from the Picker Control";

                }



                if (phoneValid == true && dateTimeValid==true && options.SelectedItem!=null)
                {
                    Navigation.PushAsync(new BookingScreenContinue(comp_name.Text, service, options.SelectedItem.ToString()));
                    //DisplayAlert("Alert", "Order has been booked", "OK");
                    //Navigation.PopAsync();
                    //Navigation.PopAsync();
                    //Navigation.PopAsync();
                }

            }
            else
            {
                table.Text = "Invalid Message: Please fill all the details";
                table.TextColor= Color.Red;
            }
            
        }
    }
}