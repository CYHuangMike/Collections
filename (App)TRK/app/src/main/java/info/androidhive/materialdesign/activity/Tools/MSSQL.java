package info.androidhive.materialdesign.activity.Tools;


import android.annotation.SuppressLint;
import android.os.StrictMode;
import android.util.Log;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;

public class MSSQL {
    private boolean _isOpened=false;
    String ipaddress, db, username, password;
    public static Connection connect;
    public Statement st;
    public boolean isOpened()
    {
        return _isOpened;
    }

    @SuppressLint("NewApi")
    private Connection ConnectionHelper(String user, String password,
                                        String database, String server) {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder()
                .permitAll().build();
        StrictMode.setThreadPolicy(policy);
        Connection connection = null;
        String ConnectionURL = null;
        try {
            Class.forName("net.sourceforge.jtds.jdbc.Driver");
            ConnectionURL = "jdbc:jtds:sqlserver://" + server + ";"
                    + "databaseName=" + database + ";user=" + user
                    + ";password=" + password + ";";
            connection = DriverManager.getConnection(ConnectionURL);
        } catch (SQLException se) {
            Log.e("ERRO", se.getMessage());
        } catch (ClassNotFoundException e) {
            Log.e("ERRO", e.getMessage());
        } catch (Exception e) {
            Log.e("ERRO", e.getMessage());
        }
        return connection;
    }

    public MSSQL() {

        //設定jdbc連結字串，請依你的SQL Server設定值修改

        try {

            ipaddress = "iii0.database.windows.net:1433";//"192.168.1.128";
            db = "PrjTRK";
            username = "iii";
            password = "P@ssw0rd";
            connect = ConnectionHelper(username, password, db, ipaddress);

            if (connect.isClosed()==false)
            {
                _isOpened=true;
                System.out.println("connect ok");
            }
            else
            {
                _isOpened=false;
                System.out.println("connect fail");
            }


        }

        catch (Exception ex) {

            ex.printStackTrace();

        }

    }
}
