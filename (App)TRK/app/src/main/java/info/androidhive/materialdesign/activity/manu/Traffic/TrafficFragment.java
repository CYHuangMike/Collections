package info.androidhive.materialdesign.activity.manu.Traffic;

import android.content.DialogInterface;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import info.androidhive.materialdesign.R;

/**
 * Created by iii on 2016/4/26.
 */
public class TrafficFragment extends Fragment {
    public TrafficFragment() {
        // Required empty public constructor
    }
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        //利用inflater物件的inflate()方法取得介面佈局檔,並將最後的結果傳回給系統
        return inflater.inflate(R.layout.fragment_traffic, container, false);
    }

}
