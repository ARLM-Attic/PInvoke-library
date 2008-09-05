using System;
using System.Collections.Generic;
using System.Text;
using Foosun.Model;

namespace Foosun.Web.UI
{
    public class DialogPage : BasePage
    {
        /// <summary>
        /// 对话窗口操作权限
        /// </summary>
        protected enum EnumDialogAuthority
        {
            /// <summary>
            /// 完全公开
            /// </summary>
            Publicity,
            /// <summary>
            /// 只对管理员可用
            /// </summary>
            ForAdmin,
            /// <summary>
            /// 只对个人用户可用
            /// </summary>
            ForPerson
        }
        protected EnumDialogAuthority _BrowserAuthor = EnumDialogAuthority.ForAdmin;

        public DialogPage()
        {
            this.Load += new EventHandler(DialogPage_Load);
        }
        /// <summary>
        /// LOAD事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DialogPage_Load(object sender, EventArgs e)
        {
            if (_BrowserAuthor == EnumDialogAuthority.ForPerson)
            {
                CheckUserLogin();
            }
            else if (_BrowserAuthor == EnumDialogAuthority.ForAdmin)
            {
                CheckAdminLogin();
            }
            else if (_BrowserAuthor == (EnumDialogAuthority.ForPerson | EnumDialogAuthority.ForAdmin))
            {
                EnumLoginState state;
                if (!Validate_Session())
                    LoginResultShow(EnumLoginState.Err_TimeOut);
                else
                {
                    string UserNum = Global.Current.UserNum;
                    state = _UserLogin.CheckAdminLogin(UserNum);
                    if (state != EnumLoginState.Succeed)
                    {
                        state = _UserLogin.CheckUserLogin(UserNum, false);
                        if (state != EnumLoginState.Succeed)
                            LoginResultShow(state);
                    }
                    else
                        LoginResultShow(state);
                }
            }
            else
            { }
        }
        /// <summary>
        /// 设置权限，要在子类的构造函数里设置,默认值为管理员可用
        /// </summary>
        protected EnumDialogAuthority BrowserAuthor
        {
            set { _BrowserAuthor = value; }
        }
    }
}
