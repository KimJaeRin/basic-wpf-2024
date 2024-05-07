using Caliburn.Micro;
using ex07_EmployeeMngApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ex07_EmployeeMngApp.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        // 멤버변수
        private int id;
        private string empName;
        private decimal salary;
        private string deptName;
        private string addr;
        private BindableCollection<Employees> listEmployees;

        private Employees selectedEmployee;

        public Employees SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                // 데이터를 TextBox들에 전달.

                if (selectedEmployee != null) 
                {
                    ID = value.ID;
                    EmpName = value.EmpName;
                    Salary = value.Salary;
                    DeptName = value.DeptName;
                    Addr = value.Addr;

                    NotifyOfPropertyChange(() => SelectedEmployee);  //View에 데이터가 표시되려면 필수
                    NotifyOfPropertyChange(() => ID); 
                    NotifyOfPropertyChange(() => EmpName); 
                    NotifyOfPropertyChange(() => Salary); 
                    NotifyOfPropertyChange(() => DeptName);
                    NotifyOfPropertyChange(() => Addr);
                }
            }
        }

        //속성
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyOfPropertyChange(() => ID);
                NotifyOfPropertyChange(() => CanDelEmployee); // 삭제여부 속성도 변경했다 공지

            }
        }
        public string EmpName
        {
            get { return empName; }
            set
            {
                empName = value;
                NotifyOfPropertyChange(() => EmpName);
                NotifyOfPropertyChange(() => CanSaveEmployee);
                

            }
        }
        public decimal Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                NotifyOfPropertyChange(() => Salary);
                NotifyOfPropertyChange(() => CanSaveEmployee);

            }
        }
        public string DeptName
        {
            get { return deptName; }
            set
            {
                deptName = value;
                NotifyOfPropertyChange(() => DeptName);
                NotifyOfPropertyChange(() => CanSaveEmployee);

            }
        }
        public string Addr
        {
            get { return addr; }
            set
            {
                addr = value;
                NotifyOfPropertyChange(() => Addr);
                NotifyOfPropertyChange(() => CanSaveEmployee);

            }
        }

        public BindableCollection<Employees> ListEmployees
        {
            get => listEmployees;
            set
            {
                listEmployees = value;
                // 값이 변경된것을 시스템에 알려줘야함
                NotifyOfPropertyChange(() => listEmployees);
            }
        }

        public MainViewModel()
        {
            DisplayName = "직원관리 시스템";

            //조회실행
            GetEmployees();
        }

        // 저장버튼 활성화 여부판단
        public bool CanSaveEmployee
        {
            get
            {
                if (string.IsNullOrEmpty(EmpName) || Salary == 0 || string.IsNullOrEmpty(DeptName))
                    return false;
                else
                    return true;

            }
        }

        // Caliburn.Micro가 Xaml의 버튼 x:Name과 동일한 이름의 메서드로 매핑
        public void SaveEmployee() 
        {
           // MessageBox.Show("저장버튼 동작");
           using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
           {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                if (ID == 0) //INSERT
                    cmd.CommandText = Models.Employees.INSERT_QUERY;
                else // UPDATE
                    cmd.CommandText = Models.Employees.UPDATE_QUERY;
            
                SqlParameter prmEmpName = new SqlParameter("@EmpName", EmpName);
                cmd.Parameters.Add(prmEmpName);
                SqlParameter prmSalary = new SqlParameter("@Salary", Salary);
                cmd.Parameters.Add(prmSalary);
                SqlParameter prmDeptName = new SqlParameter("@DeptName", DeptName);
                cmd.Parameters.Add(prmDeptName);
                SqlParameter prmAddr = new SqlParameter("@Addr", Addr ?? (object)DBNull.Value); // 주소가 빈 값일때 컬럼에 null값을 입력
                cmd.Parameters.Add(prmAddr);

                if (ID != 0) // 업데이트면 ID 파라미터가 필요
                {
                    SqlParameter prmID = new SqlParameter("@ID", ID);
                    cmd.Parameters.Add(prmID);
                }

                var result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("저장성공");
                }
                else
                {
                    MessageBox.Show("저장실패");
                }
                GetEmployees(); // 그리드 재조회
                NewEmployee(); // 모든 입력컨트롤 초기화
           }

        }

        public void GetEmployees()
        {
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.Employees.SELECT_QUERY, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ListEmployees = new BindableCollection<Employees>();

                while (reader.Read()) 
                {
                    ListEmployees.Add(new Employees()
                    {
                        ID = (int)reader["ID"],
                        EmpName = reader["EmpName"].ToString(),
                        Salary = (decimal)reader["Salary"],
                        DeptName = reader["DeptName"].ToString(),
                        Addr = reader["Addr"].ToString()
                    });
                }
            }
        }

        // 삭제 버튼 누를 수 있는지 여부 확인
        public bool CanDelEmployee
        {
            get { return ID != 0; }   // TextBox ID 속성의 값이 0이면 false, 0이 아니면 true
        }


        public void DelEmployee()
        {
            if (ID == 0)
            {
                MessageBox.Show("삭제불가");
                return;
            }

            if (MessageBox.Show("삭제하시겠습니까", "삭제여부", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.No) 
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.Employees.DELETE_QUERY, conn);
                SqlParameter prmID = new SqlParameter("@ID", ID);
                cmd.Parameters.Add(prmID);

                var res = cmd.ExecuteNonQuery();
                if (res >= 0) 
                {
                    MessageBox.Show("삭제성공");
                }
                else
                {
                    MessageBox.Show("삭제실패");
                }
                GetEmployees();
                NewEmployee();
            }
        }
        public void NewEmployee()
        {
            ID = 0;
            Salary = 0;
            EmpName = DeptName = Addr = "";
        }
    }
}
