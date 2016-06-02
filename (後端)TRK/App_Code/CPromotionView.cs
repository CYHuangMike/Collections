using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// CPromotionView 的摘要描述
/// </summary>
public class CPromotionView
{
    public string id;

    public CPromotionView()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    /*
       activityID int, --活動編號-----------綁一起的, 要系統幫忙取出編號, 那就要先查詢最後一號 編號, 在GridView上的要幫忙做
       companyID nvarchar(20), --廠商ID-----
       title nvarchar(20), --標題(可看)
       classifyID int, --分類代碼------------固定
       content nvarchar(MAX), --內容(可看)
       picPath nvarchar(20), --圖片路徑
       startDate DateTime, --開始日期(可看)
       endDate DateTime, --結束日期(可看)
       statusID int --送審代碼 (要可看狀態)
   */

    public string 審核狀態 { get; set; }
    public string 活動代碼 { get; set; }
    public string 主題 { get; set; }
    public string 發布日期 { get; set; }//0523
    public string 開始日期 { get; set; }
    public string 結束日期 { get; set; }
}