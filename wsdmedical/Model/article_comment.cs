using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class article_comment
    {
        public int id { get; set; }
        public int channel_id { get; set; }
        public int article_id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_ip { get; set; }
        public string content { get; set; }
        public int is_lock { get; set; }
        public DateTime add_time { get; set; }
        public int is_reply { get; set; }
        public string reply_content { get; set; }
        public DateTime reply_time { get; set; }

        public article_comment()
        {
            id = 0;
            channel_id = 0;
            article_id = 0;
            user_id = 0;
            user_name = string.Empty;
            user_ip = string.Empty;
            content = string.Empty;
            is_lock = 0;
            add_time = DateTime.MinValue;
            is_reply = 0;
            reply_content = string.Empty;
            reply_time = DateTime.MinValue;
        }
    }
}
