using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowAdmin_CouponList : System.Web.UI.Page
{
    logincheckDataContext db = new logincheckDataContext();
    CQrCouponFactory factory = new CQrCouponFactory();
    private int couponID;
    private string couponTitle;
    private string couponBname;
    int facilityID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadFacDropList();
            getDefaultFacilityID();
            loadCouponGV(0);
        }
    }

    //查詢 沒做關鍵字查詢
    protected void btnSearchQRCode_Click(object sender, EventArgs e)
    {
        string txt = txtQRCode.Text;
        //如果欄位空白, 就Load出全部
        if (txt == "")
            loadCouponGV(0);

    }

    protected void facilityList_SelectedIndexChanged(object sender, EventArgs e)
    {        
        loadCouponGV(factory.getFacilityID(facilityList.Text));
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //主鍵
        couponID = int.Parse(GridView1.Rows[GridView1.SelectedIndex].Cells[2].Text);
        couponTitle = GridView1.Rows[GridView1.SelectedIndex].Cells[3].Text;
        couponBname = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
        //show圖片
        imgQRCode.ImageUrl = "~/mPic/" + db.Coupon.FirstOrDefault(c=>c.couponID.Equals(couponID)).qrCode;
        //show圖片網址
        lblBname.Text = " 設施名稱:"+couponBname;
        lblTitle.Text = "標題:"+couponTitle;
        lblUrl.Text = "圖片網址："+ "http://prjtrk.azurewebsites.net" + "/mPic/" + db.Coupon.FirstOrDefault(c=>c.couponID.Equals(couponID)).qrCode;
    }



    private void loadFacDropList()
    {
        //品牌種類
        var type = from row in db.Facility
                   select row;
        foreach (Facility f in type)
        {
            facilityList.Items.Add(f.name);
        }
    }

    private void getDefaultFacilityID()
    {
        Session[CDictionary.SESSION_FACILITY_ID] = db.Facility.FirstOrDefault().facilityID;
    }

    //取得GridView資料
    private void loadCouponGV(int facilityID)
    {        
        factory.loadData(facilityID);
        GridView1.DataSource = factory.getAll();
        GridView1.DataBind();
    }

}