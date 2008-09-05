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
    /// �̳ǹ�����
    /// </summary>
    public class MallUtils
    {
        /// <summary>
        /// �������з�ö��
        /// </summary>
        public enum OperaCode
        {
            Equal = 1, //����
            NoEuqal = 2, //������
            Morethan = 3, //����
            MorethanOrEqual = 4, //���ڻ����
            Lessthan = 5,  //С��
            LessthanOrEqual = 6 //С�ڻ����
        }


        /// <summary>
        /// �����ϴ����ļ�
        /// </summary>
        /// <param name="categoryid">��Ʒ����id</param>
        /// <param name="MaxAllowFileCount">���������ϴ��ļ�����</param>
        /// <param name="MaxSizePerDay">ÿ������ĸ�����С����</param>
        /// <param name="MaxFileSize">�������������ļ��ֽ���</param>/// 
        /// <param name="TodayUploadedSize">�����Ѿ��ϴ��ĸ����ֽ�����</param>
        /// <param name="AllowFileType">������ļ�����, ��string[]��ʽ�ṩ</param>
        /// <param name="config">�������淽ʽ 0=����/��/�մ��벻ͬĿ¼ 1=����/��/��/��̳���벻ͬĿ¼ 2=����̳���벻ͬĿ¼ 3=���ļ����ʹ��벻ͬĿ¼</param>
        /// <param name="watermarkstatus">ͼƬˮӡλ��</param>
        /// <param name="filekey">File�ؼ���Key(��Name����)</param>
        /// <returns>�ļ���Ϣ�ṹ</returns>
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

                    // �ж� �ļ���չ��/�ļ���С/�ļ����� �Ƿ����Ҫ��
                    if (!(Utils.IsImgFilename(filename) && !filetype.StartsWith("image")))
                    {
                        int extnameid = Utils.GetInArrayID(fileextname, AllowFileExtName);

                        if (extnameid >= 0 && (filesize <= MaxSize[extnameid]) && (MaxFileSize >= filesize /*|| MaxAllSize == 0*/) && (MaxSizePerDay - TodayUploadedSize >= filesize))
                        {
                            TodayUploadedSize = TodayUploadedSize + filesize;
                            string UploadDir = Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/mall/");
                            StringBuilder savedir = new StringBuilder("");
                            //�������淽ʽ 0=����/��/�մ��벻ͬĿ¼ 1=����/��/��/��̳���벻ͬĿ¼ 2=����̳���벻ͬĿ¼ 3=���ļ����ʹ��벻ͬĿ¼
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

                            //��ʱ�ļ����Ʊ���. ���ڵ�����Զ�̸���֮��,���ϴ���������ʱ�ļ��е�·����Ϣ
                            string tempfilename = "";
                            //��֧��FTP�ϴ������Ҳ��������ظ���ʱ
                            if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                            {
                                // ���ָ��Ŀ¼������������ʱ·��
                                if (!Directory.Exists(UploadDir + "temp\\"))
                                {
                                    Utils.CreateDir(UploadDir + "temp\\");
                                }
                                tempfilename = "temp\\" + newfilename;
                            }
                            else
                            {
                                // ���ָ��Ŀ¼����������
                                if (!Directory.Exists(UploadDir + savedir.ToString()))
                                {
                                    Utils.CreateDir(UploadDir + savedir.ToString());
                                }
                            }
                            newfilename = savedir.ToString() + newfilename;

                            try
                            {
                                // �����bmp jpg pngͼƬ����
                                if ((fileextname == "bmp" || fileextname == "jpg" || fileextname == "jpeg" || fileextname == "png") && filetype.StartsWith("image"))
                                {

                                    Image img = Image.FromStream(HttpContext.Current.Request.Files[i].InputStream);
                                    if (config.Attachimgmaxwidth > 0 && img.Width > config.Attachimgmaxwidth)
                                    {
                                        attachmentinfo[saveFileCount].Sys_noupload = "ͼƬ���Ϊ" + img.Width.ToString() + ", ϵͳ����������Ϊ" + config.Attachimgmaxwidth.ToString();

                                    }
                                    if (config.Attachimgmaxheight > 0 && img.Height > config.Attachimgmaxheight)
                                    {
                                        attachmentinfo[saveFileCount].Sys_noupload = "ͼƬ�߶�Ϊ" + img.Width.ToString() + ", ϵͳ��������߶�Ϊ" + config.Attachimgmaxheight.ToString();
                                    }
                                    if (attachmentinfo[saveFileCount].Sys_noupload == "")
                                    {
                                        if (watermarkstatus == 0)
                                        {
                                            //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
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
                                                //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
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

                                                //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
                                                if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                                {
                                                    ForumUtils.AddImageSignText(img, UploadDir + tempfilename, watermarkText, config.Watermarkstatus, config.Attachimgquality, config.Watermarkfontname, config.Watermarkfontsize);
                                                }
                                                else
                                                {
                                                    ForumUtils.AddImageSignText(img, UploadDir + newfilename, watermarkText, config.Watermarkstatus, config.Attachimgquality, config.Watermarkfontname, config.Watermarkfontsize);
                                                }
                                            }


                                            //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,���ȡ��ʱĿ¼�µ��ļ���Ϣ
                                            if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                                            {
                                                // ��ü�ˮӡ����ļ�����
                                                attachmentinfo[saveFileCount].Filesize = new FileInfo(UploadDir + tempfilename).Length;
                                            }
                                            else
                                            {
                                                // ��ü�ˮӡ����ļ�����
                                                attachmentinfo[saveFileCount].Filesize = new FileInfo(UploadDir + newfilename).Length;
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    attachmentinfo[saveFileCount].Filesize = filesize;

                                    //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
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
                                //���ϴ�Ŀ¼����ʱ�ļ��ж�û���ϴ����ļ�ʱ
                                if (!(Utils.FileExists(UploadDir + tempfilename)) && (!(Utils.FileExists(UploadDir + newfilename))))
                                {

                                    attachmentinfo[saveFileCount].Filesize = filesize;

                                    //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
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
                                //�����ļ�Ԥ����ָ������
                                IPreview preview = PreviewProvider.GetInstance(fileextname.Trim());
                                if (preview != null)
                                {

                                    preview.UseFTP = (FTPs.GetMallAttachInfo.Allowupload == 1) ? true : false;

                                    //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ
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

                            //��֧��FTP�ϴ�����ʱ,ʹ��FTP�ϴ�Զ�̸���
                            if (FTPs.GetMallAttachInfo.Allowupload == 1)
                            {
                                FTPs ftps = new FTPs();

                                //�����������ظ���ģʽʱ,���ϴ����֮��ɾ������tempfilename�ļ�
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
                                attachmentinfo[saveFileCount].Sys_noupload = "�ļ���ʽ��Ч";
                            }
                            else if (MaxSizePerDay - TodayUploadedSize < filesize)
                            {
                                attachmentinfo[saveFileCount].Sys_noupload = "�ļ����ڽ��������ϴ����ֽ���";
                            }
                            else if (filesize > MaxSize[extnameid])
                            {
                                attachmentinfo[saveFileCount].Sys_noupload = "�ļ����ڸ����͸���������ֽ���";
                            }
                            else
                            {
                                attachmentinfo[saveFileCount].Sys_noupload = "�ļ����ڵ����ļ������ϴ����ֽ���";
                            }
                        }
                    }
                    else
                    {
                        attachmentinfo[saveFileCount].Sys_noupload = "�ļ���ʽ��Ч";
                    }
                    //��֧��FTP�ϴ�����ʱ
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
        /// �ϴ�����ļ�
        /// </summary>
        /// <param name="MaxFileSize">����ļ��ϴ��ߴ�</param>
        /// <param name="AllowFileType">�����ϴ��ļ�����</param>
        /// <param name="config">���ö�����Ϣ</param>
        /// <param name="filekey">File�ؼ���Key(��Name����)</param>
        /// <returns>�ļ���Ϣ�ṹ</returns>
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
                   
                    // �ж� �ļ���չ��/�ļ���С/�ļ����� �Ƿ����Ҫ��
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

                            //��ʱ�ļ����Ʊ���. ���ڵ�����Զ�̸���֮��,���ϴ���������ʱ�ļ��е�·����Ϣ
                            string tempfilename = "";
                            //��֧��FTP�ϴ������Ҳ��������ظ���ʱ
                            if (FTPs.GetMallAttachInfo.Allowupload == 1 && FTPs.GetMallAttachInfo.Reservelocalattach == 0)
                            {
                                // ���ָ��Ŀ¼������������ʱ·��
                                if (!Directory.Exists(UploadDir + "temp\\"))
                                {
                                    Utils.CreateDir(UploadDir + "temp\\");
                                }
                                tempfilename = "temp\\" + newfilename;
                            }
                            else
                            {
                                // ���ָ��Ŀ¼����������
                                if (!Directory.Exists(UploadDir + savedir.ToString()))
                                {
                                    Utils.CreateDir(UploadDir + savedir.ToString());
                                }
                            }
                            newfilename = savedir.ToString() + newfilename;

                            try
                            {
                                //���ϴ�Ŀ¼����ʱ�ļ��ж�û���ϴ����ļ�ʱ
                                if (!(Utils.FileExists(UploadDir + tempfilename)) && (!(Utils.FileExists(UploadDir + newfilename))))
                                {

                                    //��֧��FTP�ϴ������Ҳ��������ظ���ģʽʱ,�����ϴ�����ʱĿ¼��
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

                            //��֧��FTP�ϴ�����ʱ,ʹ��FTP�ϴ�Զ�̸���
                            if (FTPs.GetMallAttachInfo.Allowupload == 1)
                            {
                                FTPs ftps = new FTPs();

                                //�����������ظ���ģʽʱ,���ϴ����֮��ɾ������tempfilename�ļ�
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
                                return "�ļ���ʽ��Ч";
                            }
                            else
                            {
                                return "�ļ����ڵ����ļ������ϴ����ֽ���";
                            }
                        }
                    }
                    else
                    {
                        return "�ļ���ʽ��Ч";
                    }
                    //��֧��FTP�ϴ�����ʱ
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
