using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotelBookingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterScreen : ContentPage
    {
        public class MyTableList
        {
            public string emailID { get; set; }

            public string password { get; set; }
        }
        SqlConnection sqlConnection;
        public RegisterScreen()
        {
            InitializeComponent();
        }


        private void bt_register(object sender, EventArgs e)
        {
            if (entry_name.Text != "" && entry_password.Text != "" && entry_userId.Text != "")
            {
                bool nameValid = false;
                bool emailValid = false;
                bool passwordValid = false;

                string customerName = entry_name.Text;
                string customerEmail = entry_userId.Text;
                string u_password = entry_password.Text;


                int statusName = 0;
                for (int i = 0; i < customerName.Length; i++)
                {
                    if (((int)customerName[i] >= 65 && (int)customerName[i] <= 90) || ((int)customerName[i] >= 97 && (int)customerName[i] <= 122))
                    {
                        statusName += 1;
                    }

                }

                if (statusName == customerName.Length && customerName.Length > 1)
                {
                    nameValid = true;
                }
                else if (customerName.Length < 2)
                {
                    table.Text = "Name is too short";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }
                else
                {
                    table.Text = "Invalid Name";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }


                //atulkumar01@algomau.ca
                //atul.kumar@algomau.ca
                bool check1 = false;
                bool check2 = false;
                bool check3 = false;
                bool check4 = false;
                bool check5 = false;
                bool check6 = false;
                bool check7 = false;
                int indexAtTheRate = -1;
                int seperation = 0;

                for (int i = 0; i < customerEmail.Length; i++)
                {
                    if ((((int)customerEmail[i] >= 65 && (int)customerEmail[i] <= 90) || ((int)customerEmail[i] >= 97 && (int)customerEmail[i] <= 122)) && check1 == false && check2 == false && check3 == false && check4 == false)
                    {
                        check1 = true;
                    }
                    else if ((((int)customerEmail[i] >= 65 && (int)customerEmail[i] <= 90) || ((int)customerEmail[i] >= 97 && (int)customerEmail[i] <= 122) || ((int)customerEmail[i] >= 48 && (int)customerEmail[i] <= 57)) && seperation == 0)
                    {
                        seperation = 1;
                    }

                    else if (check1 == true && check3 == false && check4 == false && ((int)customerEmail[i] == 46) && seperation == 1)
                    {
                        seperation = 0;
                        continue;
                    }
                    else if ((int)customerEmail[i] == 64 && check1 == true && check3 == false && check4 == false && seperation == 1)
                    {
                        check3 = true;
                        seperation = 0;
                        indexAtTheRate = i;
                    }
                    else if ((int)customerEmail[i] == 46 && check1 == true && check3 == true && check4 == false && seperation == 1)
                    {
                        check4 = true;
                        seperation = 0;
                    }
                }

                int countingAtTheRates = 0;
                for (int i = 0; i < customerEmail.Length; i++)
                {
                    if ((int)customerEmail[i] == 64)
                    {
                        countingAtTheRates += 1;
                    }
                }
                if (countingAtTheRates == 1)
                {
                    check6 = true;
                }
                else
                {
                    table.Text = "Accepted email format(may contain numbers): abc@hotmail.com or abc.bde@hotmail.com";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }

                int checkNumbers = 0;
                for (int i = indexAtTheRate + 1; i < customerEmail.Length; i++)
                {
                    if ((int)customerEmail[i] >= 48 && (int)customerEmail[i] <= 57)
                    {
                        checkNumbers++;
                        break;
                    }

                }
                if (checkNumbers > 0)
                {
                    check2 = false;
                }
                else
                {
                    check2 = true;
                }
                bool noSpecialCharacter = true;
                for (int i = 0; i < customerEmail.Length; i++)
                {
                    if (((int)customerEmail[i] >= 65 && (int)customerEmail[i] <= 90) || ((int)customerEmail[i] >= 97 && (int)customerEmail[i] <= 122) || ((int)customerEmail[i] >= 48 && (int)customerEmail[i] <= 57) || (int)customerEmail[i] == 64 || (int)customerEmail[i] == 46)
                    {

                    }

                    else
                    {
                        noSpecialCharacter = false;
                        break;
                    }
                }
                if ((((int)customerEmail[0] >= 65 && (int)customerEmail[0] <= 90) || ((int)customerEmail[0] >= 97 && (int)customerEmail[0] <= 122)))
                {
                    check5 = true;
                }

                if ((((int)customerEmail[customerEmail.Length - 1] >= 65 && (int)customerEmail[customerEmail.Length - 1] <= 90) || ((int)customerEmail[customerEmail.Length - 1] >= 97 && (int)customerEmail[customerEmail.Length - 1] <= 122)))
                {
                    check7 = true;
                }

                if (check1 == true && check2 == true && check3 == true && check4 == true && check5 == true && check6 == true && check7 == true && noSpecialCharacter == true)
                {
                    emailValid = true;
                }
                else if (noSpecialCharacter == false)
                {
                    table.Text = "Email must have no other characters than A-Z, a-z, 0-9, ., @";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }
                else if (check5 == false || check7 == false)
                {
                    table.Text = "Email must start and end with either A-Z or a-z";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }
                else
                {

                    table.Text = "Accepted email format(may contain numbers): abc@hotmail.com or abc.bde@hotmail.com";
                    table.TextColor = Color.Red;
                    table.FontSize = 22;

                }

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

                else if (customerEmail.Equals(u_password))
                {
                    table.Text = "EmailId & password must not be same";
                    sameCheck = true;
                }
                else if (!status)
                {
                    table.Text = "Password must contain mix of digits and alphabets";
                }





                if (nameValid == true && emailValid == true && status==true && sameCheck==false)
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

                        }
                    }

                    if (counter == 0)
                    {

                        sqlConnection.Open();
                        using (SqlCommand command2 = new SqlCommand("INSERT INTO BookingApp.dbo.LoginCredentials VALUES(@EmailID, @Password, @Name)", sqlConnection))
                        {
                            command2.Parameters.Add(new SqlParameter("EmailID", entry_userId.Text.ToString()));
                            command2.Parameters.Add(new SqlParameter("Password", entry_password.Text.ToString()));
                            command2.Parameters.Add(new SqlParameter("Name", entry_name.Text.ToString()));
                            command2.ExecuteNonQuery();
                        }
                        sqlConnection.Close();


                        DisplayAlert("Alert", "You are now registerd!", "OK");
                        Navigation.PopAsync();
                    }
                    else
                    {
                        table.Text = "You have already an account with this email";
                    }





                    //-
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
