using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 코드 비하인드에서 Source 속석에 피이지를 넣을때는 UriKind.RelativeOrAbsolute 무조건 추가
            // 
            MainFrame.Source = new Uri("/MenuPage.Xaml", UriKind.RelativeOrAbsolute);
        }
    }
}