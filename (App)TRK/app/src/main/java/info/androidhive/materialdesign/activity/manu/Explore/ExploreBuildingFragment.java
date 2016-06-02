package info.androidhive.materialdesign.activity.manu.Explore;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.ContentValues;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;

import info.androidhive.materialdesign.Factorys.CCouponFactory;
import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.CDbManager;
import info.androidhive.materialdesign.activity.Tools.CDictionary;
import info.androidhive.materialdesign.activity.Tools.CardDbOpenHelper;
import info.androidhive.materialdesign.activity.Tools.JsonUrl;
import info.androidhive.materialdesign.activity.Tools.MyThread;
import info.androidhive.materialdesign.activity.Tools.QRCode;
import info.androidhive.materialdesign.model.CCoupon;


public class ExploreBuildingFragment extends Fragment {
    final String aspx = "Facility.aspx";
    String facilityID = "",data="";
    String ResposneJson = "", name = "", content = "", picPath = "";
    Bitmap pic;
    private SQLiteDatabase cardDb;

    private static final String DB_FILE = "card.db",
                                DB_TABLE = "card";

    public static  final String NAME_COLUMN ="name";
    public static  final String QRCODE_COLUMN ="QRcode";
    public static  final String SAVE_COLUMN ="status";
    public static  final String WPIC_COLUMN ="wPicPath";
    public static  final String CPIC_COLUMN ="cPicPath";


    CCouponFactory factory;
    CDbManager db;



    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        Log.d("購物Frg", "onAttach建立");
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_explore_building, container, false);
    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        InitialComponent();
        facilityID = getArguments().getString(CDictionary.TAG_BUILDING_TITLE);
        query(facilityID); //進資料庫撈資料
        CreatedCardDb();

    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent intent) {
        try {
            factory = new CCouponFactory();
            db = new CDbManager(getActivity());

            if (requestCode != CDictionary.SET_QRCODE)
                return;
            Bundle bundle = intent.getExtras(); //跟管理員拿送回來的包裹
            data= bundle.getString(CDictionary.GET_QRCODE); //根據識別碼KEY來拆包裹
            int couponID = Integer.parseInt(data);
            Log.d("QRCode回傳",""+couponID);
            search();
            GetCoupon(couponID);
        }catch(Exception ex)
        {

        }finally {


        }



    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
    }

    @Override
    public void onDestroy() {

        super.onDestroy();
        cardDb.close();
    }

    @Override
    public void onDetach() {
        super.onDetach();
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
        // 載完
        while (!factory.Getflag()) ;
        factory.FindCouponById(couponID);

        InsertCouponToSQLite(couponID);
        // 彈跳視窗
        new AlertDialog.Builder(getActivity())
                .setTitle("成就達成")
                .setMessage("恭喜您集到一點，可至「我的集點卡」查看集點狀況；另外加碼送來店好禮，可至「我的優惠券」查看。")
                .setPositiveButton("確定", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        Toast.makeText(getActivity().getApplicationContext(), "領取優惠券成功",
                                Toast.LENGTH_SHORT).show();
                    }
                }).show();
    }
    public void CreatedCardDb()
    {
        CardDbOpenHelper cardDbOpenHelper =
                new CardDbOpenHelper(getActivity(), DB_FILE, null, 1);


        cardDb = cardDbOpenHelper.getWritableDatabase();
        Cursor cursor = cardDb.rawQuery(
                "select DISTINCT tbl_name from sqlite_master where tbl_name = '" +
                        DB_TABLE + "'", null);


        if(cursor != null){
            if(cursor.getCount()==0)//沒database建db
            {
                //rawQuery: select
                //execSQL: insert, delete, update
                cardDb.execSQL("CREATE TABLE "+DB_TABLE+ " (" +
                        QRCODE_COLUMN + " TEXT PRIMARY KEY  NOT NULL, " +
                        NAME_COLUMN + " TEXT NOT NULL, " +
                        SAVE_COLUMN + " INTEGER NOT NULL );"

                );


                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10014', '旋轉木馬 The Carousel',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10015', '自由落體 Drop Zone',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10016', '自由搖滾 G-Speed',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10017', '天空飛行家 Air Racers',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10018', '越野大冒險 Acro-X',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10019', '飄移高手 Drift-S',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10020', '甩尾小車手 Drift Kids Racer',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10021', '滴答電車 Tic Tac Train',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10022', '小小騎士 Kids Bike',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10023', '駕照中心 License Center',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10024', '摩天輪 Circuit Wheel',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10025', '酷奇拉駕駛學校 Kochira Driving School',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10026', '草衙道電車 Taroko Park Trolley',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10027', '鈴鹿賽道樂園',0);");
                cardDb.execSQL("INSERT INTO "+DB_TABLE+" VALUES ('10028', '迷你鈴鹿賽道 Mini Suzuka Circuit',0);");

                cursor.close();
            }

        }
        Log.d("db", "CreatedCardDb ");


    }

    public void  search()  {
        Cursor c = null;

        c = cardDb.rawQuery("select name from card where QRcode=?",new String[]{data});

        if (c.getCount() == 0) {
            Log.d("search: ",data );

            Toast.makeText(this.getActivity(), "沒有這筆資料", Toast.LENGTH_LONG).show();
            return;

        } else {

            update();
        }
        c.close();


    }
    public int update()
    {
        int save = 1;
        ContentValues args = new ContentValues();
        args.put("status", save);//欄位名稱，參數
        return cardDb.update(DB_TABLE,  //資料表名稱
                args,                   //value
                "QRcode= '"+data+"'",       //where
                null                    //where 參數
        );

    }



    //card
    private View.OnClickListener btnLoyaltyCard_Click = new View.OnClickListener() {


        @Override
        public void onClick(View view) {
            int QRCODE=10014;//利用迴圈遞增
            int left=0;


            int[] imageViewID= new int[]{R.id.mark1,R.id.mark2,R.id.mark3,R.id.mark4,R.id.mark5,
                                         R.id.mark6,R.id.mark7,R.id.mark8, R.id.mark9,R.id.mark10,
                                         R.id.mark11,R.id.mark12,R.id.mark13,R.id.mark14,R.id.mark15};
            int[] cPicPath=new int[]{R.drawable.c01,R.drawable.c02,R.drawable.c03,R.drawable.c04,R.drawable.c05,
                                     R.drawable.c06,R.drawable.c07,R.drawable.c08,R.drawable.c09,R.drawable.c10,
                                     R.drawable.c11,R.drawable.c12,R.drawable.c13,R.drawable.c14,R.drawable.c15};




            Cursor leftCursor=null;
            leftCursor = cardDb.rawQuery("select * from card where status=?",new String[]{"1"});
            int check_left = leftCursor.getCount();


            if(check_left ==15)//完成15關
            {
                LayoutInflater inflater = LayoutInflater.from(getActivity());
                View myView2 = inflater.inflate(R.layout.fragment_loyaltycard2, null);

                new AlertDialog.Builder(getActivity())
                        .setView(myView2)
                        .setTitle("完成任務")
                        .setPositiveButton("確定", null).show();

            }
            else//未完成15關
            {
                Cursor c = null;
                LayoutInflater inflater = LayoutInflater.from(getActivity());
                View myView = inflater.inflate(R.layout.fragment_loyaltycard, null);
                for (int i = 0 ;i <=14;i++)
                {

                    String code="",status1="";
                    code=Integer.toString(QRCODE);//轉換QRcode 字串


                    c = cardDb.rawQuery("select status from card where QRcode=?",new String[]{code});


                    if(c.getCount()>0)
                    {
                        while(c.moveToNext())
                        {
                            status1=c.getString(0);
                            Log.i("Tag","QRCOODE: "+QRCODE+",i= "+i+" ,cursor取得的值:"+status1);//得到的第一個變數
                        }
                    }


                    if(Integer.parseInt(status1)==1)//狀態為1
                    {
                        Log.d("changeImage", "ok");
                        ImageView cardImg = (ImageView) myView.findViewById(imageViewID[i]);
                        cardImg.setImageResource(cPicPath[i]);
                        left++;
                    }
                    else
                    {
                        Log.d("stutas",status1);
                        Log.d("changeImage", "false");
                    }


                    QRCODE++;

                }
                c.close();
                String stringValue = Integer.toString(15-left);
                ((TextView) myView.findViewById(R.id.txtLeftStation)).setText(stringValue);
                new AlertDialog.Builder(getActivity())
                        .setView(myView)
                        .setPositiveButton("確定", null).show();

            }
            leftCursor.close();

        }
    };


    //跳QRCode
    private View.OnClickListener btnQRCode_Click = new View.OnClickListener() {

        @Override
        public void onClick(View view) {
            if (view.getId() == R.id.btnQRCode) {
                Intent intent = new Intent();
                intent.setClass(getActivity(), QRCode.class);
                try
                {
                    startActivityForResult(intent,CDictionary.SET_QRCODE);
                }catch (Exception e)
                {
                    return;
                }
            }
        }
    };


    private void query(final String facilityID) {
        MyThread myThread = new MyThread() {
            @Override
            public void download(String mInput, String aspx, Handler myHandler, String ResposneJson) {
                super.download(mInput, aspx, myHandler, ResposneJson);
            }

            @Override
            public void mJsonResult(String resposneJson) throws JSONException {
                name = new JSONObject(resposneJson).getString("name");//設施名稱
                content = new JSONObject(resposneJson).getString("content");//特色內容
                picPath = new JSONObject(resposneJson).getString("pic"); //圖片路徑
                LoadImg();
            }

            @Override
            public void run() {
                download(facilityID, aspx, myHandler, ResposneJson);
            }
        };
        myThread.start(getActivity());
    }

    //撈圖片
    public void LoadImg() {
        try {
            URL url = new URL(JsonUrl.url+"/mPic/"+picPath);
            Log.d("url",JsonUrl.url+"/mPic/"+picPath);
            URLConnection conn = url.openConnection();
            InputStream streamIn = conn.getInputStream();
            pic = BitmapFactory.decodeStream(streamIn);
        } catch (MalformedURLException e) {

            e.printStackTrace();
        } catch (IOException e) {

            e.printStackTrace();
        } catch (Exception e) {

            e.printStackTrace();
        }
    }

    Handler myHandler = new Handler() {
        @Override
        public void handleMessage(android.os.Message msg) {
            // TODO Auto-generated method stub
            if (msg.what == 1) {
                //Toast.makeText(getActivity(), "成功", Toast.LENGTH_SHORT).show();
                txtInfo.setText(content);
                imgLoad.setImageBitmap(pic);
                ((AppCompatActivity)getActivity()).getSupportActionBar().setTitle(name);
            } else {
                Toast.makeText(getActivity(), "失敗", Toast.LENGTH_SHORT).show();
            }
            super.handleMessage(msg);
        }
    };

    // 判斷回傳的couponId是不是已經被新增
    public boolean HadCouponID(int couponID) {
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

    // 新增資料到SQLITE
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


    private void InitialComponent() {
        txtInfo = (TextView) getView().findViewById(R.id.txtInfo);
        imgLoad = (ImageView) getView().findViewById(R.id.imgId);

        btnQRCode = (Button) getView().findViewById(R.id.btnQRCode);
        btnQRCode.setOnClickListener(btnQRCode_Click);
        btnLoyaltyCard = (Button) getView().findViewById(R.id.btnLoyaltyCard);
        btnLoyaltyCard.setOnClickListener(btnLoyaltyCard_Click);

    }


    ImageView imgLoad;
    TextView txtInfo;
    Button btnQRCode;
    Button btnLoyaltyCard;

}
