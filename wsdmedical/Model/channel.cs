using System;
using System.Collections.Generic;

namespace Model
{
    public class channel
    {

        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }

        public int sort_id { get; set; }

        public int is_video { get; set; }
        public int is_albums { get; set; }
        public int is_attach { get; set; }
        public int is_details { get; set; }

        public int is_fylx { get; set; }//翻译类型
        public int is_city { get; set; }
        public int is_hospl { get; set; }
        public int is_zyxlzt { get; set; }
        public int is_paymoney { get; set; }
        public int is_zslx { get; set; }
        public int is_mdcity { get; set; }
        public int is_szjc { get; set; }
        public int is_yybzj { get; set; }
        public channel()
        {
            id = 0;
            name = string.Empty;
            title = string.Empty;
            sort_id = 0;
            is_video = 0;
            is_albums = 0;
            is_attach = 0;
            is_details = 0;
            is_fylx = 0;
            is_city = 0;
            is_hospl = 0;
            is_zyxlzt = 0;
            is_paymoney = 0;
            is_zslx = 0;
            is_mdcity = 0;
            is_szjc = 0;
            is_yybzj = 0;
        }
    }
}