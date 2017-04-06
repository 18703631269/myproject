using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class article_attachBll
    {
        private readonly DAL.article_attachDal dal;
        public article_attachBll()
        {
            try
            {
                dal = new DAL.article_attachDal();
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public int add(List<article_attach> ls, int id)
        {
            try
            {
                return dal.add(ls, id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
        public int edit(List<article_attach> ls, int id)
        {
            try
            {
                return dal.edit(ls, id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
        public List<article_attach> GetList(int id)
        {
            try
            {
                return dal.GetList(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
        #region 基本方法
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
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 获取下载次数
        /// </summary>
        public int GetDownNum(int id)
        {
            try
            {
                return dal.GetDownNum(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取总下载次数
        /// </summary>
        public int GetCountNum(int article_id)
        {
            try
            {
                return dal.GetCountNum(article_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
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
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_attach GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        //删除更新的旧文件
        public void DeleteFile(int id, string filePath)
        {
            try
            {
                Model.article_attach model = GetModel(id);
                if (model != null && model.file_path != filePath)
                {
                    Utils.DeleteFile(model.file_path);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        #endregion  Method
    }
}
