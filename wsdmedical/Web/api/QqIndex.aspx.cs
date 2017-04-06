using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.api
{
    public partial class QqIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["login_type"] = "qq";
            string state = Guid.NewGuid().ToString();
            string app_id = "101328302";
            string return_url = "http://www.litschimed.com";
            string send_url = "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=" + app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(return_url) + "&scope=get_user_info,get_info";
            //开始发送
            Response.Redirect(send_url);
        }
    }
}