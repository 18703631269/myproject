using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;

namespace Common
{
    public class Utils
    {

        #region 对象转换处理
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// object型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
                return StrToBool(expression, defValue);

            return defValue;
        }

        /// <summary>
        /// 将字符串转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string expression, int defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(expression, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(expression, defValue));
        }

        /// <summary>
        /// string型转换为decimal型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal StrToDecimal(string expression, decimal defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            decimal intValue = defValue;
            if (expression != null)
            {
                bool IsDecimal = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsDecimal)
                    decimal.TryParse(expression, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(string expression, float defValue)
        {
            if ((expression == null) || (expression.Length > 10))
                return defValue;

            float intValue = defValue;
            if (expression != null)
            {
                bool IsFloat = Regex.IsMatch(expression, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(expression, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str, DateTime defValue)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                    return dateTime;
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str)
        {
            return StrToDateTime(str, DateTime.Now);
        }

        /// <summary>
        /// 判断对象是否可以转成int型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isNumber(object o)
        {
            int tmpInt;
            if (o == null)
            {
                return false;
            }
            if (o.ToString().Trim().Length == 0)
            {
                return false;
            }
            if (!int.TryParse(o.ToString(), out tmpInt))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 删除最后结尾的一个逗号
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            if (str.Length < 1)
            {
                return "";
            }
            return str.Substring(0, str.LastIndexOf(","));
        }
        #endregion

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }
        #endregion

        #region 生成指定长度的字符串
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }
        #endregion

        #region 截取字符串长度
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string SubStrs(int strLong, string str, string strRes = "")
        {
            string strs = FilterHTML(str);
            if (strs.Length > strLong)
            {
                strs = strs.Substring(0, strLong) + strRes;
            }
            return strs;
        }
        #endregion


        #region 过滤HTML字符
        /// <summary>
        /// 过滤HTML字符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterHTML(string html)
        {
            if (html == null)
                return "";
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        } 
        #endregion

        #region 过滤字符sql注入
        /// <summary>
        /// 过滤 Sql 语句字符串中的注入脚本
        /// </summary>
        /// <param name="source">传入的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string SqlFilter(string source)
        {
            //单引号替换成两个单引号
            source = source.Replace("'", "''");

            //半角封号替换为全角封号，防止多语句执行
            source = source.Replace(";", "；");

            //半角括号替换为全角括号
            source = source.Replace("(", "（");
            source = source.Replace(")", "）");
            source = source.Replace("--", "");
            ///////////////要用正则表达式替换，防止字母大小写得情况////////////////////

            //去除执行存储过程的命令关键字
            source = source.Replace("Exec", "");
            source = source.Replace("Execute", "");

            //去除系统存储过程或扩展存储过程关键字
            source = source.Replace("xp_", "x p_");
            source = source.Replace("sp_", "s p_");
            source = source.Replace("delete", "");
            source = source.Replace("update", "");
            source = source.Replace("insert", "");
            //防止16进制注入
            source = source.Replace("0x", "0 x");

            return source;
        }
        #endregion

        #region 生成随机字母或数字
        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        public static string GetCheckCode(int codeCount)
        {
            string str = string.Empty;
            int rep = 0;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
        #endregion

        #region 检测是否有Sql危险字符
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        #endregion

        #region MD5加密
        public static string MD5(string pwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(pwd);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');

            }
            return str;
        }
        #endregion

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        #region 替换指定的字符串
        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string originalStr, string oldStr, string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }
        #endregion

        #region 显示分页
        /// <summary>
        /// 返回分页页码
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="linkUrl">链接地址，__id__代表页码</param>
        /// <param name="centSize">中间页码数量</param>
        /// <returns></returns>
        public static string OutPageList(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            StringBuilder pageStr = new StringBuilder();
            string pageId = "__id__";
            string firstBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«上一页</a>";
            string lastBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">下一页»</a>";
            string firstStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

            if (pageIndex <= 1)
            {
                firstBtn = "<span class=\"disabled\">«上一页</span>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<span class=\"disabled\">下一页»</span>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<span class=\"current\">1</span>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<span class=\"current\">" + pageCount.ToString() + "</span>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            pageStr.Append("<span>共" + totalCount + "记录</span>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<span>...</span>\n");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<span class=\"current\">" + i + "</span>");
                }
                else
                {
                    pageStr.Append("<a href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<span>...</span>");
            }
            pageStr.Append(lastStr + lastBtn);
            return pageStr.ToString();
        }
        #endregion

        #region URL处理
        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string _url, string _keys, params string[] _values)
        {
            StringBuilder urlParams = new StringBuilder();
            try
            {
                string[] keyArr = _keys.Split(new char[] { '&' });
                for (int i = 0; i < keyArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "0")
                    {
                        _values[i] = UrlEncode(_values[i]);
                        urlParams.Append(string.Format(keyArr[i], _values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
                    urlParams.Insert(0, "?");
            }
            catch
            {
                return _url;
            }
            return _url + DelLastChar(urlParams.ToString(), "&");
        }
        #endregion

        #region 操作权限菜单
        /// <summary>
        /// 获取操作权限--所有类型
        /// </summary>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> ActionType()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Show", "显示");
            dic.Add("Add", "添加");
            dic.Add("Save", "保存");
            dic.Add("Edit", "修改");
            dic.Add("Reply", "回复");
            dic.Add("Audit", "审核");
            dic.Add("Delete", "删除");
            return dic;
        }
        #endregion

        #region 操作权限菜单
        /// <summary>
        /// 获取操作权限--只有显示
        /// </summary>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> ActionShowOnly()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Show", "显示");
            return dic;
        }
        #endregion

        #region 获得配置文件节点XML文件的绝对路径
        public static string GetXmlMapPath(string xmlName)
        {
            return GetMapPath(ConfigurationManager.AppSettings[xmlName].ToString());
        }
        public static string GetWebConfig(string xmlName)
        {
            return ConfigurationManager.AppSettings[xmlName].ToString();
        }
        #endregion

        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="_filepath">文件全名称</param>
        /// <returns>string</returns>
        public static string GetFileExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }
            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        #region 生成日期随机码
        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            #region
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
            #endregion
        }
        #endregion

        /// <summary>
        /// 删除上传的文件(及缩略图)
        /// </summary>
        /// <param name="_filepath"></param>
        public static void DeleteUpFile(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return;
            }
            string fullpath = GetMapPath(_filepath); //原图
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
            if (_filepath.LastIndexOf("/") >= 0)
            {
                string thumbnailpath = _filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" + _filepath.Substring(_filepath.LastIndexOf("/") + 1);
                string fullTPATH = GetMapPath(thumbnailpath); //宿略图
                if (File.Exists(fullTPATH))
                {
                    File.Delete(fullTPATH);
                }
            }
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        public static bool DeleteFile(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return false;
            }
            string fullpath = GetMapPath(_filepath);
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
                return true;
            }
            return false;
        }


        #region HTML代码转换成TXT格式
        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }
        #endregion


        #region TXT代码转换成HTML格式
        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把TXT代码转换成HTML格式
        public static String ToHtml(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("'", "&apos;");
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            //sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }
        #endregion

        public static string ExsitsString(string st)
        {
            string[] actionTypeArr = st.Split(',');
            string showmsg = string.Empty;
            foreach (KeyValuePair<string, string> kvp in ActionType())
            {
                for (int n = 0; n < actionTypeArr.Length; n++)
                {
                    if (kvp.Key.ToString() == actionTypeArr[n].ToString())
                    {
                        showmsg += "[" + kvp.Value + "] ";
                    }
                }
            }
            return showmsg;
            //if (st.IndexOf(',') > 0)
            //{
            //    string[] strs = st.Split(',');
            //    bool state = false;
            //    for (int i = 0; i < strs.Length; i++)
            //    {
            //        if (strs[i] == "Show")
            //        {
            //            state = true;
            //        }
            //    }
            //    if (state == true)
            //    {
            //        return "√";
            //    }
            //    else
            //    {
            //        return "×";
            //    }
            //}
            //else
            //{
            //    if (st == "Show")
            //    {
            //        return "√";
            //    }
            //    else
            //    {
            //        return "×";
            //    }
            //}
        }

        #region 替换URL
        /// <summary>
        /// 替换扩展名
        /// </summary>
        public static string GetUrlExtension(string urlPage, string staticExtension)
        {
            int indexNum = urlPage.LastIndexOf('.');
            if (indexNum > 0)
            {
                return urlPage.Replace(urlPage.Substring(indexNum), "." + staticExtension);
            }
            return urlPage;
        }
        /// <summary>
        /// 替换扩展名，如没有扩展名替换默认首页
        /// </summary>
        public static string GetUrlExtension(string urlPage, string staticExtension, bool defaultVal)
        {
            int indexNum = urlPage.LastIndexOf('.');
            if (indexNum > 0)
            {
                return urlPage.Replace(urlPage.Substring(indexNum), "." + staticExtension);
            }
            if (defaultVal)
            {
                if (urlPage.EndsWith("/"))
                {
                    return urlPage + "index." + staticExtension;
                }
                else
                {
                    return urlPage + "/index." + staticExtension;
                }
            }
            return urlPage;
        }
        #endregion

        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }

    }
}
