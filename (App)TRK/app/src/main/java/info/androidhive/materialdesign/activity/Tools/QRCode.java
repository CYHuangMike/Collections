package info.androidhive.materialdesign.activity.Tools;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.manu.Qrpack.IntentIntegrator;
import info.androidhive.materialdesign.activity.manu.Qrpack.IntentResult;

public class QRCode extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.qrcode);
        InitialComponent();
        IntentIntegrator scanIntegrator = new IntentIntegrator(QRCode.this);
        scanIntegrator.initiateScan();
    }


    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        IntentResult scanningResult =
                IntentIntegrator.parseActivityResult(requestCode, resultCode, data);
        if (scanningResult != null) {
            String scanContent = scanningResult.getContents();
            String scanFormat = scanningResult.getFormatName();

            Bundle bundle = new Bundle();
            bundle.putString(CDictionary.GET_QRCODE, scanContent); //包包裹
            Intent intent = new Intent();
            intent.putExtras(bundle); //包裹給管理員
            setResult(CDictionary.SET_QRCODE, intent);
            finish();

        }
        else {
            Toast toast = Toast.makeText(this.getApplicationContext(),
                    "No scan data received!", Toast.LENGTH_SHORT);
            toast.show();
        }
    }

    private void InitialComponent() {
        scanBtn = (Button)findViewById(R.id.scan_button);
        formatTxt = (TextView)findViewById(R.id.scan_format);
        contentTxt = (TextView)findViewById(R.id.scan_content);
    }

    private Button scanBtn;
    private TextView formatTxt, contentTxt;

}
