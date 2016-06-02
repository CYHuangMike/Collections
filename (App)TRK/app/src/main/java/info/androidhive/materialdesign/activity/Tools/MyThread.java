package info.androidhive.materialdesign.activity.Tools;


import android.app.ProgressDialog;
import android.content.Context;
import android.os.Handler;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;


//SQL INJECTION 要進ASP.NET裡面去改成資料查詢 查工廠! 這樣才能避免

public abstract class MyThread implements Runnable {

    public ProgressDialog progressDialog; //延遲等待
    private Thread t;
    boolean JSONCheck = false;
    boolean Message;
    StringBuffer buffer;
    HttpURLConnection connection = null;
    BufferedReader reader;


    //建構子
    public MyThread() {    }

    public void downloadQrCode(Handler myHandler) {
        myHandler.sendEmptyMessage(1);
        stop();
        progressDialog.dismiss();//關閉等待
    }


    public void downloadCoupon(int couponId, String aspx, String ResposneJson) {
        JSONCheck = false;//判斷JSON資料是否撈完的旗標
        buffer = null;
        final String queryString
                = "?id=" + couponId;
        final String CardURL = JsonUrl.url + "/JSON/" + aspx + queryString;
        try {
            try {
                //改 URL 就能連到其他網站取JSON
                URL url = new URL(CardURL);//撈網址
                connection = (HttpURLConnection) url.openConnection();
                connection.connect();
                InputStream stream = connection.getInputStream();
                reader = new BufferedReader(new InputStreamReader(stream));
                buffer = new StringBuffer();
                String line = "";
                while ((line = reader.readLine()) != null) {
                    buffer.append(line);
                }
                //JSON的值都放到站存區以後才會出while迴圈
                ResposneJson = buffer.toString();
                Log.i("QRCode", ResposneJson);
                JSONCheck = true;
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
                if (connection != null) {
                    connection.disconnect();
                }
                try {
                    if (reader != null) {
                        reader.close();
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            ///-------------執行續需要一個無窮回圈讓他跑完有值再出來-------------//
            while (true) {
                if (JSONCheck)
                    break;
            }
            //JSON取得Message並轉型為布林值
            Message = new JSONObject(ResposneJson).getBoolean("Message");

            if (Message) {
                //成功
                mJsonResult(ResposneJson);
            }

        } catch (JSONException e) {
            e.printStackTrace();
            progressDialog.dismiss();//關閉等待
        } finally {
            stop();
            progressDialog.dismiss();//關閉等待
        }
    }

    /// 樂園設施
    public void downloadFacility(String mInput, String aspx,String ResposneJson) {
        JSONCheck = false;//判斷JSON資料是否撈完的旗標
        buffer = null;
        final String queryString
                = "?id=" + mInput;
        final String CardURL = JsonUrl.url + "/JSON/" + aspx + queryString;
        try {
            try {
                //改 URL 就能連到其他網站取JSON
                URL url = new URL(CardURL);//撈網址
                connection = (HttpURLConnection) url.openConnection();
                //connection.setReadTimeout(5000);            //設置讀取超時為5秒
                //connection.setConnectTimeout(10000);        //設置連接網路超時為10秒
                //connection.setDoOutput(true);                //可寫入資料至伺服器
                //connection.setDoInput(true);                //可從伺服器取得資料
                connection.connect();
                InputStream stream = connection.getInputStream();
                reader = new BufferedReader(new InputStreamReader(stream));
                buffer = new StringBuffer();
                String line = "";
                while ((line = reader.readLine()) != null) {
                    buffer.append(line);
                }
                //JSON的值都放到站存區以後才會出while迴圈

                ResposneJson = buffer.toString();
                Log.i("ResposneJson", ResposneJson);
                JSONCheck = true;
                //JSON取得Message並轉型為布林值
                Message = new JSONObject(ResposneJson).getBoolean("Message");

                if (Message)
                    mJsonResult(ResposneJson);

            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
                if (connection != null) {
                    connection.disconnect();
                }
                try {
                    if (reader != null) {
                        reader.close();
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            ///-------------執行續需要一個無窮回圈讓他跑完有值再出來-------------//
            while (true) {
                if (JSONCheck)
                    break;
            }
            //JSON取得Message並轉型為布林值
            Message = new JSONObject(ResposneJson).getBoolean("Message");

            if (Message) {
                //成功
                mJsonResult(ResposneJson);
            }

        } catch (JSONException e) {
            e.printStackTrace();
            progressDialog.dismiss();//關閉等待
        } finally {
            stop();
            progressDialog.dismiss();//關閉等待
        }
    }

    public void downloadTour2(Handler myHandler) {

        myHandler.sendEmptyMessage(1);
        stop();
        progressDialog.dismiss();//關閉等待
    }


    ///最新消息系統
    public void downloadNews(int NewsClassID1,int NewsClassID2, String aspx, String ResposneJson) {
        JSONCheck = false;//判斷JSON資料是否撈完的旗標
        buffer = null;
        final String queryString
                = "?NewsClassID1=" + NewsClassID1+"&NewsClassID2="+NewsClassID2;
        final String CardURL = JsonUrl.url + "/JSON/" + aspx + queryString;
        try {
            try {
                //改 URL 就能連到其他網站取JSON
                URL url = new URL(CardURL);//撈網址
                Log.d("url", url.toString());
                connection = (HttpURLConnection) url.openConnection();
                connection.connect();
                InputStream stream = connection.getInputStream();
                reader = new BufferedReader(new InputStreamReader(stream));
                buffer = new StringBuffer();
                String line = "";
                while ((line = reader.readLine()) != null) {
                    buffer.append(line);
                }
                //JSON的值都放到站存區以後才會出while迴圈

                ResposneJson = buffer.toString();
                Log.i("ResposneJson", ResposneJson);
                JSONCheck = true;

            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
                if (connection != null) {
                    connection.disconnect();
                }
                try {
                    if (reader != null) {
                        reader.close();
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            ///-------------執行續需要一個無窮回圈讓他跑完有值再出來-------------//
            while (true) {
                if (JSONCheck)
                    break;
            }

            if (JSONCheck) {
                //成功
                mJsonResult(ResposneJson);
            }
        } catch (JSONException e) {
            e.printStackTrace();
            progressDialog.dismiss();//關閉等待
        } finally {
            stop();
            progressDialog.dismiss();//關閉等待
        }
    }


    ///導覽系統1
    public void downloadTour(int layer,String building, String aspx, String ResposneJson) {
        JSONCheck = false;//判斷JSON資料是否撈完的旗標
        buffer = null;
        final String queryString
                = "?layer=" + layer+"&building="+building;
        final String CardURL = JsonUrl.url + "/JSON/" + aspx + queryString;
        try {
            try {
                //改 URL 就能連到其他網站取JSON
                URL url = new URL(CardURL);//撈網址
                Log.d("url", url.toString());
                connection = (HttpURLConnection) url.openConnection();
                connection.connect();
                InputStream stream = connection.getInputStream();
                reader = new BufferedReader(new InputStreamReader(stream));
                buffer = new StringBuffer();
                String line = "";
                while ((line = reader.readLine()) != null) {
                    buffer.append(line);
                }
                //JSON的值都放到站存區以後才會出while迴圈

                ResposneJson = buffer.toString();
                Log.i("ResposneJson", ResposneJson);
                JSONCheck = true;

            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
                if (connection != null) {
                    connection.disconnect();
                }
                try {
                    if (reader != null) {
                        reader.close();
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            ///-------------執行續需要一個無窮回圈讓他跑完有值再出來-------------//
            while (true) {
                if (JSONCheck)
                    break;
            }

            if (JSONCheck) {
                //成功
                mJsonResult(ResposneJson);
            }
        } catch (JSONException e) {
            e.printStackTrace();
            progressDialog.dismiss();//關閉等待
        } finally {
            stop();
            progressDialog.dismiss();//關閉等待
        }
    }
    //// 戶外探索
    public void download(String mInput, String aspx, Handler myHandler, String ResposneJson) {
        JSONCheck = false;//判斷JSON資料是否撈完的旗標
        buffer = null;
        final String queryString
                = "?id=" + mInput;
        final String CardURL = JsonUrl.url + "/JSON/" + aspx + queryString;
        try {
            try {
                //改 URL 就能連到其他網站取JSON
                URL url = new URL(CardURL);//撈網址
                connection = (HttpURLConnection) url.openConnection();
                //connection.setReadTimeout(5000);            //設置讀取超時為5秒
                //connection.setConnectTimeout(10000);        //設置連接網路超時為10秒
                //connection.setDoOutput(true);                //可寫入資料至伺服器
                //connection.setDoInput(true);                //可從伺服器取得資料
                connection.connect();
                InputStream stream = connection.getInputStream();
                reader = new BufferedReader(new InputStreamReader(stream));
                buffer = new StringBuffer();
                String line = "";
                while ((line = reader.readLine()) != null) {
                    buffer.append(line);
                }
                //JSON的值都放到站存區以後才會出while迴圈

                ResposneJson = buffer.toString();
                Log.i("ResposneJson", ResposneJson);
                JSONCheck = true;

            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } finally {
                if (connection != null) {
                    connection.disconnect();
                }
                try {
                    if (reader != null) {
                        reader.close();
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            ///-------------執行續需要一個無窮回圈讓他跑完有值再出來-------------//
            while (true) {
                if (JSONCheck)
                    break;
            }
            //JSON取得Message並轉型為布林值
            Message = new JSONObject(ResposneJson).getBoolean("Message");

            if (Message) {
                //成功
                mJsonResult(ResposneJson);
                myHandler.sendEmptyMessage(1);

            } else {
                myHandler.sendEmptyMessage(2);
            }
        } catch (JSONException e) {
            e.printStackTrace();
            progressDialog.dismiss();//關閉等待
        } finally {
            stop();
            progressDialog.dismiss();//關閉等待
        }
    }

    public void mJsonResult(String resposneJson) throws JSONException {
    }

    public void run() {
    }

    public void start(Context context) {
        if (t == null) {
            //開啟等待
            progressDialog = ProgressDialog.show(context, "請稍候", "資料確認中");
            t = new Thread(this);
            t.start();
        }
    }

    public void stop() { //釋放執行續
        if (t != null) {
            t = null;
        }
    }




}
