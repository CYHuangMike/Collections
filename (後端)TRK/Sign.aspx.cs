using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[CDictionary.SESSION_COMPANY] = "";
        Session[CDictionary.SESSION_ADMIN] = "";
        message.Visible = false;
        try
        {
            checkPromotionDate();
        }
        catch (Exception)
        {
            Response.Write("<script>alert('連線異常，請稍候再試！'); window.location='EditFacility.aspx'</script>");
        }        
    }


    protected void submit_Click(object sender, EventArgs e)
    {
        bool acc = false, pwd = false;


        if (txtaccount.Text == "" || txtpassword.Text == "")
        {
            message.Visible = true;
            message.Text = "請輸入帳號及密碼";
            message.ForeColor = System.Drawing.Color.FromName("#35998E");
        }


        else
        {
            logincheckDataContext lc = new logincheckDataContext();
            var Accounttable = from row in lc.Account

                               select row;


            foreach (Account cust in Accounttable)
            {

                if (cust.account1.Equals(txtaccount.Text))
                {
                    acc = true;
                    if (cust.password.Equals(txtpassword.Text))
                    {
                        pwd = true;
                    }

                }
                if (acc && pwd)
                {
                    if ((bool)cust.authority)
                    {
                        Session[CDictionary.SESSION_ADMIN] = txtaccount.Text;
                        Response.Redirect("~/windowAdmin/companyCheckforAdmin.aspx");
                    }
                    else
                    {
                        getcompanyID();
                    }
                }

                else
                {
                    if (acc)
                    {
                        message.Visible = true;
                        message.Text = "登入密碼有誤";
                        message.ForeColor = System.Drawing.Color.FromName("#35998E");
                    }
                    else
                    {
                        message.Visible = true;
                        message.Text = "登入帳號有誤";
                        message.ForeColor = System.Drawing.Color.FromName("#35998E");
                    }
                }
            }
        }

    }

    private void getcompanyID()
    {
        logincheckDataContext lc = new logincheckDataContext();
        var Companytable = from row in lc.Company
                           where row.email.Contains(txtaccount.Text)
                           select row;
        foreach (Company cust2 in Companytable)
        {
            Session[CDictionary.SESSION_COMPANY] = cust2.companyID;
            Session[CDictionary.SESSION_COMPANYACCOUNT] = txtaccount.Text;
            Response.Redirect("~/windowCompany/companyCheck.aspx");
        }
    }

    //使用預存程序判斷促銷活動日期並下架, 參考MSDN
    private void checkPromotionDate()
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        cmd.Connection = cn;
        cn.ConnectionString = CDictionary.CONN_URL;
        cn.Open();

        cmd.CommandText = "sp_checkpromotiondate";
        cmd.CommandType = CommandType.StoredProcedure;

        reader = cmd.ExecuteReader();
        cn.Close();
    }

}