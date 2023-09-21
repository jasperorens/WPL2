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
using ClassLibTeam10.Business.Entities;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassLibTeam10.Business;

namespace WpfTeam10
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }


        private void ExpertWindow_Click(object sender, RoutedEventArgs e)
        {
           /* ExpertWindow expertWindow = new ExpertWindow();
            expertWindow.Show();*/
        }
        private void UserWindow_Click(object sender, RoutedEventArgs e)
        {
            /*UserWindow userWindow = new UserWindow();
            userWindow.Show();*/
        }

        private void SqlWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new SqlWindow(); 
            window.ShowDialog();
        }

        private void Close_Click(object sener, RoutedEventArgs e)
        {
            Close();
        }

        private void StartWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
