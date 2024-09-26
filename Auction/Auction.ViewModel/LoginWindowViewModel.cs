using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using Auction.Model;

namespace Auction.ViewModel
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        private User user;
        private string username;
        private string password;

        private string errorMessage;


       
        public User User
        {
            get { return user; }
            set
            {
                if (user == value)
                {
                    return;
                }
                user = value;
                OnPropertyChanged(new PropertyChangedEventArgs("User"));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password == value)
                {
                    return;
                }
                password = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Password"));
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (username == value)
                {
                    return;
                }
                username = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Username"));
            }
        }
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage == value)
                {
                    return;
                }
                errorMessage = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ErrorMessage"));
            }
        }

        public LoginWindowViewModel(string Username, string Password)
        {

            if (Username == "" || Password == "")
            {
                ErrorMessage = "Fields cannot be empty.";
            }
            else if (!Regex.Match(Username, @"^\w+$").Success || (!Regex.Match(Password, @"^\w+$").Success))
            {
                ErrorMessage = "Username or password can only contain letters, numbers and underscore characters.";
            }
            else if (!UserExists(Username, Password))
            {
                ErrorMessage = "Wrong username or password.";
            }
            else if (UserExists(Username, Password))
            {
                ErrorMessage = "";
                this.Username = Username;
                this.Password = Password;
                User = new User(this.Username, this.Password);
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private bool UserExists(string UserName, string UserPass)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();


                SqlCommand command = new SqlCommand("SELECT username, password FROM [User] WHERE username = @UserName AND password = @UserPass;", conn);

                SqlParameter userNameParameter = new SqlParameter("@UserName", SqlDbType.NVarChar);
                userNameParameter.Value = UserName;

                SqlParameter passWordParameter = new SqlParameter("@UserPass", SqlDbType.NVarChar);
                passWordParameter.Value = UserPass;

                command.Parameters.Add(userNameParameter);
                command.Parameters.Add(passWordParameter);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
