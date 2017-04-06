using System;
using System.Data;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    /// <summary>
    /// 文章内容
    /// </summary>
    public partial class articleBll
    {
        private readonly articleDal dal;

        public articleBll()
        {
            dal = new DAL.articleDal();
        }
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int channel_id, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                return dal.GetList(channel_id, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public DataTable GetAll(int channel_id, string strId) 
        {
            try
            {
                return dal.GetAll(channel_id, strId);
            }
            catch (Exception ex) 
            {
                throw new Exception("--article_categoryBll-->GetAll" + ex.Message, ex);
            }
        }


        public DataTable GetTitleById(string id) 
        {
            try
            {
                return dal.GetTitleById(id);
            }
            catch (Exception ex) 
            {
                throw new Exception("--article_categoryBll-->GetTitleById" + ex.Message, ex);
            }
        }


         /// <summary>
        /// 获取全部数据根据翻译类别，目的城市
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public DataTable GetAll(int channel_id, string strCatalog, string strLx, string strCs, string strYy = "")
        {
            try
            {
                return dal.GetAll(channel_id, strCatalog, strLx, strCs, strYy);
            }
            catch (Exception ex) 
            {
                throw new Exception("--article_categoryBll-->GetAll" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 接机送机
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog"></param>
        /// <param name="strCs">目的城市</param>
        /// <param name="strYy">国际机场</param>
        /// <returns></returns>
        public DataTable GetJJAll(int channel_id, string strCatalog, string strCs, string strYy = "")
        {
            try
            {
                return dal.GetJJAll(channel_id, strCatalog, strCs, strYy);
            }
            catch (Exception ex) 
            {
                throw new Exception("--article_categoryBll-->GetJJAll" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 住宿类型
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog"></param>
        /// <param name="strLx">住宿类别</param>
        /// <param name="strCs">目的城市</param>
        /// <returns></returns>
        public DataTable GetZsAll(int channel_id, string strCatalog, string strLx, string strCs)
        {
            try
            {
                return dal.GetZsAll(channel_id, strCatalog, strLx, strCs);
            }
            catch (Exception ex) 
            {
                throw new Exception("--article_categoryBll-->GetZsAll" + ex.Message, ex);
            }
        }
       
      

        

        /// <summary>
        /// 获取模糊查询 
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strId"></param>
        /// <returns></returns>
        public DataTable GetLike(int channel_id,string strCtaId, string strId) 
        {
            try
            {
                return dal.GetLike(channel_id, strCtaId, strId);
            }
            catch (Exception ex) 
            {
                throw new Exception("--article_categoryBll-->GetLike" + ex.Message, ex);
            }
        }
        
        /// <summary>
        /// 根据筛选条件和值进行筛选
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCtaId"></param>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
         public DataTable GetLikeByK(int channel_id,string strCtaId, string strKey, string strValue) 
        {
            try
            {
                return dal.GetLikeByK(channel_id, strCtaId, strKey, strValue);
            }
            catch (Exception ex) 
            {
                throw new Exception("--article_categoryBll-->GetLikeByK" + ex.Message, ex);
            }
        }

       


        public DataTable GetTopList(int channel_id,int top)
        {
            try
            {
                return dal.GetTopList(channel_id,top);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetTopList" + ex.Message, ex);
            }
        }
        public DataTable GetTop5List(int channel_id)
        {
            try
            {
                return dal.GetTop5List(channel_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetTop5List" + ex.Message, ex);
            }
        }
        public DataTable GetList(string strWhere)
        {
            try
            {
                return dal.GetList(strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetListByChannel(int channel_id)
        {
            try
            {
                return dal.GetListByChannel(channel_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->GetListByChannel" + ex.Message, ex);
            }
        }
        public DataTable GetTop50List(int channel_id)
        {
            try
            {
                return dal.GetTop50List(channel_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleBll-->GetTop50List" + ex.Message, ex);
            }
        }
        public bool Delete(int id)
        {
            try
            {
                //string content = dal.GetContent(id); //获取信息内容
                bool result = dal.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Update" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 返回信息标题
        /// </summary>
        public string GetTitle(int id)
        {
            try
            {
                return dal.GetTitle(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleBll-->GetTitle" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Update" + ex.Message, ex);
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
                throw new Exception("--article_categoryBll-->Update" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            try
            {
                return dal.GetCount(strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Update" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Update" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article model)
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
    }
}