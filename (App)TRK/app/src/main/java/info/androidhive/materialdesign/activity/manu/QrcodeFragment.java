package info.androidhive.materialdesign.activity.manu;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import info.androidhive.materialdesign.activity.manu.Qrpack.IntentIntegrator;
import info.androidhive.materialdesign.activity.manu.Qrpack.IntentResult;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import info.androidhive.materialdesign.R;

public class QrcodeFragment extends Fragment {
    public QrcodeFragment() {
        // Required empty public constructor
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        // TODO Auto-generated method stub
        super.onCreate(savedInstanceState);

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_qrcode, container, false);
        // Inflate the layout for this fragment
        return rootView;
    }


    //當Fragment底層的Activity被建立時會被執行的方法
    //在這方法裡取得Fragment的介面元件, 如同Activity的onCreate()一樣
    @Override
    public void onActivityCreated(Bundle savedInstanceState)
    {
        super.onActivityCreated(savedInstanceState);
        initialize();
    }

    private void initialize() {

        scanBtn = (Button)getView().findViewById(R.id.scan_button);
        formatTxt = (TextView)getView().findViewById(R.id.scan_format);
        contentTxt = (TextView)getView().findViewById(R.id.scan_content);

        scanBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(view.getId()==R.id.scan_button){
                    IntentIntegrator scanIntegrator = new IntentIntegrator(getActivity());
                    scanIntegrator.initiateScan();

                }
            }
        });
    }

    public void onActivityResult(int requestCode, int resultCode, Intent intent) {
        IntentResult scanningResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, intent);
        if (scanningResult != null) {
            String scanContent = scanningResult.getContents();
            String scanFormat = scanningResult.getFormatName();
            formatTxt.setText("FORMAT: " + scanFormat);
            contentTxt.setText("CONTENT: " + scanContent);

        }
        else {
            Toast toast = Toast.makeText(getActivity().getApplicationContext(),
                    "No scan data received!", Toast.LENGTH_SHORT);
            toast.show();
        }
    }
    private Button scanBtn;
    private TextView formatTxt, contentTxt;

}
