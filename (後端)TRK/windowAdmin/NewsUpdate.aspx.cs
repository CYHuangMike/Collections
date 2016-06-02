using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowCompany_companyUpdate : System.Web.UI.Page
{
    SqlDataSource x;
    string path;
    string 資料夾路徑 = "~/pic/";
    string statusID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session[CDictionary.SESSION_ACTIVITY_ID] != null)
            {
                activity.Text = Session[CDictionary.SESSION_ACTIVITY_ID].ToString();
            }
            executeQuery();
        }
    }
    private void executeQuery()
    {
        x = new SqlDataSource();
        x.ConnectionString = @"Data Source=iii0.database.windows.net;Initial Catalog=prjTRK;Persist Security Info=True;User ID=iii;Password=P@ssw0rd";
        x.SelectCommand = "Select *,convert(varchar(100),startDate,111) as SDate,convert(varchar(100),endDate,111) as EDate from Promotions where activityID ='" + activity.Text + "' ";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        //查完一定只有一筆, 所以Rows[0]選第一筆資料即可
        titleInput.Text = dv.Table.Rows[0]["title"].ToString();
        startDate.Text = dv.Table.Rows[0]["SDate"].ToString();
        endDate.Text = dv.Table.Rows[0]["EDate"].ToString();
        txtNewsContent.Text = dv.Table.Rows[0]["content"].ToString();
        picUP.ImageUrl = "~/pic/" + dv.Table.Rows[0]["picPath"].ToString();
    }

    protected void btnNewsUpdate_Click(object sender, EventArgs e)
    {
        //actID 已知的活動代碼
        //用where查狀態是否還為"待審中"
        //是待審中再進行更新動作
        //是上架就進行Question 警告使用者 更新後, 將重新送審
        x = new SqlDataSource();
        x.ConnectionString = @"Data Source=iii0.database.windows.net;Initial Catalog=prjTRK;Persist Security Info=True;User ID=iii;Password=P@ssw0rd";
        x.SelectCommand = "Select *from Promotions where activityID ='" + activity.Text + "'";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;
        statusID = dv.Table.Rows[0]["statusID"].ToString();


        //壓縮圖片並存檔
        CImageProess.get().UploadImage(FileUpload1, 資料夾路徑, CDictionary.SET_PROMOTION_PX);

        x = new SqlDataSource();
        x.ConnectionString = @"Data Source=iii0.database.windows.net;Initial Catalog=prjTRK;Persist Security Info=True;User ID=iii;Password=P@ssw0rd";

        string sql = "UPDATE Promotions set ";
        if (!string.IsNullOrEmpty(titleInput.Text))
        {
            sql += " title=N'" + titleInput.Text + "', ";
            if (FileUpload1.HasFile)
                sql += " picPath='" + CImageProess.imageName + "', ";
            sql += " startDate='" + startDate.Text + "', ";
            sql += " endDate='" + endDate.Text + "', ";
            sql += " onShelfDate = convert(varchar, DATEADD(hour, 8, GETUTCDATE()), 113),";
            sql += " statusID = 1 ,";
            sql += " content=N'" + txtNewsContent.Text + "' ";
            sql += " WHERE activityID='" + activity.Text + "'";
            x.UpdateCommand = sql;
            x.Update();
        }
        else
            return;


        Response.Redirect("~/windowAdmin/NewsEditAdmin.aspx");
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