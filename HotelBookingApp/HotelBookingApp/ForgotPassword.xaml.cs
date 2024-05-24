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
    public partial class ForgotPassword : ContentPage
    {
        SqlConnection sqlConnection;
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void bt_update(object sender, EventArgs e)
        {
            string u_password = entry_password1.Text;

            if (entry_userId.Text != "" && entry_password2.Text != "" && entry_password1.Text != "")
            {
                if (entry_password2.Text == entry_password1.Text)
                {
                    bool status = false;
                    bool statusC = false;
                    bool statusD = false;
                    if (u_password.Length >= 6 && u_password.Length <= 30)
                    {

                        for (int i = 0; i < u_password.Length; i++)
                        {
                            if ((int)u_password[i] >= 48 && (int)u_password[i] <= 57)
                            {
                                statusD = true;
                            }
                        }
                        for (int i = 0; i < u_password.Length; i++)
                        {
                            if (((int)u_password[i] >= 65 && (int)u_password[i] <= 90) || (int)u_password[i] >= 97 && (int)u_password[i] <= 122)
                            {
                                statusC = true;
                            }
                        }
                        if (statusC && statusD)
                        {
                            status = true;
                        }
                    }

                    bool sameCheck = false;

                    if (u_password == "")
                    {
                        table.Text = "No password provided";
                    }
                    else if (u_password.Length < 6 || u_password.Length > 30)
                    {
                        table.Text = "Password length is below or above required";
                    }

                    else if (entry_userId.Text.Equals(u_password))
                    {
                        table.Text = "EmailId & password must not be same";
                        sameCheck = true;
                    }
                    else if (!status)
                    {
                        table.Text = "Password must contain mix of digits and alphabets";
                    }




                    if (sameCheck == false && status == true)
                    {





                        string srvrdbname = "BookingApp";
                        string srvrname = "192.168.2.50";
                        string srvruser = "AtulKumar01";
                        string srvrpassword = "Gupta@3939";

                        string sqlconn = $"Data Source={srvrname}; Initial Catalog={srvrdbname};User Id={srvruser}; Password={srvrpassword}";
                        sqlConnection = new SqlConnection(sqlconn);



                        List<String> myTableList = new List<String>();
                        sqlConnection.Open();
                        string queryString = "Select * from BookingApp.dbo.LoginCredentials";
                        SqlCommand command = new SqlCommand(queryString, sqlConnection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            myTableList.Add(
                              reader["EmailID"].ToString()
                            );
                        }
                        reader.Close();
                        sqlConnection.Close();

                        var counter = 0;
                        foreach (var item in myTableList)
                        {
                            if (item == entry_userId.Text)
                            {
                                counter = 1;
                                break;

                            }
                        }

                        if (counter == 1)
                        {
                            sqlConnection.Open();


                            string stringQuery = $"UPDATE BookingApp.dbo.LoginCredentials SET Password='{entry_password1.Text}' WHERE EmailID='{entry_userId.Text}'";
                            using (SqlCommand command1 = new SqlCommand(stringQuery, sqlConnection))
                            {

                                command1.ExecuteNonQuery();

                            }
                            sqlConnection.Close();
                            DisplayAlert("Alert", "Your password is successfully updated", "OK");
                            Navigation.PopAsync();
                        }
                        else
                        {
                            table.Text = "No account exists with this email";
                        }


                    }
                }
                else
                {
                    table.Text = "Passwords must be same in both fields";
                }
            }



            else
            {
                table.Text = "Please fill all the details first";
            }

        }

        private void bt_back(object sender, EventArgs e)
        {
Navigation.PopAsync();
        }
    }
}