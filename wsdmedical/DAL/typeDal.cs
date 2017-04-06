using Common;
using DBAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class typeDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public typeDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        public int Add(type model)
        {
            int maxId;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("insert into dt_type(");
                builder.AppendFormat("tname,town,sort_id,add_user,is_lock,add_time,ctype)");
                builder.AppendFormat(" values (");
                builder.AppendFormat("'{0}','{1}',{2},'{3}',{4},'{5}','{6}')",
                                     model.tname,
                                     model.town,
                                     model.sort_id,
                                     model.add_user,
                                     model.is_lock,
                                     model.add_time,
                                     model.ctype);
                int results = _access.execCommand(builder.ToString());
                DataTable _dt = _access.getDataTable("SELECT max(tid) from dt_type;");
                if (results > 0)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        maxId = Convert.ToInt32(_dt.Rows[0][0]);
                    }
                    else
                    {
                        maxId = 0;
                    }
                }
                else
                {
                    maxId = 0;
                }
                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception("--typeDal-->Add" + ex.Message, ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from dt_type ");
            builder.AppendFormat(" where tid={0}", id);
            int results = _access.execCommand(builder.ToString());
            if (results > 0)
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
                throw new Exception("--typeDal-->Add" + ex.Message, ex);
            }
        }

        public bool Exists(int id)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select tid from dt_type");
            builder.AppendFormat(" where tid={0} ", id);
            DataTable _dt = _access.getDataTable(builder.ToString());
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
                throw new Exception("--typeDal-->Exists" + ex.Message, ex);
            }
        }

     

        public DataSet GetList(string strWhere)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select tid,tname,town,sort_id,is_lock,add_time,ctype ");
            builder.Append(" FROM dt_type ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return _access.getDataSet(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--typeDal-->GetList" + ex.Message, ex);
            }
        }

        public DataSet GetList(int Top, string strWhere)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" tid,tname,town,sort_id,is_lock,add_time,ctype ");
            builder.Append(" FROM dt_type ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by sort_id asc,add_time desc");
            return _access.getDataSet(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--typeDal-->GetList" + ex.Message, ex);
            }
        }


        public DataSet GetByGroup(string strWhere)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                if (strWhere.Trim() != "")
                {
                    builder.Append("select ");
                    builder.AppendFormat("{0} ", strWhere);
                    builder.AppendFormat(" FROM dt_type where {0} is not null and town='city' group by {1}", strWhere, strWhere);
                    return _access.getDataSet(builder.ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--typeDal-->GetList" + ex.Message, ex);
            }
        }
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM dt_type");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            DataTable _dt = _access.getDataTable(builder.ToString());
            recordCount = _dt.Rows.Count;

            return _access.getDataSet(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, builder.ToString(), filedOrder));
            }
            catch (Exception ex)
            {
                throw new Exception("--typeDal-->getList" + ex.Message, ex);
            }
        }


        public type GetModel(int id)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 tid,tname,town,sort_id,is_lock,add_time,add_user,ctype from dt_type ");
            builder.AppendFormat(" where tid={0}", id);
            type t = new type();
            DataSet set = _access.getDataSet(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                if ((set.Tables[0].Rows[0]["tid"] != null) && (set.Tables[0].Rows[0]["tid"].ToString() != ""))
                {
                    t.tid = int.Parse(set.Tables[0].Rows[0]["tid"].ToString());
                }
                if ((set.Tables[0].Rows[0]["tname"] != null) && (set.Tables[0].Rows[0]["tname"].ToString() != ""))
                {
                    t.tname = set.Tables[0].Rows[0]["tname"].ToString();
                }
                if ((set.Tables[0].Rows[0]["town"] != null) && (set.Tables[0].Rows[0]["town"].ToString() != ""))
                {
                    t.town = set.Tables[0].Rows[0]["town"].ToString();
                }
              
                if ((set.Tables[0].Rows[0]["sort_id"] != null) && (set.Tables[0].Rows[0]["sort_id"].ToString() != ""))
                {
                    t.sort_id = int.Parse(set.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["add_user"] != null) && (set.Tables[0].Rows[0]["add_user"].ToString() != ""))
                {
                    t.add_user = int.Parse(set.Tables[0].Rows[0]["add_user"].ToString());
                }
                if ((set.Tables[0].Rows[0]["is_lock"] != null) && (set.Tables[0].Rows[0]["is_lock"].ToString() != ""))
                {
                    t.is_lock = int.Parse(set.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if ((set.Tables[0].Rows[0]["add_time"] != null) && (set.Tables[0].Rows[0]["add_time"].ToString() != ""))
                {
                    t.add_time = DateTime.Parse(set.Tables[0].Rows[0]["add_time"].ToString());
                }
                if ((set.Tables[0].Rows[0]["ctype"] != null) && (set.Tables[0].Rows[0]["ctype"].ToString() != ""))
                {
                    t.ctype = set.Tables[0].Rows[0]["ctype"].ToString();
                }
                return t;
            }
            return null;
            }
            catch (Exception ex)
            {
                throw new Exception("--linkDal-->Add" + ex.Message, ex);
            }
        }



        public bool Update(type model)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update dt_type set ");
            builder.AppendFormat("tname='{0}',", model.tname);
            builder.AppendFormat("town='{0}',", model.town);
            builder.AppendFormat("sort_id={0},", model.sort_id);
            builder.AppendFormat("add_user={0},", model.add_user);
            builder.AppendFormat("is_lock={0},", model.is_lock);
            builder.AppendFormat("add_time='{0}',", model.add_time);
            builder.AppendFormat("ctype='{0}'", model.ctype);
            builder.AppendFormat(" where tid={0}", model.tid);
            int results = _access.execCommand(builder.ToString());
            if (results > 0)
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
                throw new Exception("--linkDal-->Update" + ex.Message, ex);
            }
        }

        public void UpdateField(int id, string strValue)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update dt_type set " + strValue);
            builder.AppendFormat(" where tid=" + id);
            _access.execCommand(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--linkDal-->UpdateField" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetName(string id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(id))
                {
                    strSql.Append("select top 1 tname from  dt_type");
                    strSql.Append(" where tid=" + id);
                    DataTable dtobj = _access.getDataTable(strSql.ToString());
                    if (dtobj.Rows.Count > 0)
                    {
                        return Convert.ToString(dtobj.Rows[0][0]);
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("--linkDal-->GetName" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCityName(string id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(id))
                {
                    strSql.Append("select (ctype+'　'+tname) as t from dt_type");
                    strSql.Append(" where tid=" + id);
                    DataTable dtobj = _access.getDataTable(strSql.ToString());
                    if (dtobj.Rows.Count > 0)
                    {
                        return Convert.ToString(dtobj.Rows[0][0]);
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("--linkDal-->GetCityName" + ex.Message, ex);
            }
        }

        

    }
}
