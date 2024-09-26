using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Model
{
    public class AuctionCollection : ObservableCollection<Auctions>
    {
        public static AuctionCollection GetAllAuctions()
        {


            AuctionCollection auctions = new AuctionCollection();
            Auctions auction = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT auction_id, product_name, product_type, product_price, best_offer, bestbidder, winner, active, timeleft FROM Auction WHERE active=1", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        auction = Auctions.GetAuctionFromResultSet(reader);
                        auctions.Add(auction);

                    }
                }

            }
            return auctions;
        }
    }
}
