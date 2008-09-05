using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public delegate void PageChangeHandler(object send, int nPageIndex);

public partial class controls_PageNavigator : System.Web.UI.UserControl
{
    public event PageChangeHandler OnPageChange;
    private int _PageCount = 0;
    private int _RecordCount = 0;
    private int _PageIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int PageCount
    {
        get 
        {
            return int.Parse(this.LblPageCount.Text);
        }
        set 
        {
            _PageCount = value;
            /*
            if (_PageCount < 2)
            {
                //this.txtNewPageIndex.Enabled = false;
                //this.LnkBtnGoto.Enabled = false;
            }
            else
            {
                //this.txtNewPageIndex.Enabled = true;
                //this.LnkBtnGoto.Enabled = true;
            }
             */
            this.LblPageCount.Text = _PageCount.ToString();
        }
    }
    public int RecordCount
    {
        get
        {
            return int.Parse(this.LblRecordCount.Text);
        }
        set
        {
            _RecordCount = value;
            this.LblRecordCount.Text = _RecordCount.ToString();
        }
    }
    public int PageIndex
    {
        get
        {
            return int.Parse(this.LblPageIndex.Text);
        }
        set
        {
            _PageIndex = value;
            this.txtNewPageIndex.Text = this.LblPageIndex.Text = _PageIndex.ToString();
            if (_PageIndex < 2)
            {
                this.LnkBtnFirst.Enabled = false;
                this.LnkBtnPrevious.Enabled = false;
            }
            else
            {
                this.LnkBtnFirst.Enabled = true;
                this.LnkBtnPrevious.Enabled = true;
            }
            if (_PageIndex >= _PageCount)
            {
                this.LnkBtnNext.Enabled = false;
                this.LnkBtnLast.Enabled = false;
            }
            else
            {
                this.LnkBtnNext.Enabled = true;
                this.LnkBtnLast.Enabled = true;
            }
        }
    }

    protected void LnkBtnFirst_Click(object sender, EventArgs e)
    {
        try
        {
            OnPageChange(sender, 1);
        }
        catch { }
    }
    protected void LnkBtnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            int n = Convert.ToInt32(this.LblPageIndex.Text);
            n--;
            OnPageChange(sender, n);
        }
        catch
        {
            throw;
        }
    }
    protected void LnkBtnNext_Click(object sender, EventArgs e)
    {
        try
        {
            int n = Convert.ToInt32(this.LblPageIndex.Text);
            n++;
            OnPageChange(sender, n);
        }
        catch 
        {
            throw;
        }
    }
    protected void LnkBtnLast_Click(object sender, EventArgs e)
    {
        try
        {
            int n = Convert.ToInt32(this.LblPageCount.Text);
            OnPageChange(sender, n);
        }
        catch
        {
            throw;
        }
    }
    protected void LnkBtnGoto_Click(object sender, EventArgs e)
    {
        int n;
        try
        {
            n = Convert.ToInt32(this.txtNewPageIndex.Text);
        }
        catch 
        {
            n = Convert.ToInt32(this.LblPageIndex.Text);
        }
        int np = Convert.ToInt32(this.LblPageCount.Text.Trim());
        if (n > np)
            n = np;
        if (n < 1)
            n = 1;
        OnPageChange(sender, n);
    }
}
