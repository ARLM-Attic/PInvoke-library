using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Discuz.Web.Admin
{

    /// <summary>
    ///	TextareaResize 控件. 
    /// </summary>
#if NET1
    public class TextareaResize : UserControl, Discuz.Control.IWebControl
#else
    public partial class TextareaResize : UserControl, Discuz.Control.IWebControl
#endif
    {

#if NET1
        #region 控件声明
        protected System.Web.UI.HtmlControls.HtmlTextArea posttextarea;
        #endregion
#endif

        public string controlname;
        public string imagepath = "";
        public int rows = 5;
        public int cols = 45;
        public bool is_replace = false;
        public string maxlength = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            posttextarea.Rows = rows;
            posttextarea.Cols = cols;

            if (maxlength != null)
            {
                posttextarea.Attributes.Add("onkeyup", "return isMaxLen(this)");
                posttextarea.Attributes.Add("maxlength", maxlength);
            }

        }

        public string Text
        {
            set { posttextarea.InnerText = value; }
            get
            {
                if (is_replace)
                {
                    return posttextarea.InnerText.Replace("'", "''");
                }
                else
                {
                    return posttextarea.InnerText;
                }
            }
        }


        private string _hintTitle = "";
        public string HintTitle
        {
            get { return _hintTitle; }
            set { _hintTitle = value; }
        }


        private string _hintInfo = "";
        public string HintInfo
        {
            get { return _hintInfo; }
            set { _hintInfo = value; }
        }

        private int _hintLeftOffSet = 0;
        public int HintLeftOffSet
        {
            get { return _hintLeftOffSet; }
            set { _hintLeftOffSet = value; }
        }

        private int _hintTopOffSet = 0;
        public int HintTopOffSet
        {
            get { return _hintTopOffSet; }
            set { _hintTopOffSet = value; }
        }

        private string _hintShowType = "up";//或"down"
        public string HintShowType
        {
            get { return _hintShowType; }
            set { _hintShowType = value; }
        }

        private int _hintHeight = 30;
        public int HintHeight
        {
            get { return _hintHeight; }
            set { _hintHeight = value; }
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