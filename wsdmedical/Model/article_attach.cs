using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 上传附件
    /// </summary>
    public class article_attach
    {
        public int id { get; set; }

        public int article_id { get; set; }

        public string file_name { get; set; }

        public string file_path { get; set; }

        public int file_size { get; set; }

        public int down_num { get; set; }
        public string file_ext { get; set; }

        public int point { get; set; }

        public DateTime add_time { get; set; }

        public article_attach()
        {
            id = 0;
            article_id = 0;
            file_name = string.Empty;
            file_path = string.Empty;
            file_size = 0;
            down_num = 0;
            file_ext = string.Empty;
            point = 0;
            add_time = DateTime.MinValue;
        }
    }
}
