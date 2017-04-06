using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Web.UI;

namespace Web.tools
{
    /// <summary>
    /// check 的摘要说明
    /// </summary>
    public class check : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        managerBll mgrBll = new managerBll();
       
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
                case "userlist": //用户列表
                    userlist(context);
                    break;
            }
        }

        public void userlist(HttpContext context)
        {
            try
            {
                DataTable _dt = mgrBll.GetGroup(2);
                string objs = JsonHelper.DataTableToJSON(_dt);
                context.Response.Write(objs);
            }
            catch (Exception ex)
            {
                throw new Exception("CqXx.ashx-->yearmonth" + ex.Message, ex);
            }
        }
        
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}