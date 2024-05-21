using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex12_wpf_toyproject.Models
{
    public class car
    {
        public string addr { get; set; }  // 주소
        public int chargeTp { get; set; }  // charge type
        public int cpId { get; set; }  // 충전기 아이디
        public string cpNm { get; set; } // 충전기 이름
        public int cpStat { get; set; } //스
        //public int cpTp { get; set; }  // 
        //public int csId { get; set; }  // 아이디
        public string csNm { get; set; } // 위치
        public double lat { get; set; } // 위도
        public double longi { get; set; } // 경도
        //public DateTime statUpdatedatetime { get; set; }
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
