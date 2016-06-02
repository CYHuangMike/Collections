package info.androidhive.materialdesign.model;


public class CCoupon {
    private int facilityID;
    private String facility;
    private  int couponID;
    private String title;
    private String content;
    private String qrCode;


    public CCoupon(){}

    public CCoupon(int facilityID, String facility, int couponID, String title, String content, String qrCode) {
        this.facilityID = facilityID;
        this.facility = facility;
        this.couponID = couponID;
        this.title = title;
        this.content = content;
        this.qrCode = qrCode;
    }

    public int getFacilityID() {
        return facilityID;
    }

    public void setFacilityID(int facilityID) {
        this.facilityID = facilityID;
    }

    public String getFacility() {
        return facility;
    }

    public void setFacility(String facility) {
        this.facility = facility;
    }

    public int getCouponID() {
        return couponID;
    }

    public void setCouponID(int couponID) {
        this.couponID = couponID;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public String getQrCode() {
        return qrCode;
    }

    public void setQrCode(String qrCode) {
        this.qrCode = qrCode;
    }

}
