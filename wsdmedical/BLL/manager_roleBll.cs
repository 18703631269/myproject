using System;
using System.Data;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// �����ɫ
    /// </summary>
    public partial class manager_roleBll
    {
        // private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ

        private readonly DAL.manager_roleDal dal;
        public manager_roleBll()
        {
            dal = new DAL.manager_roleDal();
        }

        #region ��������==============================
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
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// ����Ƿ���Ȩ��
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
        /// ���ؽ�ɫ����
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
        /// ����һ������
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
        /// ����һ������
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
                throw new Exception("--manager_roleBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
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
        /// ��������б�
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