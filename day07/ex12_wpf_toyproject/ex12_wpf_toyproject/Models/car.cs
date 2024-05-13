using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex11_Gimhae_FineDust.Models
{
    public class car
    {
        public string Addr { get; set; }  // 추가생성, DB에 넣을때 사용할 값
        public int ChargeTp { get; set; }  // 디바이스아이디
        public int CpId { get; set; }  // 디바이스 이름
        public string CpNm { get; set; } // 디바이스 위치주소
        public int CpStat { get; set; } //경도
        public int CpTp { get; set; }  // 위도
        public int CsId { get; set; }  // 디바이스 온 여부
        public string CsNm { get; set; } // PM 10mm 미세먼지 측정값
        public double Lat { get; set; } // PM 2.5mm 초미세먼지 측정값
        public double LongI { get; set; } // 상태
        public DateTime StatUpdateDatetime { get; set; }
    }
}   // 측정일시

        ////  public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[car]
        //                                                             ([Addr]
        //                                                             ,[ChargeTp]
        //                                                             ,[CpId]
        //                                                             ,[Coordx]
        //                                                             ,[Coordy]
        //                                                             ,[Ison]
        //                                                             ,[Pm10_after]
        //                                                             ,[Pm25_after]
        //                                                             ,[State]
        //                                                             ,[Timestamp]
        //                                                             ,[Company_id]
        //                                                             ,[Company_name])
        //                                                       VALUES
        //                                                             (@Dev_id
        //                                                             ,@Name
        //                                                             ,@Loc
        //                                                             ,@Coordx
        //                                                             ,@Coordy
        //                                                             ,@Ison
        //                                                             ,@Pm10_after
        //                                                             ,@Pm25_after
        //                                                             ,@State
        //                                                             ,@Timestamp
        //                                                             ,@Company_id
        //                                                             ,@Company_name)";

//        public static readonly string SELECT_QUERY = @"SELECT [car]
//                                                               ,[Addr]
//                                                               ,[ChargeTp]
//                                                               ,[CpId]
//                                                               ,[CpNm]
//                                                               ,[CpStat]
//                                                               ,[CpTp]
//                                                               ,[CsId]
//                                                               ,[CsNm]
//                                                               ,[Lat]
//                                                               ,[LongI]
//                                                               ,[StatUpdateDatetime]
//                                                           FROM [dbo].[car]
//                                                        WHERE CONVERT(CHAR(10), GETDATE(), 23) = @Timestamp";

//        public static readonly string GETDATE_QUERY = @"SELECT CONVERT(CHAR(10), Timestamp, 23) AS Save_Date
//                                                         FROM [dbo].[car]
//                                                        GROUP BY CONVERT(CHAR(10), Timestamp, 23)";
//    }
//}
