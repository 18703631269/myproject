using Common;
using DBAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class t_userDal
    {
        private IDBAccess _access;

        string connectionstring = Common.CommomFunction.SqlServerString();
        public t_userDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }
        public bool Delete(string id)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("delete from T_user ");
                builder.AppendFormat(" where uids='{0}'", id);
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
                throw new Exception("--t_userDal-->Add" + ex.Message, ex);
            }
        }
        public void UpdateField(string id, string strValue)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update T_user set " + strValue);
                builder.Append(" where uids='" + id + "'");
                _access.execCommand(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userDal-->UpdateField" + ex.Message, ex);
            }
        }

        public bool UpdateFieldByUserId(string usersid, string strValue)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update T_user set " + strValue);
                builder.Append(" where ulog='" + usersid + "'");
                int result= _access.execCommand(builder.ToString());
                if (result > 0)
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
                throw new Exception("--t_userDal-->UpdateFieldByUserId" + ex.Message, ex);
            }
        }
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("select * FROM T_user");
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
                throw new Exception("--t_userDal-->GetList" + ex.Message, ex);
            }
        }
        public bool Exists(string id)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select uids from T_user");
                builder.AppendFormat(" where uids='{0}'", id);
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
                throw new Exception("--t_userDal-->Exists" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 根据字段判断是否存在
        /// </summary>
        /// <param name="strFiled"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(string strFiled, string id)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select uids from T_user");
                builder.AppendFormat(" where {0}='{1}'", strFiled, id);
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
                throw new Exception("--t_userDal-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据登录
        /// </summary>
        /// <param name="strFiled"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable IsUser(string ulog, string upwd)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select * from T_user");
                builder.AppendFormat(" where ulog=@ulog and upwd=@upwd and ulock=@ulock", ulog, upwd);
                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@ulog", ulog), new SqlParameter("@upwd", upwd), new SqlParameter("@ulock", "0") };
                DataTable _dt = ExecuteDataTable(builder.ToString(), paras);
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userDal-->IsUser" + ex.Message, ex);
            }
        }

        public t_user GetModelByUserId(string userid)
        {
            try
            {
                t_user feedback = new t_user();
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select top 1 [uids],[urole],[uname],[ulog],[upwd],[utel],[ulxfs],[usex],[udate],[ual],[ubl],[ulock],uemail,uyzm,ustate");
                builder.AppendFormat(" from T_user ");
                builder.AppendFormat(" where ulog='{0}'", userid);

                DataSet set = _access.getDataSet(builder.ToString());
                if (set.Tables[0].Rows.Count > 0)
                {
                    if ((set.Tables[0].Rows[0]["uids"] != null) && (set.Tables[0].Rows[0]["uids"].ToString() != ""))
                    {
                        feedback.uids = new Guid(set.Tables[0].Rows[0]["uids"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["urole"] != null) && (set.Tables[0].Rows[0]["urole"].ToString() != ""))
                    {
                        feedback.urole = int.Parse(set.Tables[0].Rows[0]["urole"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["uname"] != null) && (set.Tables[0].Rows[0]["uname"].ToString() != ""))
                    {
                        feedback.uname = set.Tables[0].Rows[0]["uname"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ulog"] != null) && (set.Tables[0].Rows[0]["ulog"].ToString() != ""))
                    {
                        feedback.ulog = set.Tables[0].Rows[0]["ulog"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["upwd"] != null) && (set.Tables[0].Rows[0]["upwd"].ToString() != ""))
                    {
                        feedback.upwd = set.Tables[0].Rows[0]["upwd"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["utel"] != null) && (set.Tables[0].Rows[0]["utel"].ToString() != ""))
                    {
                        feedback.utel = set.Tables[0].Rows[0]["utel"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ulxfs"] != null) && (set.Tables[0].Rows[0]["ulxfs"].ToString() != ""))
                    {
                        feedback.ulxfs = set.Tables[0].Rows[0]["ulxfs"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["usex"] != null) && (set.Tables[0].Rows[0]["usex"].ToString() != ""))
                    {
                        feedback.usex = set.Tables[0].Rows[0]["usex"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["udate"] != null) && (set.Tables[0].Rows[0]["udate"].ToString() != ""))
                    {
                        feedback.udate = DateTime.Parse(set.Tables[0].Rows[0]["udate"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["ual"] != null) && (set.Tables[0].Rows[0]["ual"].ToString() != ""))
                    {
                        feedback.ual = set.Tables[0].Rows[0]["ual"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ubl"] != null) && (set.Tables[0].Rows[0]["ubl"].ToString() != ""))
                    {
                        feedback.ubl = set.Tables[0].Rows[0]["ubl"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ulock"] != null) && (set.Tables[0].Rows[0]["ulock"].ToString() != ""))
                    {
                        feedback.ulock = set.Tables[0].Rows[0]["ulock"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["uemail"] != null) && (set.Tables[0].Rows[0]["uemail"].ToString() != ""))
                    {
                        feedback.uemail = set.Tables[0].Rows[0]["uemail"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["uyzm"] != null) && (set.Tables[0].Rows[0]["uyzm"].ToString() != ""))
                    {
                        feedback.uyzm = set.Tables[0].Rows[0]["uyzm"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ustate"] != null) && (set.Tables[0].Rows[0]["ustate"].ToString() != ""))
                    {
                        feedback.ustate = int.Parse(set.Tables[0].Rows[0]["ustate"].ToString());
                    }
                    return feedback;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userDal-->GetModel" + ex.Message, ex);
            }
        }

        public t_user GetModel(string id)
        {
            try
            {
                t_user feedback = new t_user();
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select top 1 [uids],[urole],[uname],[ulog],[upwd],[utel],[ulxfs],[usex],[udate],[ual],[ubl],[ulock],uemail,uyzm,ustate");
                builder.AppendFormat(" from T_user ");
                builder.AppendFormat(" where uids='{0}'", id);

                DataSet set = _access.getDataSet(builder.ToString());
                if (set.Tables[0].Rows.Count > 0)
                {
                    if ((set.Tables[0].Rows[0]["uids"] != null) && (set.Tables[0].Rows[0]["uids"].ToString() != ""))
                    {
                        feedback.uids = new Guid(set.Tables[0].Rows[0]["uids"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["urole"] != null) && (set.Tables[0].Rows[0]["urole"].ToString() != ""))
                    {
                        feedback.urole = int.Parse(set.Tables[0].Rows[0]["urole"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["uname"] != null) && (set.Tables[0].Rows[0]["uname"].ToString() != ""))
                    {
                        feedback.uname = set.Tables[0].Rows[0]["uname"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ulog"] != null) && (set.Tables[0].Rows[0]["ulog"].ToString() != ""))
                    {
                        feedback.ulog = set.Tables[0].Rows[0]["ulog"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["upwd"] != null) && (set.Tables[0].Rows[0]["upwd"].ToString() != ""))
                    {
                        feedback.upwd = set.Tables[0].Rows[0]["upwd"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["utel"] != null) && (set.Tables[0].Rows[0]["utel"].ToString() != ""))
                    {
                        feedback.utel = set.Tables[0].Rows[0]["utel"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ulxfs"] != null) && (set.Tables[0].Rows[0]["ulxfs"].ToString() != ""))
                    {
                        feedback.ulxfs = set.Tables[0].Rows[0]["ulxfs"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["usex"] != null) && (set.Tables[0].Rows[0]["usex"].ToString() != ""))
                    {
                        feedback.usex = set.Tables[0].Rows[0]["usex"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["udate"] != null) && (set.Tables[0].Rows[0]["udate"].ToString() != ""))
                    {
                        feedback.udate = DateTime.Parse(set.Tables[0].Rows[0]["udate"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["ual"] != null) && (set.Tables[0].Rows[0]["ual"].ToString() != ""))
                    {
                        feedback.ual = set.Tables[0].Rows[0]["ual"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ubl"] != null) && (set.Tables[0].Rows[0]["ubl"].ToString() != ""))
                    {
                        feedback.ubl = set.Tables[0].Rows[0]["ubl"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ulock"] != null) && (set.Tables[0].Rows[0]["ulock"].ToString() != ""))
                    {
                        feedback.ulock = set.Tables[0].Rows[0]["ulock"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["uemail"] != null) && (set.Tables[0].Rows[0]["uemail"].ToString() != ""))
                    {
                        feedback.uemail = set.Tables[0].Rows[0]["uemail"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["uyzm"] != null) && (set.Tables[0].Rows[0]["uyzm"].ToString() != ""))
                    {
                        feedback.uyzm = set.Tables[0].Rows[0]["uyzm"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ustate"] != null) && (set.Tables[0].Rows[0]["ustate"].ToString() != ""))
                    {
                        feedback.ustate = int.Parse(set.Tables[0].Rows[0]["ustate"].ToString());
                    }
                    return feedback;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userDal-->GetModel" + ex.Message, ex);
            }
        }

        public bool Update(t_user model)
        {
            try
            {

                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("update T_user set ");
                builder.AppendFormat("urole=@urole,");
                builder.AppendFormat("uname=@uname,");
                builder.AppendFormat("ulog=@ulog,");
                builder.AppendFormat("upwd=@upwd,");
                builder.AppendFormat("utel=@utel,");
                builder.AppendFormat("ulxfs=@ulxfs,");
                builder.AppendFormat("usex=@usex,");
                builder.AppendFormat("ual=@ual,");
                builder.AppendFormat("ubl=@ubl,");
                builder.AppendFormat("ulock=@ulock");
                builder.AppendFormat(" where uids=@uids");
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@urole",model.urole),
                new SqlParameter("@uname",model.uname),
                new SqlParameter("@ulog",model.ulog),
                new SqlParameter("@upwd",model.upwd),
                new SqlParameter("@utel",model.utel),
                new SqlParameter("@ulxfs",model.ulxfs),
                new SqlParameter("@usex",model.usex),
                new SqlParameter("@ual", model.ual),
                new SqlParameter("@ubl", model.ubl),
                new SqlParameter("@ulock", model.ulock),
                new SqlParameter("@uids",model.uids)
                };
                bool results = ExecuteNonQuey(builder.ToString(), paras);

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userDal-->Update" + ex.Message, ex);
            }
        }

        public bool Add(t_user model)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("insert into T_user([uids],[urole] ,[uname],[ulog],[upwd],[utel],[ulxfs] ,[usex],[udate],[ual],[ubl],[ulock],[uemail])");
                builder.Append("values(@uids,@urole,@uname,@ulog,@upwd,@utel,@ulxfs,@usex,@udate,@ual,@ubl,@ulock,@uemail);");
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@uids",model.uids),
                new SqlParameter("@urole",model.urole),
                new SqlParameter("@uname",model.uname),
                new SqlParameter("@ulog",model.ulog),
                new SqlParameter("@upwd",model.upwd),
                new SqlParameter("@utel",model.utel),
                new SqlParameter("@ulxfs",model.ulxfs),
                new SqlParameter("@usex",model.usex),
                new SqlParameter("@udate", model.udate),
                new SqlParameter("@ual", model.ual),
                new SqlParameter("@ubl", model.ubl),
                new SqlParameter("@ulock", model.ulock),
                    new SqlParameter("@uemail", model.uemail)
                };
                bool results = ExecuteNonQuey(builder.ToString(), paras);
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userDal-->Add" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 用于执行添加和修改等方法
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private bool ExecuteNonQuey(string strSql, params SqlParameter[] paras)
        {
            bool falg = false;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.Text;
                    if (paras != null)
                    {
                        foreach (SqlParameter pas in paras)
                        {
                            cmd.Parameters.Add(pas);
                        }
                    }
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        cmd.Parameters.Clear();
                        falg = true;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("--t_userDal-->ExecuteNonQuey" + ex.Message, ex);
                }
            }
            return falg;
        }
        /// <summary>
        /// 用于查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private DataTable ExecuteDataTable(string strSql, params SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.Text;
                    if (paras != null)
                    {
                        foreach (SqlParameter pas in paras)
                        {
                            cmd.Parameters.Add(pas);
                        }
                    }
                    SqlDataAdapter sdata = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sdata.Fill(ds);
                    dt = ds.Tables[0];
                }
                catch (SqlException ex)
                {
                    throw new Exception("--t_userDal-->ExecuteDataTable" + ex.Message, ex);
                }
            }
            return dt;
        }

    }
}