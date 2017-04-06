using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class reply
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string user_name { get; set; }
        public string user_tel { get; set; }
        public string user_qq { get; set; }
        public string user_email { get; set; }
        public DateTime add_time { get; set; }
        public string reply_content { get; set; }
        public DateTime reply_time { get; set; }
        public int is_lock { get; set; }

        public reply()
        {
            id = 0;
            title = string.Empty;
            content = string.Empty;
            user_name = string.Empty;
            user_tel = string.Empty;
            user_qq = string.Empty;
            user_email = string.Empty;
            add_time = DateTime.MinValue;
            reply_content = string.Empty;
            reply_time = DateTime.MinValue;
            is_lock = 0;
        }
    }
}
