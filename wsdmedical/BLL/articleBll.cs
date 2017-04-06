using System;
using System.Data;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    /// <summary>
    /// ��������
    /// </summary>
    public partial class articleBll
    {
        private readonly articleDal dal;

        public articleBll()
        {
            dal = new DAL.articleDal();
        }
        /// <summary>
        /// ��ò�ѯ��ҳ����
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
        /// ��ȡȫ������
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
        /// ��ȡȫ�����ݸ��ݷ������Ŀ�ĳ���
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
        /// �ӻ��ͻ�
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog"></param>
        /// <param name="strCs">Ŀ�ĳ���</param>
        /// <param name="strYy">���ʻ���</param>
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
        /// ס������
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog"></param>
        /// <param name="strLx">ס�����</param>
        /// <param name="strCs">Ŀ�ĳ���</param>
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
        /// ��ȡģ����ѯ 
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
        /// ����ɸѡ������ֵ����ɸѡ
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
                //string content = dal.GetContent(id); //��ȡ��Ϣ����
                bool result = dal.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryBll-->Update" + ex.Message, ex);
            }
        }
        /// <summary>
        /// ������Ϣ����
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
        /// �õ�һ������ʵ��
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
        /// �޸�һ������
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
        /// ������������
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
        /// ����һ������
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
        /// ����һ������
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