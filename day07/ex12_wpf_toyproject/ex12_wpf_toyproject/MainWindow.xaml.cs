using System.Diagnostics;
using System.IO;
using System.Net;
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
using ex11_Gimhae_FineDust.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ex11_Gimhae_FineDust
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        bool isChecked = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    InitComboDateFromDB();
        //}

        //private void InitComboDateFromDB()
        //{
        //    using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(Models.car.GETDATE_QUERY, conn);
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        DataSet dSet = new DataSet();
        //        adapter.Fill(dSet);
        //        List<string> saveDates = new List<string>();

        //        foreach (DataRow row in dSet.Tables[0].Rows)
        //        {
        //            saveDates.Add(Convert.ToString(row["Save_Date"]));
        //        }

        //        CboReqDate.ItemsSource = saveDates;
        //    }
        //}

        // 실시간조회 버튼 클릭
        private async void BtnReqRealtime_Click(object sender, RoutedEventArgs e)
        {
            string data_apiKey = "P21A+gGUvPa0+LjBfqp8qX7x7lzDqYvtvSnXbVxv/idtWmTcL2Udw+1ph1xyRhpevN+KC+CzPWpXhK/CSJOSbQ==";
            string openApiUri = $"http://openapi.kepco.co.kr/service/EvInfoServiceV2/getEvSearchList?serviceKey={data_apiKey}&pageNo=1&numOfRows=10&addr=전라";
            string result = string.Empty;

            // WebRequest, WebResponse 객체
            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(openApiUri);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

                //await this.ShowMessageAsync("결과", result);
                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"OpenAPI 조회오류 {ex.Message}");
            }

            var jsonResult = JObject.Parse(result);
            var status = Convert.ToInt32(jsonResult["status"]);

            if (status == 200)
            {
                var data = jsonResult["data"];
                var jsonArray = data as JArray; // json자체에서 []안에 들어간 배열데이터만 JArray 변환가능

                var dustSensors = new List<car>();
                foreach (var item in jsonArray)
                {
                    dustSensors.Add(new car()
                    {
                        Addr = Convert.ToString(item["addr"]),
                        ChargeTp = Convert.ToInt32(item["chargetp"]),
                        CpId = Convert.ToInt32(item["cpid"]),
                        CpNm = Convert.ToString(item["cpnm"]),
                        CpStat = Convert.ToInt32(item["xpstat"]),
                        CpTp = Convert.ToInt32(item["cptp"]),
                        CsId = Convert.ToInt32(item["csid"]),
                        CsNm = Convert.ToString(item["csnm"]),
                        Lat = Convert.ToDouble(item["lat"]),
                        StatUpdateDatetime = Convert.ToDateTime(item["timestamp"]),
                    });
                }

                this.DataContext = dustSensors;
                StsResult.Content = $"OpenAPI {dustSensors.Count}건 조회완료!";
            }
        }

        //private async void BtnSaveData_Click(object sender, RoutedEventArgs e)
        //{
        //    if (GrdResult.Items.Count == 0)
        //    {
        //        await this.ShowMessageAsync("저장오류", "실시간 조회후 저장하십시오.");
        //        return;
        //    }

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
        //        {
        //            conn.Open();

        //            var insRes = 0;
        //            foreach (car item in GrdResult.Items)
        //            {
        //                SqlCommand cmd = new SqlCommand(Models.car.INSERT_QUERY, conn);
        //                cmd.Parameters.AddWithValue("@Dev_id", item.Dev_id);
        //                cmd.Parameters.AddWithValue("@Name", item.Name);
        //                cmd.Parameters.AddWithValue("@Loc", item.Loc);
        //                cmd.Parameters.AddWithValue("@Coordx", item.Coordx);
        //                cmd.Parameters.AddWithValue("@Coordy", item.Coordy);
        //                cmd.Parameters.AddWithValue("@Ison", item.Ison);
        //                cmd.Parameters.AddWithValue("@Pm10_after", item.Pm10_after);
        //                cmd.Parameters.AddWithValue("@Pm25_after", item.Pm25_after);
        //                cmd.Parameters.AddWithValue("@State", item.State);
        //                cmd.Parameters.AddWithValue("@Timestamp", item.Timestamp);
        //                cmd.Parameters.AddWithValue("@Company_id", item.Company_id);
        //                cmd.Parameters.AddWithValue("@Company_name", item.Company_Name);

        //                insRes += cmd.ExecuteNonQuery();
        //            }

        //            if (insRes > 0)
        //            {
        //                await this.ShowMessageAsync("저장", "DB저장성공!");
        //                StsResult.Content = $"DB저장 {insRes}건 성공!";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await this.ShowMessageAsync("저장오류", $"저장오류 {ex.Message}");
        //    }

        //    InitComboDateFromDB();
        //}

        // 수업 이후 추가내용. 필요시 구현할 것
        //private void CboReqDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (CboReqDate.SelectedValue != null)
        //    {
        //        using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand(Models.car.SELECT_QUERY, conn);
        //            cmd.Parameters.AddWithValue("@Timestamp", CboReqDate.SelectedValue.ToString());
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataSet dSet = new DataSet();
        //            adapter.Fill(dSet, "car");
        //            var dustSensors = new List<car>();

        //            foreach (DataRow row in dSet.Tables["car"].Rows)
        //            {
        //                dustSensors.Add(new car
        //                {
        //                    Id = Convert.ToInt32(row["Id"]),
        //                    Dev_id = Convert.ToString(row["Dev_id"]),
        //                    Name = Convert.ToString(row["Name"]),
        //                    Loc = Convert.ToString(row["Loc"]),
        //                    Coordx = Convert.ToDouble(row["Coordx"]),
        //                    Coordy = Convert.ToDouble(row["Coordy"]),
        //                    Ison = Convert.ToBoolean(row["Ison"]),
        //                    Pm10_after = Convert.ToInt32(row["Pm10_after"]),
        //                    Pm25_after = Convert.ToInt32(row["Pm25_after"]),
        //                    State = Convert.ToInt32(row["State"]),
        //                    Timestamp = Convert.ToDateTime(row["Timestamp"]),
        //                    Company_id = Convert.ToString(row["Company_id"]),
        //                    Company_Name = Convert.ToString(row["Company_name"])
        //                });
        //            }

        //            this.DataContext = dustSensors;
        //            StsResult.Content = $"DB {dustSensors.Count}건 조회완료";
        //        }
        //    }
        //    else
        //    {
        //        this.DataContext = null;
        //        StsResult.Content = $"DB 조회클리어";
        //    }
        //}

        //private void GrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var curItem = GrdResult.SelectedItem as car;

        //    var mapWindow = new MapWindow(curItem.Coordy, curItem.Coordx);
        //    mapWindow.Owner = this;
        //    mapWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        //    mapWindow.ShowDialog();
        //}
    }
}