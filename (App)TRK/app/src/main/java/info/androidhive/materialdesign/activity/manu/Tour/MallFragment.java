package info.androidhive.materialdesign.activity.manu.Tour;

import android.app.Activity;
import android.app.AlertDialog;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;

import info.androidhive.materialdesign.Factorys.TourFactory;
import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.JsonUrl;
import info.androidhive.materialdesign.activity.Tools.MyThread;
import info.androidhive.materialdesign.model.CCompany;

/**
 * Created by Ravi on 29/07/15.
 */
public class MallFragment extends Fragment {


    Boolean LoadingCheck =false;
    int layer = 1;
    String building = "E";
    CCompany PickedCompany = null;
    ArrayList<String> CompanyItem = new ArrayList<String>();
    String picPath = "",brandPicPath="";
    Bitmap pic,picBrand;
    ImageView image;

    TourFactory tourFactory = new TourFactory();

    public MallFragment() {
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ((AppCompatActivity) getActivity()).getSupportActionBar().setTitle("購物中心");
        InitialComponent();
        companyJson();
        imageFloor.setImageResource(R.drawable.e100);

    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_tour, null);
    }


    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        FragmentTransaction fragmentTransaction = getFragmentManager().beginTransaction();
        fragmentTransaction.remove(MallFragment.this);
    }

    @Override
    public void onDetach() {
        super.onDetach();
    }

    ////樓層點擊  詳細內容事件
    View.OnClickListener btnCompany_click = new View.OnClickListener() {

        @Override
        public void onClick(View v) {
            brandPicPath="";
            brandPicPath = PickedCompany.getPicBrandPath();
            Log.d("brandPicPath", brandPicPath);

            LayoutInflater inflater = LayoutInflater.from(getActivity());
            View myView = inflater.inflate(R.layout.fragment_tourinfo, null);

            ((TextView) myView.findViewById(R.id.CompanyName)).setText(PickedCompany.getCompanyName());
            ((TextView) myView.findViewById(R.id.CompanyAddr)).setText(PickedCompany.getAddr());
            ((TextView) myView.findViewById(R.id.CompanyContent)).setText(PickedCompany.getContent());


            image = (ImageView) myView.findViewById(R.id.CompanyLogo);


            new AlertDialog.Builder(getActivity())
                    .setView(myView)
                    .setPositiveButton("確定", null).show();


            MyThread mthreadBrand = new MyThread() {
                @Override
                public void run() {
                    downloadTour2(myHandler2);
                }

                @Override
                public void downloadTour2(Handler myHandler2) {
                    super.downloadTour2(myHandler2);
                    LoadImgBrand();
                }
            };

            mthreadBrand.start(getActivity());
        }

        Handler myHandler2 = new Handler() {
            @Override
            public void handleMessage(android.os.Message msg) {
                // TODO Auto-generated method stub
                if (msg.what == 1) {
                    while (true){ if (LoadingCheck()){break;}}///無窮迴圈等待Load圖片完成
                    image.setImageBitmap(picBrand);


                } else {
                }
                super.handleMessage(msg);
            }
        };

    };

    private void companyJson() {//查詢Json廠商列表

        CompanyItem.clear();
        final String aspx = "FloorTourOne.aspx";
        ////
        tourFactory.setLayer(layer);
        tourFactory.setBuilding(building);
        tourFactory.loadData(getActivity());
        while (true) {

            if (tourFactory.flag()) {
                companyAdapterList();
                break;
            }
        }

    }

    private void companyAdapterList() {

        for (CCompany c : tourFactory.getAll()) {
            CompanyItem.add(c.getCompanyName());

        }


        // 要做為 ArrayAdapter 的資料來源
        // 建立 "陣列接收器"
        ArrayAdapter<String> arrayData = new ArrayAdapter<String>(
                getActivity()
                , android.R.layout.simple_list_item_1
                , CompanyItem
        );

        // 建立 ListView 物件
        ListView lv = new ListView(getActivity());

        // 設定 ListView 的接收器, 做為選項的來源
        lv.setAdapter(arrayData);

        // ListView 設定 Trigger
        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            public void onItemClick(AdapterView<?> arg0,
                                    View arg1, int arg2, long arg3) {

                btnCompany.setVisibility(arg1.VISIBLE);
                btnCompany.setBackgroundResource(R.drawable.malllogo);

                btnCompany.setText( CompanyItem.get(arg2));//加入商家名稱


                PickedCompany = tourFactory.findByName(CompanyItem.get(arg2));
                Log.d("PickedCompany", PickedCompany.getCompanyID());
                picPath = PickedCompany.getPicLocation();

                Log.d("picPath",picPath);

                MyThread mthread = new MyThread() {
                    @Override
                    public void run() {
                        downloadTour2(myHandler);
                    }

                    @Override
                    public void downloadTour2(Handler myHandler) {
                        super.downloadTour2(myHandler);

                        LoadImg();

                    }
                };

                mthread.start(getActivity());
            }

            Handler myHandler=new Handler() {
                @Override
                public void handleMessage (android.os.Message msg){
                    // TODO Auto-generated method stub
                    if (msg.what == 1) {
                        while (true){ if (LoadingCheck()){break;}}///無窮迴圈等待Load圖片完成
                        imageFloor.setImageBitmap(pic);
                    } else {
                    }
                    super.handleMessage(msg);
                }
            };
        });

        // 設定 ListView 為 ContentView

        LinearLayout viewListCom = (LinearLayout) getView().findViewById(R.id.viewListCom);
        viewListCom.removeAllViews();
        viewListCom.addView(lv);


    }


    public void LoadImgBrand() {
        LoadingCheck =false;
        try {
            URL url = new URL(JsonUrl.url  +"/pic/"+ brandPicPath);
            Log.d("url", url.toString());
            URLConnection conn = url.openConnection();
            InputStream streamIn = conn.getInputStream();

            picBrand = BitmapFactory.decodeStream(streamIn);
            LoadingCheck =true;

        } catch (MalformedURLException e) {

            e.printStackTrace();
        } catch (IOException e) {

            e.printStackTrace();
        } catch (Exception e) {

            e.printStackTrace();
        }
    }
    public void LoadImg() {
        LoadingCheck =false;
        try {
            URL url = new URL(JsonUrl.url  +"/mPic/"+ picPath);
            Log.d("url", url.toString());
            URLConnection conn = url.openConnection();
            InputStream streamIn = conn.getInputStream();

            pic = BitmapFactory.decodeStream(streamIn);
            LoadingCheck =true;

        } catch (MalformedURLException e) {

            e.printStackTrace();
        } catch (IOException e) {

            e.printStackTrace();
        } catch (Exception e) {

            e.printStackTrace();
        }
    }

    View.OnClickListener btn1F_click = new View.OnClickListener() {

        @Override
        public void onClick(View v) {
            layer = 1;
            imageFloor.setImageResource(R.drawable.e100);
            //txtlogo.setImageResource(R.drawable.idea);

            btnCompany.setVisibility(View.INVISIBLE);
            companyJson();
        }
    };
    View.OnClickListener btn2F_click = new View.OnClickListener() {

        @Override
        public void onClick(View v) {
            layer = 2;
            //btn2F.setBackgroundColor(0x00E3FCF9);
            imageFloor.setImageResource(R.drawable.e200);
            //txtlogo.setImageResource(R.drawable.idea);
            btnCompany.setVisibility(View.INVISIBLE);
            companyJson();
        }
    };
    View.OnClickListener btn3F_click = new View.OnClickListener() {

        @Override
        public void onClick(View v) {
            layer = 3;
            imageFloor.setImageResource(R.drawable.e300);
            //txtlogo.setImageResource(R.drawable.idea);
            btnCompany.setVisibility(View.INVISIBLE);
            companyJson();
        }
    };


    private void InitialComponent() {

        btnCompany = (Button) getView().findViewById(R.id.btnCompany);
        btnCompany.setOnClickListener(btnCompany_click);
        btn1F = (Button) getView().findViewById(R.id.btn1F);
        btn1F.setOnClickListener(btn1F_click);
        btn2F = (Button) getView().findViewById(R.id.btn2F);
        btn2F.setOnClickListener(btn2F_click);
        btn3F = (Button) getView().findViewById(R.id.btn3F);
        btn3F.setOnClickListener(btn3F_click);

        imageFloor = (ImageView) getView().findViewById(R.id.imageFloor);
        txtlogo = (ImageView) getView().findViewById(R.id.txtlogo);

    }

    ////////等待Load圖片完成
    public  Boolean LoadingCheck(){
        return LoadingCheck;
    }

    private Button btnCompany;
    private Button btn1F, btn2F, btn3F;
    private ImageView imageFloor,txtlogo;

}
