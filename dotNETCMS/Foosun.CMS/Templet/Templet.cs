using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Foosun.CMS.Templet
{
    public class Templet
    {
        /// <summary>
        /// 修改文件名(文件夹)名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="oldname">原始名称</param>
        /// <param name="newname">新名称</param>
        /// <param name="type">0为文件夹,1为文件</param>
        /// <returns>成功返回1</returns>

        public int EidtName(string path, string oldname, string newname, int type)
        {
            int result = 0;
            if (type == 0)
            {
                if (Directory.Exists(path + "\\" + oldname))
                {
                    try
                    {
                        Directory.Move(path + "\\" + oldname, path + "\\" + newname.Replace(".", "")); 
                    }
                    catch(IOException e)
                    {
                        throw new Exception(e.ToString()); 
                    }
                    result = 1;
                }
                else
                { 
                    throw new Exception("参数传递错误!");
                }
            }
            else
            {
                if (File.Exists(path + "\\" + oldname))
                {
                    try
                    {
                        File.Move(path + "\\" + oldname, path + "\\" + newname);
                    }
                    catch(Exception e)
                    {
                        throw new Exception(e.ToString());
                    }
                    result = 1;
                }
                else
                {
                    throw new Exception("参数传递错误!");
                }
            }
            return result;
        }
        
        
        
        /// <summary>
        /// 删除文件或文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="filename">名称</param>
        /// <param name="type">0代表文件夹,1代表文件</param>
        /// <returns>返回值</returns>
       
        
        public int Del(string path,string filename, int type)
        {
            int result = 0;
            if (type == 0)
            {
                if (Directory.Exists(path + "\\" + filename))                 
                {
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch (Exception e)
                    {
                        throw new IOException(e.ToString());
                    }
                    result = 1;
                }
                else
                {
                    throw new IOException("参数错误!");
                }
            }
            else
            {
                if (File.Exists(path + "\\" + filename))
                {
                    try
                    {
                        File.Delete(path + "\\" + filename);
                    }
                    catch (Exception e)
                    {
                        throw new IOException(e.ToString());
                    }
                    result = 1;
                }
                else
                {
                    throw new IOException("参数错误!");
                }
            }
            return result;
        }

        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="path">当前路径</param>
        /// <param name="filename">文件夹名称</param>
        /// <returns></returns>
        
        public int AddDir(string path, string filename)
        {
            int result = 0;
            if (Directory.Exists(path + "\\" + filename))
            {
                throw new IOException("此文件夹已存在!");                
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path + "\\" + filename.Replace(".", ""));
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// 获取当前目录的父目录
        /// </summary>
        /// <param name="path">当前目录</param>
        /// <param name="temppath">当前的模板目录</param>
        /// <returns></returns>
        
        public string PathPre(string path,string temppath)
        {
            if (path != null)
            {
                int i, j;
                i = path.LastIndexOf(temppath);
                j = path.Length - i;
                path = path.Substring(i, j);
            }
            else
            {
                path = temppath;
            }
            return path;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fileContent">文件内容</param>
        /// <returns></returns>

        public int saveFile(string path, string fileContent)
        {
            int result = 0;
            if (File.Exists(path))
            {
                try
                {
                    StreamWriter Fso = new StreamWriter(path);      
                    Fso.WriteLine(fileContent);
                    Fso.Close();                                        
                    Fso.Dispose();                                               
                }
                catch (IOException e)                                    
                {
                    throw new IOException(e.ToString());
                }
                result = 1;
            }
            else
            {
                throw new Exception("文件已经被删除!");
            }
            return result;
        }

        /// <summary>
        /// 显示文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>

        public string showFileContet(string path)
        {
            string str_content = "";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader Fso = new StreamReader(path);
                    str_content = Fso.ReadToEnd();
                    Fso.Close();
                    Fso.Dispose();
                }
                catch (IOException e)
                {
                    throw new IOException(e.ToString());
                }
            }
            else
            {
                throw new Exception("找不到相应的文件!");
            }
            return str_content;
        }
    }    
}
