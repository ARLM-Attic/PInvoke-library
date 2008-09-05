using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    public class GlobalUserInfo
    {
        private string _usernum;
        private string _username;
        private string _siteid;
        private string _adminLogined;
        private bool _UnCert;
        public GlobalUserInfo(string usernum, string username, string siteid, string adminLogined)
        {
            _usernum = usernum;
            _username = username;
            _siteid = siteid;
            _adminLogined = adminLogined;
        }
        public string UserNum
        {
            get
            {
                return _usernum;
            }
            set
            {
                _usernum = value;
            }
        }

        public string adminLogined
        {
            get
            {
                return _adminLogined;
            }
            set
            {
                _adminLogined = value;
            }
        }

        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }
        public string SiteID
        {
            get
            {
                return _siteid;
            }
            set
            {
                _siteid = value;
            }
        }
        public bool uncert
        {
            get
            {
                return _UnCert;
            }
            set
            {
                _UnCert = value;
            }
        }
    }
}
