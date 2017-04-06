using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using Common;
using BLL;

namespace Web.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text.Trim();
                string userPwd = txtPassword.Text.Trim();

                if (userName.Equals("") || userPwd.Equals(""))
                {
                    msgtip.InnerHtml = "请输入用户名或密码.";
                    return;
                }
                managerBll bll = new managerBll();
                Model.manager model = bll.GetModel(userName, userPwd, true);
                if (model == null)
                {
                    msgtip.InnerHtml = "用户名或密码有误，请重试！";
                    return;
                }
                Session["userinfo"] = model;
                Session.Timeout = 45;
                manager_logBll logBll = new manager_logBll();
                logBll.Add(model.id,model.user_name,"登录","用户登录");
                Response.Redirect("index.aspx",false);
                return;
            }
            catch (Exception ex)
            {
                Common.CommomFunction.WriteLog("Login.aspx", "btnSubmit_Click", ex.Message);
            }
        }

    }
}