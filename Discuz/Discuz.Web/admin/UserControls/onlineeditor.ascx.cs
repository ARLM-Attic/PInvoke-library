using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using Discuz.Control;

namespace Discuz.Web.Admin
{

    /// <summary>
    ///	在线编辑控件
    /// </summary>
#if NET1
    public class OnlineEditor : UserControl
#else
    public partial class OnlineEditor : UserControl
#endif
    {

#if NET1
        #region 控件声明
        protected System.Web.UI.HtmlControls.HtmlTextArea DataTextarea;
        #endregion
#endif
        public string controlname;
        public int postminchars = 0;
        public int postmaxchars = 200;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { }
        }


        public string Text
        {
            set { DataTextarea.InnerText = value; }
            get { return DataTextarea.InnerText.Replace("'", "''"); }
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}