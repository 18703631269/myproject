using BLL;
using Common;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Web.UI;

namespace Web.tools
{
    /// <summary>
    /// file_upload 的摘要说明
    /// </summary>
    public class file_upload : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
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
                case "uploadimages": //上传多张图片
                    uploadimages(context);
                    break;
                case "uploadfile": //上传文件
                    uploadfile(context);
                    break;
                case "uploadfilePath": //上传文件
                    uploadfilePath(context);
                    break;
                case "uploadexeclfile": //上传文件
                    uploadexeclfile(context);
                    break;
                case "uploadtry": //上传文件
                    uploadtry(context);
                    break;
                case "zyupload":
                    zyupload(context);
                    break;
                case "cms_upload":
                    cms_upload(context);
                    break;
            }
        }
        #region 同时上传多张图片
        public void uploadimages(HttpContext context)
        {
            string strR = "";
            string folder = context.Server.MapPath("~/Files/");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (context.Request.Form.AllKeys.Any(m => m == "chunk"))
            {
                int chunk = Convert.ToInt32(context.Request.Form["chunk"]);//当前分片
                int chunks = Convert.ToInt32(context.Request.Form["chunks"]);//分片总计
                string fileName = folder + context.Request.Form["guid"];
                //前端线程threads值= 1 可用
                //FileStream addFile = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                //前端线程threads值> 1 可用
                FileStream addFile = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite, 1024, true);

                BinaryWriter AddWrite = new BinaryWriter(addFile);
                HttpPostedFile file = context.Request.Files[0];
                Stream stream = file.InputStream;

                BinaryReader TempReader = new BinaryReader(stream);
                AddWrite.Write(TempReader.ReadBytes((int)stream.Length));

                TempReader.Close();
                stream.Close();
                AddWrite.Close();
                addFile.Close();

                TempReader.Dispose();
                stream.Dispose();
                AddWrite.Dispose();
                addFile.Dispose();
                //如果是最后一个分片，则重名名临时文件为上传文件名
                if (chunk == (chunk - 1))
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    string strNewName = Guid.NewGuid() + Path.GetExtension(context.Request.Files[0].FileName);
                    fileInfo.MoveTo(folder + strNewName);
                    strR = "/Files/" + strNewName;
                }
            }
            else
            {
                //没有分片
                string strNewName = Guid.NewGuid() + Path.GetExtension(context.Request.Files[0].FileName);
                context.Request.Files[0].SaveAs(folder + strNewName);
                strR = "/Files/" + strNewName;
                //添加操作日志
            }
            context.Response.Write(strR);
        }
        #endregion

        #region 上传文件
        public void uploadfile(HttpContext context)
        {
            string msg = string.Empty;
            string totalfile = string.Empty;
            string fileName = string.Empty;//文件名
            string filefullPath = string.Empty;//文件详细路径
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    fileName = System.IO.Path.GetFileName(files[i].FileName);
                    totalfile = totalfile + fileName + "|";
                    if (fileName == "")
                    {
                        msg = "请先上传文件,再点击上传.";
                        context.Response.Write(msg);
                        context.Response.End();
                        return;
                    }
                    fileName = System.IO.Path.GetFileNameWithoutExtension(files[i].FileName) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(fileName).ToLower();
                    //保存文件
                    filefullPath = context.Server.MapPath("../Files/" + fileName);
                    files[i].SaveAs(filefullPath);

                    //string swfname = ToSwfFile.GetSwfFile(fileName);//swf文件名称 预览功能放到项目中太大,建议封装成一个wcf程序
                }
                //可以参考这样搞
                //json = "{success:true,totalCount:" + totalCount + ",data:" + JsonConvert.SerializeObject(stores, Formatting.Indented, timeConverter) + "}";
                //json = "{success:true,totalCount:0,data:[]}";

                //FTP ftp = new FTP(FTPUrl, ftpUser, ftpPasswd);
                //ftp.UploadFTP(FTPUrl, filefullPath, ftpUser, ftpPasswd);
                msg = "文件上传成功!";
            }
            else
            {
                msg = "未选中文件!";
            }
            context.Response.Write(msg);
        }
        #endregion

        #region 上传文件的绝对路径
        public void uploadfilePath(HttpContext context)
        {
            string msg = "[]";
            DataTable _dt = new DataTable();
            _dt.Columns.Add("paths", typeof(string));
            string fileName = string.Empty;//文件名
            string filefullPath = string.Empty;//文件详细路径
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    fileName = System.IO.Path.GetFileName(files[i].FileName);
                    string fullpath = context.Server.MapPath(fileName);
                    if (fileName == "")
                    {
                        context.Response.Write("[]");
                        context.Response.End();
                        return;
                    }
                    fileName = System.IO.Path.GetFileNameWithoutExtension(files[i].FileName) + System.IO.Path.GetExtension(fileName).ToLower();
                    filefullPath = context.Server.MapPath("../Files/preview/" + fileName);
                    files[i].SaveAs(filefullPath);
                    //保存文件
                    DataRow row = _dt.NewRow();
                    row[0] = fileName;
                    _dt.Rows.Add(row);
                }
                msg = JsonHelper.DataTableToJSON(_dt);
            }
            else
            {
                msg = "[]";
            }
            context.Response.Write(msg);
        }
        #endregion

        #region 上传excel文件--每次只能保存一次
        public void uploadexeclfile(HttpContext context)
        {
            string msg = string.Empty;
            string fileName = string.Empty;//文件名
            string filefullPath = string.Empty;//文件详细路径
            HttpFileCollection files = context.Request.Files;
            if (files.Count > 0)
            {
                fileName = System.IO.Path.GetFileName(files[0].FileName);
                if (fileName == "")
                {
                    msg = "请先上传文件,再点击上传.";
                    context.Response.Write(msg);
                    context.Response.End();
                    return;
                }
                string filetype = System.IO.Path.GetExtension(fileName).ToLower();
                if (filetype == ".xls" || filetype == ".xlsx")
                {
                    fileName = System.IO.Path.GetFileNameWithoutExtension(files[0].FileName) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(fileName).ToLower();
                    //保存文件
                    filefullPath = context.Server.MapPath("../Files/" + fileName);
                    files[0].SaveAs(filefullPath);
                    msg = "文件上传成功!";
                }
                else
                {
                    msg = "请上传EXCEL文件!";
                }
            }
            else
            {
                msg = "未选中文件!";
            }
            context.Response.Write(msg);
        }
        #endregion

        public void uploadtry(HttpContext context)
        {
            HttpPostedFile hpf = context.Request.Files["fup_two"];
            string strs = HttpContext.Current.Server.MapPath("Temp");
            string filename = DateTime.Now.ToLongDateString() + Path.GetExtension(hpf.FileName);
            hpf.SaveAs(strs + "/" + filename);
            if (File.Exists(strs + "/" + filename))
            {
                context.Response.Write("上傳成功!");
            }
            else
            {
                context.Response.Write("上傳失敗!");
            }
        }

        public void zyupload(HttpContext context)
        {
            //上传配置
            int size = 2;           //文件大小限制,单位MB                             //文件大小限制，单位MB
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };         //文件允许格式
            //上传图片
            Hashtable info = new Hashtable();
            ZyUploader up = new ZyUploader();
            string path = "../Files/zyupload/";

            info = up.upFile(context, path, filetype, size);                   //获取上传状态
            context.Response.Write(info["url"]);  //向浏览器返回数据json数据
        }

        private void cms_upload(HttpContext context)
        {
            sys_config siteConfig = new sys_configBll().loadConfig();
            string _delfile = DTRequest.GetString("DelFilePath");
            HttpPostedFile _upfile = context.Request.Files["Filedata"];
            bool _iswater = true; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (DTRequest.GetQueryString("IsWater") == "1")
            {
                _iswater = true;
            }
            if (DTRequest.GetQueryString("IsThumbnail") == "1")
            {
                _isthumbnail = true;
            }
            if (_upfile == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
                return;
            }
            dtcms_upload upFiles = new dtcms_upload();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);
            //删除已存在的旧文件，旧文件不为空且应是上传文件，防止跨目录删除
            if (!string.IsNullOrEmpty(_delfile) && _delfile.IndexOf("../") == -1
                && _delfile.ToLower().StartsWith("/" + siteConfig.filepath.ToLower()))
            {
                Utils.DeleteUpFile(_delfile);
            }
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
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