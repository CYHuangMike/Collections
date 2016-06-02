using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSON_Facility : System.Web.UI.Page
{
    Json jsn = new Json();
    protected void Page_Load(object sender, EventArgs e)
    {
        string facilityID = "";
        string name = ""; //設施名稱
        string content = ""; //特色內容
        string pic = ""; //圖片檔名

        try
        {
            Response.Clear();//清空
            Response.ContentType = "application/json; charset=utf-8";
            //JSON格式製作
            jsn.json建資料 += "{";



            //用 QueryString 傳輸入主鍵
            facilityID = Request.QueryString["id"];
            //如果主鍵為空
            if ("".Equals(facilityID) || facilityID == null)
            {
                jsn.錯誤格式();
                return;
            }



            //如果連線關閉就打開
            if (jsn.連線.State == ConnectionState.Closed)
                jsn.連線.Open();
            //sql查詢執行            
            jsn.sql語法 = "SELECT * FROM Facility where facilityID = @p1";

            jsn.command = new SqlCommand(jsn.sql語法, jsn.連線);
            jsn.command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@p1", facilityID)
                //new SqlParameter("@p2", value2)
            });

            jsn.dr讀取資料 = jsn.command.ExecuteReader();

            //查資料
            if (jsn.dr讀取資料.Read())
            {
                facilityID = jsn.dr讀取資料["facilityID"].ToString();
                name = jsn.dr讀取資料["name"].ToString();
                content = jsn.dr讀取資料["content"].ToString();
                pic = jsn.dr讀取資料["pic"].ToString();
            }

            //如果主鍵為空
            if ("".Equals(facilityID))
                jsn.Message旗標 = "false";
            //點數不為空
            else
            {
                jsn.json建資料 += "\"facilityID\":\"" + facilityID + "\",";
                jsn.json建資料 += "\"name\":\"" + name + "\",";
                jsn.json建資料 += "\"content\":\"" + content + "\",";
                jsn.json建資料 += "\"pic\":\"" + pic + "\",";

                jsn.Message旗標 = "true";
            }

            jsn.json建資料 += "\"Message\":\"" + jsn.Message旗標 + "\"";
            jsn.json建資料 += "}";

            jsn.dr讀取資料.Close();

        }
        catch (SqlException)
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