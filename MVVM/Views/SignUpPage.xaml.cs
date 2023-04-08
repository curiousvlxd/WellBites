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
using WellBites.MVVM.ViewModels;

namespace WellBites.Views
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {   

        private UserViewModel _userViewModel;
        private bool isHidden = true;

        public SignUpPage()
        {
            InitializeComponent();
            _userViewModel = new UserViewModel();
        }

        private void BtnGoBack_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnGoNext_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(PbPassword.Password);
            MessageBox.Show(PbPasswordRepeat.Password);
        }

        public bool isFormValid()
        {

            

            return true;
        }

        private void BtnHideunhide_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideUnhidePassword(PbPassword, TbPassword, BtnHidePass, BtnUnHidePass);
        }

        private void BtnHideunhideInRepeatPass_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideUnhidePassword(PbPasswordRepeat, TbPasswordRepeat, BtnHideRepeatPass, BtnUnHideRepeatPass);
        }

            private void HideUnhidePassword(PasswordBox pbPasswordBox, TextBox tbTextBox, Grid btnButtonHide, Grid btnButtonUnhide)
            {
            if (isHidden)
            {
                tbTextBox.Text = pbPasswordBox.Password;
                pbPasswordBox.Visibility = Visibility.Collapsed;
                tbTextBox.Visibility = Visibility.Visible;
                isHidden = false;
                btnButtonUnhide.Visibility = Visibility.Visible;
                btnButtonHide.Visibility = Visibility.Collapsed;
            }
            else
            {
                pbPasswordBox.Password = tbTextBox.Text;
                tbTextBox.Visibility = Visibility.Collapsed;
                pbPasswordBox.Visibility = Visibility.Visible;
                isHidden = true;
                btnButtonUnhide.Visibility = Visibility.Collapsed;
                btnButtonHide.Visibility = Visibility.Visible;
            }
        }
    }
}
