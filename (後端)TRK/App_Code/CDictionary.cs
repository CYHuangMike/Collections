using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// CDictionary 的摘要描述
/// </summary>
public class CDictionary
{
    public static string CONN_URL = ConfigurationManager.ConnectionStrings["prjTRKConn"].ConnectionString;
    public static string SESSION_COMPANY = "SESSION_COMPANY";
    public static string SESSION_ADMIN = "SESSION_ADMIN";
    public static string SESSION_ACTIVITY_ID = "SESSION_ACTIVITY_ID";
    public static string SESSION_ACTIVITY_NEWS_ID = "SESSION_ACTIVITY_NEWS_ID";
    public static string SESSION_BUILD = "SESSION_BUILD";
    public static string SESSION_LAYER = "SESSION_LAYER";
    public static string SESSION_COUNTER = "SESSION_COUNTER";
    public static string SESSION__PICK_BUILD = "SESSION__PICK_BUILD";
    public static string SESSION__PICK_LAYER = "SESSION__PICK_LAYER";
    public static string SESSION__PICK_COUNTER = "SESSION__PICK_COUNTER";
    public static string SESSION__PICK_FACILITY = "SESSION__PICK_FACILITY";
    public static string SESSION__OLD_BUILD = "SESSION__OLD_BUILD";
    public static string SESSION__OLD_LAYER = "SESSION__OLD_LAYER";
    public static string SESSION__OLD_COUNTER = "SESSION__OLD_COUNTER";   
    public static string SESSION_CHANGED= "SESSION_CHANGED";
    public static string SESSION_FACILITY_ID = "SESSION_FACILITY_ID";
    public static string SESSION_COUPON_ID= "SESSION_COUPON_ID";
    public static string SESSION_PASSWORDCHECK = "SESSION_PASSWORDCHECK";
    public static string SESSION_COMPANYACCOUNT = "SESSION_COMPANYACCOUNT";


    //設定圖檔PX
    public static int SET_BRAND_PX = 300; //商標
    public static int SET_FACILITY_PX = 200; //戶外設施
    public static int SET_PROMOTION_PX = 500; //促銷活動 最新消息
    
}