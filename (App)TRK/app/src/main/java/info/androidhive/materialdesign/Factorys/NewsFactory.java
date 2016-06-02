package info.androidhive.materialdesign.Factorys;

import android.content.Context;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import info.androidhive.materialdesign.activity.Tools.MyThread;
import info.androidhive.materialdesign.model.CNews;

/**
 * Created by iii on 2016/5/12.
 */
public class NewsFactory {
    public static Boolean flag=false;
    private int NewsClassID1;
    private int NewsClassID2;
    String ResposneJson;
    String aspx = "NewsJSON.aspx";

    public ArrayList<CNews> list = new ArrayList<CNews>();

    public int getNewsClassID1() {return NewsClassID1;}
    public void setNewsClassID1(int newsClassID1) {NewsClassID1 = newsClassID1;}
    public int getNewsClassID2() {return NewsClassID2;}
    public void setNewsClassID2(int newsClassID2) {NewsClassID2 = newsClassID2;}

    public CNews[] getAll()
    {
        return list.toArray(new CNews[list.size()]);
    }

    public CNews findByNews(String title)
    {
        CNews news = null;
        for(CNews n :list)
        {
            if(title == n.getNTitle())
            {
                news = n;
                Log.d("findByNews", news.getNTitle());

            }
        }
        return  news;
    }

    public Boolean flag(){
        return flag;
    }

    public void loadData(final Context context ) {
        flag=false;

        MyThread myThread = new MyThread() {

            public void mJsonResult(String responseJson) throws JSONException {
                JSONObject obj;

                try {
                    obj = new JSONObject(responseJson);
                    String data = obj.getString("data");
                    Log.d("data", data.toString());
                    JSONArray dataArray = new JSONArray(data);


                    if (data == null)
                        return ;

                    list.clear();
                    for (int i = 0; i <dataArray.length() ; i++) {

                        list.add(new CNews(dataArray.getJSONObject(i).getString("NCompanyName"),
                                dataArray.getJSONObject(i).getString("NID"),
                                dataArray.getJSONObject(i).getString("NTitle"),
                                dataArray.getJSONObject(i).getString("NContent"),
                                dataArray.getJSONObject(i).getString("NStartDate"),
                                dataArray.getJSONObject(i).getString("NEndDate"),
                                dataArray.getJSONObject(i).getString("NpicPath"),
                                dataArray.getJSONObject(i).getString("NClass")));

                        Log.d("i",dataArray.getJSONObject(i).getString("NTitle"));


                    }
                    for (CNews a:list) {
                        Log.d("list", a.getNTitle());
                    }

                    flag = true;

                } catch (JSONException e) {
                    e.printStackTrace();
                }


            }
            @Override
            public void downloadNews(int NewsClassID1, int NewsClassID2, String aspx, String ResposneJson) {
                super.downloadNews(NewsClassID1,NewsClassID2, aspx, ResposneJson);
                //Log.d("tourfactry", "download2:ok ");
            }
            @Override
            public void run() {
                downloadNews(NewsClassID1,NewsClassID2, aspx, ResposneJson);
            }
        };


        myThread.start(context);


    }


}
