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
    SqlDataSource x = new SqlDataSource();
    logincheckDataContext db = new logincheckDataContext();

    string companyID = "";
    string 資料夾路徑 = "~/pic/";

    int facilityID;
    string sql = "";

    List<string> arrayBuild = new List<string>();
    List<string> arrayLayer = new List<string>();
    List<string> arrayCounter = new List<string>();

    HashSet<int> intHashSet = new HashSet<int>();
    HashSet<string> strHashSet = new HashSet<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session[CDictionary.SESSION_ADMIN] == null)
                Response.Redirect("~/Sign.aspx");

            lblMsg.Visible = false;
            loadDropList();
            clearSession();
        }
    }

    protected void btnAddCounter_Click(object sender, EventArgs e)
    {
        getCounters();
    }

    protected void btnCheckAccount_Click(object sender, EventArgs e)
    {
        if (checkEmail())
            hasAccount();        
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (checkTxtBox())
        {
            getFacilityID(comTypeList.Text);
            createCompanyID();
            if (!hasAccount())
                insertAccount();
            insertCompany();
            updateCounter();
            clearSession();
            Response.Redirect("~/windowAdmin/companyList.aspx");
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        if ((sender as Button).Text == "清空記錄")
        {
            clearSession();
            lblCounter.Visible = false;
        }
        if ((sender as Button).Text == "查看記錄")
        {
            lblHidden.Visible = false;
            if (!hasCounters())
                loadDropList();
            showCounters();
            btnClear.Text = "清空記錄";
        }
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

    //一開始load出下拉選單
    private void loadDropList()
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
            btnClear.Text = "查看記錄";
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

            btnClear.Text = "查看記錄";
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

            btnClear.Text = "查看記錄";
            lblCounter.Visible = false;
        }
    }



    /// <summary>
    /// 櫃位處理方法
    /// </summary>


    //更新資料庫櫃位
    private void updateCounter()
    {
        string strBuild = "";
        string strLayer = "";
        string strCounter = "";


        getCounters();
        x.ConnectionString = CDictionary.CONN_URL;
        for (int i = 0; i < arrayBuild.Count; i++)
        {
            if (arrayBuild[i] != null)
            {
                strBuild = arrayBuild[i];
                strLayer = arrayLayer[i].ToString();
                strCounter = arrayCounter[i].ToString();
                sql = "";

                sql = " UPDATE Counters SET ";
                sql += " companyID=N'C" + companyID + "',";
                sql += " available='0' ";
                sql += " WHERE building=N'" + strBuild + "' AND "; //櫃位出租為false
                sql += " layer='" + strLayer + "' AND ";
                sql += " counterNum='" + strCounter + "'";

                x.UpdateCommand = sql;
                x.Update();
            }
        }

    }


    //從Session得到櫃位
    private void getCounters()
    {
        lblCounter.Visible = false;
        strHashSet.Clear();

        getSession();
        showCounters();

    }

    private void showCounters()
    {
        string 已添加櫃位 = "";
        string[] 建築物 = Session[CDictionary.SESSION_BUILD].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        string[] 樓 = Session[CDictionary.SESSION_LAYER].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        string[] 號碼 = Session[CDictionary.SESSION_COUNTER].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < 建築物.Length; i++)
        {
            已添加櫃位 = 建築物[i] + 樓[i] + 號碼[i];
            if (!strHashSet.Contains(已添加櫃位))
            {
                strHashSet.Add(已添加櫃位);
                arrayBuild.Add(建築物[i]);
                arrayLayer.Add(樓[i]);
                arrayCounter.Add(號碼[i]);
            }
        }
        lblCounter.Text = "目前新增櫃位：";
        for (int i = 0; i < arrayBuild.Count; i++)
        {

            lblCounter.Text += " [" + arrayBuild[i] + arrayLayer[i] + arrayCounter[i] + "] ";
            //lblHidden.Text += arrayBuild[i] + arrayLayer[i] + arrayCounter[i];
        }
        if (hasCounters())
            lblCounter.Visible = true;
    }

    private bool hasSessionCounter()
    {
        try
        {
            if (Session[CDictionary.SESSION_BUILD].ToString() == null ||
            Session[CDictionary.SESSION_BUILD].ToString() == "")
                return false;
        }
        catch (Exception)
        {
            return false;
        }


        return true;
    }

    private void getSession()
    {
        if (hasCounters())
        {
            //用Session記錄
            Session[CDictionary.SESSION_BUILD] += buildlist.Text + ",";
            Session[CDictionary.SESSION_LAYER] += layerlist.Text + ",";
            Session[CDictionary.SESSION_COUNTER] += counterlist.Text + ",";
        }

    }

    //先確認還有沒有櫃位
    private bool hasCounters()
    {
        if (buildlist.Text == "" ||
            layerlist.Text == "" ||
            counterlist.Text == "")
            return false;

        return true;
    }



    /// <summary>
    /// 新增資料庫
    /// </summary>

    //確認有無重複主鍵
    private bool checkEmail()
    {
        //判斷格式
        if (!reg主郵件.IsValid || !reg備用郵件.IsValid)
        {
            lblMsg.Text = "";
            return false;
        }

        //判斷有無值
        if (txtEmail.Text == "")
        {
            lblMsg.Text = "請輸入Email!!";
            lblMsg.Visible = true;
            return false;
        }

        //FirstOrDefault預設null
        if (db.Company.FirstOrDefault(m => m.email.Equals(txtEmail.Text)) != null)
        {
            lblMsg.Text = "此Email已註冊過!!";
            lblMsg.Visible = true;
            return false;
        }

        return true;
    }

    private bool hasAccount()
    {
        //判斷主鍵    
        var accounts = from row in db.Account
                       select row;
        foreach (var acc in accounts)
        {
            if (acc.account1 == txtEmail.Text)
            {
                lblMsg.Text = "此為舊廠商, 可新增資料!!";
                lblMsg.Visible = true;
                return true;
            }
        }

        lblMsg.Text = "通過";
        lblMsg.Visible = true;
        return false;
    }

    //生成companyID
    private void createCompanyID()
    {
        x.ConnectionString = CDictionary.CONN_URL;
        x.SelectCommand = "SELECT TOP 1 companyID FROM Company ORDER BY companyID DESC ";
        DataView dv = x.Select(DataSourceSelectArguments.Empty) as DataView;
        string lastID = dv.Table.Rows[0]["companyID"].ToString();

        string[] split = lastID.Split(new char[] { 'C' });

        lastID = "";
        foreach (string item in split)
            lastID += item;

        lastID = (int.Parse(lastID) + 1).ToString();
        companyID = lastID.PadLeft(4, '0');
    }

    //新增Account
    private void insertAccount()
    {
        checkEmail();

        x.ConnectionString = CDictionary.CONN_URL;
        sql = "";

        sql = "Insert into Account (account,password,authority) Values ";

        sql += " (N'" + txtEmail.Text + "',";
        sql += " N'111',";
        sql += " N'0')";
        try
        {
            x.InsertCommand = sql;
            x.Insert();
        }
        catch (Exception e)
        {
            string p = e.ToString();
        }
    }

    private bool checkTxtBox()
    {

        if (txtName.Text.Equals("") ||
            txtManager.Text.Equals(""))
            return false;

        return true;
    }

    //有帳號, id, 櫃位 就可以 新增廠商
    private void insertCompany()
    {
        //壓縮圖片並存檔
        CImageProess.get().UploadImage(FileUpload1, 資料夾路徑, CDictionary.SET_BRAND_PX);

        x.ConnectionString = CDictionary.CONN_URL;
        sql = "";
        sql = "Insert into Company (companyID,name,manager,tel,email,sparEmail,addr,picBrandPath,content,facilityID) Values ";

        sql += "(N'C" + companyID + "',";
        sql += " N'" + txtName.Text + "',";
        sql += " N'" + txtManager.Text + "',";
        sql += " N'" + txtPhone.Text + "',";
        sql += " N'" + txtEmail.Text + "',";
        sql += " N'" + txtSpareEmail.Text + "',";
        sql += " N'" + txtAddress.Text + "',";
        sql += "'" + CImageProess.imageName + "',";
        sql += " N'" + txtContent.Text + "',";
        sql += " N'" + facilityID + "')";

        x.InsertCommand = sql;
        x.Insert();

    }


    private void clearSession()
    {
        Session[CDictionary.SESSION_BUILD] = "";
        Session[CDictionary.SESSION_LAYER] = "";
        Session[CDictionary.SESSION_COUNTER] = "";
    }
}