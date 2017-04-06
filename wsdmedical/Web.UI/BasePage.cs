using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Web.UI
{
    
   public  class BasePage : System.Web.UI.Page
    {
        protected internal Model.sys_config config = new BLL.sys_configBll().loadConfig();
       public BasePage()
       {
           if (config.webstatus == 0)
           {
               HttpContext.Current.Response.Redirect("Error/error.html");
               return;
           }
       }
    }
}
