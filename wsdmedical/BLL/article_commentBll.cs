using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class article_commentBll
    {
        private readonly article_commentDal dal;

        public article_commentBll()
        {
            dal = new DAL.article_commentDal();
        }

        #region 基本方法===========================
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
                throw new Exception("--article_commentBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回数据总数(AJAX分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            try
            {
                return dal.GetCount(strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentBll-->GetCount" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_comment model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentBll-->Add" + ex.Message, ex);
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
                throw new Exception("--article_commentBll-->UpdateField" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_comment model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentBll-->Update" + ex.Message, ex);
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
                throw new Exception("--article_commentBll-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_comment GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentBll-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                return dal.GetList(Top, strWhere, filedOrder);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentBll-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataTable GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentBll-->GetList" + ex.Message, ex);
            }
        }

        #endregion
    }
}
