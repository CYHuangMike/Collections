using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowAdmin_ManliCard : System.Web.UI.Page
{
    SqlDataSource x = new SqlDataSource();
    int companyId = 1; //先假設用


    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Visible = false;
    }

    //查詢點數
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        x.ConnectionString = @"Data Source=iii0.database.windows.net;Initial Catalog=prjTRK;Persist Security Info=True;User ID=iii;Password=P@ssw0rd";
        x.SelectCommand = "Select cardPoint from ManliCard where cardNumber='" + txtMcard.Text + "'";

        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;
        //查完一定只有一筆, 所以Rows[0]選第一筆資料即可
        if (dv.Count > 0)
        {
            Label1.Text = "卡號點數：" + dv.Table.Rows[0]["cardPoint"].ToString();
            Label1.Visible = true;
        }
        return;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtMcard.Text = "";
        Label1.Text = "";
        Label1.Visible = false;
    }

}