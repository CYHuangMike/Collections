package info.androidhive.materialdesign.activity.Tools;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Camera;
import android.graphics.Matrix;
import android.graphics.PointF;
import android.graphics.RectF;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.view.animation.AnimationUtils;
import android.widget.ImageButton;
import android.widget.ImageView;

import info.androidhive.materialdesign.R;


public class ImageViewHelper {
    //這邊只需要換成sdcard的圖片陣列 就可以仿一個看漫畫的app
    private int[] images = {
            R.drawable.dm1, R.drawable.dm2, R.drawable.dm3,R.drawable.dm4, R.drawable.dm5, R.drawable.dm6
            , R.drawable.dm7, R.drawable.dm8, R.drawable.dm9, R.drawable.dm10, R.drawable.dm11, R.drawable.dm12
    };

    private int imageIndex = 0;
    private ImageView imageView;
    private Matrix matrix = new Matrix();
    private Matrix savedMatrix = new Matrix();

    private Bitmap bitmap;

    private float minScaleR;// 最小縮放比例

    private static final int NONE = 0;// 初始狀態
    private static final int DRAG = 1;// 拖曳狀態
    private static final int ZOOM = 2;// 縮放狀態
    private int mode = NONE;

    private PointF prev = new PointF();
    private PointF mid = new PointF();
    private float dist = 1f;
    private DisplayMetrics dm;
    private ImageButton zoomInButton;
    private ImageButton zoomOutButton;
    private Context mContext;
    private Camera camera;
    public ImageViewHelper(Context mContext, DisplayMetrics dm, ImageView imageView, Bitmap bitmap, ImageButton zoomInButton, ImageButton zoomOutButton){
        camera = new Camera();
        this.dm = dm;
        this.imageView = imageView;
        this.zoomInButton = zoomInButton;
        this.zoomOutButton = zoomOutButton;
        this.bitmap = bitmap;
        this.mContext = mContext;
        setImageSize();
        minZoom();
        center();

        imageView.setImageMatrix(matrix);

    }
    public Matrix getMatrix(){
        return matrix;
    }
    public void setZoomIn(){

        minScaleR = Math.min(
                (float) dm.widthPixels / (float) bitmap.getWidth(),
                (float) dm.heightPixels / (float) bitmap.getHeight());
        if (minScaleR < 1.0) {
            matrix.postScale(minScaleR+1f, minScaleR+1f);
        }
        else{
            matrix.postScale(minScaleR, minScaleR);
        }
    }
    public void setZoomOut(){
        minScaleR = Math.max(
                (float) dm.widthPixels / (float) bitmap.getWidth(),
                (float) dm.heightPixels / (float) bitmap.getHeight());
        if (minScaleR > 1.0) {
            matrix.postScale((minScaleR-(int)minScaleR),(minScaleR-(int)minScaleR));
        }
        else{
            matrix.postScale(minScaleR, minScaleR);
        }
    }

    //取得最小的比例, 假設圖片比螢幕大
    //則螢幕(寬/長)/圖片(寬/長)會小於1 那麼也就是將圖片進行縮小
    //反之 則進行放大 而圖片越小 放大倍數則會越大
    //如果螢幕跟圖片大小相同 則倍數會為1 即不變
    public void minZoom() {
        minScaleR = Math.min(
                (float) dm.widthPixels / (float) bitmap.getWidth(),
                (float) dm.heightPixels / (float) bitmap.getHeight());
        if (minScaleR <= 1.0) {
            matrix.postScale(minScaleR, minScaleR);
        }
        else{
            matrix.postScale(1.5f, 1.5f);
        }
    }

    public void center() {
        center(true, true);
    }

    //橫向、縱向置中
    public void center(boolean horizontal, boolean vertical) {

        Matrix m = new Matrix();
        m.set(matrix);
        RectF rect = new RectF(0, 0, bitmap.getWidth(), bitmap.getHeight());
        m.mapRect(rect);

        float height = rect.height();
        float width = rect.width();

        float deltaX = 0, deltaY = 0;

        if (vertical) {
            // 圖片小於螢幕大小，則置中顯示。
            //大於螢幕，上方則留空白則往上移，下方留空白則往下移
            int screenHeight = dm.heightPixels;
            if (height < screenHeight) {
                deltaY = (screenHeight - height) / 2 - rect.top;
            } else if (rect.top > 0) {
                deltaY = -rect.top;
            } else if (rect.bottom < screenHeight) {
                deltaY = imageView.getHeight() - rect.bottom;
            }
        }

        if (horizontal) {
            int screenWidth = dm.widthPixels;
            if (width < screenWidth) {
                deltaX = (screenWidth - width) / 2 - rect.left;
            } else if (rect.left > 0) {
                deltaX = -rect.left;
            } else if (rect.right < screenWidth) {
                deltaX = screenWidth - rect.right;
            }
        }
        matrix.postTranslate(deltaX, deltaY);
    }

    //兩點的距離
    public float spacing(MotionEvent event) {
        float x = event.getX(0) - event.getX(1);
        float y = event.getY(0) - event.getY(1);
        return (float) Math.sqrt(x * x + y * y);
    }

    //兩點的中點
    public void midPoint(PointF point, MotionEvent event) {
        float x = event.getX(0) + event.getX(1);
        float y = event.getY(0) + event.getY(1);
        point.set(x / 2, y / 2);
    }
    //建立圖片移動縮放事件 
    public void setImageSize(){
        zoomInButton.setOnClickListener(new OnClickListener(){
            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                setZoomIn();
                center();
                imageView.setImageMatrix(matrix);
            }

        });
        zoomOutButton.setOnClickListener(new OnClickListener(){
            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub
                setZoomOut();
                center();
                imageView.setImageMatrix(matrix);
            }

        });

        imageView.setOnTouchListener(new OnTouchListener(){

            @Override
            public boolean onTouch(View arg0, MotionEvent event) {
                switch (event.getAction() & MotionEvent.ACTION_MASK) {

                    case MotionEvent.ACTION_DOWN:
                        savedMatrix.set(matrix);
                        prev.set(event.getX(), event.getY());
                        mode = DRAG;
                        break;

                    case MotionEvent.ACTION_POINTER_DOWN:
                        dist = spacing(event);
                        // 如果兩點距離超過10, 就判斷為多點觸控模式 即為縮放模式
                        if (spacing(event) > 10f) {
                            savedMatrix.set(matrix);
                            midPoint(mid, event);
                            mode = ZOOM;
                        }
                        break;
                    case MotionEvent.ACTION_UP:
                    case MotionEvent.ACTION_POINTER_UP:
                        //如果進入左邊30個px 則代表換下一張圖
                        if(event.getX()<30){
                            imageIndex++;
                            if(imageIndex<0) imageIndex = images.length-1;
                            else if(imageIndex>images.length-1) imageIndex = 0;
                            Log.e("imageIndex left", imageIndex + "");
                            camera.save();//用來儲存最初的狀態

                            imageView.startAnimation(AnimationUtils.loadAnimation(mContext, R.anim.slide_in_left));
                            camera.getMatrix(matrix);
                            imageView.setImageResource(images[imageIndex]);
                            imageView.startAnimation(AnimationUtils.loadAnimation(mContext, R.anim.slide_in_right));
                            center();
                            camera.restore();//還原最初的狀態

                        }//如果進入右邊30px 則代表換成上一張圖
                        else if(event.getX()>dm.widthPixels-30){
                            imageIndex--;
                            if(imageIndex<0) imageIndex = images.length-1;
                            else if(imageIndex>images.length-1) imageIndex = 0;
                            Log.e("imageIndex right", imageIndex + "");
                            camera.save();

                            imageView.startAnimation(AnimationUtils.loadAnimation(mContext, android.R.anim.slide_out_right));
                            camera.getMatrix(matrix);
                            imageView.setImageResource(images[imageIndex]);
                            imageView.startAnimation(AnimationUtils.loadAnimation(mContext, android.R.anim.slide_in_left));
                            center();
                            camera.restore();

                        }
                        mode = NONE;
                        break;
                    case MotionEvent.ACTION_MOVE:
                        if (mode == DRAG) {
                            matrix.set(savedMatrix);
                            matrix.postTranslate(event.getX() - prev.x, event.getY()
                                    - prev.y);


                        } else if (mode == ZOOM) {
                            float newDist = spacing(event);//偵測兩根手指移動的距離
                            if (newDist > 10f) {
                                matrix.set(savedMatrix);
                                float tScale = newDist / dist;
                                matrix.postScale(tScale, tScale, mid.x, mid.y);

                            }

                        }
                        break;
                }
                imageView.setImageMatrix(matrix);
                center();
                return true;
            }

        });
    }
}
