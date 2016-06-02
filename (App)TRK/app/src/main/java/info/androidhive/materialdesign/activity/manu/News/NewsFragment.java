package info.androidhive.materialdesign.activity.manu.News;

import android.app.Activity;
import android.app.AlertDialog;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
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
import android.widget.ListView;
import android.widget.TextView;
import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;

import info.androidhive.materialdesign.Factorys.NewsFactory;
import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.Tools.JsonUrl;
import info.androidhive.materialdesign.activity.Tools.MyThread;
import info.androidhive.materialdesign.model.CNews;

/**
 * Created by Ravi on 29/07/15.
 */
public class NewsFragment extends Fragment {

    int NewsClassID1=3;
    int NewsClassID2=4;
    String picPath = "";
    Bitmap pic;
    Boolean LoadingCheck =false;
    CNews PickedNews = null;
    NewsFactory newsFactory = new NewsFactory();
    private List<CNews> NewsItem = new ArrayList<CNews>();
    ArrayList<String> NewsTitle = new ArrayList<String>();
    public NewsFragment() {
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        ((AppCompatActivity) getActivity()).getSupportActionBar().setTitle("最新消息");

        InitialComponent();
        btnNewsOne.setBackgroundResource(R.drawable.newbtn2);
        NewsJson();
        NewsAdapterList();

    }



    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_news, null);
    }


    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        FragmentTransaction fragmentTransaction = getFragmentManager().beginTransaction();
        fragmentTransaction.remove(NewsFragment.this);
    }

    @Override
    public void onDetach() {
        super.onDetach();
    }


    private void NewsJson() {//查詢Json廠商列表
        NewsItem.clear();
        final String aspx = "FloorTourOne.aspx";
        ////
        newsFactory.setNewsClassID1(NewsClassID1);
        newsFactory.setNewsClassID2(NewsClassID2);
        newsFactory.loadData(getActivity());
        while (true) {

            if (newsFactory.flag()) {
                NewsAdapterList();
                break;
            }
        }

    }


    private void NewsAdapterList() {//外部類別
        NewsItem.clear();
        NewsTitle.clear();
        for (CNews cNews : newsFactory.getAll()) {
            NewsItem.add(cNews);

        }
        for (CNews c : newsFactory.getAll()) {
            NewsTitle.add(c.getNTitle());

        }
        ArrayAdapter<CNews> adapter=new NewsAdapter();//
        listView.setAdapter(adapter);// 設定 ListView 的接收器, 做為選項的來源
        listViewOnClick();//設onclick 事件
    }




    private class NewsAdapter extends ArrayAdapter<CNews> {
        public NewsAdapter() {
            super(getActivity(), R.layout.textcar, NewsItem);
        }

        @Override
        public View getView(int position,View convertView, ViewGroup parent) {
            View itemView = convertView;
            if(itemView == null)
                itemView = getActivity().getLayoutInflater().inflate(R.layout.textcar,parent, false);

            CNews currentNews = NewsItem.get(position);
            ((TextView) itemView.findViewById(R.id.News_txtTitle)).setText(currentNews.getNTitle());
            ((TextView) itemView.findViewById(R.id.News_txtdate)).setText(currentNews.getNStartDate()+"~"+currentNews.getNEndDate());

            return  itemView;
        }

    }

    private void listViewOnClick() {
        // ListView 設定 Trigger
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {

            public void onItemClick(AdapterView<?> arg0,
                                    View arg1, int arg2, long arg3) {


                PickedNews = newsFactory.findByNews(NewsTitle.get(arg2));
                picPath="";
                picPath = PickedNews.getNPicPath();
                LayoutInflater inflater = LayoutInflater.from(getActivity());
                View myView = inflater.inflate(R.layout.fragment_newsinfo, null);

                ((TextView) myView.findViewById(R.id.NTitle)).setText(PickedNews.getNTitle());
                ((TextView) myView.findViewById(R.id.NStartDate)).setText(PickedNews.getNStartDate());
                ((TextView) myView.findViewById(R.id.NEndDate)).setText(PickedNews.getNEndDate());
                ((TextView) myView.findViewById(R.id.NContent)).setText(PickedNews.getNContent());


                image = (ImageView) myView.findViewById(R.id.NewsImage);

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
                        LoadImg();
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
                        image.setImageBitmap(pic);


                    } else {
                    }
                    super.handleMessage(msg);
                }
            };

        });
    }

    //圖片檔轉bitmap
    public void LoadImg() {
        LoadingCheck =false;
        try {
            URL url = new URL(JsonUrl.url  +"/pic/"+ picPath);
            Log.d("url", url.toString());
            URLConnection conn = url.openConnection();
            InputStream streamIn = conn.getInputStream();

            pic= BitmapFactory.decodeStream(streamIn);
            LoadingCheck =true;

        } catch (MalformedURLException e) {

            e.printStackTrace();
        } catch (IOException e) {

            e.printStackTrace();
        } catch (Exception e) {

            e.printStackTrace();
        }
    }
    View.OnClickListener btnNewsOne_click = new View.OnClickListener() {

        @Override
        public void onClick(View v) {
            NewsClassID1=3;
            NewsClassID2=4;
            btnNewsOne.setBackgroundResource(R.drawable.newbtn2);
            btnNewsTwo.setBackgroundResource(R.drawable.newbtn1);
            NewsJson();
            NewsAdapterList();


        }
    };

    View.OnClickListener btnNewsTwo_click = new View.OnClickListener() {

        @Override
        public void onClick(View v) {
            NewsClassID1=1;
            NewsClassID2=2;
            btnNewsTwo.setBackgroundResource(R.drawable.newbtn2);
            btnNewsOne.setBackgroundResource(R.drawable.newbtn1);
            NewsJson();
            NewsAdapterList();

        }
    };
    private void InitialComponent() {

        listView = (ListView)getView().findViewById(R.id.listView_News);
        btnNewsOne = (Button) getView().findViewById(R.id.btnNewsOne);
        btnNewsOne.setOnClickListener(btnNewsOne_click);
        btnNewsTwo = (Button) getView().findViewById(R.id.btnNewsTwo);
        btnNewsTwo.setOnClickListener(btnNewsTwo_click);


    }
    public  Boolean LoadingCheck(){
        return LoadingCheck;
    }
   //等待Load圖片完成

    private Button btnNewsOne,btnNewsTwo;
    private ListView listView;//建listview物件
    ImageView image;



}
