package info.androidhive.materialdesign.activity.Tools;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;


import com.google.android.gms.appindexing.AppIndex;
import com.google.android.gms.common.api.GoogleApiClient;


import info.androidhive.materialdesign.R;
import info.androidhive.materialdesign.activity.manu.Traffic.TrafficActivity;
import info.androidhive.materialdesign.activity.manu.DmFragment;
import info.androidhive.materialdesign.activity.manu.HomeFragment;
import info.androidhive.materialdesign.activity.manu.ManliCardFragment;

import info.androidhive.materialdesign.activity.manu.PresentMCFragment;

import info.androidhive.materialdesign.activity.manu.Tour.RestaurantFragment;



public class MainActivity extends AppCompatActivity implements FragmentDrawer.FragmentDrawerListener {

    //private static String TAG = MainActivity.class.getSimpleName();

    private Toolbar mToolbar;
    private FragmentDrawer drawerFragment;

    private GoogleApiClient client;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_main);
        mToolbar = (Toolbar) findViewById(R.id.toolbar);

        setSupportActionBar(mToolbar);
        getSupportActionBar().setDisplayShowHomeEnabled(true);
        drawerFragment = (FragmentDrawer)
                getSupportFragmentManager().findFragmentById(R.id.fragment_navigation_drawer);
        drawerFragment.setUp(R.id.fragment_navigation_drawer, (DrawerLayout) findViewById(R.id.drawer_layout), mToolbar);
        drawerFragment.setDrawerListener(this);

        // display the first navigation drawer view on app launch
        displayView(0);

        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        client = new GoogleApiClient.Builder(this).addApi(AppIndex.API).build();
    }

    // 在Activity中加入以下程式碼 //返回按鍵觸發
//    @Override
//    public void onBackPressed() {
//        android.app.FragmentManager fm = this.getFragmentManager();
//
//        if (fm.getBackStackEntryCount() == 0) {
//            this.finish();
//        } else {
//            fm.popBackStack();
//        }
//    }

    @Override
    public void onDrawerItemSelected(View view, int position) {
        displayView(position);
    }

    private void displayView(int position) {
        FragmentManager fragmentManager = getSupportFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        Fragment fragment = null;
        String title = getString(R.string.app_name);

        switch (position) {
            case 0:
                title = getString(R.string.title_home);
                fragment = new HomeFragment();
                break;

            case 1:
                title = getString(R.string.title_retaurant);
                fragment = new RestaurantFragment();
                break;

            case 2:
                title = getString(R.string.title_searchMC);
                fragment = new ManliCardFragment();
                break;

            case 3:
                title = getString(R.string.title_coupon);
                fragment = new PresentMCFragment();
                break;

            case 4:
                title = getString(R.string.title_DM);
                fragment = new DmFragment();
                break;

            case 5:
                title = getString(R.string.title_traffic);
                //Activity m = new TrafficActivity();
                Intent intent = new Intent();
                intent.setClass(MainActivity.this,TrafficActivity.class);
                startActivity(intent);
                break;


            default:
                break;
        }

        if (fragment != null) {
            fragmentTransaction.replace(R.id.container_body, fragment);
            fragmentTransaction.commit();
            // set the toolbar title
            getSupportActionBar().setTitle(title);
        }
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }


        return super.onOptionsItemSelected(item);
    }
}

