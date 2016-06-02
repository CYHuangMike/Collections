package info.androidhive.materialdesign.activity.manu.Traffic;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;

import info.androidhive.materialdesign.R;

public class TrafficActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.fragment_traffic);

        LatLng gpsKHS = new LatLng(22.5810287, 120.32928919999995);
        GoogleMap map  = ((MapFragment) getFragmentManager().findFragmentById(R.id.map)).getMap();
        Marker l_markKHStation = map.addMarker(new MarkerOptions().position(gpsKHS)
                .title("大魯閣草衙道").snippet("806高雄市前鎮區中山四路100號"));

        map.moveCamera( CameraUpdateFactory.newLatLngZoom(gpsKHS, 16) );
    }
}
