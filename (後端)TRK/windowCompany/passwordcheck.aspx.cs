using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowCompany_passwordcheck : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {


        Session[CDictionary.SESSION_PASSWORDCHECK] = "";
        message.Visible = false;

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        bool acc = false, pwd = false;


        if (txtPasswordCheck.Text == "")
        {
            message.Visible = true;
            message.Text = "請輸入密碼";
        }


        else
        {
            logincheckDataContext lc = new logincheckDataContext();
            var Accounttable = from row in lc.Account

                               select row;


            foreach (Account cust in Accounttable)
            {

                if (cust.account1.Equals(Session[CDictionary.SESSION_COMPANYACCOUNT].ToString()))
                {
                    acc = true;
                    if (cust.password.Equals(txtPasswordCheck.Text))
                    {
                        pwd = true;
                    }

                }
                if (acc && pwd)
                {
                    Session[CDictionary.SESSION_PASSWORDCHECK] = txtPasswordCheck.Text;
                    Response.Redirect("~/windowCompany/companyDetail.aspx");
                }

                else
                {

                    message.Visible = true;
                    message.Text = "輸入密碼有誤";
                }
            }
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtPasswordCheck.Text = "";
        message.Visible = false;
    }
}