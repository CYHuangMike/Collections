using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// CQrCouponFactory 的摘要描述
/// </summary>
public class CQrCouponFactory
{
        int position = 0;
        SqlDataSource x;

    //list是用來內部程式顯示用的.
    public List<CQrCoupon> list = new List<CQrCoupon>();
    private string sql;

    public CQrCouponFactory()
    {
    }

    //連線
    public void getSqlCon()
    {
        x = new SqlDataSource();
        x.ConnectionString = CDictionary.CONN_URL;
    }


    public int getFacilityID(string facilityName)
    {
        int facilityID = 0;
        getSqlCon();
        sql = "";
        sql += " SELECT facilityID FROM Facility ";
        sql += " WHERE name='"+ facilityName + "'";

        x.SelectCommand = sql;
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        int count = 0;
        if (dv.Count > 0)
        {
            foreach (DataRow r in dv.Table.Rows)
            {
                facilityID = int.Parse(r["facilityID"].ToString());
            }
        }
        return facilityID;
    }



    public void loadData(int facilityID)
    {
        getSqlCon();
        sql = "";
        sql += " SELECT c.couponID AS 識別碼, c.title AS 主題, ";
        sql += " c.counts AS 下載次數, f.name AS 設施名稱 ";
        sql += " FROM Coupon AS c INNER JOIN Facility AS f";
        sql += " ON c.facilityID = f.facilityID";

        if (facilityID != 0)
            sql += " WHERE f.facilityID = '" + facilityID + "'";


        x.SelectCommand = sql;
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        int count = 0;
        if (dv.Count > 0)
        {
            foreach (DataRow r in dv.Table.Rows)
            {
                if (r["下載次數"].ToString().ToLower().Equals("null") || r["下載次數"].ToString()=="")
                    count = 0;
                else
                    count = (int)r["下載次數"];


                //欄位資料用list<CQrCoupon>物件化
                list.Add(new CQrCoupon()
                {
                    設施名稱 = r["設施名稱"].ToString(),
                    識別碼 = (int)r["識別碼"],
                    標題 = r["主題"].ToString(),                    
                });
            }
        }
    }


    //使用searchById和getCurrent時機
    //searchById先用活動代碼查詢現在這筆資料位置 position
    //再用getCurrent去得到該筆list物件
    public bool searchById(string couponId)
    {
        int count = 0;
        foreach (CQrCoupon c in list)
        {
            if (c.識別碼.Equals(couponId))
            {
                position = count;
                return true;
            }
            count++;
        }
        return false;
    }

    public CQrCoupon getCurrent()
    {
        return list[position];
    }

    public CQrCoupon[] getAll()
    {
        return list.ToArray();
    }





    //新增
    public void executeInsert(string title, string content, string qrCode, int facilityID)
    {
        getSqlCon();
        sql = "";
        sql += " INSERT INTO Coupon(title, content, qrCode,facilityID) VALUES (";
        sql += " N'"+ title + "',";
        sql += " N'" + content + "',";
        sql += " N'" + qrCode + "',";
        sql += " N'" + facilityID + "')";

        x.InsertCommand = sql;
        x.Insert();
    }


    //修改
    public void executeUpdate(string couponID, string title, string content, int facilityID)
    {
        getSqlCon();
        sql = "";
        sql += " UPDATE Coupon SET ";
        sql += " title=N'" + title + "',";
        sql += " content=N'" + content + "',";        
        sql += " facilityID=N'" + facilityID + "'";
        sql += " WHERE couponID='"+couponID+"'";

        x.UpdateCommand = sql;
        x.Update();
    }


    //刪除
    public void executeDelete(string couponID)
    {
        getSqlCon();
        sql = "";
        sql += " DELETE FROM Coupon ";
        sql += " WHERE couponID='" + couponID + "'";

        x.DeleteCommand = sql;
        x.Delete();
    }

}