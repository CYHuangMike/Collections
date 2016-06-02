using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSON_FloorTourOne : System.Web.UI.Page
{
    List<String> CompanyName = new List<String>();


    Json json = new Json();
    protected void Page_Load(object sender, EventArgs e)
    {
        int layer = Convert.ToInt32(Request.QueryString["layer"]);
        String building = Request.QueryString["building"];
        int facitity = 2;

        try
        {
            Response.Clear();
            Response.ContentType = "application/json ;charset=utf-8";
            json.json加資料.Append("{\"data\":[");

            //當連線關閉時，開啟Json
            if (json.連線.State == ConnectionState.Closed)
                json.連線.Open();
            if (building == "W")
            {
                facitity = 2;
            }
            else
            {
                facitity = 1;
            }

            //sql
            json.sql語法 = " select coun.companyID AS companyID , com.name AS companyName, coun.picLocation as piclocation," +
                           " com.content as content,com.addr as addr,com.picBrandPath as picBrandPath" +
                           " from Counters as coun inner join Company as com on coun.companyID = com.companyID" +
                           " where coun.facilityID = "+ facitity+" and coun.building = '" + building + "' and coun.layer =" + layer;

            SqlCommand cmd = new SqlCommand(json.sql語法, json.連線);
            json.dr讀取資料 = cmd.ExecuteReader();


            while (json.dr讀取資料.Read())
            {

                json.json加資料.Append("{");

                json.json加資料.Append("\"companyID\":\"" + json.dr讀取資料["companyID"].ToString() + "\",");
                json.json加資料.Append("\"picLocation\":\"" + json.dr讀取資料["piclocation"].ToString() + "\",");
                json.json加資料.Append("\"CompanyName\":\"" + json.dr讀取資料["companyName"].ToString() + "\",");
                json.json加資料.Append("\"content\":\"" + json.dr讀取資料["content"].ToString() + "\",");
                json.json加資料.Append("\"addr\":\"" + json.dr讀取資料["addr"].ToString() + "\",");
                json.json加資料.Append("\"piclocation\":\"" + json.dr讀取資料["piclocation"].ToString() + "\",");
                json.json加資料.Append("\"picBrandPath\":\"" + json.dr讀取資料["picBrandPath"].ToString() + "\"");
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