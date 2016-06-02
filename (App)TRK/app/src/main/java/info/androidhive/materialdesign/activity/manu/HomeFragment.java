package info.androidhive.materialdesign.activity.manu;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.CDictionary;
import info.androidhive.materialdesign.activity.manu.Explore.ExploreFragment;

import info.androidhive.materialdesign.activity.manu.News.NewsFragment;
import info.androidhive.materialdesign.activity.manu.Tour.MallFragment;

public class HomeFragment extends Fragment {
    Bundle savedInstanceState;
    private ViewPager mViewPaper;
    private List<ImageView> images;
    private List<View> dots;
    private int currentItem;
    //記錄上一次點的位置
    private int oldPosition = 0;

    //圖片標題
    /*private String[]  titles = new String[]{
            "xxx",
    };*/
    // private TextView title;
    private ViewPagerAdapter adapter;
    private ScheduledExecutorService scheduledExecutorService;
    public HomeFragment() {
    }


    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        Log.d("首頁", "onAttach建立");
    }


    @Override
    public void onCreate(Bundle savedInstanceState)
    {
        CDictionary.COUNT_HOME++;
        super.onCreate(savedInstanceState);
        Log.d("首頁", "onCreate建立"+CDictionary.COUNT_HOME);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        Log.d("首頁", "onCreateView建立");

        View rootView = inflater.inflate(R.layout.fragment_home, container, false);
        return rootView;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState)
    {
        Log.d("首頁", "onActivityCreated建立");
        super.onActivityCreated(savedInstanceState);
        ((AppCompatActivity)getActivity()).getSupportActionBar().setTitle("首頁");
        InitialComponent();
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
    }

    @Override
    public void onDetach() {
        super.onDetach();
        Log.d("首頁", "onDetach刪除");
    }


    //按鈕購物商城
    private View.OnClickListener btnMall_Click = new View.OnClickListener()
    {
        public void onClick(View v)
        {
            MallFragment mallFragment = new MallFragment();
            FragmentTransaction fragTran = getFragmentManager().beginTransaction();
            fragTran.replace(R.id.container_body, mallFragment,
                    CDictionary.TAG_FRAGMENT_EXPLORESHOPPING);
            fragTran.addToBackStack(null);
            fragTran.commit();
        }
    };

    //按鈕最新消息
    private View.OnClickListener btnNews_Click = new View.OnClickListener()
    {
        public void onClick(View v)
        {
            NewsFragment fnewsFragment = new NewsFragment();
            FragmentTransaction fragTran = getFragmentManager().beginTransaction();
            fragTran.replace(R.id.container_body, fnewsFragment,
                    CDictionary.TAG_FRAGMENT_NEW);
            fragTran.addToBackStack(null);
            fragTran.commit();
        }
    };

    //按鈕:遊戲中心
    private View.OnClickListener btnGameCentre_Click = new View.OnClickListener()
    {
        public void onClick(View v)
        {

            GameCentreFragment gameCentreFragment = new GameCentreFragment();
            FragmentTransaction fragTran = getFragmentManager().beginTransaction();
           fragTran.replace(R.id.container_body, gameCentreFragment,
                   CDictionary.TAG_FRAGMENT_GAMECENTRE);
            fragTran.addToBackStack(null);
           fragTran.commit();
        }
    };
    //存放圖片的id
    int[] imageIds;
    //按鈕樂園探索
    private View.OnClickListener btnExploer_Click = new View.OnClickListener()
    {
        public void onClick(View v)
        {
            FragmentTransaction fragTran = getFragmentManager().beginTransaction();
            ExploreFragment exploreFragment = new ExploreFragment();
            fragTran.replace(R.id.container_body, exploreFragment,CDictionary.TAG_FRAGMENT_EXPLORE);
            fragTran.addToBackStack(null);
            fragTran.commit();
        }
    };

    ImageView imageView;
    private void InitialComponent()
    {
        btnGameCentre = (Button)getView().findViewById(R.id.btnGameCentre);
        btnGameCentre.setOnClickListener(btnGameCentre_Click);
        btnExploer = (Button)getView().findViewById(R.id.btnExploer);
        btnExploer.setOnClickListener(btnExploer_Click);
        btnNews = (Button)getView().findViewById(R.id.btnNews);
        btnNews.setOnClickListener(btnNews_Click);
        btnMall = (Button)getView().findViewById(R.id.btnMall);
        btnMall.setOnClickListener(btnMall_Click);
        mViewPaper = (ViewPager) getView().findViewById(R.id.vp);

        //顯示的圖片
        images = new ArrayList<ImageView>();

        imageIds = new int[]{
                R.drawable.a,
                R.drawable.b,
                R.drawable.c,
                R.drawable.d,
                R.drawable.e
        };

        for(int i = 0; i < imageIds.length; i++){
            imageView = new ImageView(getActivity());
            // Log.i("imageIds","" +imageIds[i]);
            imageView.setBackgroundResource(imageIds[i]);
            images.add(imageView);
        }
        //顯示的小點
        dots = new ArrayList<View>();
        dots.add(getView().findViewById(R.id.dot_0));
        dots.add(getView().findViewById(R.id.dot_1));
        dots.add(getView().findViewById(R.id.dot_2));
        dots.add(getView().findViewById(R.id.dot_3));
        dots.add(getView().findViewById(R.id.dot_4));

        // title = (TextView) getView().findViewById(R.id.title);
        //title.setText(titles[0]);

        adapter = new ViewPagerAdapter();
        mViewPaper.setAdapter(adapter);

        mViewPaper.addOnPageChangeListener(new ViewPager.OnPageChangeListener(){

            @Override
            public void onPageSelected(int position) {
                //title.setText(titles[position]);
                dots.get(position).setBackgroundResource(R.drawable.dot_focused);
                dots.get(oldPosition).setBackgroundResource(R.drawable.dot_normal);

                oldPosition = position;
                currentItem = position;
            }

            @Override
            public void onPageScrolled(int arg0, float arg1, int arg2) {

            }

            @Override
            public void onPageScrollStateChanged(int arg0) {

            }
        });

    }

    private class ViewPagerAdapter extends PagerAdapter {

        @Override
        public int getCount() {
            return images.size();
        }

        @Override
        public boolean isViewFromObject(View arg0, Object arg1) {
            return arg0 == arg1;
        }

        @Override
        public void destroyItem(ViewGroup view, int position, Object object) {
            // TODO Auto-generated method stub
//			super.destroyItem(container, position, object);
//			view.removeView(view.getChildAt(position));
//			view.removeViewAt(position);
            view.removeView(images.get(position));
        }

        @Override
        public Object instantiateItem(ViewGroup view, int position) {
            // TODO Auto-generated method stub
            view.addView(images.get(position));
            return images.get(position);
        }

    }
    @Override
    public void onStart() {
        // TODO Auto-generated method stub
        super.onStart();
        scheduledExecutorService = Executors.newSingleThreadScheduledExecutor();
        scheduledExecutorService.scheduleWithFixedDelay(
                new ViewPageTask(),
                5,
                5,
                TimeUnit.SECONDS);
    }
    private class ViewPageTask implements Runnable{

        @Override
        public void run() {
            currentItem = (currentItem + 1) % imageIds.length;
            mHandler.sendEmptyMessage(0);
        }
    }
    private Handler mHandler = new Handler(){
        public void handleMessage(android.os.Message msg) {
            mViewPaper.setCurrentItem(currentItem);
        };
    };
    @Override
    public void onStop() {
        // TODO Auto-generated method stub
        super.onStop();
        if(scheduledExecutorService != null){
            scheduledExecutorService.shutdown();
            scheduledExecutorService = null;
        }
    }

    private Button btnGameCentre;
    private Button btnExploer;
    private Button btnNews;
    private Button btnMall;
}
