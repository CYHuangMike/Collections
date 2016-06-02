using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CompanyDetailforAdmin : Page
{
    string 資料夾路徑 = "~/pic/";
    SqlDataSource x = new SqlDataSource();
    logincheckDataContext db = new logincheckDataContext();
    //先假設用
    string companyId = "";
    string counterId = "";
    int facilityID;

    string oldBuild = "";
    string oldLayer = "";
    string oldCounter = "";

    string strBuild = "";
    string strLayer = "";
    string strCounter = "";


    HashSet<int> intHashSet = new HashSet<int>();
    HashSet<string> strHashSet = new HashSet<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        companyId = Request.QueryString["id"];
        if (string.IsNullOrEmpty(companyId))
            Response.Redirect("~/windowAdmin/companyList.aspx");

        try
        {
            //先判斷身分
            counterId = Request.QueryString["counter"];

            if (!string.IsNullOrEmpty(counterId) && //適用已配櫃廠商
                !string.IsNullOrEmpty(Session[CDictionary.SESSION_COUNTER].ToString()))
                counterId = Session[CDictionary.SESSION_COUNTER].ToString();
        }
        catch (Exception) { }

        if (!Page.IsPostBack) //第一次登入網頁
        {

            //沒有櫃位 就換按鈕
            if (hasCounter())
                loadCurrentCounter();
            else
            {
                getOptionsAndloadDropList();
                btnChangeCounter.Text = "確定新增";
            }

            loadCompany();
        }
    }

    //換櫃位
    protected void btnChangeCounter_Click(object sender, EventArgs e)
    {
        string btnName = (sender as Button).Text;

        //釋放櫃位 改權限為0
        if (btnName.Equals("撤櫃(換櫃)"))
        {
            updateCounter("撤櫃(換櫃)", buildlist.Text, layerlist.Text, counterlist.Text, 1);
            getOptionsAndloadDropList();//打開下拉選單
            btnChangeCounter.Text = "確定新增";
        }
        //廠商進駐櫃位 改權限為0 if (btnName.Equals("確定新增"))
        else
        {
            if (hasCounterInDropList())
            {
                updateCounter(companyId, buildlist.Text, layerlist.Text, counterlist.Text, 0);
                reloadCounterDropList();
                btnChangeCounter.Text = "撤櫃(換櫃)";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('更新成功')", true);
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!checkEmailAndTxtBox())
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('更新失敗，請檢查欄位')", true);
            return;
        }


        updateCompany();

        if (btnChangeCounter.Text.Equals("確定新增") && hasCounterInDropList())
        {
            updateCounter(companyId, buildlist.Text, layerlist.Text, counterlist.Text, 1);
            reloadCounterDropList();
        }
        Session[CDictionary.SESSION_COUNTER] = "";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('更新成功')", true);
    }

    //刪除廠商
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //先把廠商的櫃位刪除
        var counter = from row in db.Counters
                      where row.companyID.Equals(companyId)
                      select row;
        foreach (Counters c in counter)
        {
            updateCounter("撤櫃(換櫃)", c.building, c.layer.ToString(), c.counterNum.ToString(), 1);
        }
        if (companyId == "C0001")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " ", "alert('此帳號為Admin，不可以刪．')", true);
            return;
        }
        deletePromotions(); //刪活動
        deleteCompany(); //刪廠商
        //deleteAccont(); //刪帳號
        Session[CDictionary.SESSION_COUNTER] = "";
        Response.Redirect("~/windowAdmin/companyList.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(counterId))
        {
            updateCounter(companyId,
                      Session[CDictionary.SESSION__OLD_BUILD].ToString(),
                      Session[CDictionary.SESSION__OLD_LAYER].ToString(),
                      Session[CDictionary.SESSION__OLD_COUNTER].ToString(),
                      0);
        }
        Response.Redirect(Request.Url.ToString());
    }


    protected void comTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        getFacilityID(comTypeList.Text);
        loadBuild(facilityID);
        loadLayer(buildlist.Text);
        loadCounter(buildlist.Text, layerlist.Text);
    }


    protected void buildlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getFacilityID(comTypeList.Text);
        loadLayer(buildlist.Text);
        loadCounter(buildlist.Text, layerlist.Text);
    }

    protected void layerlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getFacilityID(comTypeList.Text);
        loadCounter(buildlist.Text, layerlist.Text);
    }


    /// <summary>
    /// 下拉選單相關方法
    /// </summary>

    //開啟閒置櫃位的 下拉選單openCounterOption
    private void getOptionsAndloadDropList()
    {
        loadFacility();
        getFacilityID(comTypeList.Text);
        loadBuild(facilityID);
        loadLayer(buildlist.Text);
        loadCounter(buildlist.Text, layerlist.Text);
    }

    //設施
    private void loadFacility()
    {
        comTypeList.Items.Clear();

        var comType = from row in db.Facility
                      select row;
        foreach (Facility cust in comType)
        {
            if (cust.facilityID < 4)
                comTypeList.Items.Add(cust.name);
        }
    }
    //設施ID
    private void getFacilityID(string facility)
    {
        this.facilityID = db.Facility.FirstOrDefault(m => m.name == facility).facilityID;
        Session[CDictionary.SESSION__PICK_FACILITY] = this.facilityID;
    }

    //建築
    private void loadBuild(int _facilityID)
    {
        lblHidden.Visible = false;
        strHashSet.Clear();
        buildlist.Items.Clear();

        var build = from row in db.Counters
                    where row.facilityID.Equals(_facilityID)
                    select row;
        foreach (Counters cust in build)
        {
            if (!strHashSet.Contains(cust.building))
            {
                strHashSet.Add(cust.building);
                buildlist.Items.Add(cust.building);
            }
        }
        try
        {
            Session[CDictionary.SESSION__PICK_BUILD] = buildlist.Items[0];
        }
        catch (Exception)
        {
            lblHidden.Text = "此為戶外區";
            lblHidden.Visible = true;
            //btnClear.Text = "查看記錄";
            lblCounter.Visible = false;
        }
    }

    //樓層
    private void loadLayer(string build)
    {
        lblHidden.Visible = false;
        intHashSet.Clear();
        layerlist.Items.Clear();

        var lay = from row in db.Counters
                  where row.building.Equals(build)
                  select row;

        foreach (Counters cust in lay)
        {
            if (!intHashSet.Contains(cust.layer) && cust.building == build && cust.facilityID == this.facilityID)
            {
                intHashSet.Add(cust.layer);
                layerlist.Items.Add(cust.layer.ToString());
            }
        }
        try
        {
            layerlist.Items[0].ToString();
        }
        catch (Exception)
        {
            lblHidden.Text = "此大樓僅一樓";
            lblHidden.Visible = true;

            //btnClear.Text = "查看記錄";
            lblCounter.Visible = false;
        }

    }

    //櫃位
    private void loadCounter(string build, string layer)
    {
        lblHidden.Visible = false;
        counterlist.Items.Clear();

        var countnum = from row in db.Counters
                       select row;
        foreach (Counters cust in countnum)
        {
            //櫃位可用才加
            if (cust.building == build && cust.layer.ToString() == layer &&
                (bool)cust.available && cust.facilityID == this.facilityID)
                counterlist.Items.Add(cust.counterNum.ToString());
        }
        try
        {
            counterlist.Items[0].ToString();
        }
        catch (Exception)
        {
            lblHidden.Text = "目前此樓層已無櫃位";
            lblHidden.Visible = true;

            //btnClear.Text = "查看記錄";
            lblCounter.Visible = false;
        }
    }

    /// <summary>
    /// 更新/刪除區
    /// </summary>
    /// 

    //更新櫃位(不用設施ID)
    private void updateCounter(string companyID, string build, string layer, string counter, int bit)
    {
        x.ConnectionString = CDictionary.CONN_URL;

        string sql = "UPDATE Counters set";
        if (companyID.Equals("撤櫃(換櫃)"))
            sql += " companyID=NULL,";
        else
            sql += " companyID='" + companyID + "',";
        sql += " available='" + bit + "'";
        sql += " WHERE building=N'" + build + "' and";
        sql += " layer=N'" + layer + "' and";
        sql += " counterNum=N'" + counter + "'";

        x.UpdateCommand = sql;
        x.Update();

        setSession();
        if (!companyID.Equals("撤櫃(換櫃)"))
            Response.Redirect("~/windowAdmin/CompanyDetailforAdmin.aspx?id=" + companyId + "&counter=" + counterId);

    }
    //更新基本資料
    private void updateCompany()
    {
        //壓縮圖片並存檔
        CImageProess.get().UploadImage(FileUpload1, 資料夾路徑, CDictionary.SET_BRAND_PX);

        x.ConnectionString = CDictionary.CONN_URL;

        string sql = "UPDATE Company set";
        sql += " name=N'" + txtName.Text + "',";
        sql += " manager=N'" + txtManager.Text + "',";
        sql += " tel=N'" + txtPhone.Text + "',";
        sql += " addr=N'" + txtAddress.Text + "',";
        if (FileUpload1.HasFile)
            sql += " picBrandPath='" + CImageProess.imageName + "', ";
        sql += " content=N'" + txtContent.Text + "'";
        sql += " WHERE email='" + txtEmail.Text + "'";

        x.UpdateCommand = sql;
        x.Update();
    }

    //刪除促銷活動
    private void deletePromotions()
    {
        x.ConnectionString = CDictionary.CONN_URL;

        string sql = "Delete Promotions WHERE";
        sql += " companyID='" + companyId + "'";

        x.UpdateCommand = sql;
        x.Update();
    }
    //刪除基本資料
    private void deleteCompany()
    {
        x.ConnectionString = CDictionary.CONN_URL;

        string sql = "Delete From Company WHERE";
        sql += " companyID='" + companyId + "'";

        x.UpdateCommand = sql;
        x.Update();
    }
    //刪除帳號
    private void deleteAccont()
    {
        x.ConnectionString = CDictionary.CONN_URL;

        string sql = "DELETE FROM account WHERE";
        sql += " account='" + txtEmail.Text + "'";

        x.UpdateCommand = sql;
        x.Update();
    }



    /// <summary>
    /// 查水表區
    /// </summary>

    //基本資料
    private void loadCompany()
    {
        x.ConnectionString = CDictionary.CONN_URL;
        x.SelectCommand = "Select * from Company where companyID='" + companyId + "'";

        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;

        txtName.Text = dv.Table.Rows[0]["name"].ToString();
        txtEmail.Text = dv.Table.Rows[0]["email"].ToString();
        txtSpareEmail.Text = dv.Table.Rows[0]["sparEmail"].ToString();
        txtManager.Text = dv.Table.Rows[0]["manager"].ToString();
        txtPhone.Text = dv.Table.Rows[0]["tel"].ToString();
        txtAddress.Text = dv.Table.Rows[0]["addr"].ToString();
        txtContent.Text = dv.Table.Rows[0]["content"].ToString();
        picUP.ImageUrl = "~/pic/" + dv.Table.Rows[0]["picBrandPath"].ToString();
    }

    //載入目前櫃位到下拉選單
    private void loadCurrentCounter()
    {
        clearDropList();
        string[] sp = counterId.Split(new Char[] { '-' });
        oldBuild = sp[0];
        oldLayer = sp[1];
        oldCounter = sp[2];

        buildlist.Items.Add(oldBuild);
        layerlist.Items.Add(oldLayer);
        counterlist.Items.Add(oldCounter);

        facilityID = (int)db.Counters.FirstOrDefault(m => m.building.Equals(oldBuild) &&
                                                          m.layer.Equals(oldLayer) &&
                                                          m.counterNum.Equals(oldCounter)).facilityID;

        comTypeList.Items.Add(db.Facility.FirstOrDefault(m => m.facilityID == facilityID).name);

        setSession();
    }

    private void reloadCounterDropList()
    {
        strBuild = buildlist.Text;
        strLayer = layerlist.Text;
        strCounter = counterlist.Text;

        clearDropList();

        buildlist.Items.Add(strBuild);
        layerlist.Items.Add(strLayer);
        counterlist.Items.Add(strCounter);

        setSession();
    }


    //判斷此廠商有無櫃位
    private bool hasCounter()
    {
        if (db.Counters.FirstOrDefault(m => m.companyID == companyId) == null)
            return false;

        return true;
    }

    //確認下拉選單有沒有櫃位
    private bool hasCounterInDropList()
    {
        if (buildlist.Text == "" ||
            layerlist.Text == "" ||
            counterlist.Text == "")
            return false;

        return true;
    }

    private void clearDropList()
    {
        buildlist.Items.Clear();
        layerlist.Items.Clear();
        counterlist.Items.Clear();
    }


    //紀錄櫃位
    private void setSession()
    {
        Session[CDictionary.SESSION_COUNTER] = buildlist.Text + "-" + layerlist.Text + "-" + counterlist.Text;
        counterId = Session[CDictionary.SESSION_COUNTER].ToString();
        Session[CDictionary.SESSION__OLD_BUILD] = buildlist.Text;
        Session[CDictionary.SESSION__OLD_LAYER] = layerlist.Text;
        Session[CDictionary.SESSION__OLD_COUNTER] = counterlist.Text;
    }


    private bool checkEmailAndTxtBox()
    {
        //判斷格式
        if (!reg備用信箱.IsValid)
            return false;


        if (txtSpareEmail.Text.Equals(txtEmail.Text))
        {
            lblMsg.Text = " *請勿與主要E-Mail重複";
            return false;
        }

        if (txtManager.Text == "")
            return false;


        return true;
    }
}