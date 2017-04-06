using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    public static class CommomFunction
    {
        public static string SqlServerString()
        {
            return ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString();
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Strings"></param>
        public static void WriteFile(string Path, string Strings)
        {

            if (!System.IO.File.Exists(Path))
            {
                System.IO.FileStream f = System.IO.File.Create(Path);
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, true, System.Text.Encoding.UTF8);
            f2.WriteLine(Strings);
            f2.Close();
            f2.Dispose();

        }

        /// <summary>
        /// 错误日志功能
        /// </summary>
        /// <param name="pageName">出错页面</param>
        /// <param name="strMsg">出错方法</param>
        /// <param name="errormsg">错误日志</param>
        public static void WriteLog(string pageName, string strMsg,string errormsg)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                string logPath = HttpContext.Current.Server.MapPath("/") + "log/mylog.txt";
                fs = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.Write);
                fs.Close();
                sw = new StreamWriter(logPath, true, Encoding.Default);
                strMsg ="操作时间:"+ DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "页面: " + pageName + "方法: " + strMsg+"日志: "+errormsg;
                sw.WriteLine(strMsg);
                sw.Flush();
            }
            catch
            {
                HttpContext.Current.Response.Write("写入错误日志失败.");
            }
            finally
            {
                fs.Close();
                sw.Close();
            }
        }
    }
}
