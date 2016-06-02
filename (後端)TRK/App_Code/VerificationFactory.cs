using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// VerificationFactory 的摘要描述
/// </summary>
public class VerificationFactory
{
    SqlDataSource x;

    public VerificationFactory() { }

    //list :顯示內部介面內容.
    public List<VerificationView> list = new List<VerificationView>();//全部顯示
    public List<NewsDetailView> listNEW = new List<NewsDetailView>();//詳細內容

    //Connect
    public void getSqlCon()
    {
        x = new SqlDataSource();
        x.ConnectionString = @"Data Source=iii0.database.windows.net;Initial Catalog=prjTRK;Persist Security Info=True;User ID=iii;Password=P@ssw0rd";

    }

    //Admin edit verification of admin
    public void loadDataAcminById()
    {
        getSqlCon();
        x.SelectCommand = " SELECT prom.activityID as 活動代碼, prom.title as 活動主題, approv.status as 審核狀態, CONVERT(varchar(100), prom.onShelfDate, 113) as 發布日期, CONVERT (varchar(100), prom.startDate, 111) as 開始日期, CONVERT (varchar(100), prom.endDate, 111) as 結束日期" +
                          " FROM Promotions prom INNER JOIN ApproveStatus approv " +
                          " ON (prom.statusID = approv.statusID) where prom.companyID='C0001'"+
                          " order by prom.onShelfDate DESC";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;
        if (dv.Count >= 01)
        {
            foreach (DataRow r in dv.Table.Rows)
            {
                //欄位資料list<VerificationView>物件化 
                list.Add(new VerificationView()
                {
                    活動代碼 = r["活動代碼"].ToString(),
                    主題 = r["活動主題"].ToString(),
                    發布日期 = r["發布日期"].ToString(),
                    開始日期 = r["開始日期"].ToString(),
                    結束日期 = r["結束日期"].ToString(),
                    審核狀態 = r["審核狀態"].ToString()
                });
            }
        }

    }

    //全部最新消息
    public void loadDataById()
    {
        getSqlCon();
        x.SelectCommand = " SELECT prom.activityID as 活動代碼, prom.title as 活動主題, approv.status as 審核狀態, CONVERT(varchar(100), prom.onShelfDate, 113) as 發布日期, CONVERT (varchar(100), prom.startDate, 111) as 開始日期, CONVERT (varchar(100), prom.endDate, 111) as 結束日期" +
                          " FROM Promotions prom INNER JOIN ApproveStatus approv " +
                          " ON (prom.statusID = approv.statusID) "+
                          " order by prom.onShelfDate DESC";

        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;
        if (dv.Count > 02)
        {
            foreach (DataRow r in dv.Table.Rows)
            {
                //欄位資料list<VerificationView>物件化
                list.Add(new VerificationView()
                {
                    活動代碼 = r["活動代碼"].ToString(),
                    主題 = r["活動主題"].ToString(),
                    發布日期 = r["發布日期"].ToString(),
                    開始日期 = r["開始日期"].ToString(),
                    結束日期 = r["結束日期"].ToString(),
                    審核狀態 = r["審核狀態"].ToString()
                });
            }
        }

    }


    // load 最新消息詳細資料
    public void loadNewsDetail(string newsID)
    {
        getSqlCon();
        x.SelectCommand = "SELECT prom.activityID as 活動代碼, prom.title as 活動主題,Class.classifyID as 公告分類," +
           " prom.picPath as 圖片,prom.content as 內容,prom.comment as 備註,approv.status as 審核狀態, CONVERT(varchar(100)," +
           //" CONVERT(varchar(100), prom.onShelfDate, 113) as 發布日期," +//0521
           " prom.startDate, 111) as 開始日期, CONVERT(varchar(100), prom.endDate, 111) as 結束日期,com.name as 廠商名稱" +
           " FROM Promotions prom INNER JOIN ApproveStatus approv ON (prom.statusID = approv.statusID)" +
           " inner join  Company com on(prom.companyID = com.companyID)" +
           " inner join Classfication Class on(prom.classifyID = Class.classifyID)" +
           " Where activityID ='" + newsID + "'";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        if (dv.Count > 0)
        {
            foreach (DataRow r in dv.Table.Rows)
            {
                //欄位資料list<VerificationView>物件化
                listNEW.Add(new NewsDetailView()
                {
                    活動主題 = r["活動主題"].ToString(),
                    發布單位 = r["廠商名稱"].ToString(),
                    公告分類 = r["公告分類"].ToString(),
                    審核狀態 = r["審核狀態"].ToString(),
                    //發布日期 = r["發布日期"].ToString(),//0521
                    開始日期 = r["開始日期"].ToString(),
                    結束日期 = r["結束日期"].ToString(),
                    活動代碼 = r["活動代碼"].ToString(),
                    圖片 = r["圖片"].ToString(),
                    備註 = r["備註"].ToString(),
                    內容 = r["內容"].ToString(),

                });
            }
        }
    }

    //UPDATE statusNews
    public void updateNewsDetailTrue(string newsID, string comment)
    {
        // string datetime = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss");

        string statusID = "1";

        getSqlCon();
        x.UpdateCommand = "UPDATE Promotions set" +
        " statusID=N'" + statusID + "'," +
        " comment=N'" + comment + "'," +
        " onShelfDate = convert(varchar, DATEADD(hour, 8, GETUTCDATE()), 113)" +
        " WHERE activityID ='" + newsID + "'";
        x.Update();

    }
    public void updateNewsDetailFalse(string newsID, string comment)
    {
        string statusID = "4";
        getSqlCon();


        x.UpdateCommand = "UPDATE Promotions set" +
                " statusID=N'" + statusID + "'," +
                " comment=N'" + comment + "'," +
                " onShelfDate = convert(varchar, DATEADD(hour, 8, GETUTCDATE()), 113)" +
                " WHERE activityID ='" + newsID + "'";
        x.Update();
    }

    //刪除
    public void executeDelete(string getsql)
    {
        getSqlCon();
        x.DeleteCommand = getsql;
        x.Delete();
    }

}