using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 管理角色表
    /// </summary>
    [Serializable]
    public class manager_role
    {

        public int id { get; set; }
        public string role_name { get; set; }
        public int role_type { get; set; }
        public int is_sys { get; set; }

        public List<manager_role_value> manager_role_values { get; set; }
        public manager_role()
        {
            id = 0;
            role_name = "";
            role_type = 0;
            is_sys = 0;
        }
   
    }
}