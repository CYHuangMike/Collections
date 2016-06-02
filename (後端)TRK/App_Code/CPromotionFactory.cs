using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// CPromotionFactory 的摘要描述
/// </summary>
public class CPromotionFactory
{
    int position = 0;
    SqlDataSource x;

    //list是用來內部程式顯示用的.
    public List<CPromotionView> list = new List<CPromotionView>();
    public string companyID = "";
    

    public CPromotionFactory()
    {
    }
    
    //連線
    public void getSqlCon()
    {
        x = new SqlDataSource();
        x.ConnectionString = CDictionary.CONN_URL;
    }


    //新增 -- 重源
    public void executeInsert(string getsql)
    {
        getSqlCon();
        x.InsertCommand = getsql;
        x.Insert();
    }


    //修改
    public void executeUpdate(string getsql)
    {
        getSqlCon();
        x.UpdateCommand = getsql;
        x.Update();
    }


    //刪除
    public void executeDelete(string getsql)
    {
        getSqlCon();
        x.DeleteCommand = getsql;
        x.Delete();
    }

    //用sql指令
    public void loadDataBySql(string getsql)
    {
        getSqlCon();
        x.SelectCommand = getsql;

        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        if (dv.Count > 0)
        {            
            foreach (DataRow r in dv.Table.Rows)
            {
                //欄位資料list<CPromotionView>物件化
                list.Add(new CPromotionView()
                {
                    活動代碼 = r["活動代碼"].ToString(),
                    審核狀態 = r["審核狀態"].ToString(),
                    主題 = r["活動主題"].ToString(),
                    開始日期 = r["開始日期"].ToString(),
                    結束日期 = r["結束日期"].ToString()
                });
            }
        }
    }

    //外部傳廠商id就可以載入活動資料
    public void loadDataById(string id)
    {
        getSqlCon();
        x.SelectCommand = " SELECT prom.activityID as 活動代碼, approv.status as 審核狀態, prom.title as 活動主題,CONVERT(varchar(100), prom.onShelfDate, 113) as 發布日期, CONVERT (varchar(100), prom.startDate, 111) as 開始日期, CONVERT (varchar(100), prom.endDate, 111) as 結束日期" +
                          " FROM Promotions prom INNER JOIN ApproveStatus approv " +
                          " ON (prom.statusID = approv.statusID) " +
                          " WHERE prom.companyID='" + id + "'" +
                          " order by prom.onShelfDate DESC";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        if (dv.Count > 0)
        {
            foreach (DataRow r in dv.Table.Rows)
            {
                //欄位資料list<CPromotionView>物件化
                list.Add(new CPromotionView()
                {
                    活動代碼 = r["活動代碼"].ToString(),
                    審核狀態 = r["審核狀態"].ToString(),
                    主題 = r["活動主題"].ToString(),
                    發布日期 = r["發布日期"].ToString(),
                    開始日期 = r["開始日期"].ToString(),
                    結束日期 = r["結束日期"].ToString()
                });
            }
        }   
    }


    //使用searchById和getCurrent時機
    //searchById先用活動代碼查詢現在這筆資料位置
    //再用getCurrent去得到該筆list物件
    public bool searchById(string id)
    {
        int count = 0;
        foreach (CPromotionView c in list)
        {
            if (c.活動代碼.Equals(id))
            {
                position = count;
                return true;
            }
            count++;
        }
        return false;
    }
    public CPromotionView getCurrent()
    {
        return list[position];
    }
    public CPromotionView[] getAll()
    {
        return list.ToArray();
    }

}