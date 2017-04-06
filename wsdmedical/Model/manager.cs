using System;
namespace Model
{
    /// <summary>
    /// 管理员信息表
    /// </summary>
    [Serializable]
    public partial class manager
    {

        public int id { get; set; }
        public int role_id { get; set; }
        public int role_type { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string real_name { get; set; }
        public string telephone { get; set; }
        public string email{ get; set; }
        public int is_lock{ get; set; }
        public DateTime add_time{ get; set; }
        public manager()
        {
            id = 0;
            role_id = 0;
            role_type = 2;
            user_name = "";
            password = "";
            salt = "";
            real_name = "";
            telephone = "";
            email = "";
            is_lock = 0;
            add_time = DateTime.Now;
        }
    }
}