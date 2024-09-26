using Auction.Model;
using Auction.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace Auction.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainVievModel = new MainWindowViewModel(Mediator.Instance);
            this.DataContext = mainVievModel;

            offerBtn.Visibility = Visibility.Hidden;
            newBtn.Visibility = Visibility.Hidden;
            delBtn.Visibility = Visibility.Hidden;
        }



        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            NewProduct newWindow = new NewProduct();
            newWindow.DataContext = new NewProductViewModel(Mediator.Instance);
            newWindow.ShowDialog();
        }



        private void offerBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel mainVievModel = (MainWindowViewModel)DataContext;
            mainVievModel.CurrentAuction.Best_bidder = (string)fullName.Content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            logIn.Content = "Log out";
            this.Visibility = Visibility.Collapsed;

            offerBtn.Visibility = Visibility.Hidden;
            newBtn.Visibility = Visibility.Hidden;
            delBtn.Visibility = Visibility.Hidden;

            ViewGlobalVariable.mainWindow = this;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }


        private void dataGridName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Resetovanje selekcije
            if (!(e.OriginalSource is DataGridCell))
            {
                dataGridName.SelectedItem = null; // Ocisti selekciju ako nije kliknuto na celiju
            }
        }
    }
}
