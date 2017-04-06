using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Common
{
    public class FileUploadHelper
    {
        /// <summary>
        /// 服务器控件上传
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="strPath"></param>
        /// <param name="fileSize"></param>
        /// <param name="allowedExtensions"></param>
        /// <param name="blDatePath"></param>
        /// <returns></returns>
        public static string UploadFile(FileUpload fileUpload, string strPath, int fileSize, string[] allowedExtensions, bool blDatePath)
        {
            string uploadedPath = "";
            bool fileOk = false;
            string filePath = HttpContext.Current.Server.MapPath(strPath);
            string fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();

            if (fileUpload.HasFile)
            {
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i].ToLower())
                    {
                        fileOk = true;
                    }
                }
            }

            if (fileOk)
            {
                try
                {
                    if (fileUpload.PostedFile.ContentLength < fileSize * 1024)
                    {

                        if (blDatePath)
                        {
                            string dateFileName = GetDateTimeFileName();
                            fileUpload.PostedFile.SaveAs(filePath + dateFileName + fileExtension);
                            uploadedPath = dateFileName + fileExtension;
                        }
                        else
                        {
                            fileUpload.PostedFile.SaveAs(filePath + fileUpload.FileName);
                            uploadedPath = fileUpload.FileName;
                        }

                    }
                    else
                    {
                        uploadedPath = "";
                        HttpContext.Current.Response.Write("<script>alert('文件大小不符合格式!');</script>");
                    }
                }
                catch
                {
                    uploadedPath = "";
                }
            }
            else
            {
                uploadedPath = "";
                HttpContext.Current.Response.Write("<script>alert('文件类型不符合格式!');</script>");
            }
            return uploadedPath;
        }

        public static string GetDateTimeFileName()
        {
            string File = DateTime.Now.ToString();
            File = File.Replace(":", "");
            File = File.Replace(" ", "");
            File = File.Replace("-", "");
            File = File.Replace("/", "");
            Random Ran = new Random();
            File = File + Ran.Next(9999);
            return File;
        }

        #region ajax上傳
        public static string AjaxUploadFile(string fileId, string thisPath, int fileSize, string[] allowedExtensions, bool blDatePath)
        {
            bool fileOk = false;
            HttpPostedFile hpf = HttpContext.Current.Request.Files[fileId];
            string strPath = HttpContext.Current.Server.MapPath(thisPath);
            string filename = DateTime.Now.ToLongDateString() + Path.GetExtension(hpf.FileName);
            if (hpf.ContentLength < fileSize * 1024)
            {
                return "0";//上傳文件超過最大限制
            }
            if (hpf.ContentLength > 0 && hpf != null)
            {
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (Path.GetExtension(hpf.FileName) == allowedExtensions[i].ToLower())
                    {
                        fileOk = true;
                    }
                }
            }
            if (fileOk == false)
            {
                return "1";//上傳文件類型不正確
            }
            hpf.SaveAs(strPath + "/" + filename);
            if (File.Exists(strPath + "/" + filename))
            {
                return "success";
            }
            else
            {
                return "error";
            }
        }
        #endregion
    }
}
