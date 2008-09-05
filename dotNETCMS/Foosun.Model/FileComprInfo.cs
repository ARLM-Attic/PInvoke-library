using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    public class FileComprInfo
    {
        private string _FileName;
        private long _StFileSize;
        private DateTime _StModifyTime;
        private long _FaFileSize;
        private DateTime _FaModifyTime;
        public FileComprInfo(string filename)
        {
            _FileName = filename;
        }
        /// <summary>
        /// 取得实际文件名
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
        }
        /// <summary>
        /// 标准文件大小
        /// </summary>
        public long StFileSize
        {
            get { return _StFileSize; }
            set { _StFileSize = value; }
        }
        /// <summary>
        /// 标准文件修改时间
        /// </summary>
        public DateTime StModifyTime
        {
            get { return _StModifyTime; }
            set { _StModifyTime = value; }
        }
        /// <summary>
        /// 实际文件大小
        /// </summary>
        public long FaFileSize
        {
            get { return _FaFileSize; }
            set { _FaFileSize = value; }
        }
        /// <summary>
        /// 实际的修改时间
        /// </summary>
        public DateTime FaModifyTime
        {
            get { return _FaModifyTime; }
            set { _FaModifyTime = value; }
        }
    }
    public class FileNameComparer : IComparer<FileComprInfo>
    {
        public int Compare(FileComprInfo x, FileComprInfo y)
        {
            return x.FileName.CompareTo(y.FileName);
        }
    }
    public class FileSizeComparer : IComparer<FileComprInfo>
    {
        public int Compare(FileComprInfo x, FileComprInfo y)
        {
            return x.FaFileSize.CompareTo(y.FaFileSize);
        }
    }
    public class FileTimeComparer : IComparer<FileComprInfo>
    {
        public int Compare(FileComprInfo x, FileComprInfo y)
        {
            return x.FaModifyTime.CompareTo(y.FaModifyTime);
        }
    }
}
