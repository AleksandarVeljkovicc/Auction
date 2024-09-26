using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Collections;
using System.Collections.ObjectModel;

namespace Auction.Model
{
    public class User : INotifyPropertyChanged
    {
        private string name;
        private string lastname;
        private bool? isAdmin;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                {
                    return;
                }
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        public string Lastname
        {
            get { return lastname; }
            set
            {
                if (lastname == value)
                {
                    return;
                }
                lastname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Lastname"));
            }
        }

        public string FullName
        {
            get { return Name.Trim() + " " + Lastname.Trim(); } 
        }

        public bool? IsAdmin
        {
            get { return isAdmin; }
            set
            {
                if (isAdmin == value)
                {
                    return;
                }
                isAdmin = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsAdmin"));
            }
        }


        public User(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT isAdmin, firstName, lastName FROM [User] WHERE username = @Username AND password = @Password;", conn);

                SqlParameter userName = new SqlParameter("@Username", SqlDbType.NVarChar);
                userName.Value = username;

                SqlParameter pw = new SqlParameter("@Password", SqlDbType.NVarChar);
                pw.Value = password;

                command.Parameters.Add(userName);
                command.Parameters.Add(pw);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.IsAdmin = (bool?)reader["isAdmin"];
                        this.Name = (string)reader["firstName"];
                        this.Lastname = (string)reader["lastName"];
                    }               
                }

            }
        }

    }
}
