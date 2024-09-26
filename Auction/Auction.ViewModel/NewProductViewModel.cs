using Auction.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Auction.ViewModel
{
    public class NewProductViewModel : INotifyPropertyChanged
    {
        private Auctions product; 
        private Mediator mediator;

        public Auctions Product
        {
            get { return product; }
            set
            {
                if (product == value)
                {
                    return;
                }
                product = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Product"));
            }
        }

        public NewProductViewModel(Mediator mediator)
        {
            this.mediator = mediator;

            SaveCommand = new RelayCommand(SaveExecute, CanSave);

            Product = new Auctions();

        }


        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                if (saveCommand == value)
                {
                    return;
                }
                saveCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SaveCommand"));
            }
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Product.Time > 0)
            {
                Product.Time--;
                Product.Timeleft = string.Format("{0}:{1}", Product.Time / 60, Product.Time % 60);
            }
            else
            {
                Product.DispatcherTimer.Stop();
                Product.Timeleft = "Auction closed.";
                if (Product.Best_bidder != "")
                {
                    Product.Winner = Product.Best_bidder;
                }
                if (Product.Best_offer == null)
                {
                    UpdateEmpty(Product.Auction_id);
                }
                else
                {
                    UpdateWithWinner(Product.Best_offer, Product.Best_bidder, Product.Winner, Product.Auction_id);
                }
            }

        }


        void SaveExecute(object obj)
        {
            if (Product.HasErrors)
            {
                OnDone(new DoneEventArgs("Check your input.")); 
            }
            else if (Product != null && !Product.HasErrors)
            {
                Product.Save();

                OnDone(new DoneEventArgs("Product added, auction begins now."));

                mediator.Notify("AuctionChange", Product);

                Product.DispatcherTimer = new DispatcherTimer();
                Product.DispatcherTimer.Tick += dispatcherTimer_Tick;
                Product.DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                Product.DispatcherTimer.Start();

            }
        }

        bool CanSave(object obj)
        {
            return true;
        }

        public delegate void DoneEventHandler(object sender, DoneEventArgs e);

        public class DoneEventArgs : EventArgs
        {
            private string message;

            public string Message
            {
                get { return message; }
                set
                {
                    if (message == value)
                    {
                        return;
                    }
                    message = value;
                }
            }

            public DoneEventArgs(string message)
            {
                this.message = message;
            }
        }


        public event DoneEventHandler Done;

        public void OnDone(DoneEventArgs e)
        {
            if (Done != null)
            {
                Done(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) 
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


        public static void UpdateWithWinner(double? bestOffer, string bestBidder, string winner, int auctionId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Auction SET best_offer=@BestOffer, bestbidder=@BestBidder,timeleft='Auction closed.', winner=@Winner WHERE auction_id=@AuctionId;", conn);

                SqlParameter bstOffer = new SqlParameter("@BestOffer", SqlDbType.Float);
                bstOffer.Value = bestOffer;

                SqlParameter bstBidder = new SqlParameter("@BestBidder", SqlDbType.NVarChar);
                bstBidder.Value = bestBidder;

                SqlParameter wner = new SqlParameter("@Winner", SqlDbType.NVarChar);
                wner.Value = winner;

                SqlParameter auctId = new SqlParameter("@AuctionId", SqlDbType.Int, 11);
                auctId.Value = auctionId;

                command.Parameters.Add(bstOffer);
                command.Parameters.Add(bstBidder);
                command.Parameters.Add(wner);
                command.Parameters.Add(auctId);

                int rows = command.ExecuteNonQuery();

            }
        }

        public static void UpdateEmpty(int auctionId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Auction SET best_offer=NULL, bestbidder=NULL,timeleft='Auction closed.', winner=NULL WHERE auction_id=@AuctionId;", conn);

                SqlParameter auctId = new SqlParameter("@AuctionId", SqlDbType.Int, 11);
                auctId.Value = auctionId;

                command.Parameters.Add(auctId);

                int rows = command.ExecuteNonQuery();

            }
        }

    }
}
