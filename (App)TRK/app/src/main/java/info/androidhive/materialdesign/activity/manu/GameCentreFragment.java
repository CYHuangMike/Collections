package info.androidhive.materialdesign.activity.manu;

import android.app.AlertDialog;
import android.content.ContentValues;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.net.Uri;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import info.androidhive.materialdesign.Factorys.CCouponFactory;
import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.CDbManager;
import info.androidhive.materialdesign.activity.Tools.MainActivity;
import info.androidhive.materialdesign.model.CCoupon;

public class GameCentreFragment extends Fragment {

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        //利用inflater物件的inflate()方法取得介面佈局檔,並將最後的結果傳回給系統
        return inflater.inflate(R.layout.fragment_gamecentre, container, false);

    }

    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        //當Fragment底層的Activity被建立時會被執行的方法
        //在這方法裡取得Fragment的介面元件, 如同Activity的onCreate()一樣
        super.onActivityCreated(savedInstanceState);
        ((AppCompatActivity) getActivity()).getSupportActionBar().setTitle("飄移賽車");
        InitialComponent();

    }
    CCouponFactory factory = new CCouponFactory();
    CDbManager db;
    public boolean HadCouponID(int couponID) {
        if (db == null)
            db= new CDbManager(getActivity());
        //想辦法判斷這個建築物已經領過!
        Cursor table = db.GetBySql(" SELECT * FROM tCoupon");
        if (table.getCount() > 0) {
            for (CCoupon cou : db.GetAllExitedCoupon()) {
                Log.d("QRCode", "couponID:" + cou.getCouponID());
                if (couponID == cou.getCouponID())
                    return true;
            }
        }
        return false;
    }
    private void GetCoupon(int couponID)
    {

        // 判斷有沒有領過該Coupon
        if (HadCouponID(couponID)) {
            Toast.makeText(getActivity(), "優惠券已經領過囉! 可至「我的優惠券」查看．",
                    Toast.LENGTH_SHORT).show();
            return;
        }
        // 從資料庫載入該筆資料
        factory.LoadData(couponID, getActivity());
        Log.d("遊戲中心", "loaddata");
        // 載完
        while (!factory.Getflag()) ;
        factory.FindCouponById(couponID);

        InsertCouponToSQLite(couponID);
        // 彈跳視窗
        new AlertDialog.Builder(getActivity())
                .setTitle("輸入成功")
                .setMessage("可至「我的優惠券」查看。")
                .setPositiveButton("確定", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        Toast.makeText(getActivity().getApplicationContext(), "領取優惠券成功",
                                Toast.LENGTH_SHORT).show();
                    }
                }).show();
    }
    public void InsertCouponToSQLite(int couponID) {
        ContentValues cv = new ContentValues();
        cv.put("fFacilityID", factory.GetCurrent().getFacilityID());
        Log.d("資料", String.valueOf(factory.GetCurrent().getFacilityID()));
        cv.put("fFacility", factory.GetCurrent().getFacility());
        Log.d("資料", factory.GetCurrent().getFacility());
        cv.put("fCouponId", factory.GetCurrent().getCouponID());
        Log.d("資料", String.valueOf(factory.GetCurrent().getCouponID()));
        cv.put("fTitle", factory.GetCurrent().getTitle());
        Log.d("資料", factory.GetCurrent().getTitle());
        cv.put("fContent", factory.GetCurrent().getContent());
        Log.d("資料", factory.GetCurrent().getContent());
        cv.put("fQrCode", factory.GetCurrent().getQrCode());
        cv.put("fUsed", "false");
        db.Create("tCoupon", cv);
    }
    private View.OnClickListener btnRacing_Click = new View.OnClickListener() {


        public void onClick(View v) {
            Intent intent = getActivity().getPackageManager().getLaunchIntentForPackage("com.III.crossyRoad");
            if (intent == null) {
                intent = new Intent(Intent.ACTION_VIEW);
                intent.setData(Uri.parse("https://drive.google.com/uc?export=download&id=0B5liOdkF07NJTXhFX01qYjVuTFU"));
            }
            intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            getActivity().startActivity(intent);
        }
    };

    int id = 10030;
    private View.OnClickListener button_Click = new View.OnClickListener() {
        public void onClick(View v) {
            if ("WELCOMETOTRK".equals(editText.getText().toString())){
                Log.d("遊戲中心","比對成功");
                GetCoupon(id);

            }else {
                Toast.makeText(getActivity(), "輸入錯誤兌換碼!",
                        Toast.LENGTH_SHORT).show();
                return;
            }
        }
    };


    private void InitialComponent() {
        //注意, Fragment必須透過getView取得介面元件
        btnRacing = (Button) getView().findViewById(R.id.btnRacing);
        btnRacing.setOnClickListener(btnRacing_Click);
        button = (Button) getView().findViewById(R.id.button);
        button.setOnClickListener(button_Click);
        editText =(EditText) getView().findViewById(R.id.editText);
    }

    private Button btnRacing;
    private Button button;
    private EditText editText;
}

