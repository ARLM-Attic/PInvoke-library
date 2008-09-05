using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{

    public class SysUserFields
    {
        public SysUserFields(string userNum)
        {

            this._usernum = userNum;
        }
        public SysUserFields(SysUserInfo _userInfo)
        {

            this._usernum = _userInfo.UserNum ;
        }
        private int _id;
        private string _usernum = string.Empty;
        private string _province = string.Empty;
        private string _city = string.Empty;
        private string _address = string.Empty;
        private string _postcode = string.Empty;
        private string _fatel = string.Empty;
        private string _worktel = string.Empty;
        private string _qq = string.Empty;
        private string _msn = string.Empty;
        private string _fax = string.Empty;
        private string _character = string.Empty;
        private string _userfan = string.Empty;
        private string _nation = string.Empty;
        private string _nativeplace = string.Empty;
        private string _job = string.Empty;
        private string _education = string.Empty;
        private string _lastschool = string.Empty;
        private string _orgsch = string.Empty;
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string userNum
        {
            set { _usernum = value; }
            get { return _usernum; }
        }
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        public string Postcode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        public string FaTel
        {
            set { _fatel = value; }
            get { return _fatel; }
        }
        public string WorkTel
        {
            set { _worktel = value; }
            get { return _worktel; }
        }
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        public string character
        {
            set { _character = value; }
            get { return _character; }
        }
        public string UserFan
        {
            set { _userfan = value; }
            get { return _userfan; }
        }
        public string Nation
        {
            set { _nation = value; }
            get { return _nation; }
        }
        public string nativeplace
        {
            set { _nativeplace = value; }
            get { return _nativeplace; }
        }
        public string Job
        {
            set { _job = value; }
            get { return _job; }
        }
        public string education
        {
            set { _education = value; }
            get { return _education; }
        }
        public string Lastschool
        {
            set { _lastschool = value; }
            get { return _lastschool; }
        }
        public string orgSch
        {
            set { _orgsch = value; }
            get { return _orgsch; }
        }
    }

}
