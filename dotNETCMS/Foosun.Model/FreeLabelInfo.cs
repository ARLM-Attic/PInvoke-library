using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    public class FreeLabelInfo
    {
        private int _Id;
        private string _LabelName;
        private string _LabelSQL;
        private string _StyleContent;
        private string _Description;
        private string _SiteID;
        public FreeLabelInfo(int id, string lblname, string lblsql, string stycontent, string description)
        {
            _Id = id;
            _LabelName = lblname;
            _LabelSQL = lblsql;
            _StyleContent = stycontent;
            _Description = description;
        }
        public int Id
        {
            get { return _Id; }
        }
        public string LabelName
        {
            get { return _LabelName; }
        }
        public string LabelSQL
        {
            get { return _LabelSQL; }
        }
        public string StyleContent
        {
            get { return _StyleContent; }
        }
        public string Description
        {
            get { return _Description; }
        }
        public string SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
    }
    public class FreeLablelDBInfo
    {
        private string _name;
        private string _description;
        private string _dbtype;
        public FreeLablelDBInfo(string SName, string SDesc,string STypeName)
        {
            _name = SName;
            _description = SDesc;
            _dbtype = STypeName;
        }
        public string Name
        {
            get { return _name; }
        }
        public string Description
        {
            get { return _description; }
        }
        public string TypeName
        {
            get { return _dbtype; }
        }
    }
}
