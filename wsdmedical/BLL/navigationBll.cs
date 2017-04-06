using System;
using System.Data;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 系统导航菜单
    /// </summary>
    public partial class navigationBll
    {
        //private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息


        private readonly DAL.navigationDal dal;
        public navigationBll()
        {
            dal = new DAL.navigationDal();
        }

        #region 基本方法===============================
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
                throw new Exception("--navigationBll-->Exists" + ex.Message, ex);
            }
        }

        public bool Exists(string name)
        {
            try
            {
                return dal.Exists(name);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->Exists" + ex.Message, ex);
            }
        }
        public int GetIdByPage(string pagename)
        {
            try
            {
                return dal.GetIdByPage(pagename);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->GetIdByPage" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.navigation model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.navigation model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->Exists" + ex.Message, ex);
            }
        }
        public string GetIds(int parent_id)
        {
            try
            {
                return dal.GetIds(parent_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->GetIds" + ex.Message, ex);
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
                throw new Exception("--navigationBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.navigation GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="nav_type">导航类别</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id)
        {
            try
            {
                return dal.GetList(parent_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->GetList" + ex.Message, ex);
            }
        }


        #endregion

        #region 扩展方法===============================
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            try
            {
                return dal.UpdateField(id, strValue);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->Exists" + ex.Message, ex);
            }
        }
        #endregion

        public DataTable GetListByChannelId(int channel_id)
        {
            try
            {
                return dal.GetListByChannelId(channel_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationBll-->GetListByChannelId" + ex.Message, ex);
            }
        }
    }
}

