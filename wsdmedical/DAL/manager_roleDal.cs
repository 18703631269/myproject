using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Common;
using DBAccess;

namespace DAL
{
    /// <summary>
    /// ���ݷ�����:�����ɫ--�Ѹ�Ϊsqlserver
    /// </summary>
    public partial class manager_roleDal
    {
        //���ݿ����ǰ׺
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public manager_roleDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region ��������============================
        /// <summary>
        /// �õ����ID
        /// </summary>
        private int GetMaxId()
        {
            try
            {
                string strSql = "select top 1 id from dt_manager_role order by id desc";
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(_dt.Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->GetMaxId" + ex.Message, ex);
            }
        }

        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) from dt_manager_role");
                strSql.AppendFormat(" where id={0}", id);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// ���ؽ�ɫ����
        /// </summary>
        public string GetTitle(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 role_name from dt_manager_role");
                strSql.Append(" where id=" + id);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                string title = Convert.ToString(_dt.Rows[0][0]);
                return title;
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->GetTitle" + ex.Message, ex);
            }
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.manager_role model)
        {

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into dt_manager_role(");
                strSql.Append("role_name,role_type,is_sys)");
                strSql.Append(" values (");
                strSql.AppendFormat("'{0}',{1},{2})", model.role_name, model.role_type, model.is_sys);
                _access.execCommand(strSql.ToString());
                //ȡ���²����ID
                model.id = GetMaxId();
                StringBuilder strSql2;
                if (model.manager_role_values != null)
                {
                    foreach (Model.manager_role_value modelt in model.manager_role_values)
                    {
                        strSql2 = new StringBuilder();
                        strSql2.Append("insert into dt_manager_role_value(");
                        strSql2.Append("role_id,nav_name,action_type)");
                        strSql2.Append(" values (");
                        strSql2.AppendFormat("{0},'{1}','{2}')", model.id, modelt.nav_name, modelt.action_type);
                        _access.execCommand(strSql2.ToString());
                    }
                }
                return model.id;
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->GetTitle" + ex.Message, ex);
            }

        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.manager_role model)
        {

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update dt_manager_role set ");
                strSql.AppendFormat("role_name='{0}',", model.role_name);
                strSql.AppendFormat("role_type='{0}',", model.role_type);
                strSql.AppendFormat("is_sys={0}", model.is_sys);
                strSql.AppendFormat(" where id={0}", model.id);
                _access.execCommand(strSql.ToString());
                //��ɾ���ý�ɫ����Ȩ��
                StringBuilder strSql2 = new StringBuilder();
                strSql2.AppendFormat("delete from dt_manager_role_value where role_id={0} ", model.id);
                _access.execCommand(strSql2.ToString());
                //���Ȩ��
                if (model.manager_role_values != null)
                {
                    StringBuilder strSql3;
                    foreach (Model.manager_role_value modelt in model.manager_role_values)
                    {
                        strSql3 = new StringBuilder();
                        strSql3.Append("insert into dt_manager_role_value(");
                        strSql3.Append("role_id,nav_name,action_type)");
                        strSql3.Append(" values (");
                        strSql3.AppendFormat("{0},'{1}','{2}')", model.id,modelt.nav_name, modelt.action_type);
                        _access.execCommand(strSql3.ToString());
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->Update" + ex.Message, ex);
            }

        }

        /// <summary>
        /// ɾ��һ�����ݣ����ӱ������������
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                //ɾ�������ɫȨ��
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from dt_manager_role_value ");
                strSql.AppendFormat(" where role_id={0};",id);
                int result = _access.execCommand(strSql.ToString());
                StringBuilder strSql2 = new StringBuilder();
                strSql2.AppendFormat("delete from dt_manager_role ");
                strSql2.AppendFormat(" where id={0};",id);

                int result2 = _access.execCommand(strSql2.ToString());
                if (result >= 0&&result2>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.manager_role GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 id,role_name,role_type,is_sys from dt_manager_role ");
                strSql.AppendFormat(" where id={0}",id);

                Model.manager_role model = new Model.manager_role();
                DataSet ds = _access.getDataSet(strSql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region ������Ϣ
                    if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                    {
                        model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                    }
                    model.role_name = ds.Tables[0].Rows[0]["role_name"].ToString();
                    if (ds.Tables[0].Rows[0]["role_type"].ToString() != "")
                    {
                        model.role_type = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
                    }
                    if (ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                    {
                        model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
                    }
                    #endregion

                    #region �ӱ���Ϣ
                    StringBuilder strSql2 = new StringBuilder();
                    strSql2.Append("select id,role_id,nav_name,action_type from dt_manager_role_value ");
                    strSql2.AppendFormat(" where role_id={0}",id);
                   
                    DataSet ds2 = _access.getDataSet(strSql2.ToString());
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        List<Model.manager_role_value> models = new List<Model.manager_role_value>();
                        Model.manager_role_value modelt;
                        foreach (DataRow dr in ds2.Tables[0].Rows)
                        {
                            modelt = new Model.manager_role_value();
                            if (dr["id"].ToString() != "")
                            {
                                modelt.id = int.Parse(dr["id"].ToString());
                            }
                            if (dr["role_id"].ToString() != "")
                            {
                                modelt.role_id = int.Parse(dr["role_id"].ToString());
                            }
                            modelt.nav_name = dr["nav_name"].ToString();
                            modelt.action_type = dr["action_type"].ToString();
                            models.Add(modelt);
                        }
                        model.manager_role_values = models;
                    }
                    #endregion

                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,role_name,role_type,is_sys ");
                strSql.Append(" FROM dt_manager_role ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return _access.getDataTable(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataTable GetAuditById(string nav_name,int role_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select id,role_id,nav_name,action_type FROM dt_manager_role_value where nav_name='{0}' and role_id={1} ",nav_name,role_id);
                return _access.getDataTable(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_roleDal-->GetAuditById" + ex.Message, ex);
            }
        }
        #endregion  Method
    }
}