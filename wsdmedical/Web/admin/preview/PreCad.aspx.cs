using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace  Web.admin.preview
{
    public partial class PreCad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  string strU=Server.UrlDecode(Request.QueryString["f"]);
            string strU =System.Web.HttpUtility.UrlDecode(Request.QueryString["f"]);
            if (!string.IsNullOrWhiteSpace(strU))
            {
                Literal1.Text = "~"+strU;
            }
        }
    }
}