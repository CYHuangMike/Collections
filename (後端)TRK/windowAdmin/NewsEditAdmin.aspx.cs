using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowAdmin_NewsEditAdmin : System.Web.UI.Page
{
    string sql = "";
    string 主鍵;
    VerificationFactory factory = new VerificationFactory();
    List<VerificationView> _list = new List<VerificationView>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            showAdminVerification();
           
        }
        catch (Exception)
        {
            throw;
        }
    }

    //全部
    private void showAdminVerification()
    {
        factory.list.Clear();
        factory.loadDataAcminById();
        GridView3.DataSource = factory.list;//DataSource :資料加入list陣列裡面放入DataSource.
        GridView3.DataBind();//資料繫結
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        主鍵 = (string)this.GridView3.DataKeys[rowIndex]["活動代碼"];
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        sql = "DELETE FROM Promotions WHERE activityID=" + 主鍵;
        factory.executeDelete(sql);

        showAdminVerification();
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //記得在aspx裡面還是做一個空白欄位給下面的按鈕放

        if (e.Row.RowType == DataControlRowType.DataRow) //GridView初始化到資料行才進入
        {
            Button btnEdit = new Button();
            btnEdit.Text = "編輯";
            btnEdit.CommandArgument = e.Row.Cells[2].Text; //主鍵值會給按鈕的CommandArgument
            btnEdit.Click += onGoButton_Click;
            e.Row.Cells[1].Controls.Add(btnEdit);
        }

    }
     private void onGoButton_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn == null)
            return;
        string id = btn.CommandArgument;
        Session[CDictionary.SESSION_ACTIVITY_ID] = id;
        Response.Redirect("NewsUpdate.aspx?");
    }
}