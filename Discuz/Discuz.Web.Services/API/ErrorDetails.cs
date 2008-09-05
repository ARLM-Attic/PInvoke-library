using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;
using System.Collections;

namespace Discuz.Web.Services.API
{
    public class ErrorDetails : Hashtable
    {
        public ErrorDetails()
        {
            this.Add(1, "未知错误,请重新提交");
            this.Add(2, "服务目前不可使用");
            this.Add(3, "未知方法");
            this.Add(4, "整合程序已达到允许的最大同时请求数");
            this.Add(5, "请求来自一个未被当前整合程序允许的远程地址");
            this.Add(100, "指定的参数不存在或不是有效参数");
            this.Add(101, "所提交的api_key未关联到任何设定程序");
            this.Add(102, "session_key已过期或失效,请重定向让用户重新登录并获得新的session_key");
            this.Add(103, "当前会话所提交的call_id没有大于前一次的call_id");
            this.Add(104, "签名(sig)参数不正确");
        }
    }
}
