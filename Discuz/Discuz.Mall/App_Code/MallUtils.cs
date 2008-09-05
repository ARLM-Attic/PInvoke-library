using System;
using System.Web;
using System.IO;
using System.Text;
using System.Drawing;

using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Plugin.Preview;

namespace Discuz.Mall
{
    /// <summary>
    /// 商城工具类
    /// </summary>
    public class MallUtils
    {
        /// <summary>
        /// 操作运行符枚举
        /// </summary>
        public enum OperaCode
        {
            Equal = 1, //等于
            NoEuqal = 2, //不等于
            Morethan = 3, //大于
            MorethanOrEqual = 4, //大于或等于
            Lessthan = 5,  //小于
            LessthanOrEqual = 6 //小于或等于
        }


        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="categoryid">商品分类id</param>
        /// <param name="MaxAllowFileCount">最大允许的上传文件个数</param>
        /// <param name="MaxSizePerDay">每天允许的附件大小总数</param>
        /// <param name="MaxFileSize">单个最大允许的文件字节数</param>/// 
        /// <param name="TodayUploadedSize">今天已经上传的附件字节总数</param>
        /// <param name="AllowFileType">允许的文件类型, 以string[]形式提供</param>
        /// <param name="config">附件保存方式 0=按年/月/日存入不同目录 1=按年/月/日/论坛存入不同目录 2=按论坛存入不同目录 3=按文件类型存入不同目录</param>
        /// <param name="watermarkstatus">图片水印位置</param>
        /// <param name="filekey">File控件的Key(即Name属性)</param>
        /// <returns>文件信息结构</returns>
        public static Goodsattachmentinfo[] SaveRequestFiles(int categoryid, int MaxAllowFileCount, int MaxSizePerDay, int MaxFileSize, int TodayUploadedSize, string AllowFileType, int watermarkstatus, GeneralConfigInfo config, string filekey)
        {
            string[] tmp = Utils.SplitString(AllowFileType, "\r\n");
            string[] AllowFileExtName = new string[tmp.Length];
            int[] MaxSize = new int[tmp.Length];

            for (int i = 0; i < tmp.Length; i++)
            {
                AllowFileExtName[i] = Utils.CutString(tmp[i], 0, tmp[i].LastIndexOf(","));
                MaxSize[i] = Utils.StrToInt(Utils.CutString(tmp[i], tmp[i].LastIndexOf(",") + 1), 0);
            }

            int saveFileCount = 0;

            int fCount = HttpContext.Current.Request.Files.Count;

            for (int i = 0; i < fCount; i++)
            {
                if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
                {
                    saveFileCount++;
                }
            }

            Goodsattachmentinfo[] attachmentinfo = new Goodsattachmentinfo[saveFileCount];
            if (saveFileCount > MaxAllowFileCount)
                return attachmentinfo;

            saveFileCount = 0;

            Random random = new Random(unchecked((int)DateTime.Now.Ticks));


            for (int i = 0; i < fCount; i++)
            {
                if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
                {
                    string filename = Path.GetFileName(HttpContext.Current.Request.Files[i].FileName);
                    string fileextname = Utils.CutString(filename, filename.LastIndexOf(".") + 1).ToLower();
                    string filetype = HttpContext.Current.Request.Files[i].ContentType.ToLower();
                    int filesize = HttpContext.Current.Request.Files[i].ContentLength;
                    string newfilename = "";

                    attachmentinfo[saveFileCount] = new Goodsattachmentinfo();

                    attachmentinfo[saveFileCount].Sys_noupload = "";

                    // 判断 文件扩展名/文件大小/文件类型 是否符合要求
                    if (!(Utils.IsImgFilename(filename) && !filetype.StartsWith("image")))
                    {
                        int extnameid = Utils.GetInArrayID(fileextname, AllowFileExtName);

                        if (extnameid >= 0 && (filesize <= MaxSize[extnameid]) && (MaxFileSize >= filesize /*|| MaxAllSize == 0*/) && (MaxSizePerDay - TodayUploadedSize >= filesize))
                        {
                            TodayUploadedSize = TodayUploadedSize + filesize;
                            string UploadDir = Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/mall/");
                            StringBuilder savedir = new StringBuilder("");
                            //附件保存方式 0=按年/月/日存入不同目录 1=按年/月/日/论坛存入不同目录 2=按论坛存入不同目录 3=按文件类型存入不同目录
                            if (config.Attachsave == 1)
                            {
                                savedir.Append(DateTime.Now.ToString("yyyy"));
                                savedir.Append(Path.DirectorySeparatorChar);
                                savedir.Append(DateTime.Now.ToString("MM"));
                                savedir.Append(Path.DirectorySeparatorChar);
                                savedir.Append(DateTime.Now.ToString("dd"));
                                savedir.Append(Path.DirectorySeparatorChar);
                                savedir.Append(categoryid.ToString());
                                savedir.Append(Path.DirectorySeparatorChar);
                            }
                            else if (config.Attachsave == 2)
                            {
                                savedir.Append(categoryid);
                                savedir.Append(Path.DirectorySeparatorChar);
                            }
                            else if (config.Attachsave == 3)
                            {
                                savedir.Append(fileextname);
                                savedir.Append(Path.DirectorySeparatorChar);
                            }
                            else
                            {
                                savedir.Append(DateTime.Now.ToString("yyyy"));
                                savedir.Append(Path.DirectorySeparatorChar);
                                savedir.Append(DateTime.Now.ToString("MM"));
                                savedir.Append(Path.DirectorySeparatorChar);
                                savedir.Append(DateTime.Now.ToString("dd"));
                                savedir.Append(Path.DirectorySeparatorChar);
                            }


                            newfilename = (Environment.TickCount & int.MaxValue).ToString() + i.ToString() + random.Next(1000, 9999).ToString() + "." + fileextname;

                            //临时文件名称变量. 用于当启动远程附件之后,先上传到本地临时文件夹的路径信息
                            string tempfilename = "";
                            //当支持FTP上传附件且不保留本地附件时
                            if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                            {
                                // 如果指定目录不存在则建立临时路径
                                if (!Directory.Exists(UploadDir + "temp\\"))
                                {
                                    Utils.CreateDir(UploadDir + "temp\\");
                                }
                                tempfilename = "temp\\" + newfilename;
                            }
                            else
                            {
                                // 如果指定目录不存在则建立
                                if (!Directory.Exists(UploadDir + savedir.ToString()))
                                {
                                    Utils.CreateDir(UploadDir + savedir.ToString());
                                }
                            }
                            newfilename = savedir.ToString() + newfilename;

                            try
                            {
                                // 如果是bmp jpg png图片类型
                                if ((fileextname == "bmp" || fileextname == "jpg" || fileextname == "jpeg" || fileextname == "png") && filetype.StartsWith("image"))
                                {

                                    Image img = Image.FromStream(HttpContext.Current.Request.Files[i].InputStream);
                                    if (config.Attachimgmaxwidth > 0 && img.Width > config.Attachimgmaxwidth)
                                    {
                                        attachmentinfo[saveFileCount].Sys_noupload = "图片宽度为" + img.Width.ToString() + ", 系统允许的最大宽度为" + config.Attachimgmaxwidth.ToString();

                                    }
                                    if (config.Attachimgmaxheight > 0 && img.Height > config.Attachimgmaxheight)
                                    {
                                        attachmentinfo[saveFileCount].Sys_noupload = "图片高度为" + img.Width.ToString() + ", 系统允许的最大高度为" + config.Attachimgmaxheight.ToString();
                                    }
                                    if (attachmentinfo[saveFileCount].Sys_noupload == "")
                                    {
                                        if (watermarkstatus == 0)
                                        {
                                            //当支持FTP上传附件且不保留本地附件模式时,则先上传到临时目录下
                                            if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                            {
                                                HttpContext.Current.Request.Files[i].SaveAs(UploadDir + tempfilename);
                                            }
                                            else
                                            {
                                                HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
                                            }
                                            attachmentinfo[saveFileCount].Filesize = filesize;
                                        }
                                        else
                                        {
                                            if (config.Watermarktype == 1 && File.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic)))
                                            {
                                                //当支持FTP上传附件且不保留本地附件模式时,则先上传到临时目录下
                                                if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                                {
                                                    ForumUtils.AddImageSignPic(img, UploadDir + tempfilename, Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic), config.Watermarkstatus, config.Attachimgquality, config.Watermarktransparency);
                                                }
                                                else
                                                {
                                                    ForumUtils.AddImageSignPic(img, UploadDir + newfilename, Utils.GetMapPath(BaseConfigs.GetForumPath + "watermark/" + config.Watermarkpic), config.Watermarkstatus, config.Attachimgquality, config.Watermarktransparency);
                                                }
                                            }
                                            else
                                            {
                                                string watermarkText;
                                                watermarkText = config.Watermarktext.Replace("{1}", config.Forumtitle);
                                                watermarkText = watermarkText.Replace("{2}", "http://" + DNTRequest.GetCurrentFullHost() + "/");
                                                watermarkText = watermarkText.Replace("{3}", Utils.GetDate());
                                                watermarkText = watermarkText.Replace("{4}", Utils.GetTime());

                                                //当支持FTP上传附件且不保留本地附件模式时,则先上传到临时目录下
                                                if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                                {
                                                    ForumUtils.AddImageSignText(img, UploadDir + tempfilename, watermarkText, config.Watermarkstatus, config.Attachimgquality, config.Watermarkfontname, config.Watermarkfontsize);
                                                }
                                                else
                                                {
                                                    ForumUtils.AddImageSignText(img, UploadDir + newfilename, watermarkText, config.Watermarkstatus, config.Attachimgquality, config.Watermarkfontname, config.Watermarkfontsize);
                                                }
                                            }


                                            //当支持FTP上传附件且不保留本地附件模式时,则读取临时目录下的文件信息
                                            if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                            {
                                                // 获得加水印后的文件长度
                                                attachmentinfo[saveFileCount].Filesize = new FileInfo(UploadDir + tempfilename).Length;
                                            }
                                            else
                                            {
                                                // 获得加水印后的文件长度
                                                attachmentinfo[saveFileCount].Filesize = new FileInfo(UploadDir + newfilename).Length;
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    attachmentinfo[saveFileCount].Filesize = filesize;

                                    //当支持FTP上传附件且不保留本地附件模式时,则先上传到临时目录下
                                    if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + tempfilename);
                                    }
                                    else
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
                                    }
                                }
                            }
                            catch
                            {
                                //当上传目录和临时文件夹都没有上传的文件时
                                if (!(Utils.FileExists(UploadDir + tempfilename)) && (!(Utils.FileExists(UploadDir + newfilename))))
                                {

                                    attachmentinfo[saveFileCount].Filesize = filesize;

                                    //当支持FTP上传附件且不保留本地附件模式时,则先上传到临时目录下
                                    if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + tempfilename);
                                    }
                                    else
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
                                    }
                                }
                            }

                            try
                            {
                                //加载文件预览类指定方法
                                IPreview preview = PreviewProvider.GetInstance(fileextname.Trim());
                                if (preview != null)
                                {

                                    preview.UseFTP = (FTPs.GetMallAttachInfo.Allowupload == 1) ? true : false;

                                    //当支持FTP上传附件且不保留本地附件模式时
                                    if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                    {
                                        preview.OnSaved(UploadDir + tempfilename);
                                    }
                                    else
                                    {
                                        preview.OnSaved(UploadDir + newfilename);
                                    }
                                }
                            }
                            catch
                            { }

                            //当支持FTP上传附件时,使用FTP上传远程附件
                            if (FTPs.GetMallAttachInfo.Allowupload == 1)
                            {
                                FTPs ftps = new FTPs();

                                //当不保留本地附件模式时,在上传完成之后删除本地tempfilename文件
                                if (FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                {
                                    ftps.UpLoadFile(newfilename.Substring(0, newfilename.LastIndexOf("\\")), UploadDir + tempfilename, FTPs.FTPUploadEnum.ForumAttach);
                                }
                                else
                                {
                                    ftps.UpLoadFile(newfilename.Substring(0, newfilename.LastIndexOf("\\")), UploadDir + newfilename, FTPs.FTPUploadEnum.ForumAttach);
                                }
                            }

                        }
                        else
                        {
                            if (extnameid < 0)
                            {
                                attachmentinfo[saveFileCount].Sys_noupload = "文件格式无效";
                            }
                            else if (MaxSizePerDay - TodayUploadedSize < filesize)
                            {
                                attachmentinfo[saveFileCount].Sys_noupload = "文件大于今天允许上传的字节数";
                            }
                            else if (filesize > MaxSize[extnameid])
                            {
                                attachmentinfo[saveFileCount].Sys_noupload = "文件大于该类型附件允许的字节数";
                            }
                            else
                            {
                                attachmentinfo[saveFileCount].Sys_noupload = "文件大于单个文件允许上传的字节数";
                            }
                        }
                    }
                    else
                    {
                        attachmentinfo[saveFileCount].Sys_noupload = "文件格式无效";
                    }
                    //当支持FTP上传附件时
                    if (FTPs.GetMallAttachInfo.Allowupload == 1)
                    {
                        attachmentinfo[saveFileCount].Filename = FTPs.GetMallAttachInfo.Remoteurl + "/" + newfilename.Replace("\\", "/");
                    }
                    else
                    {
                        attachmentinfo[saveFileCount].Filename = "mall/" + newfilename;
                    }
                    attachmentinfo[saveFileCount].Description = fileextname;
                    attachmentinfo[saveFileCount].Filetype = filetype;
                    attachmentinfo[saveFileCount].Attachment = filename;
                    attachmentinfo[saveFileCount].Postdatetime = DateTime.Now.ToString();
                    attachmentinfo[saveFileCount].Sys_index = i;
                    saveFileCount++;
                }
            }
            return attachmentinfo;

        }

        /// <summary>
        /// 上传店标文件
        /// </summary>
        /// <param name="MaxFileSize">最大文件上传尺寸</param>
        /// <param name="AllowFileType">允许上传文件类型</param>
        /// <param name="config">配置对象信息</param>
        /// <param name="filekey">File控件的Key(即Name属性)</param>
        /// <returns>文件信息结构</returns>
        public static string SaveRequestFile(int MaxFileSize, string AllowFileType, GeneralConfigInfo config, string filekey)
        {
            
            string[] tmp = Utils.SplitString(AllowFileType, "\r\n");
     
            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                if (!HttpContext.Current.Request.Files[i].FileName.Equals("") && HttpContext.Current.Request.Files.AllKeys[i].Equals(filekey))
                {
                    string filename = Path.GetFileName(HttpContext.Current.Request.Files[i].FileName);
                    string fileextname = Utils.CutString(filename, filename.LastIndexOf(".") + 1).ToLower();
                    string filetype = HttpContext.Current.Request.Files[i].ContentType.ToLower();
                    int filesize = HttpContext.Current.Request.Files[i].ContentLength;
                    string newfilename = "";
                   
                    // 判断 文件扩展名/文件大小/文件类型 是否符合要求
                    if (!(Utils.IsImgFilename(filename) && !filetype.StartsWith("image")))
                    {
                        int extnameid = Utils.GetInArrayID(fileextname, tmp);

                        if (extnameid >= 0 && MaxFileSize >= filesize)
                        {
                            string UploadDir = Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/mall/");
                            StringBuilder savedir = new StringBuilder("");
                                savedir.Append(DateTime.Now.ToString("yyyy"));
                                savedir.Append(Path.DirectorySeparatorChar);
                                savedir.Append(DateTime.Now.ToString("MM"));
                                savedir.Append(Path.DirectorySeparatorChar);
                                savedir.Append(DateTime.Now.ToString("dd"));
                                savedir.Append(Path.DirectorySeparatorChar);

                            newfilename = (Environment.TickCount & int.MaxValue).ToString() + i.ToString() + random.Next(1000, 9999).ToString() + "." + fileextname;

                            //临时文件名称变量. 用于当启动远程附件之后,先上传到本地临时文件夹的路径信息
                            string tempfilename = "";
                            //当支持FTP上传附件且不保留本地附件时
                            if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                            {
                                // 如果指定目录不存在则建立临时路径
                                if (!Directory.Exists(UploadDir + "temp\\"))
                                {
                                    Utils.CreateDir(UploadDir + "temp\\");
                                }
                                tempfilename = "temp\\" + newfilename;
                            }
                            else
                            {
                                // 如果指定目录不存在则建立
                                if (!Directory.Exists(UploadDir + savedir.ToString()))
                                {
                                    Utils.CreateDir(UploadDir + savedir.ToString());
                                }
                            }
                            newfilename = savedir.ToString() + newfilename;

                            try
                            {
                                //当上传目录和临时文件夹都没有上传的文件时
                                if (!(Utils.FileExists(UploadDir + tempfilename)) && (!(Utils.FileExists(UploadDir + newfilename))))
                                {

                                    //当支持FTP上传附件且不保留本地附件模式时,则先上传到临时目录下
                                    if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + tempfilename);
                                    }
                                    else
                                    {
                                        HttpContext.Current.Request.Files[i].SaveAs(UploadDir + newfilename);
                                    }
                                }
                            }
                            catch
                            {
                               
                            }

                            //当支持FTP上传附件时,使用FTP上传远程附件
                            if (FTPs.GetMallAttachInfo.Allowupload == 1)
                            {
                                FTPs ftps = new FTPs();

                                //当不保留本地附件模式时,在上传完成之后删除本地tempfilename文件
                                if (FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                {
                                    ftps.UpLoadFile(newfilename.Substring(0, newfilename.LastIndexOf("\\")), UploadDir + tempfilename, FTPs.FTPUploadEnum.ForumAttach);
                                }
                                else
                                {
                                    ftps.UpLoadFile(newfilename.Substring(0, newfilename.LastIndexOf("\\")), UploadDir + newfilename, FTPs.FTPUploadEnum.ForumAttach);
                                }
                            }

                        }
                        else
                        {
                            if (extnameid < 0)
                            {
                                return "文件格式无效";
                            }
                            else
                            {
                                return "文件大于单个文件允许上传的字节数";
                            }
                        }
                    }
                    else
                    {
                        return "文件格式无效";
                    }
                    //当支持FTP上传附件时
                    if (FTPs.GetMallAttachInfo.Allowupload == 1)
                    {
                        return FTPs.GetMallAttachInfo.Remoteurl + "/" + newfilename.Replace("\\", "/");
                    }
                    else
                    {
                        return "mall/" + newfilename;
                    }
                }
            }
            return "";

        }
    }
}
