using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class news : System.Web.UI.Page
{
    SqlDataSource x;
    string companyID = "";
    string path;
    string 資料夾路徑 = "~/pic/";
    protected void Page_Load(object sender, EventArgs e)
    {
        //FileUpload1.Attributes.Add("onchange", "previewFileProduct(this)");

        if (Session[CDictionary.SESSION_COMPANY] == null)
        {
            Response.Redirect("~/Sign.aspx");
        }
        companyID = Session[CDictionary.SESSION_COMPANY].ToString();


        //companyID = "C0002";
        executeQuery();
    }

    protected void btnNewsdate_Click(object sender, EventArgs e)
    {
        pictureUpload();
        excuteNonQuery(getInsertSql());

    }

    private void pictureUpload()
    {
        //壓縮圖片並存檔
        CImageProess.get().UploadImage(FileUpload1, 資料夾路徑, CDictionary.SET_PROMOTION_PX);
    }

    private void excuteNonQuery(string sql)
    {
        SqlDataSource x = new SqlDataSource();
        x.ConnectionString = @"Data Source=iii0.database.windows.net;Initial Catalog=prjTRK;Persist Security Info=True;User ID=iii;Password=P@ssw0rd";
        x.InsertCommand = sql;
        x.Insert();
        Response.Redirect("~/windowCompany/companyCheck.aspx");
    }
    private void executeQuery()
    {
        x = new SqlDataSource();
        x.ConnectionString = @"Data Source=iii0.database.windows.net;Initial Catalog=prjTRK;Persist Security Info=True;User ID=iii;Password=P@ssw0rd";
        x.SelectCommand = "Select * from Company where companyID='" + companyID + "'";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        txtCounterInput.Text = dv.Table.Rows[0]["name"].ToString();
    }

    private string getInsertSql()
    {
        string sql = "INSERT INTO Promotions(";
        sql += " companyID ,";
        sql += " title ,";
        sql += " classifyID ,";
        sql += " content ,"; 
        sql += " onShelfDate ,";
        sql += " startDate ,";
        sql += " endDate ,";
        sql += " picPath ,";
        sql += " statusID ";
        sql += ")VALUES(";
        sql += "'" + companyID + "',";
        sql += "N'" + titleInput.Text + "',";
        sql += "'2',";
        sql += "N'" + txtNewsContent.Text + "',";
        sql += " convert(varchar, DATEADD(hour, 8, GETUTCDATE()), 113),";
        sql += "'" + startDate.Text + "',";
        sql += "'" + endDate.Text + "',";
        sql += "'" + CImageProess.imageName + "',";
        sql += "'3')";
        return sql;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        CancleAll();
    }

    private void CancleAll()
    {
        titleInput.Text = "";
        startDate.Text = "";
        endDate.Text = "";
        txtNewsContent.Text = "";
    }
}