using System;

namespace  Web.admin.preview
{
    public partial class PreText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            htxt.Value = Request.QueryString["type"];
            if (!string.IsNullOrEmpty(htxt.Value)) 
            {
                string txtFiles = Server.MapPath("~/TempFile/" + htxt.Value);
                string fileName = htxt.Value.Substring(htxt.Value.LastIndexOf('\\') + 1);
                Response.ContentType = "text/plain";
                Response.ContentEncoding = System.Text.Encoding.UTF8;  //保持和文件的编码格式一致
                Response.AddHeader("content-disposition", "filename=" + fileName);
                Response.WriteFile(txtFiles);
                Response.End();
            }
        }
    }
}