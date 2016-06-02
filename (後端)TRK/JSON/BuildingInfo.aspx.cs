using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSON_BuildingInfo : System.Web.UI.Page
{
    Json jsn = new Json();

    protected void Page_Load(object sender, EventArgs e)
    {
        string 建築物 = "";
        string 特色內容 = "";
        string 圖片路徑 = "";

        try
        {
            Response.Clear();//清空
            Response.ContentType = "application/json; charset=utf-8";
            //JSON格式製作
            jsn.json建資料 += "{";



            //用 QueryString 傳輸入主鍵
            建築物 = Request.QueryString["id"];
            //如果主鍵為空
            if ("".Equals(建築物) || 建築物 == null)
            {
                jsn.錯誤格式();
                return;
            }



            //如果連線關閉就打開
            if (jsn.連線.State == ConnectionState.Closed)
                jsn.連線.Open();
            //sql查詢執行            
            jsn.sql語法 = "select * from Building where buildingID = @p1";

            jsn.command = new SqlCommand(jsn.sql語法, jsn.連線);
            jsn.command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@p1", 建築物)
                //new SqlParameter("@p2", value2)
            });

            jsn.dr讀取資料 = jsn.command.ExecuteReader();

            //查資料
            if (jsn.dr讀取資料.Read())
            {
                建築物 = jsn.dr讀取資料["buildingID"].ToString();
                特色內容 = jsn.dr讀取資料["content"].ToString();
                圖片路徑 = jsn.dr讀取資料["picPath"].ToString();
            }
            
            //如果主鍵為空
            if ("".Equals(建築物))
                jsn.Message旗標 = "false";
            //點數不為空
            else
            {
                jsn.json建資料 += "\"buildingID\":\"" + 建築物 + "\",";
                jsn.json建資料 += "\"content\":\"" + 特色內容 + "\",";
                jsn.json建資料 += "\"picPath\":\"" + 圖片路徑 + "\",";
                
                jsn.Message旗標 = "true";
            }

            jsn.json建資料 += "\"Message\":\"" + jsn.Message旗標 + "\"";
            jsn.json建資料 += "}";

            jsn.dr讀取資料.Close();

        }
        catch (SqlException err)
        {
            //連線逾時
            jsn.錯誤格式();
        }
        finally
        {
            jsn.連線.Close();
            Response.Write(jsn.json建資料);
            Response.End();
        }

    }
}