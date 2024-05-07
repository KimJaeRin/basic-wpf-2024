using ex07_EmployeeMngApp.Helpers;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ex07_EmployeeMngApp.Views
{
    /// <summary>
    /// MainViews.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : MetroWindow
    {
        public MainView()
        {
            InitializeComponent();

            Common.DialogCoordinator = DialogCoordinator.Instance;
            this.DataContext = Common.DialogCoordinator;
        }
    }
}
