using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class news : System.Web.UI.Page
{

    CPromotionFactory factory = new CPromotionFactory();
    List<CPromotionView> _list = new List<CPromotionView>();
    string sql = "";
    string 主鍵;


    /// <summary>
    /// 進入資料庫時機: 新增, 刪除, 修改, 一開始載入資料的show全部活動();
    /// 使用工廠list時機: 查詢, 按鈕查詢有 全部活動/上架/下架/show待審核()/show退審()
    /// </summary>

    protected void Page_Load(object sender, EventArgs e)
    {

        btnAll.BackColor = Color.FromName("#57BBB0 ");
        
        btnAdd.BackColor = Color.FromName("#E3FCF9");
        Button1.BackColor = Color.FromName("#E3FCF9");
        btnSearch.BackColor = Color.FromName("#E3FCF9");



        if (Session[CDictionary.SESSION_COMPANY] == null)
        {
            Response.Redirect("~/Sign.aspx");
        }
        

        factory.companyID = Session[CDictionary.SESSION_COMPANY].ToString();

        try
        {
            show全部活動();
            show待審核();
            show退審();
        }
        catch (Exception)
        {
            //Response.Redirect("~/windowCompany/companyCheck.aspx");
            throw;
        }
    }

    //點選按鈕變換顏色
    protected void buttoncolor(Button clickbtn)
    {

        btnAll.BackColor = Color.FromName("#E3FCF9 ");
        btnAdd.BackColor = Color.FromName("#E3FCF9");
        Button1.BackColor = Color.FromName("#E3FCF9");
        btnSearch.BackColor = Color.FromName("#E3FCF9");

        clickbtn.BackColor = Color.FromName("#57BBB0 ");
    }

    //按鈕看全部活動
    protected void btnAll_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        GridView1.DataSource = factory.list;
        //GridView1.DataSource = factory.getAll();
        GridView1.DataBind();
    }

    //按鈕看上架
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        searchByStatusID(1);
        GridView1.DataSource = _list;
        GridView1.DataBind();
    }

    //按鈕看下架
    protected void Button1_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        searchByStatusID(2);
        GridView1.DataSource = _list;
        GridView1.DataBind();
    }

    //按鈕新增
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        Response.Redirect("news.aspx");
    }



    //GridView1 進資料庫撈(sql指令寫在工廠)
    private void show全部活動()
    {
        factory.list.Clear();
        factory.loadDataById(factory.companyID);
        GridView1.DataSource = factory.list;

        //如果要改變上架商品的刪除標籤 變成 下架, Code應該寫在這裡

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

    //不進資料庫, 使用list搜尋
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




    /// <summary>
    /// GridView刪除事件
    /// </summary>
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
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        主鍵 = (string)this.GridView3.DataKeys[rowIndex]["活動代碼"];
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //查出的資料要先判斷是不是上架商品?
        //上架商品按刪除會變下架

        factory.searchById(主鍵);
        string 審核狀態 = factory.getCurrent().審核狀態;
        if (審核狀態.Equals("上架"))
        {
            sql = "UPDATE Promotions set ";
            sql += " statusID = 2 ";
            sql += " WHERE activityID='" + 主鍵 + "'";
            factory.executeUpdate(sql);
        }
        else {
            sql = "DELETE FROM Promotions WHERE activityID=" + 主鍵;
            factory.executeDelete(sql);
        }

        show全部活動();
        show待審核();
        show退審();
    }
    protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        sql = "DELETE FROM Promotions WHERE activityID=" + 主鍵;
        factory.executeDelete(sql);

        show全部活動();
        show待審核();
    }
    protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        sql = "DELETE FROM Promotions WHERE activityID=" + 主鍵;
        factory.executeDelete(sql);

        show全部活動();
        show退審();
    }




    /// <summary>
    /// GridView HyperLink
    /// </summary>
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //記得在aspx裡面還是做一個空白欄位給下面的按鈕放

        if (e.Row.RowType == DataControlRowType.DataRow) //GridView初始化到資料行才進入
        {
            Button btn待審編輯 = new Button();
            btn待審編輯.Text = "編輯";
            btn待審編輯.CommandArgument = e.Row.Cells[3].Text; //主鍵值會給按鈕的CommandArgument
            btn待審編輯.Click += onGoButton_Click;
            e.Row.Cells[1].Controls.Add(btn待審編輯);
        }

    }

    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //GridView初始化到資料行才進入
        {
            Button btn退審編輯 = new Button();
            btn退審編輯.Text = "編輯";
            btn退審編輯.CommandArgument = e.Row.Cells[3].Text;
            btn退審編輯.Click += onGoButton_Click;
            e.Row.Cells[1].Controls.Add(btn退審編輯);
        }
    }

    private void onGoButton_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        if (btn == null)
            return;
        string id = btn.CommandArgument;
        Session[CDictionary.SESSION_ACTIVITY_ID] = id;
        Response.Redirect("companyUpdate.aspx?");
    }
}