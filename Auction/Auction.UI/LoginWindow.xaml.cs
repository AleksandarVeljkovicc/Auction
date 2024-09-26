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
using System.Windows.Shapes;

namespace Auction.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void pwBox_GotFocus(object sender, RoutedEventArgs e)
        {
            pwBox.Password = "";
            pwBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBox.Text = "";
            txtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindowViewModel loginViewModel = new LoginWindowViewModel(txtBox.Text, pwBox.Password);
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.DataContext = loginViewModel;

            if (loginViewModel.ErrorMessage == "")
            {

                ViewGlobalVariable.mainWindow.fullName.Content = loginViewModel.User.FullName;
                if (loginViewModel.User.IsAdmin == false)
                {
                    ViewGlobalVariable.mainWindow.offerBtn.Visibility = Visibility.Visible;

                }
                else if (loginViewModel.User.IsAdmin == true)
                {
                    ViewGlobalVariable.mainWindow.newBtn.Visibility = Visibility.Visible;
                    ViewGlobalVariable.mainWindow.delBtn.Visibility = Visibility.Visible;

                }

                Close();
                ViewGlobalVariable.mainWindow.Visibility = Visibility.Visible;
            }
            else
            {
                msgLabel.Text = loginViewModel.ErrorMessage;
                msgLabel.Foreground = new SolidColorBrush(Colors.Red);
                msgLabel.FontWeight = FontWeights.Bold;
            }
        }

    }
}
