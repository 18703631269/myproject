using System;
using System.Data;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// ϵͳ�����˵�
    /// </summary>
    public partial class navigationBll
    {
        //private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ


        private readonly DAL.navigationDal dal;
        public navigationBll()
        {
            dal = new DAL.navigationDal();
        }

        #region ��������===============================
        /// <summary>
        /// �Ƿ���ڸü�¼
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
        /// ����һ������
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
        /// ����һ������
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
        /// ɾ��һ������
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
        /// �õ�һ������ʵ��
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
        /// ȡ����������б�
        /// </summary>
        /// <param name="parent_id">��ID</param>
        /// <param name="nav_type">�������</param>
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

        #region ��չ����===============================
        /// <summary>
        /// �޸�һ������
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

