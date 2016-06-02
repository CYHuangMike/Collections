using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class windowAdmin_EditFacility : System.Web.UI.Page
{
    logincheckDataContext db = new logincheckDataContext();
    int facilityID = 0;
    string 資料夾路徑 = "~/mPic/";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        //出ID
        var facility = from row in db.Facility
                 select row;
        int lastID = db.Facility.OrderByDescending(facilityID => facilityID.facilityID).FirstOrDefault().facilityID;
        int intFacilityID = lastID + 1;

        //壓縮圖片並存檔
        CImageProess.get().UploadImage(FileUpload1, 資料夾路徑, CDictionary.SET_FACILITY_PX);


        //LINQ寫入資料庫
        Facility data = new Facility();
        data.facilityID = intFacilityID;
        data.name = txtName.Text;
        data.content = txtContent.Text;
        data.pic = CImageProess.imageName;
        db.Facility.InsertOnSubmit(data);
        try
        {
            db.SubmitChanges();
            Response.Write("<script>alert('新增成功'); window.location='EditFacility.aspx'</script>");
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
        }
        //Response.Redirect(Request.Url.ToString());
    }

    //LINQ更新
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        CImageProess.get().UploadImage(FileUpload1, 資料夾路徑, CDictionary.SET_FACILITY_PX);

        var facility = from row in db.Facility
                       where row.facilityID.Equals(int.Parse(Session[CDictionary.SESSION_FACILITY_ID].ToString()))
                       select row;

        foreach (Facility item in facility)
        {
            item.name = txtName.Text;
            item.content = txtContent.Text;
            if (FileUpload1.HasFile)
                item.pic = CImageProess.imageName;
                      
        }

        try
        {
            db.SubmitChanges();
            Response.Write("<script>alert('更新成功'); window.location='EditFacility.aspx'</script>");
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
            // Provide for exceptions.
        }

        //Response.Redirect(Request.Url.ToString());
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        facilityID = int.Parse(GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text);
        Session[CDictionary.SESSION_FACILITY_ID] = facilityID.ToString();
        var facility = from row in db.Facility
                       where row.facilityID.Equals(facilityID)
                       select row;
        foreach (Facility item in facility)
        {
            txtID.Text = item.facilityID.ToString();
            txtName.Text = item.name;
            txtContent.Text = item.content;
            picUP.ImageUrl = "~/mPic/" + item.pic;
        }
    }



    private void clear()
    {
        txtID.Text = "";
        txtName.Text = "";
        txtContent.Text = "";
    }
}