using System;
namespace Model
{
    /// <summary>
    /// 管理日志表
    /// </summary>
    [Serializable]
    public  class manager_log
    {

        public int id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string action_type { get; set; }
        public string remark { get; set; }
        public string user_ip { get; set; }
        public DateTime add_time { get; set; }
        public manager_log()
        {
            id = 0;
            user_id = 0;
            user_name = "";
            action_type = "";
            remark = "";
            user_ip = "";
            add_time = DateTime.Now;
        }

    }
}