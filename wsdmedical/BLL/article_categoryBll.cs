using System;
using System.Data;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// 扩展属性表
    /// </summary>
    public partial class article_categoryBll
    {

        private readonly DAL.article_categoryDal dal;
        public article_categoryBll()
        {
            dal = new DAL.article_categoryDal();
        }

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
                throw new Exception("--article_categoryBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取父类的title
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetParantName(int id)
        {
            try
            {
                return dal.GetParentName(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetParantName" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_category model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Add" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            try
            {
                dal.UpdateField(id, strValue);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->UpdateField" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            try
            {
                return dal.GetTitle(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetTitle" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_category model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Update" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                dal.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_category GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetModel" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 取得该频道下所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id, int channel_id)
        {
            try
            {
                return dal.GetList(parent_id,channel_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetTopList(int parent_id, int channel_id,int? top=0)
        {
            try
            {
                return dal.GetTopList(parent_id, channel_id,top);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetTopList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取该频道下的所有信息
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="top"></param>
        /// <returns></returns>
        public DataTable GetChanList(int channel_id, int? top = 0)
        {
            try
            {
                return dal.GetChanList(channel_id, top);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetChanList" + ex.Message, ex);
            }
        }

        public DataTable GetCList(int channel_id,string strTitle, int? top = 0)
        {
            try
            {
                return dal.GetCList(channel_id, strTitle, top);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetCList" + ex.Message, ex);
            }
        }

    }
}

