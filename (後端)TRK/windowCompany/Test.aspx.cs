using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
    CPromotionFactory factory = new CPromotionFactory();
    SqlDataSource x = new SqlDataSource();
    List<CPromotionView> _list = new List<CPromotionView>();
    string sql = "";
    string 主鍵;


    /// <summary>
    /// 進入資料庫時機: 新增, 刪除, 修改, 一開始載入資料的show全部活動();
    /// 使用工廠list時機: 查詢, 按鈕查詢有 全部活動/上架/下架/show待審核()/show退審()
    /// </summary>



    protected void Page_Load(object sender, EventArgs e)
    {
        //factory.companyID = Session["廠商ID"].ToString(); //預備接收登入畫面過來的session
        factory.companyID = "C0002";

        try
        {
            show全部活動();
            show待審核();
            show退審();            
        }
        catch (Exception)
        {
            //Response.Redirect("~/Test.aspx");
            throw;
        }   
    }
 
    //GridView1 使用廠商ID重新整理(sql指令寫在工廠)
    private void show全部活動()
    {
        factory.list.Clear();
        factory.loadDataById(factory.companyID);
        GridView1.DataSource = factory.list;
        GridView1.DataBind();
    }

    //按鈕看全部活動
    protected void btnAll_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = factory.list;
        GridView1.DataBind();
    }

    //按鈕看上架
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        searchByStatusID(1);
        GridView1.DataSource = _list;
        GridView1.DataBind();
    }
    
    //按鈕看下架
    protected void Button1_Click(object sender, EventArgs e)
    {
        searchByStatusID(2);
        GridView1.DataSource = _list;
        GridView1.DataBind();
    }

    //GridView3
    protected void show待審核()
    {        
        searchByStatusID(3);
        GridView3.DataSource = _list;
        GridView3.DataBind();
    }

    //GridView4
    protected void show退審()
    {
        searchByStatusID(4);
        GridView4.DataSource = _list;
        GridView4.DataBind();
    }

    private void searchByStatusID(int statusID)
    {
        _list.Clear();
        foreach (CPromotionView item in factory.list)
        {
            switch (statusID)
            {
                case 1:
                    if (item.審核狀態.Equals("上架"))
                        _list.Add(item);
                    break;
                case 2:
                    if (item.審核狀態.Equals("下架"))
                        _list.Add(item);
                    break;
                case 3:
                    if (item.審核狀態.Equals("待審中"))
                        _list.Add(item);
                    break;
                case 4:
                    if (item.審核狀態.Equals("退審"))
                        _list.Add(item);
                    break;
            }
        }
    }

    //GridView刪除事件
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        sql = "DELETE FROM Promotions WHERE activityID=" + 主鍵;
        factory.executeDelete(sql);
        show全部活動();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        主鍵 = (string)this.GridView1.DataKeys[rowIndex]["活動代碼"];
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
        //show 待審核
    }


    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        主鍵 = (string)DataBinder.Eval(e.Row.DataItem, "活動代碼");
        //從此事件帶值到下一個網頁, 但是主鍵的值要去哪裡抓? RowCommand事件啟動比此事件還要晚
        //Session["活動代碼"] = 1;
        Session["活動代碼"] = 主鍵;
    }
}