package info.androidhive.materialdesign.activity.manu.Explore;

import android.app.Activity;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.CDictionary;
import info.androidhive.materialdesign.activity.Tools.CScreen;

public class ExploreFragment extends Fragment {
    Bundle bundle = new Bundle();

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_explore, container, false);
        return rootView;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {

        super.onActivityCreated(savedInstanceState);
        //改ActionBar
        ((AppCompatActivity) getActivity()).getSupportActionBar().setTitle("樂園探索");

        InitialComponent();
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
    }

    @Override
    public void onDetach() {

        super.onDetach();
    }


    View.OnTouchListener view_TouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View view, MotionEvent event) {
            FragmentTransaction fragmentTransaction = getFragmentManager().beginTransaction();
            Fragment exployreBuilding = new ExploreBuildingFragment();

            StringBuilder sb = new StringBuilder();
            if (event.getAction() == MotionEvent.ACTION_DOWN) {
                //將使用裝置的座標換算回來
                CScreen.getScreenInch(getActivity());
                float mX = event.getX(0) * CScreen.ScaleWidthPixels;
                float mY = event.getY(0) * CScreen.ScaleHeightPixels;

                //G:自由搖滾
                if (134 <= mX && mX <= 253 && 339 <= mY && mY <= 480) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "4");
                    fragmentTransaction.replace(R.id.container_body, exployreBuilding);
                }
                //D:自由落體
                if (151 <= mX && mX <= 264 && 547 <= mY && mY <= 688) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "5");
                    fragmentTransaction.replace(R.id.container_body, exployreBuilding);
                }
                //R:鈴鹿賽道
                if (317 <= mX && mX <= 440 && 110 <= mY && mY <= 248) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "6");
                    fragmentTransaction.replace(R.id.container_body, exployreBuilding);
                }
                //C:旋轉木馬
                if (49 <= mX && mX <= 190 && 811 <= mY && mY <= 952) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "7");
                    fragmentTransaction.replace(R.id.container_body,exployreBuilding);
                }
                //F:摩天輪
                if (528 <= mX && mX <= 701 && 177 <= mY && mY <= 434) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "8");
                    fragmentTransaction.replace(R.id.container_body,exployreBuilding);
                }
                //A:天空飛行家
                if (345 <= mX && mX <= 493 && 540 <= mY && mY <= 675) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "9");
                    fragmentTransaction.replace(R.id.container_body,exployreBuilding);
                }
                //11越野大冒險
                if (17 <= mX && mX <= 130 && 530 <= mY && mY <= 674) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "11");
                    fragmentTransaction.replace(R.id.container_body,exployreBuilding);
                }
                //12飄移高手
                if (303 <= mX && mX <= 394 && 396 <= mY && mY <= 519) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "12");
                    fragmentTransaction.replace(R.id.container_body, exployreBuilding);
                }
                //13甩尾小車手
                if (870 <= mX && mX <= 997 && 469 <= mY && mY <= 600) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "13");
                    fragmentTransaction.replace(R.id.container_body,exployreBuilding);
                }
                //14 滴答電車
                if (926 <= mX && mX <= 1028 && 283 <= mY && mY <= 431) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "14");
                    fragmentTransaction.replace(R.id.container_body, exployreBuilding);
                }
                //15小小騎士
                if (595 <= mX && mX <= 711 && 473 <= mY && mY <= 603) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "15");
                    fragmentTransaction.replace(R.id.container_body,exployreBuilding);
                }
                //19酷奇拉駕駛學校
                if (764 <= mX && mX <= 870 && 293 <= mY && mY <= 427) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "19");
                    fragmentTransaction.replace(R.id.container_body, exployreBuilding);
                }
                //20草衙道電車
                if (292 <= mX && mX <= 412 && 807 <= mY && mY <= 931) {
                    bundle.putString(CDictionary.TAG_BUILDING_TITLE, "20");
                    fragmentTransaction.replace(R.id.container_body,exployreBuilding);
                }

                exployreBuilding.setArguments(bundle);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();
            }
            return true;
        }
    };

    private void InitialComponent() {
        view = (ImageView) getView().findViewById(R.id.imageView);
        view.setOnTouchListener(view_TouchListener);
    }

    ImageView view;

}
