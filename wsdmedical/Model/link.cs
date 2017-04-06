using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class link
    {
        public int id { get; set; }
        public string title { get; set; }

        public string link_url { get; set; }
        public string img_url { get; set; }

        public int sort_id { get; set; }

        public int is_lock { get; set; }

        public int add_user { get; set; }

        public DateTime add_time { get; set; }

        public link()
        {
            id = 0;
            title = string.Empty;
            link_url = string.Empty;
            img_url = string.Empty;
            sort_id = 0;
            is_lock = 0;
            add_user = 0;
            add_time = DateTime.MinValue;
        }
    }
}
