using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 图片相册
    /// </summary>
    public class article_albums
    {
        public int id { get; set; }
        public int article_id { get; set; }

        public string thumb_path { get; set; }

        public string original_path { get; set; }

        public string remark { get; set; }

        public DateTime add_time { get; set; }

        public article_albums()
        {
            id = 0;
            article_id = 0;
            thumb_path = string.Empty;
            original_path = string.Empty;
            remark = string.Empty;
            add_time = DateTime.MinValue;
        }
    }
}
