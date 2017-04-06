using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    public static class AuthorityPage
    {
        /// <summary>
        /// 权限判断
        /// </summary>
        /// <param name="ctl">控制控件</param>
        /// <param name="qxname">当前权限</param>
        /// <param name="qxstr">该用户拥有的权限</param>
        public static void QuanXianVis(System.Web.UI.Control ctl, string qxname, DataTable _dt,int role_types)
        {
            if (role_types == 1)
            {
                ctl.Visible = true;
            }
            else
            {
                if (!StrIFInStr(qxname, _dt))
                {
                    ctl.Visible = false;
                }
                else
                {
                    ctl.Visible = true;
                }
            }
        }//按钮权限管理

        public static bool StrIFInStr(string Str1, DataTable _dt)
        {
            DataRow[] dr = _dt.Select(string.Format(@"{0}='{1}'", "action_type", Str1));
            if (dr.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } //权限判断
    }
}
