using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Common;
using DBAccess;

namespace DAL
{
    /// <summary>
    /// 数据访问类:管理员--已更改为sqlser
    /// </summary>
    public partial class managerDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public managerDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region 基本方法=============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId()
        {
            try
            {
                string strSql = "select top 1 id from dt_manager order by id desc";
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
                throw new Exception("--managerDal-->GetMaxId" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select id from dt_manager where id={0};", id);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
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
                throw new Exception("--managerDal-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select id from dt_manager  where user_name='{0}';", user_name);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
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
                throw new Exception("--managerDal-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select top 1 salt from dt_manager  where user_name='{0}';", user_name);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    return _dt.Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--managerDal-->GetSalt" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager model)
        {
            try
            {
                int newId = 0;
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("insert into dt_manager(role_id,role_type,user_name,[password],salt,real_name,telephone,email,is_lock,add_time) values (");
                strSql.AppendFormat("{0},{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}')",model.role_id,model.role_type,model.user_name,model.password,model.salt,model.real_name,model.telephone,model.email,model.is_lock,model.add_time);
                _access.execCommand(strSql.ToString());
                //取得新插入的ID
                newId = GetMaxId();
                return newId;
            }
            catch(Exception ex)
            {
                throw new Exception("--managerDal-->Add" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update dt_manager set ");
            strSql.AppendFormat("role_id={0},",model.role_id);
            strSql.AppendFormat("role_type={0},",model.role_type);
            strSql.AppendFormat("user_name='{0}',",model.user_name);
            strSql.AppendFormat("[password]='{0}',",model.password);
            strSql.AppendFormat("real_name='{0}',",model.real_name);
            strSql.AppendFormat("telephone='{0}',",model.telephone);
            strSql.AppendFormat("email='{0}',",model.email);
            strSql.AppendFormat("is_lock={0},",model.is_lock);
            strSql.AppendFormat("add_time='{0}'",model.add_time);
            strSql.AppendFormat(" where id={0};",model.id);

            int rows = _access.execCommand(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("delete from dt_manager where id={0};", id);
                int rows = _access.execCommand(strSql.ToString());
                if (rows > 0)
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
                throw new Exception("--managerDal-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select  top 1 id,role_id,role_type,user_name,[password],salt,real_name,telephone,email,is_lock,add_time from dt_manager where id={0}", id);

                Model.manager model = new Model.manager();
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0]["id"].ToString() != "")
                    {
                        model.id = int.Parse(_dt.Rows[0]["id"].ToString());
                    }
                    if (_dt.Rows[0]["role_id"].ToString() != "")
                    {
                        model.role_id = int.Parse(_dt.Rows[0]["role_id"].ToString());
                    }
                    if (_dt.Rows[0]["role_type"].ToString() != "")
                    {
                        model.role_type = int.Parse(_dt.Rows[0]["role_type"].ToString());
                    }
                    model.user_name = _dt.Rows[0]["user_name"].ToString();
                    model.password = _dt.Rows[0]["password"].ToString();
                    model.salt = _dt.Rows[0]["salt"].ToString();
                    model.real_name = _dt.Rows[0]["real_name"].ToString();
                    model.telephone = _dt.Rows[0]["telephone"].ToString();
                    model.email = _dt.Rows[0]["email"].ToString();
                    if (_dt.Rows[0]["is_lock"].ToString() != "")
                    {
                        model.is_lock = int.Parse(_dt.Rows[0]["is_lock"].ToString());
                    }
                    if (_dt.Rows[0]["add_time"].ToString() != "")
                    {
                        model.add_time = DateTime.Parse(_dt.Rows[0]["add_time"].ToString());
                    }
                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--managerDal-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select id from dt_manager  where user_name='{0}' and [password]='{1}' and is_lock=1", user_name, password);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    return GetModel(Convert.ToInt32(_dt.Rows[0][0]));
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("--managerDal-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select ");
                if (Top > 0)
                {
                    strSql.Append(" top " + Top.ToString());
                }
                strSql.Append(" id,role_id,role_type,user_name,[password],salt,real_name,telephone,email,is_lock,add_time ");
                strSql.Append(" FROM dt_manager ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder);
                return _access.getDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--managerDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * FROM dt_manager");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                DataTable _dt = _access.getDataTable(strSql.ToString());
                recordCount = _dt.Rows.Count;
                return _access.getDataSet(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            }
            catch (Exception ex)
            {
                throw new Exception("--managerDal-->GetList" + ex.Message, ex);
            }
        }


        public DataTable GetGroup(int group_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * FROM dt_manager where role_id=2;");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--managerDal-->GetGroup" + ex.Message, ex);
            }
        }
        #endregion
    }
}