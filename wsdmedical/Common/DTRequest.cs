using System;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;

namespace Common
{
	/// <summary>
	/// Request操作类
	/// </summary>
	public class DTRequest
	{
		/// <summary>
		/// 获得指定Url参数的值
		/// </summary>
		/// <param name="strName">Url参数</param>
		/// <returns>Url参数的值</returns>
		public static string GetQueryString(string strName)
		{
            return GetQueryString(strName, false);
		}

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";

            if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.QueryString[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.QueryString[strName];
        }


        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";

            if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.Form[strName];
        }

		/// <summary>
		/// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
		/// </summary>
		/// <param name="strName">参数</param>
		/// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetString(strName, false);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if ("".Equals(GetQueryString(strName)))
                return GetFormString(strName, sqlSafeCheck);
            else
                return GetQueryString(strName, sqlSafeCheck);
        }
      
       

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName)
        {
            return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
        }

		/// <summary>
		/// 获得指定Url参数的int类型值
		/// </summary>
		/// <param name="strName">Url参数</param>
		/// <param name="defValue">缺省值</param>
		/// <returns>Url参数的int类型值</returns>
		public static int GetQueryInt(string strName, int defValue)
		{
			return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
		}

        /// <summary>
        /// int类型值
        /// </summary>
        public static int StrToInt(string strs)
        {
            try
            {
              return  Convert.ToInt32(strs.Trim());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

		/// <summary>
		/// 获得当前页面客户端的IP
		/// </summary>
		/// <returns>当前页面客户端的IP</returns>
		public static string GetIPAddress()
		{
            try
            {
                string IP4Address = String.Empty;
                foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }
                if (IP4Address != String.Empty)
                {
                    return IP4Address;
                }

                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }
                return IP4Address;
            }
            catch (Exception ex)//获取失败
            {
                return "0.0.0.0";
            }
		}

	}
}