using System;
namespace Model
{
    /// <summary>
    /// 管理角色权限表
    /// </summary>
    [Serializable]
    public class manager_role_value
    {

        public int id { get; set; }
        public int role_id { get; set; }
        public string nav_name { get; set; }
        public string action_type { get; set; }
        public manager_role_value()
        {
            id = 0;
            role_id = 0;
            nav_name = string.Empty;
            action_type = string.Empty;
        }
    
    }
}