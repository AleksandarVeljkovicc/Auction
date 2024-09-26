using Auction.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace Auction.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Auctions currentAuction;  
        private AuctionCollection auctionList; 
        private ListCollectionView auctionListView; 

        private Mediator mediator;  

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e) 
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public Auctions CurrentAuction
        {
            get { return currentAuction; }
            set
            {
                if (currentAuction == value)
                {
                    return;
                }
                currentAuction = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentAuction"));

                CommandManager.InvalidateRequerySuggested();    // Osvezava stanje komandi
            }
        }
        public AuctionCollection AuctionList
        {
            get { return auctionList; }
            set
            {
                if (auctionList == value)
                {
                    return;
                }
                auctionList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AuctionList"));
            }
        }
        public ListCollectionView AuctionListView
        {
            get { return auctionListView; }
            set
            {
                if (auctionListView == value)
                {
                    return;
                }
                auctionListView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AuctionListView"));
            }
        }


        public MainWindowViewModel(Mediator mediator) 
        {
            this.mediator = mediator;

            DeleteCommand = new RelayCommand(DeleteExecute, CanDelete);

            
            OfferCommand = new RelayCommand(Offer, CanOffer);
            

            AuctionList = AuctionCollection.GetAllAuctions();

            AuctionListView = new ListCollectionView(AuctionList);


            CurrentAuction = new Auctions();
            
            mediator.Register("AuctionChange", AuctionChanged);
           
        }



        private void AuctionChanged(object obj)
        {
            Auctions auct = (Auctions)obj;

            int index = AuctionList.IndexOf(auct);

            if (index != -1)
            {
                AuctionList.RemoveAt(index);
                AuctionList.Insert(index, auct);
            }
            else
            {
                AuctionList.Add(auct);
            }
        }



        private ICommand deleteCommand;

        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                if (deleteCommand == value)
                {
                    return;
                }
                deleteCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeleteCommand"));
            }
        }

        void DeleteExecute(object obj)
        {
            if (CurrentAuction != null)
            {
                CurrentAuction.DeleteAuction();
                AuctionList.Remove(CurrentAuction);
                CurrentAuction = null; // Postavi na null nakon brisanja
                CommandManager.InvalidateRequerySuggested(); // Osvezi stanje komandi
            }
        }


        bool CanDelete(object obj)
        {
            // Ako je CurrentAuction null, dugme je onemoguceno
            if (CurrentAuction == null)
                return false;

            return true; // Ako su svi uslovi ispunjeni, dugme je omoguceno
        }


        private ICommand offerCommand;

        public ICommand OfferCommand
        {
            get { return offerCommand; }
            set
            {
                if (offerCommand == value)
                {
                    return;
                }
                offerCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OfferCommand"));
            }
        }


        bool CanOffer(object obj)
        {
            // Prvo proveri da li je CurrentAuction null
            if (CurrentAuction == null)
                return false;

            // Ako je aukcija zatvorena, onemoguci dugme
            if (CurrentAuction.Timeleft == "Auction closed.")
                return false;

            return true; // U svim drugim slučajevima, dugme je omoguceno
        }

        void Offer(object obj)
        {
            
            if (CurrentAuction.Best_offer == null)
            {             
                CurrentAuction.Best_offer = CurrentAuction.Product_price + 1;
                CurrentAuction.Time = 120;
                CurrentAuction.DispatcherTimer.Start();                           
            }
            else
            {
                CurrentAuction.Best_offer += 1;
                CurrentAuction.Time = 120;
                CurrentAuction.DispatcherTimer.Start();             
            }      
        }      
    }
}
