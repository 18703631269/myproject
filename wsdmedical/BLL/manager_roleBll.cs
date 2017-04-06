using System;
using System.Data;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 管理角色
    /// </summary>
    public partial class manager_roleBll
    {
        // private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息

        private readonly DAL.manager_roleDal dal;
        public manager_roleBll()
        {
            dal = new DAL.manager_roleDal();
        }

        #region 基本方法==============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            try
            {
                return dal.Exists(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 检查是否有权限
        /// </summary>
        public bool Exists(int role_id, int id, string action_type)
        {
            try
            {
                Model.manager_role model = dal.GetModel(role_id);
                if (model != null)
                {
                    if (model.role_type == 1)
                    {
                        return true;
                    }
                    Model.manager_role_value modelt = model.manager_role_values.Find(p => p.id == id && p.action_type == action_type);
                    if (modelt != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            try
            {
                return dal.GetTitle(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager_role model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager_role model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                return dal.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager_role GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            try
            {
                return dal.GetList(strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetAuditById(string nav_name,int role_id)
        {
            try
            {
                return dal.GetAuditById(nav_name,role_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleBll-->GetAuditById" + ex.Message, ex);
            }
        }
        #endregion
    }
}