package info.androidhive.materialdesign.activity.manu;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;


import org.json.JSONException;
import org.json.JSONObject;

import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.MyThread;


public class ManliCardFragment extends Fragment {

    String cardNumber;
    String cardPoint;
    String ResposneJson;
    MyThread myThread;

    public ManliCardFragment() {
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        //利用inflater物件的inflate()方法取得介面佈局檔,並將最後的結果傳回給系統
        return inflater.inflate(R.layout.fragment_cardsearch, container, false);
    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        //當Fragment底層的Activity被建立時會被執行的方法
        //在這方法裡取得Fragment的介面元件, 如同Activity的onCreate()一樣
        super.onActivityCreated(savedInstanceState);
        InitialComponent();
    }


    @Override
    public void onDestroy() {
        if (myThread != null)
            myThread.stop();
        super.onDestroy();
    }



    //查詢中心
    private View.OnClickListener btnSearch_Click = new View.OnClickListener() {
        public void onClick(View v) {
            final String mInput = edtType.getText().toString();
            final String aspx = "ManliCardPoint.aspx";

            myThread = new MyThread() {
                @Override
                public void run() {
                    download(mInput, aspx, myHandler, ResposneJson);
                }

                @Override
                public void download(String mInput, String aspx, Handler myHandler, String ResposneJson) {
                    super.download(mInput, aspx, myHandler, ResposneJson);
                }

                public void mJsonResult(String resposneJson) throws JSONException {
                    cardNumber = new JSONObject(resposneJson).getString("cardNumber");//卡號
                    cardPoint = new JSONObject(resposneJson).getString("cardPoint");  //點數
                }
            };

            myThread.start(getActivity());
        }
    };


    Handler myHandler = new Handler() {
        @Override
        public void handleMessage(android.os.Message msg) {
            // TODO Auto-generated method stub
            if (msg.what == 1) {
                Toast.makeText(getActivity(), "成功", Toast.LENGTH_SHORT).show();
                Log.d("JSON", "成功");

                txtShow.setText(cardPoint);
            } else {
                Toast.makeText(getActivity(), "查無此卡號", Toast.LENGTH_SHORT).show();
                txtShow.setText("查無此卡號");
            }
            super.handleMessage(msg);
        }
    };


    private View.OnClickListener btnHow_Click = new View.OnClickListener() {
        public void onClick(View v) {
            // 彈跳視窗
            new AlertDialog.Builder(getActivity())
                    .setTitle("兌換說明")
                    .setMessage("每100元享紅利點數1點 ;\n每100點折抵消費30元。\n" +
                            "step1:於指定優惠日至館內「i曼麗(KIOSK)」選定【會員專區】，並將曼麗卡放至感應區，點選【點數好禮】。\n" +
                            "step2:請在【週一~週四卡友好康日】選擇指定日當日優惠。\n" +
                            "step3:選定優惠並扣除會員點數5點，將跑出當日優惠券，即可持本券前往指定櫃位享有優惠。")
                    .setPositiveButton("確定", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {
                        }
                    }).show();
        }
    };

    private void InitialComponent() {
        //注意, Fragment必須透過getView取得介面元件
        edtType = (EditText) getView().findViewById(R.id.edtType);
        //edtType.addTextChangedListener(edtType_TextChanged);
        txtShow = (TextView) getView().findViewById(R.id.txtShow);
        btnSearch = (Button) getView().findViewById(R.id.btnSearch);
        btnSearch.setOnClickListener(btnSearch_Click);
        btnHow = (Button) getView().findViewById(R.id.btnHow);
        btnHow.setOnClickListener(btnHow_Click);
    }
    private Button btnHow;
    private Button btnSearch;
    private TextView txtShow;
    private EditText edtType;
}
