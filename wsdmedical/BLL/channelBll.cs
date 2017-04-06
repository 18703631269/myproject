using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 系统频道表
    /// </summary>
    public partial class channelBll
    {
        private readonly DAL.channelDal dal;

        public channelBll()
        {
            dal = new DAL.channelDal();
        }

        /// <summary>
        /// 返回频道名称
        /// </summary>
        public string GetChannelName(int id)
        {
            try
            {
                return dal.GetChannelName(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--channelBll-->GetChannelName" + ex.Message, ex);
            }
        }



        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
            }
            catch (Exception ex)
            {
                throw new Exception("--channelBll-->GetList" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.channel GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--channelBll-->GetModel" + ex.Message, ex);
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
                throw new Exception("--channelBll-->Delete" + ex.Message, ex);
            }
        }
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
                throw new Exception("--channelBll-->UpdateField" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.channel model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--channelBll-->Add" + ex.Message, ex);
            }
        }

        public int Edit(Model.channel model)
        {
            try
            {
                return dal.Edit(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--channelBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 查询名称是否存在
        /// </summary>
        public bool Exists(string name)
        {
            try
            {
                return dal.Exists(name);
            }
            catch (Exception ex)
            {
                throw new Exception("--channelBll-->Exists" + ex.Message, ex);
            }
        }
    }
}