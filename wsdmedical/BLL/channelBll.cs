using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// ϵͳƵ����
    /// </summary>
    public partial class channelBll
    {
        private readonly DAL.channelDal dal;

        public channelBll()
        {
            dal = new DAL.channelDal();
        }

        /// <summary>
        /// ����Ƶ������
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
        /// ��ò�ѯ��ҳ����
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
        /// �õ�һ������ʵ��
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
                throw new Exception("--channelBll-->Delete" + ex.Message, ex);
            }
        }
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
                throw new Exception("--channelBll-->UpdateField" + ex.Message, ex);
            }
        }
        /// <summary>
        /// ����һ������
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
        /// ��ѯ�����Ƿ����
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