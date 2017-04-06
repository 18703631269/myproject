using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.UI
{
    public class HttpModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
        }

        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                //判断是否为页面
                HttpContext context = ((HttpApplication)sender).Context;
                string requestPath = context.Request.Path.ToLower(); //获得当前页面(含目录)
                ArrayList Urls = IsValidPage(requestPath);
                if (Urls.Count > 0)
                {
                    Model.sys_config siteConfig = new BLL.sys_configBll().loadConfig(); //获得站点配置信息
                    #region  检查网站重写状态0表示不开启重写、1开启重写、2生成静态
                    if (siteConfig.staticstatus == 0)
                    {
                        return;
                    }
                    else
                    {
                        foreach (Model.sys_url model in Urls)
                        {
                            string newPattern = Utils.GetUrlExtension(model.pattern, siteConfig.staticextension); //替换扩展名
                            if (Regex.IsMatch(requestPath, string.Format("^/{0}$", newPattern), RegexOptions.None | RegexOptions.IgnoreCase)
                               || (model.path == "index.aspx" && Regex.IsMatch(requestPath, string.Format("^/{0}$", model.pattern), RegexOptions.None | RegexOptions.IgnoreCase)))
                            {
                                string queryString = Regex.Replace(requestPath, string.Format("/{0}", newPattern), model.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                                context.RewritePath(string.Format("/{0}", model.name), string.Empty, queryString);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public ArrayList IsValidPage(string requestPath)
        {
            ArrayList Urls = new ArrayList();
            if (requestPath.IndexOf('.') > 0)
            {
                string strs = requestPath.Split('.')[1];
                if (strs != "aspx"&&(strs=="html"||strs=="php"||strs=="jsp"))
                {
                    BLL.sys_configBll bll = new BLL.sys_configBll();
                    List<Model.sys_url> ls = bll.GetList("");
                    foreach (Model.sys_url model in ls)
                    {
                        if (!string.IsNullOrEmpty(model.querystring))
                        {
                            model.querystring = model.querystring.Replace("^", "&");
                        }
                        else
                        {
                            model.querystring = "";
                        }
                        Urls.Add(model);
                    }
                    return Urls;
                }
                return Urls;
            }
            else
            {
                return Urls;
            }
        }

        /// <summary>
        /// 截取安装目录和站点目录部分
        /// </summary>
        /// <param name="webPath">站点安装目录</param>
        /// <param name="sitePath">站点目录</param>
        /// <param name="requestPath">当前页面路径</param>
        /// <returns>String</returns>
        private string CutStringPath(string webPath, string sitePath, string requestPath)
        {
            if (requestPath.StartsWith(webPath))
            {
                requestPath = requestPath.Substring(webPath.Length);
            }
            sitePath += "/";
            if (requestPath.StartsWith(sitePath))
            {
                requestPath = requestPath.Substring(sitePath.Length);
            }
            return "/" + requestPath;
        }
        /// <summary>
        /// 遍历指定路径的子目录，检查是否匹配
        /// </summary>
        /// <param name="cacheKey">缓存KEY</param>
        /// <param name="webPath">网站安装目录，以“/”结尾</param>
        /// <param name="dirPath">指定的路径，以“/”结尾</param>
        /// <param name="requestPath">获取的URL全路径</param>
        /// <returns>布尔值</returns>
        private bool IsDirExist(string webPath, string dirPath, string requestPath)
        {
            ArrayList list = new ArrayList(); //取得所有目录
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(dirPath));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                list.Add(dir.Name.ToLower());
            }
            string requestFirstPath = string.Empty; //获得当前页面除站点安装目录的虚拟目录名称
            string tempStr = string.Empty; //临时变量
            if (requestPath.StartsWith(webPath))
            {
                tempStr = requestPath.Substring(webPath.Length);
                if (tempStr.IndexOf("/") > 0)
                {
                    requestFirstPath = tempStr.Substring(0, tempStr.IndexOf("/"));
                }
            }
            if (requestFirstPath.Length > 0 && list.Contains(requestFirstPath.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
