using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {   

        public UserViewModel _userViewModel;
        private bool isHiddenPass = true;
        private bool isHiddenPassRepeat = true;
        private readonly Regex dateRegex = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");
        private readonly Regex heightRegex = new Regex(@"^[1-9][0-9]{2}$");
        private readonly Regex weightRegex = new Regex(@"^[1-9][0-9]{1,2}$");
        private readonly Regex usernameRegex = new Regex(@"^[a-zA-Z0-9]{3,20}$");
        private readonly Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

        public SignUpPage(UserViewModel userViewModel)
        {
            InitializeComponent();
            _userViewModel = userViewModel;
        }

        private void BtnGoBack_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnGoNext_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsFormValid()) return;

            DateTime selectedDate = dpDateOfBirth.SelectedDate.Value;

            _userViewModel.User.Username = TbUsername.Text;
            _userViewModel.User.Email = TbEmail.Text;
            _userViewModel.User.CreatePasswordHash(TbPassword.Text);
            _userViewModel.User.DateOfBirth = selectedDate;
            _userViewModel.User.Height = int.Parse(tbHeight.Text);
            _userViewModel.User.Weight = int.Parse(tbWeight.Text);
            _userViewModel.User.Activity = (Activity)cbActivity.SelectedItem;
            _userViewModel.User.Sex = (Sex)cbSex.SelectedItem;
            _userViewModel.UserManagerService.AddUser(_userViewModel.User);
            ((MainWindow)Application.Current.MainWindow).frame.Content = new DashboardPage(_userViewModel);
        }

        public bool IsFormValid()
        {

            if (string.IsNullOrEmpty(TbUsername.Text))
            {
                return false;
            }

            if (string.IsNullOrEmpty(TbEmail.Text))
            {
                return false;
            }

            if (string.IsNullOrEmpty(PbPassword.Password) || string.IsNullOrEmpty(PbPasswordRepeat.Password))
            {
                return false;
            }

            if (PbPassword.Password != PbPasswordRepeat.Password)
            {
                return false;
            }

            if (string.IsNullOrEmpty(dpDateOfBirth.Text))
            {
                return false;
            }

            if (cbActivity.SelectedItem == null)
            {
                return false;
            }

            if (cbSex.SelectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void BtnHideunhide_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideUnhidePassword(PbPassword, TbPassword, BtnHidePass, BtnUnHidePass, ref isHiddenPass);
        }

        private void BtnHideunhideInRepeatPass_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            HideUnhidePassword(PbPasswordRepeat, TbPasswordRepeat, BtnHideRepeatPass, BtnUnHideRepeatPass, ref isHiddenPassRepeat);
        }

            private void HideUnhidePassword(PasswordBox pbPasswordBox, TextBox tbTextBox, Grid btnButtonHide, Grid btnButtonUnhide, ref bool isHidden)
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void WeightValidation(object sender, TextCompositionEventArgs e)
        {
            ValueValidation(weightRegex, e);
        }

        private void ValueValidation(Regex regex, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DateValidation(object sender, TextCompositionEventArgs e)
        {
            ValueValidation(dateRegex, e);
        }

        private void PasswordValidation(object sender, TextCompositionEventArgs e)
        {
            ValueValidation(passwordRegex, e);
        }

        private void EmailValidation(object sender, TextCompositionEventArgs e)
        {
            ValueValidation(emailRegex, e);
        }

        private void UsernameValidation(object sender, TextCompositionEventArgs e)
        {
            ValueValidation(usernameRegex, e);
        }

        private void HeightValidation(object sender, TextCompositionEventArgs e)
        {
            ValueValidation(heightRegex, e);
        }
    }
}
