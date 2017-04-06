using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 文章类
    /// </summary>
    public  class article
    {
        public int id { get; set; }
        public int channel_id { get; set; }
        public int category_id { get; set; }
        public string img_url { get; set; }
        public string title { get; set; }
        public string content { get; set; }

        public int sort_id { get; set; }

        public int click { get; set; }

        public int status { get; set; }

        public int is_hot{get;set;}

        public int user_id { get; set; }

        public DateTime add_time { get; set; }

        public DateTime update_time { get; set; }

        public string video_url { get; set; }
        public string keyword { get; set; }
        public string details_one { get; set; }

        public string details_two { get; set; }

        public string details_three { get; set; }

        public string details_four { get; set; }

        public string detail_title_one { get; set; }

        public string detail_title_two { get; set; }

        public string detail_title_three { get; set; }

        public string detail_title_four { get; set; }


        public string fylx { get; set; }//翻译类型
        public string city { get; set; }
        public string hospl { get; set; }
        public string zyxlzt { get; set; }
        public string paymoney { get; set; }
        public string zslx { get; set; }
        public string mdcity { get; set; }
        public string szjc { get; set; }
        public double chjj { get; set; }   //付款金额
        public article()
        {
            id = 0;
            channel_id = 0;
            category_id = 0;
            img_url = string.Empty;
            title = string.Empty;
            content = string.Empty;
            sort_id = 0;
            click = 0;
            status = 0;
            is_hot = 0;
            user_id = 0;
            add_time = DateTime.MinValue;
            update_time = DateTime.MinValue;
            video_url = string.Empty;
            keyword = string.Empty;
            details_one = string.Empty;
            details_two=string.Empty;
            details_three = string.Empty;
            details_four = string.Empty;

            detail_title_one = string.Empty;
            detail_title_two = string.Empty;
            detail_title_three = string.Empty;
            detail_title_four = string.Empty;
            fylx =string.Empty;
            city  =string.Empty;
            hospl =string.Empty;
            zyxlzt =string.Empty;
            paymoney =string.Empty;
            zslx  =string.Empty;
            mdcity =string.Empty;
            szjc = string.Empty;
            chjj = 0;
        }
    }
}