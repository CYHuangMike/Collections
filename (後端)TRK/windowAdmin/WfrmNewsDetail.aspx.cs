using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WfrmNewsDetail : System.Web.UI.Page
{
    string NewsID = "";
    string comment = "";
     VerificationFactory factory = new VerificationFactory();
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
      
        if (!Page.IsPostBack)
        {
            //用QueryString 活動代碼會帶到網址列上 測試沒問題
            if (Request.QueryString["fId"] != null)
            {
                NewsID = Request.QueryString["fId"];
                Session[CDictionary.SESSION_ACTIVITY_NEWS_ID] = NewsID;
            }

        }
       
        method();
    }

   public void method() {

        factory.listNEW.Clear();
        factory.loadNewsDetail(NewsID);

        foreach (NewsDetailView item in factory.listNEW)
        {
            txtTitle.Text = item.活動主題.ToString();
            startDate.Text = item.開始日期.ToString();
            endDate.Text = item.結束日期.ToString();
            txtcheck.Text = item.審核狀態.ToString();
            txtNewsID.Text = item.活動代碼.ToString();
            txtNewsClass.Text = item.公告分類.ToString();
            txtResponsible.Text = item.發布單位.ToString();
            txtContent.Text = item.內容.ToString();
            txtRemark.Text = item.備註.ToString();
            picUP.ImageUrl = "~/pic/"+ item.圖片.ToString();
           // comment = txtRemark.Text;

        }
         
    }

    protected void VerificationTrue_Click(object sender, EventArgs e)
    {
        comment = txtRemark.Text;
        NewsID = Session[CDictionary.SESSION_ACTIVITY_NEWS_ID].ToString();
        factory.updateNewsDetailTrue(NewsID, comment);
        Response.Redirect("~/windowAdmin/companyCheckforAdmin.aspx");
    }

    protected void VerificationFalse_Click(object sender, EventArgs e)
    {
        comment = txtRemark.Text;
        NewsID = Session[CDictionary.SESSION_ACTIVITY_NEWS_ID].ToString();
        factory.updateNewsDetailFalse(NewsID, comment);
        Response.Redirect("~/windowAdmin/companyCheckforAdmin.aspx");
    }
}