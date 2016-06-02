using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class news : System.Web.UI.Page
{
    string filter = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[CDictionary.SESSION_CHANGED] = 0;
    }
    protected void sqlcompanySearch(object sender, SqlDataSourceSelectingEventArgs e)
    {
        if (String.IsNullOrEmpty(txtCname.Text))
        {
            e.Cancel = true;
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtCname.Text))
        {
            txtMsg.Text = "請至少輸入一個查詢！";
            SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand+" ";
        }
        else
        {
            //加入Where條件式-ID
            if (!String.IsNullOrEmpty(txtCname.Text))
            {
                filter = " Where 廠商名稱 like @name";
                SqlDataSource1.SelectCommand = SqlDataSource1.SelectCommand + filter;
                if (SqlDataSource1.SelectParameters["name"] == null)
                {
                    SqlDataSource1.SelectParameters.Add("name", TypeCode.String, "");
                }

                SqlDataSource1.SelectParameters["name"].DefaultValue = "%" + txtCname.Text + "%";
            }
        }
    }


}