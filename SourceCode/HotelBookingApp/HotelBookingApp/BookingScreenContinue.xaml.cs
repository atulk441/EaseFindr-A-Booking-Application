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
	public partial class BookingScreenContinue : ContentPage
	{
        static string service = "";
        static string category = "";
        SqlConnection sqlConnection;
		public BookingScreenContinue (string company_name, string service1, string category1)
		{
			InitializeComponent ();
			comp_name.Text = company_name;
            service = service1;
            category=category1; 
        }

    private void bt_confirm(object sender, EventArgs e)
        {
            if (cvcNumber.Text != "" && expiryDate.Text != "" && cvcNumber.Text != "")
            {
                bool status1 = false;
                bool status2 = false;
                bool status3 = false;
                int counter = 0;
                for (int i = 0; i < cardNumber.Text.Length; i++)
                {
                    if ((int)cardNumber.Text[i] >= 48 && (int)cardNumber.Text[i] <= 57)
                    {
                        counter = counter + 1;
                    }
                }
                if (counter == 16)
                {
                    status1 = true;
                }
                else
                {
                    invalid_message.Text = "Invalid Message: Card Number must contain 16 numberic digits";
                }

                counter = 0;
                for (int i = 0; i < expiryDate.Text.Length; i++)
                {
                    if (expiryDate.Text[i] == '/')
                    {
                        continue;
                    }
                    if ((int)expiryDate.Text[i] >= 48 && (int)expiryDate.Text[i] <= 57)
                    {

                        counter = counter + 1;
                    }
                }
                if (counter == 4)
                {
                    status2 = true;
                }
                else
                {
                    invalid_message.Text = "Invalid Message: Expiry date is not in correct format";
                }


                counter = 0;
                for (int i = 0; i < cvcNumber.Text.Length; i++)
                {
                    if ((int)cvcNumber.Text[i] >= 48 && (int)cvcNumber.Text[i] <= 57)
                    {
                        counter = counter + 1;
                    }
                }
                if (counter == 3)
                {
                    status3 = true;
                }
                else
                {
                    invalid_message.Text = "Invalid Message: CVV must contain 3 numberic digits";
                }

                if (status1 == true && status2 == true && status3 == true)
                {
                    string srvrdbname = "StudentRegistration";
                    string srvrname = "192.168.2.50";
                    string srvruser = "AtulKumar01";
                    string srvrpassword = "Gupta@3939";

                    string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
                    sqlConnection = new SqlConnection(sqlconn);
                    sqlConnection.Open();

                    using (SqlCommand command2 = new SqlCommand("INSERT INTO BookingApp.dbo.Reservations VALUES(@Service, @Name, @Category)", sqlConnection))
                    {
                        command2.Parameters.Add(new SqlParameter("Service", comp_name.Text.ToString()));
                        command2.Parameters.Add(new SqlParameter("Name", service.ToString()));
                        command2.Parameters.Add(new SqlParameter("Category", category.ToString()));

                        command2.ExecuteNonQuery();
                    }
                    sqlConnection.Close();


                    DisplayAlert("Alert", "Order has been booked", "OK");
                    Navigation.PopAsync();
                    Navigation.PopAsync();
                    Navigation.PopAsync();
                    Navigation.PopAsync();


                }
                else
                {
                    invalid_message.Text = "Invalid Message: Value is not valid";
                }
            }
            else
            {
                invalid_message.Text = "Invalid Message: Please fill all the details first";
            }

        }

        private void bt_back(object sender, EventArgs e)
        {
			Navigation.PopAsync();
        }
    }
}