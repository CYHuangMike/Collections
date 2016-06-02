package info.androidhive.materialdesign.Factorys;

import android.content.Context;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import info.androidhive.materialdesign.activity.Tools.MyThread;
import info.androidhive.materialdesign.model.CCoupon;

/**
 * Created by Ray on 2016/5/21.
 */
public class CCouponFactory {
    private int position;
    public ArrayList<CCoupon> list = new ArrayList<CCoupon>();
    private MyThread myThread;
    private String aspx = "Coupon.aspx";
    private String responseJson = "";
    private Boolean flag = false;

    public CCouponFactory() {
    }

    /// <Summary>
    /// 從資料庫撈資料並分成CCoupon物件裝到ArrayList裡以便呼叫
    /// </Summary>
    public void LoadData(final int couponId, Context context) {
        myThread = new MyThread() {

            @Override
            public void run() {
                downloadCoupon(couponId, aspx, responseJson);
            }

            @Override
            public void downloadCoupon(int couponId, String aspx, String ResponseJson) {
                super.downloadCoupon(couponId, aspx, ResponseJson);
            }

            @Override
            public void mJsonResult(String responseJson) throws JSONException {
                JSONObject obj;
                try {
                    obj = new JSONObject(responseJson);
                    String data = obj.getString("data");
                    Log.d("QRCode", "資料庫:" + data.toString());

                    JSONArray dataArray = new JSONArray(data);

                    list.clear();
                    for (int i = 0; i < dataArray.length(); i++) {
                        list.add(new CCoupon(
                                dataArray.getJSONObject(i).getInt("FacilityID"),
                                dataArray.getJSONObject(i).getString("Facility"),
                                dataArray.getJSONObject(i).getInt("CouponID"),
                                dataArray.getJSONObject(i).getString("Title"),
                                dataArray.getJSONObject(i).getString("Content"),
                                dataArray.getJSONObject(i).getString("QRCode"))
                        );

                        for (CCoupon a : list) {
                            Log.d("QRCode", String.valueOf(a.getCouponID()));
                        }
                        flag = true;
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        myThread.start(context);
    }

    // 用來判斷資料撈完了沒
    public Boolean Getflag() {
        return flag;
    }

    /// <Summary>
    /// 呼叫list資料的工具
    /// </Summary>
    public Boolean FindCouponById(int couponID) {
        int count = 0;
        for (CCoupon c : list) {
            if (c.getCouponID() == couponID) {
                position = count;
                return true;
            }
            count++;
        }
        return false;
    }

    public CCoupon GetCurrent() {
        return list.get(position);
    }

    public CCoupon[] GetAll() {
        return list.toArray(new CCoupon[list.size()]);
    }
}
