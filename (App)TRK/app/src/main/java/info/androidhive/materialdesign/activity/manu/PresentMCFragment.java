package info.androidhive.materialdesign.activity.manu;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;

import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.CDbManager;
import info.androidhive.materialdesign.activity.Tools.JsonUrl;

import info.androidhive.materialdesign.activity.Tools.MyThread;
import info.androidhive.materialdesign.model.CCoupon;

/**
 * Created by iii on 2016/4/26.
 */
public class PresentMCFragment extends Fragment {
    CDbManager db;
    String picPath = "";
    Boolean loadQrCodeFlag;
    Bitmap qrCodePic;
    CCoupon clickCoupon;
    CouponThread th;
    ProgressDialog progressDialog; //延遲等待
    StringBuffer buffer;
    String strJSON;
    HttpURLConnection conn = null;
    BufferedReader reader;
    private List<CCoupon> myCoupon = new ArrayList<CCoupon>();
    private List<Bitmap> bitmapList = new ArrayList<Bitmap>();

    public PresentMCFragment() {
        // Required empty public constructor
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        //利用inflater物件的inflate()方法取得介面佈局檔,並將最後的結果傳回給系統
        return inflater.inflate(R.layout.fragment_present, container, false);
    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        InitialCompoent();
        if (db == null) {
            db = new CDbManager(getActivity());
        }
        Cursor table = db.GetBySql(" SELECT * FROM tCoupon");
        if (!(table.getCount() > 0)) {
            Toast.makeText(getActivity().getApplicationContext(), "目前還沒有優惠券",
                    Toast.LENGTH_SHORT).show();
            return;
        }
        if (list != null){
            list.setAdapter(null);
        }
        if (th == null){th = new CouponThread();}
        th.start();
        registerCallBack(); // 點擊
    }

    @Override
    public void onDetach() {
        super.onDetach();
    }

    /// <Summary>
    /// 執行續類別
    /// </Summary>
    public class CouponThread implements Runnable {
        private Thread t;
        public void download() {
            buffer = null;
            bitmapList.clear();

            for (CCoupon cou : db.GetAllExitedCoupon()) {
                myCoupon.add(cou);

                String MemURL = JsonUrl.url + "/JSON/Facility.aspx?id=" + cou.getFacilityID();
                Log.d("url", MemURL);

                try {
                    URL url = new URL(MemURL);//撈網址
                    HttpURLConnection connection = (HttpURLConnection) url.openConnection();
                    connection.connect();//
                    InputStream stream = connection.getInputStream();
                    reader = new BufferedReader(new InputStreamReader(stream));
                    buffer = new StringBuffer();
                    String line;
                    while ((line = reader.readLine()) != null) {
                        buffer.append(line);
                    }
                    strJSON = buffer.toString();
                    picPath = new JSONObject(strJSON).getString("pic"); //圖片路徑

                    //撈設施圖片
                    url = new URL(JsonUrl.url + "/mPic/" + picPath);
                    Log.d("url", JsonUrl.url + "/mPic/" + picPath);
                    conn = (HttpURLConnection) url.openConnection();
                    InputStream streamIn = conn.getInputStream();
                    bitmapList.add(BitmapFactory.decodeStream(streamIn));
                    Log.d("bitmap", "" + BitmapFactory.decodeStream(streamIn));

                } catch (JSONException e) {
                    e.printStackTrace();
                } catch (MalformedURLException e) {
                    e.printStackTrace();
                } catch (IOException e) {
                    e.printStackTrace();
                } finally {
                    if (conn != null) {
                        conn.disconnect();
                    }
                    try {
                        if (reader != null) {
                            reader.close();
                        }
                    } catch (IOException e) {
                        e.printStackTrace();
                    }

                    th.stop();//執行續停止
                    progressDialog.dismiss();//關閉等待
                    myHandler.sendEmptyMessage(1);
                }
            }
        }

        public void start() {
            if (t == null) {
                progressDialog = ProgressDialog.show(getActivity(), "請稍候", "資料載入中請稍候");//開啟等待
                t = new Thread(this);
                t.start();
            }
        }

        public void stop() {
            if (t != null) {
                t = null;
            }
        }

        public void run() {
            download();
        }
    }

    Handler myHandler = new Handler() {
        @Override
        public void handleMessage(android.os.Message msg) {
            // TODO Auto-generated method stub
            if (msg.what == 1) {
                couponListView(); // 生成關鍵
                Log.d("清空", "生成");
            } else if (msg.what == 2) {
                while (true) {
                    if (LoadingCheck()) {
                        break;
                    }
                }
                imgQrcode.setImageBitmap(qrCodePic);
            }
            super.handleMessage(msg);
        }
    };

    // 轉接器接上
    private void couponListView() {
        ArrayAdapter<CCoupon> adapter = new couponAdapter(); //外部類別
        list.setAdapter(adapter);
    }

    private void registerCallBack() {
        list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View viewClisked, int position, long id) {
                clickCoupon = myCoupon.get(position);

                LayoutInflater inflater = LayoutInflater.from(getActivity());
                View myView = inflater.inflate(R.layout.qrcode, null);

                ((TextView) myView.findViewById(R.id.txtTitle)).setText(clickCoupon.getFacility());
                imgQrcode = (ImageView) myView.findViewById(R.id.imgQrcode);

                MyThread myThread = new MyThread() {
                    @Override
                    public void run() {
                        LoadQrCode(clickCoupon.getQrCode());
                        myHandler.sendEmptyMessage(2);
                        stop();
                        progressDialog.dismiss();//關閉等待
                    }
                };

                myThread.start(getActivity());
                new AlertDialog.Builder(getActivity())
                        .setView(myView)
                        .setPositiveButton("確定", null).show();
            }
        });
    }

    //撈QRCode圖片
    public void LoadQrCode(String qrCodePath) {
        try {
            loadQrCodeFlag = false;
            Log.d("urlCoupon", JsonUrl.url + "/mPic/" + qrCodePath);
            URL url = new URL(JsonUrl.url + "/mPic/" + qrCodePath);
            URLConnection conn = url.openConnection();
            InputStream streamIn = conn.getInputStream();
            qrCodePic = BitmapFactory.decodeStream(streamIn);
            loadQrCodeFlag = true;
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    //等待Load QrCode完成
    public Boolean LoadingCheck() {
        return loadQrCodeFlag;
    }

    /// <Summary>
    /// 內部類別轉接器
    /// </Summary>
    private class couponAdapter extends ArrayAdapter<CCoupon> {
        public couponAdapter() {
            super(getActivity(), R.layout.coupon_item, myCoupon);
        }
        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            //Make sure we have view to work with(may have been given null)
            View itemView = convertView;
            if (itemView == null)
                itemView = getActivity().getLayoutInflater().inflate(R.layout.coupon_item, parent, false);

            CCoupon currentCoupon = myCoupon.get(position);

            //Fill the view
            ImageView imageView = (ImageView) itemView.findViewById(R.id.backgroundImg);
            try {
                if (bitmapList.size() > 0)
                    imageView.setImageBitmap(bitmapList.get(position));
                Log.d("bitmap", "" + bitmapList.size());
            } catch (Exception e) {
                Log.d("Error", "優惠卷載圖" + e.toString());
            }
            TextView gift = (TextView) itemView.findViewById(R.id.gif);
            gift.setText(currentCoupon.getFacility() + currentCoupon.getContent());

            return itemView;
        }
    }


    private void InitialCompoent() {
        list = (ListView) getView().findViewById(R.id.listView);
    }
    ListView list;
    ImageView imgQrcode;


}
