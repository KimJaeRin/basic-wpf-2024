using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex11_Gimhae_FineDust.Helpers
{
    public class Common
    {

        // SQL 연결
        public static readonly string CONNSTRING = "Data Source=localhost;" +
                                                    "Initial Catalog=EMS;" +
                                                    "Persist Security Info=True;" +
                                                    "User ID=ems_user;" +
                                                    "Encrypt=False;" +
                                                    "Password=ems_p@ss";
    }
}
