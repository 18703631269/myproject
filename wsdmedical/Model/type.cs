using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class type
    {
        public int tid { get; set; }
        public string tname { get; set; }

        public string town { get; set; }


        public int sort_id { get; set; }

        public int is_lock { get; set; }

        public int add_user { get; set; }

        public DateTime add_time { get; set; }

        public string ctype { get; set; }
        public type()
        {
            tid = 0;
            tname = string.Empty;
            town = string.Empty;
            sort_id = 0;
            is_lock = 0;
            add_user = 0;
            add_time = DateTime.MinValue;
            ctype = string.Empty;
        }
    }
}
