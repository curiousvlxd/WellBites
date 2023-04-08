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

namespace WellBites.Views
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private bool isHidden = true;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnForgotPassword_OnClick(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnSignIn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnSignUp_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).FrameMain.Content = new SignUpPage();
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
    }
}
