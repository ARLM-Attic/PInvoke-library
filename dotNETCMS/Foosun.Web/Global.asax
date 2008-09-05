<%@ Application Language="C#" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码
       
    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception x = Server.GetLastError().GetBaseException();
        string errmsg = x.ToString();
        Regex re = new Regex(@"文件(.*)不存在");
        if (re.Match(errmsg).Success)
        {
            Foosun.Web.UI.WebHint.ShowError("您所浏览的页面不存在", "/", true);
        }
        else
        {
            Foosun.Web.UI.WebHint.ShowError(errmsg, "", false);
        }
        //Foosun.Web.UI.WebHint.ShowError(x.Message, "", false);
    }

    void Session_Start(object sender, EventArgs e) 
    {
         
    }

    void Session_End(object sender, EventArgs e) 
    {
             
    }
       
</script>
