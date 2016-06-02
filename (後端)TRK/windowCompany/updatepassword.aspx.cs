using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowCompany_updatepassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        message.Visible = false;
        if (Session[CDictionary.SESSION_COMPANYACCOUNT] == null)
        {
            Response.Redirect("~/Sign.aspx");
        }
        if (Session[CDictionary.SESSION_PASSWORDCHECK] == null)
        {
            Response.Redirect("~/windowCompany/passwordcheck.aspx");
        }
    }



    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        if (txtpassword2.Text == "" || txtpassword1.Text == "")
        {
            message.Visible = true;
            message.Text = "請輸入密碼";
        }


        else
        {
            if (txtpassword1.Text == txtpassword2.Text)
            {
                logincheckDataContext lc = new logincheckDataContext();
                var Accounttable = from row in lc.Account
                                   where row.account1.Equals(Session[CDictionary.SESSION_COMPANYACCOUNT].ToString())
                                   select row;
                foreach (Account cust in Accounttable)
                {
                    cust.password = txtpassword1.Text;
                    
                }

                lc.SubmitChanges();

            }
            else
            {
                message.Visible = true;
                message.Text = "新密碼與確認密碼不相符";
            }
        }
        Response.Write("<script>alert('更改密碼成功!請使用新密碼再重新登入，謝謝!')</script>");
        Response.Write("<script>location.href='../Sign.aspx'</script>");
        //Response.Redirect("~/Sign.aspx");

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtpassword1.Text = "";
        txtpassword2.Text = "";
        message.Visible = false;
    }
}