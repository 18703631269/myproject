using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace  Web.admin.preview
{
    public partial class PreHtml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litR.Text =System.Web.HttpUtility.UrlDecode(Request.QueryString["f"]);
            lit.Text = litR.Text;
        }
    }
}