using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSON_NewsJSON : System.Web.UI.Page
{
    Json json = new Json();

    protected void Page_Load(object sender, EventArgs e)
    {
        int NewClassID1 = Convert.ToInt32(Request.QueryString["NewsClassID1"]);
        int NewClassID2 = Convert.ToInt32(Request.QueryString["NewsClassID2"]);


        try
        {
            Response.Clear();
            Response.ContentType = "application/json ;charset=utf-8";
            json.json加資料.Append("{\"data\":[");

            //當連線關閉時，開啟Json
            if (json.連線.State == ConnectionState.Closed)
                json.連線.Open();

            //sql
            json.sql語法 = " select  Top (15) prom.activityID as NID, com.name as NCompanyName, prom.title as NTitle, prom.content as NContent," +
                          " CONVERT(varchar(100), prom.startDate, 111) as NStartDate," +
                          " CONVERT(varchar(100), prom.endDate, 111) as NEndDate, class.item as NClass," +
                          " prom.picPath as NpicPath from Promotions prom inner join Company com on prom.companyID=com.companyID" +
                          " inner join Classfication class on prom.classifyID=class.classifyID" +
                          " WHERE prom.statusID = 1 and(class.classifyID = " + NewClassID1 + " or class.classifyID= " + NewClassID2 + ") order by prom.onShelfDate desc";


           /* json.sql語法 = " select prom.activityID as NID, com.name as NCompanyName, prom.title as NTitle, prom.content as NContent," +
                           " CONVERT(varchar(100), prom.startDate, 111) as NStartDate," +
                           " CONVERT(varchar(100), prom.endDate, 111) as NEndDate, class.item as NClass," +
                           " prom.picPath as NpicPath from Promotions prom inner join Company com on prom.companyID=com.companyID" +
                           " inner join Classfication class on prom.classifyID=class.classifyID" +
                           " WHERE prom.statusID = 1 and (class.classifyID = " + NewClassID1 + " or class.classifyID= " + NewClassID2 + ")";
*/
            SqlCommand cmd = new SqlCommand(json.sql語法, json.連線);
            json.dr讀取資料 = cmd.ExecuteReader();


            while (json.dr讀取資料.Read())
            {

                json.json加資料.Append("{");

                json.json加資料.Append("\"NID\":\"" + json.dr讀取資料["NID"].ToString() + "\",");
                json.json加資料.Append("\"NCompanyName\":\"" + json.dr讀取資料["NCompanyName"].ToString() + "\",");
                json.json加資料.Append("\"NTitle\":\"" + json.dr讀取資料["NTitle"].ToString() + "\",");
                json.json加資料.Append("\"NContent\":\"" + json.dr讀取資料["NContent"].ToString() + "\",");
                json.json加資料.Append("\"NStartDate\":\"" + json.dr讀取資料["NStartDate"].ToString() + "\",");
                json.json加資料.Append("\"NEndDate\":\"" + json.dr讀取資料["NEndDate"].ToString() + "\",");
                json.json加資料.Append("\"NClass\":\"" + json.dr讀取資料["NClass"].ToString() + "\",");
                json.json加資料.Append("\"NpicPath\":\"" + json.dr讀取資料["NpicPath"].ToString() + "\"");
                json.json加資料.Append("},");

            }
            json.json加資料 = new StringBuilder(json.json加資料.ToString().TrimEnd(','));//移除最後一個「,」字元
            json.json加資料.Append("]");
            json.json加資料.Append("}");
            json.dr讀取資料.Close();


        }
        catch
        {
            json.錯誤格式();
        }

        json.連線.Close();
        Response.Write(json.json加資料.ToString());
        Response.End();
    }
}