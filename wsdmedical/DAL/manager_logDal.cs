using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Common;
using DBAccess;

namespace DAL
{
    /// <summary>
    /// 数据访问类:管理员日志--已改为sqlserver
    /// </summary>
    public partial class manager_logDal
    {
        //数据库表名前缀
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public manager_logDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region 基本方法==============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId()
        {
            try
            {
                string strSql = "select top 1 id from dt_manager_log order by id desc";
                object obj = _access.getSinggleObj<int>(strSql.ToString());
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return int.Parse(obj.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->GetMaxId" + ex.Message, ex);
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
                strSql.Append("select id from dt_manager_log");
                strSql.AppendFormat(" where id={0} ",id);
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
                throw new Exception("--manager_logDal-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id as H from dt_manager_log ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt.Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->GetCount" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager_log model)
        {

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into dt_manager_log(");
                strSql.Append("user_id,user_name,action_type,[remark],user_ip,add_time)");
                strSql.Append(" values (");
                strSql.AppendFormat("{0},'{1}','{2}','{3}','{4}','{5}')", model.user_id, model.user_name, model.action_type, model.remark, model.user_ip, model.add_time);
                return _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->Add" + ex.Message, ex);
            }


        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager_log GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 id,user_id,user_name,action_type,[remark],user_ip,add_time");
                strSql.Append(" from dt_manager_log ");
                strSql.AppendFormat(" where id={0}",id);
                Model.manager_log model = new Model.manager_log();
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    return DataRowToModel(_dt.Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager_log DataRowToModel(DataRow row)
        {
            try
            {
                Model.manager_log model = new Model.manager_log();
                if (row != null)
                {
                    if (row["id"] != null && row["id"].ToString() != "")
                    {
                        model.id = int.Parse(row["id"].ToString());
                    }
                    if (row["user_id"] != null && row["user_id"].ToString() != "")
                    {
                        model.user_id = int.Parse(row["user_id"].ToString());
                    }
                    if (row["user_name"] != null)
                    {
                        model.user_name = row["user_name"].ToString();
                    }
                    if (row["action_type"] != null)
                    {
                        model.action_type = row["action_type"].ToString();
                    }
                    if (row["remark"] != null)
                    {
                        model.remark = row["remark"].ToString();
                    }
                    if (row["user_ip"] != null)
                    {
                        model.user_ip = row["user_ip"].ToString();
                    }
                    if (row["add_time"] != null && row["add_time"].ToString() != "")
                    {
                        model.add_time = DateTime.Parse(row["add_time"].ToString());
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->DataRowToModel" + ex.Message, ex);
            }
        }

        ///// <summary>
        ///// 删除7天前的日志数据
        ///// </summary>
        public int Delete(int dayCount)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from dt_manager_log ");
                strSql.Append(" where datediff(day,add_time,GETDATE()) > " + dayCount);

                return _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->Delete" + ex.Message, ex);
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
                strSql.Append(" id,user_id,user_name,action_type,[remark],user_ip,add_time ");
                strSql.Append(" FROM dt_manager_log ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder);
                return _access.getDataSet(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                recordCount = 0;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * FROM dt_manager_log");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                DataTable _dt = _access.getDataTable(PagingHelper.CreateCountingSql(strSql.ToString()));
                if (_dt.Rows.Count > 0)
                { 
                 recordCount=Convert.ToInt32(_dt.Rows[0][0]);
                }
                return _access.getDataSet(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logDal-->GetList" + ex.Message, ex);
            }
        }

        #endregion
    }

}
