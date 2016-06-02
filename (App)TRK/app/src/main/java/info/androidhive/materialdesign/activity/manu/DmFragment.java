package info.androidhive.materialdesign.activity.manu;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.DisplayMetrics;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.ImageView;

import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.ImageViewHelper;

public class DmFragment extends Fragment {
    public DmFragment() {
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
        View rootView = inflater.inflate(R.layout.fragment_dm, container, false);
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
        dm = new DisplayMetrics();
        getActivity().getWindowManager().getDefaultDisplay().getMetrics(dm);

        imageView = (ImageView)getView().findViewById(R.id.image_view);
        zoomInButton = (ImageButton)getView().findViewById(R.id.zoomInButton);
        zoomOutButton = (ImageButton)getView().findViewById(R.id.zoomOutButton);

        bitmap = BitmapFactory.decodeResource(getResources(), R.drawable.dm1);
        imageView.setImageBitmap(bitmap);

        new ImageViewHelper(getActivity(),dm,imageView,bitmap,zoomInButton,zoomOutButton);
    }


    private ImageView imageView;
    private DisplayMetrics dm;
    private Bitmap bitmap;

    private ImageButton zoomInButton;
    private ImageButton zoomOutButton;
}
