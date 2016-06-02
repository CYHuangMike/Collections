using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSON_GetAllFacility : System.Web.UI.Page
{
    Json jsn = new Json();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();//清空
            Response.ContentType = "application/json; charset=utf-8";
            jsn.json加資料.Append("{");
            //如果連線關閉就打開
            if (jsn.連線.State == ConnectionState.Closed)
                jsn.連線.Open();
            //sql查詢執行            
            jsn.sql語法 = " SELECT * FROM Facility";

            SqlCommand cmd = new SqlCommand(jsn.sql語法, jsn.連線);
            jsn.dr讀取資料 = cmd.ExecuteReader();

            //JSON格式製作
            jsn.json加資料.Append("\"data\":[");
            //查詢
            while (jsn.dr讀取資料.Read())
            {
                jsn.json加資料.Append("{");
                jsn.json加資料.Append("\"FacilityID\":\"" + jsn.dr讀取資料["FacilityID"].ToString() + "\",");
                jsn.json加資料.Append("\"FacilityName\":\"" + jsn.dr讀取資料["name"].ToString() + "\",");
                jsn.json加資料.Append("\"Content\":\"" + jsn.dr讀取資料["content"].ToString() + "\",");
                jsn.json加資料.Append("\"Pic\":\"" + jsn.dr讀取資料["pic"].ToString() + "\"");

                jsn.json加資料.Append("},");
            }
            jsn.json加資料 = new StringBuilder(jsn.json加資料.ToString().TrimEnd(','));//移除最後一個「,」字元

            jsn.json加資料.Append("],");
            jsn.json加資料.Append("\"Message\":\"true\"");
            jsn.json加資料.Append("}");
            jsn.dr讀取資料.Close();
        }
        catch
        {
            jsn.錯誤格式2();
        }
        finally
        {
            jsn.連線.Close();
            Response.Write(jsn.json加資料.ToString());
            Response.End();
        }
    }
}