using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JSON_ManliCardPoint : System.Web.UI.Page
{
    Json jsn = new Json();

    protected void Page_Load(object sender, EventArgs e)
    {
        string 點數 = "";

        try
        {
            Response.Clear();//清空
            Response.ContentType = "application/json; charset=utf-8";
            //JSON格式製作
            jsn.json建資料 += "{";



            //用 QueryString 傳輸入卡號
            string 卡號 = Request.QueryString["id"];
            //如果卡號為空
            if ("".Equals(卡號) || 卡號==null)
            {
                jsn.錯誤格式();
                return;
            }



            //如果連線關閉就打開
            if (jsn.連線.State == ConnectionState.Closed)
                jsn.連線.Open();
            //sql查詢執行            
            jsn.sql語法 = "select * from ManliCard where cardNumber = @p1";
            
            SqlCommand cmd = new SqlCommand(jsn.sql語法, jsn.連線);
            cmd.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@p1", 卡號)
                //new SqlParameter("@p2", value2)
            });

            jsn.dr讀取資料 = cmd.ExecuteReader();

            //查點數
            if (jsn.dr讀取資料.Read())
                點數 = jsn.dr讀取資料["cardPoint"].ToString();
            //如果點數為空
            if ("".Equals(點數))
                jsn.Message旗標 = "false";
            //點數不為空
            else
            {
                jsn.json建資料 += "\"cardNumber\":\"" + 卡號 + "\",";
                jsn.json建資料 += "\"cardPoint\":\"" + 點數 + "\",";
                jsn.Message旗標 = "true";
            }

            jsn.json建資料 += "\"Message\":\"" + jsn.Message旗標 + "\"";
            jsn.json建資料 += "}";

            jsn.dr讀取資料.Close();

        }
        catch(SqlException err)
        {
            jsn.錯誤格式();
        }
        finally
        {
            jsn.連線.Close();
            Response.Write(jsn.json建資料);
            Response.End();
        }
    }
    /*
            //無法防sql injection
            jsn.dr讀取資料 = (new SqlCommand(jsn.sql語法, jsn.連線)).ExecuteReader();

            //防sql injection 但只能帶一筆參數
             SqlDataAdapter myAdapter = new SqlDataAdapter(jsn.sql語法, jsn.連線);
             myAdapter.SelectCommand.Parameters.AddWithValue("@id", 卡號);
             jsn.dr讀取資料 = myAdapter.SelectCommand.ExecuteReader();
    */

}