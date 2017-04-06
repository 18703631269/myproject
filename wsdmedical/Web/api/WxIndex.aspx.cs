using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.api
{
    public partial class WxIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["login_type"] = "wx";
            string newguid = Guid.NewGuid().ToString();
            string send_url = "https://open.weixin.qq.com/connect/qrconnect?appid=wx7f871ded4afe04d3&redirect_uri=http://www.litschimed.com&response_type=code&scope=snsapi_login&state=" + newguid + "#wechat_redirect";
            //开始发送
            Response.Redirect(send_url);
        }
    }
}