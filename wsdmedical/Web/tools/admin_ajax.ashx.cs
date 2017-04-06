using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using Web.UI;
using Common;
using BLL;

namespace ProjectSdy.Web.tools
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //检查管理员是否登录
            if (!new ManagePage().IsAdminLogin())
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"尚未登录或已超时，请登录后操作！\"}");
                return;
            }
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {
                case "manager_validate": //验证管理员用户名是否重复
                    manager_validate(context);
                    break;
                case "get_navigation_list": //获取后台导航字符串
                    get_navigation_list(context);
                    break;
                case "GetPageAuthority": //获取后台导航字符串
                    GetPageAuthority(context);
                    break;
                case "navigation_validate": //验证别名是否重复
                    navigation_validate(context);
                    break;
                case "channel_name_validate"://验证频道名
                    channel_name_validate(context);
                    break;
            }
        }

        #region 验证管理员用户名是否重复========================
        private void manager_validate(HttpContext context)
        {
            string user_name = DTRequest.GetString("param");
            if (string.IsNullOrEmpty(user_name))
            {
                context.Response.Write("{ \"info\":\"请输入用户名\", \"status\":\"n\" }");
                return;
            }
            managerBll bll = new managerBll();
            if (bll.Exists(user_name))
            {
                context.Response.Write("{ \"info\":\"用户名已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"用户名可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 获取后台导航字符串==============================
        private void get_navigation_list(HttpContext context)
        {
            Model.manager adminModel = new ManagePage().GetAdminInfo(); //获得当前登录管理员信息
            if (adminModel == null)
            {
                return;
            }
            Model.manager_role roleModel = new manager_roleBll().GetModel(adminModel.role_id); //获得管理角色信息
            if (roleModel == null)
            {
                return;
            }
            DataTable dt = new navigationBll().GetList(0);
            this.get_navigation_childs(context, dt, 0, roleModel.role_type, roleModel.manager_role_values);

        }
        private void get_navigation_childs(HttpContext context, DataTable oldData, int parent_id, int role_type, List<Model.manager_role_value> ls)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            bool isWrite = false; //是否输出开始标签
            for (int i = 0; i < dr.Length; i++)
            {
                //检查是否显示在界面上====================
                bool isActionPass = true;
                if (int.Parse(dr[i]["is_lock"].ToString()) == 0)
                {
                    isActionPass = false;
                }
                //检查管理员权限==========================
                if (isActionPass && role_type > 1)//表示不是超级管理员,根据权限显示各自的列表
                {
                    string[] actionTypeArr = dr[i]["action_type"].ToString().Split(',');
                    foreach (string action_type_str in actionTypeArr)
                    {
                        //如果存在显示权限资源，则检查是否拥有该权限
                        if (action_type_str == "Show")
                        {
                            Model.manager_role_value modelt = ls.Find(p => p.nav_name == dr[i]["id"].ToString() && p.action_type == "Show");
                            if (modelt == null)
                            {
                                isActionPass = false;
                            }
                        }
                    }
                }
                //如果没有该权限则不显示
                if (!isActionPass)
                {
                    if (isWrite && i == (dr.Length - 1) && parent_id > 0)
                    {
                        context.Response.Write("</ul>\n");
                    }
                    continue;
                }
                //如果是顶级导航
                if (parent_id == 0)
                {
                    context.Response.Write("<div class=\"list-group\">\n");
                    context.Response.Write("<h1 title=\"" + dr[i]["sub_title"].ToString() + "\">");
                    if (!string.IsNullOrEmpty(dr[i]["icon_url"].ToString().Trim()))
                    {
                        context.Response.Write("<img src=\"" + dr[i]["icon_url"].ToString() + "\" />");
                    }
                    context.Response.Write("</h1>\n");
                    context.Response.Write("<div class=\"list-wrap\">\n");
                    context.Response.Write("<h2>" + dr[i]["title"].ToString() + "<i></i></h2>\n");
                    //调用自身迭代
                    this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), role_type, ls);
                    context.Response.Write("</div>\n");
                    context.Response.Write("</div>\n");
                }
                else //下级导航
                {
                    if (!isWrite)
                    {
                        isWrite = true;
                        context.Response.Write("<ul>\n");
                    }
                    context.Response.Write("<li>\n");
                    context.Response.Write("<a navid=\"" + dr[i]["id"].ToString() + "\"");
                    string link_url = dr[i]["link_url"].ToString();
                    if (!string.IsNullOrEmpty(link_url))
                    {
                        //判断该链接是否包含后缀 如果不包含后面加?qx=XXX,如果包含后面加&qx=XXX
                        if (link_url.Contains("?"))
                        {
                            if (int.Parse(dr[i]["channel_id"].ToString()) > 0)
                            {
                                context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "&channel_id=" + dr[i]["channel_id"].ToString() + "&qx=" + dr[i]["name"] + "\" target=\"mainframe\"");
                            }
                            else
                            {
                                context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "&qx=" + dr[i]["name"] + "\" target=\"mainframe\"");
                            }
                        }
                        else
                        {
                            if (int.Parse(dr[i]["channel_id"].ToString()) > 0)
                            {
                                context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "?channel_id=" + dr[i]["channel_id"].ToString() + "&qx=" + dr[i]["name"] + "\" target=\"mainframe\"");
                            }
                            else
                            {
                                context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "?qx=" + dr[i]["name"] + "\" target=\"mainframe\"");
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(dr[i]["icon_url"].ToString()))
                    {
                        context.Response.Write(" icon=\"" + dr[i]["icon_url"].ToString() + "\"");
                    }
                    context.Response.Write(" target=\"mainframe\">\n");
                    context.Response.Write("<span>" + dr[i]["title"].ToString() + "</span>\n");
                    context.Response.Write("</a>\n");
                    //调用自身迭代
                    this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), role_type, ls);
                    context.Response.Write("</li>\n");

                    if (i == (dr.Length - 1))
                    {
                        context.Response.Write("</ul>\n");
                    }
                }
            }
        }
        #endregion

        #region 获取该页面权限--暂停使用
        private void GetPageAuthority(HttpContext context)
        {
            Model.manager adminModel = new ManagePage().GetAdminInfo(); //获得当前登录管理员信息
            if (adminModel == null)
            {
                return;
            }
            string pagename = context.Request.Params["pagename"];
            if (adminModel.role_type == 1)//表示是超级管理员
            {
                DataTable _dt = new DataTable();
                _dt.Columns.Add("action_type", typeof(String));
                foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
                {
                    DataRow dr = _dt.NewRow();
                    dr[0] = kvp.Key;
                    _dt.Rows.Add(dr);
                }
                string objs = JsonHelper.DataTableToJSON(_dt);
                context.Response.Write(objs);
            }
            else
            {
                navigationBll bll = new navigationBll();
                manager_roleBll bllRole = new manager_roleBll();
                int tid = bll.GetIdByPage(pagename);//表示页面对应的id
                DataTable _dt = bllRole.GetAuditById(tid.ToString(),adminModel.role_id);
                string objs = JsonHelper.DataTableToJSON(_dt);
                context.Response.Write(objs);
            }
        }
        #endregion

        #region 验证别名是否重复========================
        private void navigation_validate(HttpContext context)
        {
            string navname = DTRequest.GetString("param");
            string old_name = DTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(navname))
            {
                context.Response.Write("{ \"info\":\"该别名不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (navname.ToLower() == old_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该别名可使用\", \"status\":\"y\" }");
                return;
            }
            //检查保留的名称开头
            if (navname.ToLower().StartsWith("channel_"))
            {
                context.Response.Write("{ \"info\":\"该别名系统保留，请更换！\", \"status\":\"n\" }");
                return;
            }
            BLL.navigationBll bll = new BLL.navigationBll();
            if (bll.Exists(navname))
            {
                context.Response.Write("{ \"info\":\"该别名已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该别名可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证频道名称是否是否可用========================
        private void channel_name_validate(HttpContext context)
        {
            string channel_name = DTRequest.GetString("param");
            string old_channel_name = DTRequest.GetString("old_channel_name");
            if (string.IsNullOrEmpty(channel_name))
            {
                context.Response.Write("{ \"info\":\"频道名称不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (channel_name.ToLower() == old_channel_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
                return;
            }
            BLL.channelBll bll = new BLL.channelBll();
            if (bll.Exists(channel_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}