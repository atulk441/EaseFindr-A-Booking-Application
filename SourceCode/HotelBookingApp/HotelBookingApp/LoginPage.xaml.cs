using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;

namespace HotelBookingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage

    {

        public class MyTableList
        {
            public string emailID { get; set; }

            public string password { get; set; }
            public string name { get; set; }
        }
        SqlConnection sqlConnection;
        SqlConnection sqlConnectionV1;
        public LoginPage()
        {
            InitializeComponent();
          
        }

        private void bt_login(object sender, EventArgs e)
        {
            if (entry_userId.Text != "" && entry_password.Text != "")
            {
                string srvrdbname = "BookingApp";
                string srvrname = "192.168.2.50";
                string srvruser = "AtulKumar01";
                string srvrpassword = "Gupta@3939";

                string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
                sqlConnection = new SqlConnection(sqlconn);
                List<MyTableList> myTableList = new List<MyTableList>();
                sqlConnection.Open();
                string queryString = "Select * from BookingApp.dbo.LoginCredentials";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    myTableList.Add(new MyTableList
                    {
                        emailID = reader["EmailID"].ToString(),
                        password = reader["Password"].ToString(),
                        name = reader["Name"].ToString(),
                    });
                }

                reader.Close();
                sqlConnection.Close();
                var counter = 0;
                foreach (var item in myTableList)
                {
                    if (entry_userId.Text == item.emailID && entry_password.Text == item.password)
                    {

                        //sqlConnection = new SqlConnection(sqlconn);

                        sqlConnection.Open();

                        using (SqlCommand command2 = new SqlCommand("DELETE FROM BookingApp.dbo.CurrentUser", sqlConnection))
                        {
                            command2.ExecuteNonQuery();
                        }
                        //sqlConnection.Close();

                        using (SqlCommand command2 = new SqlCommand("INSERT INTO BookingApp.dbo.CurrentUser VALUES(@EmailID, @Name)", sqlConnection))
                        {
                            command2.Parameters.Add(new SqlParameter("EmailID", entry_userId.Text.ToString()));
                            //command2.Parameters.Add(new SqlParameter("Password", entry_password.Text.ToString()));
                            command2.Parameters.Add(new SqlParameter("Name", item.name.ToString()));
                            command2.ExecuteNonQuery();
                        }
                        sqlConnection.Close();

                

                        Navigation.PushAsync(new ReservationServices());
                        entry_userId.Text = "";
                        entry_password.Text = "";
                        counter++;
                    }
                }
                if (counter == 0)
                {
                    lbl_message.Text = "EmailID or Password didn't have any matching in the database";
                }

            }
            else
            {
                lbl_message.Text = "Please fill all the details first";

            }
        }

        private void lbl_forgotPass(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPassword());
            entry_userId.Text = "";
            entry_password.Text = "";
            lbl_message.Text = "";
        }

        private void lbl_register(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterScreen());
            entry_userId.Text = "";
            entry_password.Text = "";
            lbl_message.Text = "";

        }
    }
}