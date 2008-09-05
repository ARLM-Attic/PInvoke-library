<%@ page language="c#" runat="server" %>
<script language="c#" runat="server">
    //-----------变量定义-----------------------------------------------------------
    public string code;//统计前台显示方式
    public string str_fs_url;//当前地址
    public string statid;//统计ID
    //------------------------------------------------------------------------------
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="code">前台统计样式</param>
    /// <param name="str_fs_url">当前地址</param>
    /// <param name="statid">统计分类ID</param>
    /// code by chenzhaohui
    
    public void Page_Load(Object src,EventArgs e)
    {
    //----------取得服务器变量集合---------------------------------------------------
    System.Collections.Specialized.NameValueCollection ServerVariables = System.Web.HttpContext.Current.Request.ServerVariables; 
    //----------参数传递ID，判断统计时采取的方式-------------------------------------
    code = Request.QueryString["code"];
    statid = Request.QueryString["id"];
    //----------取得URL地址../stat/mystat.aspx---------------------------------------
    str_fs_url = ServerVariables["URL"].ToString();
    str_fs_url = str_fs_url.Substring(0, str_fs_url.IndexOf("mystat.aspx", 0, str_fs_url.Length));
    str_fs_url = "http://" + ServerVariables["HTTP_HOST"].ToString() + str_fs_url;
    //-------------------------------------------------------------------------------
	
    }
</script>
<!-------------------打出各个变量（code,strtheurl）--------------------------------->
document.write("<script>var code='<%=code%>';var url='<%=str_fs_url%>'; var statid='<%=statid %>'</script>")
<!------将得到的参数（width,come）传递给实现统计的页面stat.aspx,stat.aspx.cs-------->
document.write("<script type=\"text/javascript\" language=\"javascript\" charset=\"utf-8\" src=\""+url+"stat.aspx?code="+code+"&statid="+ statid +"&come="+escape(document.referrer)+"&width="+(screen.width)+"\"></script>")
<!---------------------------------------------------------------------------------->