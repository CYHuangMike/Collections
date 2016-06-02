package info.androidhive.materialdesign.Factorys;

import android.content.Context;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import info.androidhive.materialdesign.activity.Tools.MyThread;
import info.androidhive.materialdesign.model.CCompany;

/**
 * Created by iii on 2016/5/12.
 */
public class TourFactory {
    public static Boolean flag=false;
    private int layer ;
    private String building;
    String ResposneJson;
    String aspx = "FloorTourOne.aspx";


    public ArrayList<CCompany> list = new ArrayList<CCompany>();

    //public TourFactory()  { loadData();}

    public  int getLayer(){return layer;}
    public void setLayer(int layer) {this.layer = layer;}
    public String getBuilding() {return building;}
    public void setBuilding(String building) {this.building = building;}

    public CCompany[] getAll()
    {
        return list.toArray(new CCompany[list.size()]);
    }

    public CCompany findByName(String name)
    {
        CCompany company = null;
        for(CCompany c :list)
        {
            if(name == c.getCompanyName())
            {
                company = c;
                Log.d("findbyname", company.getCompanyName());
                //break;
            }
        }
        return  company;
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

                        list.add(new CCompany(dataArray.getJSONObject(i).getString("CompanyName"),
                                dataArray.getJSONObject(i).getString("companyID"),
                                dataArray.getJSONObject(i).getString("content"),
                                dataArray.getJSONObject(i).getString("addr"),
                                dataArray.getJSONObject(i).getString("picLocation"),
                                dataArray.getJSONObject(i).getString("picBrandPath")));
                        Log.d("i",dataArray.getJSONObject(i).getString("CompanyName"));


                    }
                    for (CCompany a:list) {
                        Log.d("list", a.getCompanyName());
                    }

                    flag = true;

                } catch (JSONException e) {
                    e.printStackTrace();
                }


            }
            @Override
            public void downloadTour(int layer, String building, String aspx, String ResposneJson) {
                super.downloadTour(layer,building, aspx, ResposneJson);

            }
            @Override
            public void run() {
                downloadTour(layer,building, aspx, ResposneJson);
            }
        };


        myThread.start(context);


    }


}
