package info.androidhive.materialdesign.activity.Tools;

import android.app.Activity;
import android.util.DisplayMetrics;
import android.util.Log;

/**
 * 取得當前裝置螢幕的 高/寬 像素 和 換算比例
 */
public class CScreen {
    public static float ScaleWidthPixels;
    public static float ScaleHeightPixels;
    public static void getScreenInch(Activity activity)
    {
        //螢幕的像素是根據DisplayMetrics類來得到的
        DisplayMetrics dm = new DisplayMetrics();
        activity.getWindowManager().getDefaultDisplay().getMetrics(dm);
        //手機的寬度(像素)
        float widthPixels= dm.widthPixels;
        //手機的高度(像素)
        float heightPixels= dm.heightPixels;


        // 換算長寬比例 目前使用模擬器Nexus5 1080x1776
        // 座標X就乘以寬比例, 座標Y就乘以長比例
        ScaleWidthPixels = 1080/widthPixels;
        ScaleHeightPixels = 1776/heightPixels;

    }
}
