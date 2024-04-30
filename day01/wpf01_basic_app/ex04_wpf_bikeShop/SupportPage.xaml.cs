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

namespace ex04_wpf_bikeShop
{
    /// <summary>
    /// SupportPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SupportPage : Page
    {
        public SupportPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var bikeList = new List<Bike>();
            bikeList.Add(new Bike() { Speed = 40, Color = Colors.DarkSlateBlue });
            bikeList.Add(new Bike() { Speed = 30, Color = Colors.DarkRed });
            bikeList.Add(new Bike() { Speed = 50, Color = Colors.Gray });
            bikeList.Add(new Bike() { Speed = 40, Color = Colors.White });
            bikeList.Add(new Bike() { Speed = 55, Color = Colors.Black });

            //ListBox에 데이터 할당
            LsbBikes.DataContext = bikeList;
        }

    
        private void LsbBikes_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var selItem = (LsbBikes.SelectedItem as Bike);
            MessageBox.Show(selItem.Speed.ToString() + " / " + selItem.Color.ToString());
        }
    }
}
