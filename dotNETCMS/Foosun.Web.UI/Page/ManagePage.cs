using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using Foosun.Config;
using Foosun.CMS;
using Foosun.Model;

namespace Foosun.Web.UI
{
    public class ManagePage : BasePage
    {
       /// <summary>
       ///私有变量-用户的编号
       /// </summary>
        private string _UserNum;
        /// <summary>
        ///私有变量-用户的名称
        /// </summary>
        private string _UserName;
        /// <summary>
        /// 管理员是否登陆
        /// </summary>
        private string _adminLogined;
        /// <summary>
        /// 私有变量-权限代码,在子类中必须初始化的
        /// </summary>
        private string _Authority_Code = string.Empty;
        /// <summary>
        /// 私有变量-当前的频道编号
        /// </summary>
        private string _SiteID = string.Empty;
        /// <summary>
        /// 私有变量-当前的栏目编号
        /// </summary>
        private string _ClassID = string.Empty;
        /// <summary>
        /// 私有变量-当前的专题编号
        /// </summary>
        private string _SpecailID = string.Empty;
        /// <summary>
        /// 检查权限
        /// </summary>
        protected void CheckAdminAuthority()
        {
            EnumLoginState state = EnumLoginState.Err_AdminTimeOut;
            if (Validate_Session())
            {
                state = _UserLogin.CheckAdminAuthority(_Authority_Code, _ClassID, _SpecailID, _SiteID, _adminLogined);
            }
            if (state != EnumLoginState.Succeed)
            {
                LoginResultShow(state);
            }
            else
            {
                _UserNum = Foosun.Global.Current.UserNum;
                _SiteID = Foosun.Global.Current.SiteID.Trim();
                _UserName = Foosun.Global.Current.UserName;
                _adminLogined = Foosun.Global.Current.adminLogined;
            }
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns>EnumLoginState</returns>
        protected bool CheckAuthority()
        {
            EnumLoginState state = EnumLoginState.Err_AdminTimeOut;
            if (Validate_Session())
            {
                state = _UserLogin.CheckAdminAuthority(_Authority_Code, _ClassID, _SpecailID, _SiteID, _adminLogined);
            }
            if (state == EnumLoginState.Succeed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// LOAD事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagePage_Load(object sender, EventArgs e)
        {
            CheckAdminAuthority();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="popCode">权限代码</param>
        /// <param name="ClassID">栏目编号</param>
        /// <param name="specialID">专题编号</param>
        public ManagePage(string popCode,string ClassID,string specialID)
        {
            _Authority_Code = popCode;
            _ClassID = ClassID;
            _SpecailID = specialID;
            _SiteID = "";
            this.Load += new EventHandler(ManagePage_Load);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="popCode">权限代码</param>
        public ManagePage(string popCode)
        {
            _adminLogined = popCode;
            _ClassID = "";
            _SpecailID = "";
            _SiteID = "";
            this.Load += new EventHandler(ManagePage_Load);
        }
        /// <summary>
        /// 退出处理
        /// </summary>
        protected override void Logout()
        {
            base.Logout();
            string _dirDumm = Foosun.Config.UIConfig.dirDumm;
            string TmpPath = Foosun.Config.UIConfig.dirUser + "/";
            if ((_dirDumm).Trim() != "") { _dirDumm = "/" + _dirDumm; }
            TmpPath = Foosun.Config.UIConfig.dirMana + "/";
            ExecuteJs("top.location.href=\"" + _dirDumm + "/" + TmpPath + "login.aspx\";");
            Context.Response.End();
        }
        /// <summary>
        /// 获取用户编号
        /// </summary>
        protected string UserNum
        {
            get { return _UserNum; }
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        protected string UserName
        {
            get { return _UserName; }
        }
        /// <summary>
        /// 获取当前的频道编号
        /// </summary>
        protected string SiteID
        {
            get { return _SiteID; }
        }
        /// <summary>
        /// 获取或设置栏目编号
        /// </summary>
        protected string ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }
        /// <summary>
        /// 获得或设置专题编号
        /// </summary>
        protected string SpecailID
        {
            get { return _SpecailID; }
            set { _SpecailID = value; }
        }
        /// <summary>
        /// 设置权限编号，要在子类的构造函数里设置
        /// </summary>
        protected string Authority_Code
        {
            set { _Authority_Code = value; }
        }
    }
}
