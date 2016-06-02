using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowCompany_AddNewsByAdmin : System.Web.UI.Page
{
    int classifyID = 1;
    string path;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (Session[CDictionary.SESSION_ADMIN] == null)
            {
                Response.Redirect("~/Sign.aspx");
            }


            logincheckDataContext lc = new logincheckDataContext();
            var classtype = from row in lc.Classfication
                            select row;

            foreach (Classfication cust in classtype)
            {
                if (cust.classifyID != 2) //Admin has no Company Promotion option
                {
                    drdtype.Items.Add(cust.item);
                }
            }

        }

    }
    protected void drdtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        logincheckDataContext lc = new logincheckDataContext();
        var classtype = from row in lc.Classfication
                        where row.item.Equals(drdtype.Text)
                        select row;


        Classfication t = lc.Classfication.FirstOrDefault(m => m.item == drdtype.Text);
        if (t != null)
        {
            classifyID = t.classifyID;
        }

    }

    protected void btnNewsdate_Click(object sender, EventArgs e)
    {
        string fileName = Guid.NewGuid().ToString() + ".jpg";
        FileUpload1.SaveAs(Server.MapPath("../pic/" + fileName));//存到指定路徑
        path = fileName;
        excuteNonQuery();
        Response.Redirect("~/windowAdmin/companyCheckforAdmin.aspx");
    }


    private void excuteNonQuery()
    {
        logincheckDataContext lc = new logincheckDataContext();
        Promotions news_add = new Promotions();
        news_add.title = titleInput.Text;
        news_add.classifyID = classifyID;
        news_add.startDate = Convert.ToDateTime(startDate.Text);
        news_add.endDate = Convert.ToDateTime(endDate.Text);
        news_add.onShelfDate = DateTime.Now.ToUniversalTime().AddHours(8);
        news_add.picPath = path.ToString();
        news_add.content = txtNewsContent.Text;
        news_add.statusID = 1;
        news_add.companyID = "C0001";
        news_add.comment = null;
        lc.Promotions.InsertOnSubmit(news_add);
        lc.SubmitChanges();
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