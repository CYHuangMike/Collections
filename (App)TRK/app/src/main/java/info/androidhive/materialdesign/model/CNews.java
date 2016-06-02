package info.androidhive.materialdesign.model;

/**
 * Created by iii on 2016/5/11.
 */
public class CNews {
    private String NCompanyName;//廠商名稱
    private String NID;//最新消息id
    private String NTitle;//最新消息標題
    private String NContent;//最新消息內容
    private String NStartDate;//開始日期
    private String NEndDate;//結束日期
    private String NPicPath;//圖片路徑
    private String NClass;//最新消息類別

    public CNews(String NCompanyName, String NID, String NTitle, String NContent, String NStartDate, String NEndDate,String NPicPath,String NClass)
    {
        this.NCompanyName = NCompanyName;
        this.NID = NID;
        this.NTitle = NTitle;
        this.NContent = NContent;
        this.NStartDate = NStartDate;
        this.NEndDate = NEndDate;
        this.NPicPath=NPicPath;
        this.NClass=NClass;
    }

    public String getNCompanyName() {return NCompanyName;}
    public void setNCompanyName(String NCompanyName) {this.NCompanyName = NCompanyName;}
    public String getNID() {return NID;}
    public void setNID(String NID) {this.NID = NID;}
    public String getNTitle() {return NTitle;}
    public void setNTitle(String NTitle) {this.NTitle = NTitle;}
    public String getNContent() {return NContent;}
    public void setNContent(String NContent) {this.NContent = NContent;}
    public String getNStartDate() {return NStartDate;}
    public void setNStartDate(String NStartDate) {this.NStartDate = NStartDate;}
    public String getNEndDate() {return NEndDate;}
    public void setNEndDate(String NEndDate) {this.NEndDate = NEndDate;}
    public String getNPicPath() {return NPicPath;}
    public void setNPicPath(String nPicPath) {NPicPath = nPicPath;}
    public String getNClass() {return NClass;}
    public void setNClass(String NClass) {this.NClass = NClass;}
}
