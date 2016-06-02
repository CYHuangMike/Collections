package info.androidhive.materialdesign.activity.Tools;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.util.ArrayList;
import java.util.List;

import info.androidhive.materialdesign.model.CCoupon;

/**
 * Created by Ray on 2016/5/21.
 */
public class CDbManager extends SQLiteOpenHelper {

    // Database Version
    private static final int DATABASE_VERSION = 1;
    // Database Name
    private static final String DATABASE_NAME = "couponDB.db";
    // Contacts table name
    private static final String TABLE_COUPON = "tCoupon";

    public CDbManager(Context context) {
        /*
        參數1:誰要用就傳誰
        參數2:資料庫, 寫死一個
        參數3:Cursor建議在其他地方做所以放null
        參數4:版本,因為資料表都用同一個資料庫, 所以版本給1
        */
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    public void Create(String tableName, ContentValues data) {
        //參數1:資料表名稱
        //參數2:有沒有哪一個欄位是必填?放到UI檢查所以這裡給null
        //參數3:要放的資料,去工廠放
        getWritableDatabase().insert(tableName, null, data);
    }

    public void Update(String tableName, ContentValues data, int pk) {
        getWritableDatabase().update(tableName, data, " _id=" + String.valueOf(pk), null);
    }

    // 如果dataBase還沒有建立, 就會啟動onCreate建立資料庫
    // onCreate就要建立資料表
    @Override
    public void onCreate(SQLiteDatabase db) {
        String sql = "CREATE TABLE " + TABLE_COUPON + " (";
        sql += " _id INTERGER PRIMARY KEY,"; //google sqlite辨識用的主鍵
        sql += " fFacilityID TEXT,";
        sql += " fFacility TEXT,";
        sql += " fCouponId TEXT NOT NULL,";
        sql += " fTitle TEXT,";
        sql += " fContent TEXT,";
        sql += " fQrCode TEXT,";
        sql += " fUsed TEXT)";

        db.execSQL(sql);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int i, int i1) {
        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_COUPON);
        // Creating tables again
        onCreate(db);
    }

    public Cursor GetBySql(String sql) {
        //參數2:傳回的參數,現在沒有,就null
        return getReadableDatabase().rawQuery(sql, null);
    }

    // Getting All ExitedCoupon
    public List<CCoupon> GetAllExitedCoupon() {
        List<CCoupon> list = new ArrayList<CCoupon>();
        // Select All Query
        String selectQuery = "SELECT * FROM " + TABLE_COUPON;

        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        // looping through all rows and adding to list
        if (cursor.moveToFirst()) {
            do {
                CCoupon coupon = new CCoupon();
                coupon.setFacilityID(Integer.parseInt(cursor.getString(1)));
                coupon.setFacility(cursor.getString(2));
                coupon.setCouponID(Integer.parseInt(cursor.getString(3)));
                coupon.setTitle(cursor.getString(4));
                coupon.setContent(cursor.getString(5));
                coupon.setQrCode(cursor.getString(6));
                // Adding coupon to list
                list.add(coupon);
            } while (cursor.moveToNext());
        }
        // return list
        return list;
    }
}
