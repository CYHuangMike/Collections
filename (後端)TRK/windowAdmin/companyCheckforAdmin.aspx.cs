using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class news : System.Web.UI.Page
{
    VerificationFactory factory = new VerificationFactory();
    List<VerificationView> _list = new List<VerificationView>();

    /// <summary>
    /// 一開始載入資料的show全部活動();
    /// 使用工廠list時機: 全部活動/上架/下架/show待審核()/show退審()
    /// </summary>



    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.BackColor = Color.FromName("#57BBB0 ");
        btnAll.BackColor = Color.FromName("#E3FCF9 ");
        
        btnoffshelf.BackColor = Color.FromName("#E3FCF9");
        btnOnshelf.BackColor = Color.FromName("#E3FCF9");

        if (Session[CDictionary.SESSION_ADMIN] == null)
        {
            Response.Redirect("~/Sign.aspx");

        }


        try
        {
            showAllVerification();
            showCheckVerification();//顯示待審消息
            showOnshelfVerification();//顯示上架消息
            showOffshelfVerification();//顯示下架消息
            showReturnVerification();//顯示退審消息
        }
        catch (Exception)
        {
            throw;
        }
    }
    //不進資料庫, 使用list搜尋
    private void searchByStatusID(int statusID)
    {
        _list.Clear();
        foreach (VerificationView item in factory.list)
        {
            switch (statusID)
            {
                case 1:
                    if (item.審核狀態.Equals("退審"))
                        _list.Add(item);
                    break;
                case 2:
                    if (item.審核狀態.Equals("待審中"))
                        _list.Add(item);
                    break;
                case 3:
                    if (item.審核狀態.Equals("下架"))
                        _list.Add(item);
                    break;
                case 4:
                    if (item.審核狀態.Equals("上架"))
                        _list.Add(item);
                    break;
            }
        }
    }
    //全部
    private void showAllVerification()
    {
        factory.list.Clear();
        factory.loadDataById();
        GridView1.DataSource = factory.list;//DataSource :資料加入list陣列裡面放入DataSource.
        GridView1.DataBind();//資料繫結
    }
    //待審
    private void showCheckVerification()
    {
        searchByStatusID(2);//顯示待審消息
        DateGridView1();
    }
    //on shelf 上架
    private void showOnshelfVerification()
    {
        searchByStatusID(4);
        GridView2.DataSource = _list;
        GridView2.DataBind();
    }
    //off shelf 下架
    private void showOffshelfVerification()
    {
        searchByStatusID(3);
        GridView3.DataSource = _list;
        GridView3.DataBind();
    }
    //Return 退審
    private void showReturnVerification()
    {
        searchByStatusID(1);
        GridView4.DataSource = _list;
        GridView4.DataBind();

    }

    //資料加入list
    private void DateGridView1()
    {
        GridView1.DataSource = _list;
        GridView1.DataBind();
    }

    private void DateGridView1(SortDirection pSortDirection,string PSortExpression)
    {
        string sSort = string.Empty;
        if(pSortDirection == SortDirection.Ascending)
        {
            sSort = PSortExpression;
        }
        else
        {
            sSort = PSortExpression + " DESC";
        }

        


    }
   

    //點選按鈕變換顏色
    protected void buttoncolor(Button clickbtn)
    {
        btnAll.BackColor = Color.FromName("#E3FCF9 ");
        btnAdd.BackColor = Color.FromName("#E3FCF9");
        btnoffshelf.BackColor = Color.FromName("#E3FCF9");
        btnOnshelf.BackColor = Color.FromName("#E3FCF9");

        clickbtn.BackColor = Color.FromName("#57BBB0 ");
    }

    protected void btnAll_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        showAllVerification();
    }

    protected void btnOnshelf_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        searchByStatusID(4);//上架
        DateGridView1();
    }

    protected void btnoffshelf_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        searchByStatusID(3);//下架
        DateGridView1();
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        buttoncolor((Button)sender);
        searchByStatusID(2);//待審
        DateGridView1();
    }
}