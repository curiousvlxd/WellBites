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
using WellBites.Models;
using WellBites.MVVM.ViewModels;
using WellBites.MVVM.Views;

namespace WellBites.Views
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {   
        private UserViewModel _userViewModel;
        private bool isHidden = true;
        public AuthPage(UserViewModel userViewModel)
        {
            InitializeComponent();
            _userViewModel = userViewModel;
        }

        private void BtnForgotPassword_OnClick(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnSignIn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_userViewModel.UserManagerService.AuthenticateUser(TbUsername.Text, PbPassword.Password))
            {
                ((MainWindow)Application.Current.MainWindow).frame.Content = new DashboardPage(_userViewModel);
            }
        }

        private void BtnSignUp_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).frame.Content = new SignUpPage(_userViewModel);
        }

        private void BtnHideunhide_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isHidden)
            {
                TbPassword.Text = PbPassword.Password;
                PbPassword.Visibility = Visibility.Collapsed;
                TbPassword.Visibility = Visibility.Visible;
                isHidden = false;
                BtnUnHidePass.Visibility = Visibility.Visible;
                BtnHidePass.Visibility = Visibility.Collapsed;
            }
            else
            {
                PbPassword.Password = TbPassword.Text;
                TbPassword.Visibility = Visibility.Collapsed;
                PbPassword.Visibility = Visibility.Visible;
                isHidden = true;
                BtnUnHidePass.Visibility = Visibility.Collapsed;
                BtnHidePass.Visibility = Visibility.Visible;
            }
        }

        private void PbPassword_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TbPassword.Text = PbPassword.Password;
        }

        private void TbPassword_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            PbPassword.Password = TbPassword.Text;
        }
    }
}
