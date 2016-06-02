package info.androidhive.materialdesign.model;

/**
 * Created by iii on 2016/5/11.
 */
public class CCompany {
    private String CompanyName;//廠商名稱
    private String CompanyID;//廠商id
    private String content;//廠商簡介
    private String addr;//館內位置
    private String picLocation;//地圖路徑
    private String picBrandPath;//商標路徑

    public CCompany(String CompanyName,String CompanyID,String content,String addr,String picLocation,String picBrandPath)
    {
        this.CompanyName = CompanyName;
        this.CompanyID = CompanyID;
        this.content = content;
        this.addr = addr;
        this.picLocation = picLocation;
        this.picBrandPath = picBrandPath;
    }


    public String getCompanyName() {return CompanyName;}
    public void setCompanyName(String companyName) {CompanyName = companyName;}
    public String getCompanyID() {return CompanyID;}
    public void setCompanyID(String companyID) {CompanyID = companyID;}
    public String getContent() {return content;}
    public void setContent(String content) {this.content = content;}
    public String getAddr() {return addr;}
    public void setAddr(String addr) {this.addr = addr;}
    public String getPicBrandPath() {return picBrandPath;}
    public void setPicBrandPath(String picBrandPath) {this.picBrandPath = picBrandPath;}
    public String getPicLocation() {return picLocation;}
    public void setPicLocation(String picLocation) {this.picLocation = picLocation;
}
}
