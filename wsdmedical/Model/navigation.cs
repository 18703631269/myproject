using System;
namespace Model
{
    /// <summary>
    /// 系统导航菜单
    /// </summary>
    [Serializable]
    public partial class navigation
    {

        public int id { get; set; }
        public int parent_id { get; set; }
        public int channel_id { get; set; }
        public string nav_type { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string sub_title { get; set; }
        public string icon_url { get; set; }
        public string link_url { get; set; }
        public int sort_id { get; set; }
        public int is_lock { get; set; }
        public string remark { get; set; }
        public string action_type { get; set; }
        public int is_sys { get; set; }
        public int class_layer { get; set; }
        public navigation()
        {
            id = 0;
            parent_id = 0;
            nav_type = "";
            name = "";
            title = "";
            sub_title = "";
            icon_url = "";
            link_url = "";
            sort_id = 99;
            is_lock = 0;
            remark = "";
            action_type = "";
            is_sys = 0;
            class_layer = 0;
            channel_id = 0;
        }
    }
}