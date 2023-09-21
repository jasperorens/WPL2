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

namespace WpfTeam06
{
    /// <summary>
    /// Interaction logic for ExpertWindow.xaml
    /// </summary>
    public partial class ExpertWindow : Window
    {
        public ExpertWindow()   
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public List<Expert> DemoList { get; set; }
        private void LoadData()
        {
            DemoList = new List<Expert>();
            {
                new Expert("Bakker", "Stokbrood", 1, "Trade", 12.3f, 0);
                new Expert("Kaasmaker", "Mozarella", 1, "Trade", 15f, 0);
                new Expert("Architect", "Skyscrapers", 1, "Business", 20f, 0);
            }
            

            DgdExpert.ItemsSource = null;
            DgdExpert.ItemsSource = DemoList;
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            StackNewExpert.Visibility = Visibility.Hidden;
        }
        private void MnuNewExpert_Click(object sender, RoutedEventArgs e)
        {
            StackNewExpert.Visibility = Visibility.Visible;
        }

        private void BtnAddExpert_Click(object sender, RoutedEventArgs e)
        {
            if (TxtProfession.Text.Length > 0 && TxtSpecialization.Text.Length > 0 && TxtCategory.Text.Length > 0 && TxtPrice.Text.Length > 0 && TxtID.Text.Length > 0 && TxtActive.Text.Length > 0 )
            {
                //Expert professional = new Expert();
                //Experts.Add(TxtProfession.Text, TxtSpecialization.Text, TxtCategory.Text, Convert.ToSingle(TxtPrice.Text), Convert.ToInt32(TxtID.Text), Convert.ToInt32(TxtActive.Text));
                LoadData(); StackNewExpert.Visibility = Visibility.Hidden;
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
