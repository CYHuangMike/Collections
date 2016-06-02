using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// CImageProess 的摘要描述
/// </summary>
public class CImageProess : System.Web.UI.Page
{
    private static CImageProess instance;
    private string fileName; //取目前最大的ID 命名為xxxxxxx.jpg ; png ; gif
    public static string imageName { get; set; } //圖片檔名.副檔名, 用來存資料庫
    /// <summary>
    /// 只載入一次 靜態
    /// </summary>
    /// <returns></returns>
    public static CImageProess get()
    {
        if (instance == null)
        {
            instance = new CImageProess();
        }
        return instance;
    }
    /// <summary>
    /// 取出客戶端上傳照片
    /// </summary>
    public void UploadImage(FileUpload myFileUpload, string filePath, int maxPx)
    {        
        fileName = Guid.NewGuid().ToString(); // 亂數得要圖片的檔名
        imageName = ""; // ex:檔名 1604151001.jpg

        //是否允許上傳
        bool fileAllow = false;

        //設定允許上載的延伸檔名類型
        string[] allowExtensions = { ".jpg", ".gif", ".png" };

        //取得網站根目錄路徑 大圖先丟到delete資料夾
        string path = HttpContext.Current.Request.MapPath("~/pic/delete/");

        //檢查是否有檔案
        if (myFileUpload.HasFile)
        {
            //取得上載檔案副檔名，並轉換成小寫字母
            string fileExtension =
                System.IO.Path.GetExtension(myFileUpload.FileName).ToLower();


            //檢查副檔名是否符合限定類型
            for (int i = 0; i < allowExtensions.Length; i++)
            {
                if (fileExtension == allowExtensions[i])
                {
                    fileExtension = ".jpg";
                    fileAllow = true;
                }

            }
            if (fileAllow)
            {
                try
                {                    
                    //將 檔名 跟 附檔名存入 資料庫 設為路徑                    
                    imageName = fileName + fileExtension;
                    //儲存檔案到磁碟 
                    myFileUpload.SaveAs(path + fileName + fileExtension); //路徑 + 檔名 + jpg/png                    
                    //maxPx 宣告一個圖片尺寸的設定值(Max寬度)
                    addImage(myFileUpload, filePath, fileExtension, maxPx);//將大圖縮小後 刪除 大圖  存小圖 // 
                }
                catch (Exception)
                {

                }
            }
        }
    }

    /// <summary>
    /// 將上傳的照片縮成小圖
    /// </summary>
    /// <param name="myFileUpload">圖片上傳按鈕</param>
    /// <param name="fileExtension">傳入 jpg , gif , png </param>
    private void addImage(FileUpload myFileUpload, string filePath, string fileExtension, int maxPx)
    {   
        string m大圖路徑 = "~/pic/delete/" + this.fileName + fileExtension;
        System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(m大圖路徑));    
        
        // 取得影像的格式 (Bmp, Gif, Icon, Jpeg, Png, Tiff, Wmf, ...etc) 
        ImageFormat thisFormat = image.RawFormat;

        //縮圖用    
        int fixWidth = 0;
        int fixHeight = 0;

        //如果圖片的寬大於最大值(maxPx pixel)，或圖片的高大於最大值(maxPx pixel)，就往下執行 
        if (image.Width > maxPx || image.Height > maxPx)
        {
            //圖片的寬大於圖片的高 
            if (image.Width >= image.Height)
            {
                //設定修改後的圖寬 
                fixWidth = maxPx;

                //設定修改後的圖高 
                fixHeight = 
                    Convert.ToInt32((Convert.ToDouble(fixWidth) / Convert.ToDouble(image.Width)) * Convert.ToDouble(image.Height));
            }
            else
            {
                //設定修改後的圖高 
                fixHeight = maxPx;

                //設定修改後的圖寬 
                fixWidth = 
                    Convert.ToInt32((Convert.ToDouble(fixHeight) / Convert.ToDouble(image.Height)) * Convert.ToDouble(image.Width));
            }
        }
        else  //圖片沒有超過設定值，不執行縮圖                      
        {
            fixHeight = image.Height;
            fixWidth = image.Width;
        }

        //輸出一個新圖(就是修改過的圖) 
        Bitmap imageOutput = new Bitmap(image, fixWidth, fixHeight);

        //副檔名不應該這樣給，但因為此範例沒有讀取檔案的部份所以demo就直接給啦 
        string fixSaveName = string.Concat(this.fileName, fileExtension);

        //釋放掉圖檔
        image.Dispose();

        //將修改過的圖以原格式存入原位置
        imageOutput.Save(string.Concat(Server.MapPath(filePath), fixSaveName), thisFormat);

        // 釋放記憶體 (這行若寫在 imageOutput.Save() 之前，會造成修改結果無法存回原始圖檔，只能另存成一個新的圖檔)。 
        // 若要將修改結果，存成另一個新圖檔，就將此行移至 imageOutput.Save() 之前，並指派一個不同檔名給 fixSaveName 變數 
        imageOutput.Dispose();

        FilePicDelete(m大圖路徑);//取得小圖後 刪除大圖
    }

    /// <summary>
    /// 刪除單個檔檔或圖片
    /// </summary>
    /// <param name="path">當前檔的熱門片DVD路徑</param>
    /// <returns>是否刪除成功</returns>
    public bool FilePicDelete(string path)
    {
        bool ret = false;
        System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));
        if (file.Exists)
        {
            file.Delete();
            ret = true;
        }
        return ret;
    }
}