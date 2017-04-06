using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Web.UI;

namespace Web.admin
{
    public partial class index : ManagePage
    {
        public Model.manager admin_info; //管理员信息

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    admin_info = GetAdminInfo();//登录用户信息
                }
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("index.aspx", "Page_Load", ex.Message);
            }
        }

        //安全退出
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session["userinfo"] = null;
            Response.Redirect("login.aspx");
        }
    }
}