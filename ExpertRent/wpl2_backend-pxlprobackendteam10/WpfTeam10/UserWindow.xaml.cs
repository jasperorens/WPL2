using ClassLibTeam10.Business.Entities;
using ClassLibTeam10.Business;
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
using ClassLibTeam10.Data;

namespace WpfTeam06
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public List<User> DemoList { get; set; }
        private void LoadData()
        {
            DemoList = new List<User>();
            {
                new User("JosBakker@FakeEmail.com", "Broodjes123", "MeesterBakker", "Bakker", "Jos","+324545454", "Belgium", "Broodstraat", "8", "2400", "Limburg");
               
            }
            DgdUser.ItemsSource = null;
            DgdUser.ItemsSource = DemoList;
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            StackNewUser.Visibility = Visibility.Hidden;
        }
        private void MnuNewUser_Click(object sender, RoutedEventArgs e)
        {
            StackNewUser.Visibility = Visibility.Visible;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (TxtEmail.Text.Length > 0 && TxtPassword.Text.Length > 0 && TxtUsername.Text.Length > 0 && TxtName.Text.Length > 0 && TxtFirstname.Text.Length > 0 && TxtTelephone.Text.Length > 0 && TxtCountry.Text.Length > 0 && TxtStreet.Text.Length > 0 && TxtStreetnr.Text.Length > 0 && TxtPostalcode.Text.Length > 0 && TxtProvince.Text.Length > 0)
            {
                User gebruiker = new User(TxtEmail.Text, TxtPassword.Text, TxtUsername.Text, TxtName.Text, TxtFirstname.Text, TxtTelephone.Text, TxtCountry.Text, TxtStreet.Text, TxtStreetnr.Text, TxtPostalcode.Text, TxtProvince.Text);
                UserData userdata = new UserData();
                userdata.RegisterUserToDB(gebruiker);
                LoadData(); StackNewUser.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Incorrect data!");
            }
        }

        private void MnuClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
