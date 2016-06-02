using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// JSON 的摘要描述
/// </summary>
public class Json
{
    
    public SqlConnection 連線 = new SqlConnection(ConfigurationManager.ConnectionStrings["prjTRKConn"].ConnectionString);
    public SqlDataReader dr讀取資料 { get; set; }
    public SqlCommand command { get; set; }
    public string sql語法 { get; set; }
    public string json建資料 { get; set; }
    public StringBuilder json加資料 = new StringBuilder();
    public string Message旗標 { get; set; }
    public void 錯誤格式()
    {
        Message旗標 = "false";
        json建資料 += "\"Message\":\"" + Message旗標 + "\"";
        json建資料 += "}";
        return;
    }
    public void 錯誤格式2()
    {
        Message旗標 = "false";
        json加資料.Append("\"Message\":\"" + Message旗標 + "\"");
        json加資料.Append("}");
        return;
    }
}