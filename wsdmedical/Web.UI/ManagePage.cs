using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Common;

namespace Web.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        protected  Model.configxml modelxml;
        public Model.sys_config sysconfig;
        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            modelxml = new Model.configxml();//通过对configxml的初始化,来赋值
            sysconfig = new BLL.sys_configBll().loadConfig();
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            if (!IsAdminLogin())
            {
                Response.Write("<script>parent.location.href='" + "/" + "admin" + "/login.aspx'</script>");
                Response.End();
            }
        }

        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session["userinfo"] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Model.manager GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Model.manager model = Session["userinfo"] as Model.manager;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(int id, string action_type)
        {
            //首先获取到用户群组
            Model.manager model = GetAdminInfo();
            BLL.manager_roleBll bll = new BLL.manager_roleBll();
            bool result = bll.Exists(model.role_id, id, action_type);

            if (!result)
            {
                string msgbox = "parent.jsdialog(\"错误提示\", \"您没有管理该页面的权限，请勿非法进入！\", \"back\")";
                Response.Write("<script type=\"text/javascript\">" + msgbox + "</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 写入管理日志
        /// </summary>
        /// <param name="action_type"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool AddAdminLog(string action_type, string remark)
        {
            Model.manager model = GetAdminInfo();
            int newId = new BLL.manager_logBll().Add(model.id, model.user_name, action_type,Common.Utils.SqlFilter(remark));
            if (newId > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region JS提示============================================
        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        protected void JscriptMsg(string msgtitle, string url)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="callback">JS回调函数</param>
        /// parent.loadMenuTree 父亲页面刷新
        protected void JscriptMsg(string msgtitle, string url, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        #endregion

    }
}
