using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class companyDetail : System.Web.UI.Page
{
    /// <summary>
    /// 此篇示範沒有使用工廠
    /// </summary>


    SqlDataSource x;
    string 資料夾路徑 = "~/pic/";
    string companyID = "";
    string counterID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[CDictionary.SESSION_COMPANY] == null)
        {
            Response.Redirect("~/Sign.aspx");
        }
        if (Session[CDictionary.SESSION_PASSWORDCHECK] == null)
        {
            Response.Redirect("~/windowCompany/passwordcheck.aspx");
        }

        companyID = Session[CDictionary.SESSION_COMPANY].ToString();

        //判斷是否為第一次載入頁面, 避免按下更新按鈕以後, 重新刷新頁面, 結果改過的都被刷回原來的
        if (!Page.IsPostBack)
        {            
            try
            {
                queryCompany();
                queryCounter();
            }
            catch (Exception)
            {
                     
            }
            
        }
    }

    //查廠商
    private void queryCompany()
    {
        x = new SqlDataSource();
        x.ConnectionString = CDictionary.CONN_URL;
        x.SelectCommand = "Select * from Company where companyID='" + companyID + "'";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        //查完一定只有一筆, 所以Rows[0]選第一筆資料即可
        txtName.Text = dv.Table.Rows[0]["name"].ToString();
        txtEmail.Text = dv.Table.Rows[0]["email"].ToString();
        txtSpareEmail.Text = dv.Table.Rows[0]["sparEmail"].ToString();
        txtManager.Text = dv.Table.Rows[0]["manager"].ToString();
        txtPhone.Text = dv.Table.Rows[0]["tel"].ToString();
        txtAddress.Text = dv.Table.Rows[0]["addr"].ToString();
        txtContent.Text = dv.Table.Rows[0]["content"].ToString();
        picUP.ImageUrl = "~/pic/" + dv.Table.Rows[0]["picBrandPath"].ToString();
    }

    //查櫃位
    private void queryCounter()
    {
        string 建築物 = "";

        x = new SqlDataSource();
        x.ConnectionString = CDictionary.CONN_URL;
        x.SelectCommand = "Select * from Counters where companyID='" + companyID + "'";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;
        try
        {
            //查完一定只有一筆, 所以Rows[0]選第一筆資料即可
            if (dv.Table.Rows[0]["building"].ToString() == "E")
                建築物 = "東側 ";
            else if (dv.Table.Rows[0]["building"].ToString() == "W")
                建築物 = "西側 ";

            counterID += 建築物;
            counterID += dv.Table.Rows[0]["layer"].ToString() + " 樓 ";
            counterID += dv.Table.Rows[0]["counterNum"].ToString() + "號";

            txtCounter.Text = counterID;
        }
        catch (Exception)
        {
            txtCounter.Text = "目前還沒有櫃位，請聯絡管理員協助";
        }
        
    }


    //按鈕更新
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //壓縮圖片並存檔
        CImageProess.get().UploadImage(FileUpload1, 資料夾路徑, CDictionary.SET_BRAND_PX);

        x = new SqlDataSource();
        x.ConnectionString = CDictionary.CONN_URL;

        string sql = "UPDATE Company set ";
        if (!string.IsNullOrEmpty(txtContent.Text))
        {
            sql += " name=N'" + txtName.Text + "', ";
            if (FileUpload1.HasFile)
                sql += " picBrandPath='" + CImageProess.imageName + "', ";
            sql += " manager=N'" + txtManager.Text + "', ";
            sql += " tel=N'" + txtPhone.Text + "', ";
            sql += " sparEmail=N'" + txtSpareEmail.Text + "', ";
            sql += " addr=N'" + txtAddress.Text + "', ";
            sql += " content=N'" + txtContent.Text + "' ";
            sql += "WHERE companyID='" + companyID + "'";

            x.UpdateCommand = sql;
            x.Update();
        }
        else
            return;

        Response.Write("<script>alert('更改資料成功!')</script>");
        Response.Write("<script>location.href='companyCheck.aspx'</script>");
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnpasswordchange_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/windowCompany/updatepassword.aspx");
    }
}