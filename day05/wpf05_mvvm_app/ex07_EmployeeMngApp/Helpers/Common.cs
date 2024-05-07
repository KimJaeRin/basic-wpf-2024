using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex07_EmployeeMngApp.Helpers
{
    public class Common
    {
        public static readonly string CONNSTRING = "Data Source=127.0.0.1;" +
            "Initial Catalog=EMS;" + "Persist Security Info=True;User ID=ems_user;"
            +"Password=ems_p@ss;Encrypt=False";


        // Metro방식의 다이얼로그 적용을 위해선 반드시 필요
        public static IDialogCoordinator DialogCoordinator { get; set; }
    }
}
