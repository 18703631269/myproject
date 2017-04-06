using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class sys_config
    {
        public string webname{get;set;}
        public string weburl{get;set;}
        public string webcompany{get;set;}
        public string webaddress{get;set;}
        public string webtel{get;set;}
        public string webfax{get;set;}
        public string webmail{get;set;}
        public string webcrod{get;set;}
        public int staticstatus { get; set; } /*是否关闭网站*/
        public string staticextension { get; set; }/*关闭网站原因*/
        //public int memberstatus { get; set; }
        //public int commentstatus { get; set; }
        //public int logstatus { get; set; }
        public int webstatus { get; set; }
        //public string webcountcode{get;set;}

        public string smsapiurl{get;set;}
        public string smsusername{get;set;}
        public string smspassword{get;set;}

        public string emailsmtp{get;set;}
        public int emailssl{get;set;}
        public int emailport { get; set; }
        public string emailfrom{get;set;}
        public string emailusername{get;set;}
        public string emailpassword{get;set;}
        public string emailnickname{get;set;}

        public string filepath{get;set;}
        public int filesave{get;set;}
        public string fileextension{get;set;}
        public string videoextension{get;set;}
        public int attachsize{get;set;}
        public int videosize{get;set;}
        public int imgsize{get;set;}
        public int imgmaxheight{get;set;}
        public int imgmaxwidth{get;set;}
        public int thumbnailheight{get;set;}
        public int thumbnailwidth{get;set;}
        public int watermarktype{get;set;}
        public int watermarkposition { get; set; }
        public int watermarkimgquality { get; set; }
        public string watermarkpic{get;set;}
        public int watermarktransparency { get; set; }
        public string watermarktext{get;set;}
        public string watermarkfont{get;set;}
        public int watermarkfontsize { get; set; }

        public string sysdatabaseprefix { get; set; }
        public string sysencryptstring { get; set; }
    }
}
