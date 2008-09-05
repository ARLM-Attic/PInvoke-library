using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace Discuz.Album
{
    public class Globals
    {
        /// <summary>
        /// 获取缩略图地址
        /// </summary>
        /// <param name="fileName">图片地址</param>
        /// <returns></returns>
        public static string GetThumbnailImage(string fileName)
        {
            string extname = Path.GetExtension(fileName);

            if (extname == string.Empty)
                return string.Empty;

            return fileName.Replace(extname, "_thumbnail" + extname);
        }

        /// <summary>
        /// 获取方图缩略图文件名
        /// </summary>
        /// <param name="fileName">原图文件名</param>
        /// <returns></returns>
        public static string GetSquareImage(string fileName)
        {
            string extname = Path.GetExtension(fileName);

            if (extname == string.Empty)
                return string.Empty;

            return fileName.Replace(extname, "_square" + extname);
        }

        /// <summary>
        ///	上传Space文件
        /// </summary>
        /// <param name="uploadFile">上传文件对象</param>
        /// <param name="saveDir">保存地址</param>
        /// <param name="createThumbnailImage">是否生成缩略图</param>
        /// <returns></returns>
        public static string UploadSpaceFile(HttpPostedFile uploadFile, string saveDir, bool createThumbnailImage)
        {
            string sSavePath = saveDir;
            int nFileLen = uploadFile.ContentLength;
            byte[] myData = new Byte[nFileLen];
            uploadFile.InputStream.Read(myData, 0, nFileLen);
            //string sFilename = Path.GetFileName(uploadFile.FileName).ToLower();
            string fileextname = Path.GetExtension(uploadFile.FileName).ToLower();
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));
            string sFilename = Environment.TickCount.ToString() + random.Next(1000, 9999).ToString() + fileextname;
            while (File.Exists(sSavePath + sFilename))
            {
                //sFilename = Path.GetFileNameWithoutExtension(uploadFile.FileName) + file_append.ToString() + Path.GetExtension(uploadFile.FileName).ToLower();
                sFilename = Environment.TickCount.ToString() + random.Next(1000, 9999).ToString() + fileextname;
            }


            if (Common.Utils.InArray(Path.GetExtension(uploadFile.FileName).ToLower(), ".jpg,.jpeg,.gif,.png") && createThumbnailImage)
            { //上传图片文件jpg,gif

                //Bitmap myBitmap;
                try
                {
                    FileStream newFile = new FileStream(sSavePath + sFilename, FileMode.Create);
                    newFile.Write(myData, 0, myData.Length);
                    newFile.Close();


                    //myBitmap = new Bitmap(sSavePath + sFilename);
                    //myBitmap.Dispose();
                    //if ((Path.GetExtension(uploadFile.FileName).ToLower() == ".jpg"))
                    //GetThumbnailImage(150,150,sSavePath + sFilename);

                    string extension = Path.GetExtension(sSavePath + sFilename);
                    Common.Thumbnail.MakeThumbnailImage(sSavePath + sFilename, (sSavePath + sFilename).Replace(extension, "_thumbnail" + extension), 150, 150);
                    Common.Thumbnail.MakeSquareImage(sSavePath + sFilename, (sSavePath + sFilename).Replace(extension, "_square" + extension), 100);
                    return sFilename;
                }
                catch (ArgumentException errArgument)
                {
                    File.Delete(sSavePath + sFilename);
                    string errinfo = errArgument.Message;
                    return sFilename;
                }
            }
            else //上传除图片文件以外的全部文件
            {
                try
                {
                    uploadFile.SaveAs(sSavePath + sFilename);
                    return sFilename;
                }
                catch (ArgumentException errArgument)
                {
                    File.Delete(sSavePath + sFilename);
                    string errinfo = errArgument.Message;
                    return sFilename;
                }

            }
        }

    }
}
