using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


// 缺顯示列表 需可編輯刪除 使用灰色小框框

public partial class windowAdmin_QRCode : System.Web.UI.Page
{
    logincheckDataContext db = new logincheckDataContext();
    private int couponID;
    int facilityID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadFacDropList();
            getDefaultFacilityID();
            lblMsg.Visible = false;
        }
        
    }


    protected void facilityList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session[CDictionary.SESSION_FACILITY_ID] = 
            db.Facility.FirstOrDefault(m=>m.name.Equals(facilityList.SelectedItem.Text)).facilityID;
    }

    //按鈕產生QRCode & 寫入資料庫
    protected void btnQRCode_Click(object sender, EventArgs e)
    {
        string strTitle = txtTitle.Text;
        string strContent = txtContent.Text;        
        int couponID = 0;
        try
        {
            couponID = db.Coupon.OrderByDescending(cpID => cpID.couponID).FirstOrDefault().couponID+1;
        }
        catch (Exception)
        {
            couponID = 10010;
        }
        
        

        string docupath = Request.PhysicalApplicationPath; //抓取實際目錄路徑
        if (strTitle != "" && strContent != "") //判斷使用者輸入
        {
            string mContent = txtContent.Text;
            string mTime = DateTime.Now.Millisecond.ToString(); //取得當亂數

            lblMsg.Text = "qr_" + couponID + ".png"; //顯示檔名 
            QRCodeEncoder mEncoder = new QRCodeEncoder(); //建立 encoder
            System.Drawing.Bitmap qrcode = mEncoder.Encode(couponID.ToString()); //將內容轉碼成 QR code

            qrcode.Save(docupath + "mPic\\qrcode\\qr_" + couponID + ".jpg"); //把 QRcode 另存為 jpg 圖檔,並放置於 images 資料夾底下
            imgQRCode.ImageUrl = "~\\mPic\\qrcode\\qr_" + couponID + ".jpg"; //設定組件 Image 的來源
        }
        else // txtContent 沒內容就不轉 QRcode + 提示訊息
        {
            lblMsg.Text = "請輸入資料!!";
            lblMsg.Visible = true;
            return;
        }

        //LINQ寫入資料庫
        Coupon coupon = new Coupon();
        coupon.title = strTitle;
        coupon.content = strContent;
        coupon.qrCode = "qrcode/qr_" + couponID + ".jpg";
        coupon.facilityID = (int)Session[CDictionary.SESSION_FACILITY_ID];
        db.Coupon.InsertOnSubmit(coupon);
        db.SubmitChanges();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtTitle.Text = "";
        txtContent.Text = "";
        lblMsg.Visible = false;
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

}