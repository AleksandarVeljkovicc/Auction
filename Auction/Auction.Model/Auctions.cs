using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace Auction.Model
{
    public class Auctions: INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private int auction_id;
        private string product_name;
        private double product_price;
        private string product_type;
        private bool active;
        private double? best_offer;
        private string best_bidder;
        private string winner;
        private string timeleft;
        private DispatcherTimer dispatcherTimer;
        private int time = 120;
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e) 
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public int Auction_id
        {
            get { return auction_id; }
            set
            {
                if (auction_id == value)
                {
                    return;
                }
                auction_id = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Auction_id"));
            }
        }

        public string Timeleft
        {
            get { return timeleft; }
            set
            {
                if (timeleft == value)
                {
                    return;
                }
                timeleft = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Timeleft"));
            }
        }

        public double? Best_offer
        {
            get { return best_offer; }
            set
            {
                if (best_offer == value)
                {
                    return;
                }
                best_offer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Best_offer"));
            }
        }

        public string Best_bidder
        {
            get { return best_bidder; }
            set
            {
                if (best_bidder == value)
                {
                    return;
                }
                best_bidder = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Best_bidder"));
            }
        }

        public string Winner
        {
            get { return winner; }
            set
            {
                if (winner == value)
                {
                    return;
                }
                winner = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Winner"));
            }
        }

        public string Product_name
        {
            get { return product_name; }
            set
            {
                if (product_name == value)
                {
                    return;
                }
                product_name = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("Product name can't be empty.");
                    SetErrors("Product_name", errors);
                    valid = false;
                }

                if (Product_name.Length < 3)
                {
                    errors.Add("Product name can not have less than 3 characters.");
                    SetErrors("Product_name", errors);
                    valid = false;
                }


                if (!Regex.Match(value, @"^[a-zA-Z0-9\- /.,]*$").Success)
                {
                    errors.Add("Product name can only contain letters, numbers, hyphens, spaces, slashes, periods, and commas.");
                    SetErrors("Product_name", errors);
                    valid = false;
                }



                if (valid)
                {
                    ClearErrors("Product_name");
                }
                OnPropertyChanged(new PropertyChangedEventArgs("Product_name"));
            }
        }

      

         public double Product_price
         {
             get { return product_price; }
             set
             {
                 if (product_price == value)
                 {
                     return;
                 }
                 product_price = value;

                 List<string> errors = new List<string>();
                 bool valid = true;

                 if (value <= 0)   
                 {

                     errors.Add("Product price can't be less or equal to 0.");
                     SetErrors("Product_price", errors);
                     valid = false;
                 }

              
                 if (valid)
                 {
                     ClearErrors("Product_price");
                 }

                 OnPropertyChanged(new PropertyChangedEventArgs("Product_price"));
             }
         }

      
        public string Product_type
        {
            get { return product_type; }
            set
            {
                if (product_type == value)
                {
                    return;
                }
                product_type = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("Product type can't be empty.");
                    SetErrors("Product_type", errors);
                    valid = false;
                }

                if (Product_type.Length < 3)
                {
                    errors.Add("Product type can not have less than 3 characters.");
                    SetErrors("Product_type", errors);
                    valid = false;
                }


               
                if (!Regex.Match(value, @"^[a-zA-Z0-9\- /.,]*$").Success)
                {
                    errors.Add("Product type can only contain letters, numbers, hyphens, spaces, slashes, periods, and commas.");
                    SetErrors("Product_name", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("Product_type");
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Product_type"));
            }
        }

        public bool Active
        {
            get { return active; }
            set
            {
                if (active == value)
                {
                    return;
                }
                active = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Active"));
            }
        }

        public DispatcherTimer DispatcherTimer
        {
            get { return dispatcherTimer; }
            set
            {
                if (dispatcherTimer == value)
                {
                    return;
                }
                dispatcherTimer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DispatcherTimer"));
            }
        }

        public int Time
        {
            get { return time; }
            set
            {
                if (time == value)
                {
                    return;
                }
                time = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
        }


        public Auctions(string Product_name)
        {
            this.Product_name = Product_name;
        }


        public Auctions()
        {
            Product_name = "";
            Product_type = "";
            Product_price = 1;
        }
        public Auctions(int Auction_id, string Product_name, string Product_type, double Product_price, double? Best_offer, string Best_bidder, string Winner, bool Active, string Timeleft)
        {
            this.Auction_id = Auction_id;
            this.Product_name = Product_name;         
            this.Product_type = Product_type;
            this.Active = Active;
            this.Product_price = Product_price;
            this.Best_offer = Best_offer;
            this.Winner = Winner;
            this.Best_bidder = Best_bidder;
            this.Timeleft = Timeleft;
        }

        public static Auctions GetAuctionFromResultSet(SqlDataReader reader)
        {
            Auctions auction = new Auctions((int)reader["auction_id"], (string)reader["product_name"], (string)reader["product_type"], (double)reader["product_price"], getDoubleFromDB(reader["best_offer"]), getStringFromDB(reader["bestbidder"]), getStringFromDB(reader["winner"]), (bool)reader["active"], getStringFromDB(reader["timeleft"]));
            return auction;
            
        }

        public static double? getDoubleFromDB(object value)
        {
            if (value == DBNull.Value) return null;
            return Convert.ToDouble(value);
        }

        public static string getStringFromDB(object value)
        {
            if (value == DBNull.Value) return "";
            return Convert.ToString(value);
        }

        public void Save()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();
              
                SqlCommand command = new SqlCommand("INSERT INTO Auction (product_name, product_price, product_type) VALUES (@Product_name, @Product_price, @Product_type); SELECT IDENT_CURRENT('Auction');", conn); //IDENT_CURRENT-citanje poslednjeg unetog zapisa

                SqlParameter productName = new SqlParameter("@Product_name", SqlDbType.NVarChar);
                productName.Value = this.Product_name;

                SqlParameter productPrice = new SqlParameter("@Product_price", SqlDbType.Float);
                productPrice.Value = this.Product_price;

                SqlParameter productType = new SqlParameter("@Product_type", SqlDbType.NVarChar);
                productType.Value = this.Product_type;



                command.Parameters.Add(productName);
                command.Parameters.Add(productPrice);
                command.Parameters.Add(productType);



                var id = command.ExecuteScalar(); 
                if (id != null)
                {
                    this.Auction_id = Convert.ToInt32(id);
                }

            }
        }


        public void DeleteAuction()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Auction SET active=0 WHERE auction_id=@Auction_id", conn);

                SqlParameter myParam = new SqlParameter("@Auction_id", SqlDbType.Int, 11);
                myParam.Value = this.Auction_id;

                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();

            }
        }


        public bool HasErrors
        {
            get
            {
                return (errors.Count > 0);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (errors.Values);
            }
            else
            {
                if (errors.ContainsKey(propertyName))
                {
                    return (errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }

        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            errors.Remove(propertyName);
            errors.Add(propertyName, propertyErrors);

            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private void ClearErrors(string propertyName)
        {
            errors.Remove(propertyName);

            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Auctions productObj = (Auctions)obj;

            if (productObj.Auction_id == this.Auction_id) return true;

            return false;
        }


        public override int GetHashCode()
        {
            return Auction_id.GetHashCode();
        }
    }
}




