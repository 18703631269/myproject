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
    public class replayDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public replayDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }
        public bool Delete(int id)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("delete from dt_reply ");
                builder.AppendFormat(" where id={0}", id);
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
                throw new Exception("--replayDal-->Add" + ex.Message, ex);
            }
        }
        public void UpdateField(int id, string strValue)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update dt_reply set " + strValue);
                builder.Append(" where id=" + id);
                _access.execCommand(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--replayDal-->Add" + ex.Message, ex);
            }
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("select * FROM dt_reply");
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
                throw new Exception("--replayDal-->Add" + ex.Message, ex);
            }
        }
        public bool Exists(int id)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select id from dt_reply");
                builder.AppendFormat(" where id={0}", id);
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
                throw new Exception("--replayDal-->Add" + ex.Message, ex);
            }
        }

        public reply GetModel(int id)
        {
            try
            {
                reply feedback = new reply();
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select top 1 id,title,content,user_name,user_tel,user_qq,user_email,add_time,reply_content,reply_time,is_lock");
                builder.AppendFormat(" from dt_reply ");
                builder.AppendFormat(" where id={0}", id);

                DataSet set = _access.getDataSet(builder.ToString());
                if (set.Tables[0].Rows.Count > 0)
                {
                    if ((set.Tables[0].Rows[0]["id"] != null) && (set.Tables[0].Rows[0]["id"].ToString() != ""))
                    {
                        feedback.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["title"] != null) && (set.Tables[0].Rows[0]["title"].ToString() != ""))
                    {
                        feedback.title = set.Tables[0].Rows[0]["title"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["content"] != null) && (set.Tables[0].Rows[0]["content"].ToString() != ""))
                    {
                        feedback.content = set.Tables[0].Rows[0]["content"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["user_name"] != null) && (set.Tables[0].Rows[0]["user_name"].ToString() != ""))
                    {
                        feedback.user_name = set.Tables[0].Rows[0]["user_name"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["user_tel"] != null) && (set.Tables[0].Rows[0]["user_tel"].ToString() != ""))
                    {
                        feedback.user_tel = set.Tables[0].Rows[0]["user_tel"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["user_qq"] != null) && (set.Tables[0].Rows[0]["user_qq"].ToString() != ""))
                    {
                        feedback.user_qq = set.Tables[0].Rows[0]["user_qq"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["user_email"] != null) && (set.Tables[0].Rows[0]["user_email"].ToString() != ""))
                    {
                        feedback.user_email = set.Tables[0].Rows[0]["user_email"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["add_time"] != null) && (set.Tables[0].Rows[0]["add_time"].ToString() != ""))
                    {
                        feedback.add_time = DateTime.Parse(set.Tables[0].Rows[0]["add_time"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["reply_content"] != null) && (set.Tables[0].Rows[0]["reply_content"].ToString() != ""))
                    {
                        feedback.reply_content = set.Tables[0].Rows[0]["reply_content"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["reply_time"] != null) && (set.Tables[0].Rows[0]["reply_time"].ToString() != ""))
                    {
                        feedback.reply_time = DateTime.Parse(set.Tables[0].Rows[0]["add_time"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["is_lock"] != null) && (set.Tables[0].Rows[0]["is_lock"].ToString() != ""))
                    {
                        feedback.is_lock = int.Parse(set.Tables[0].Rows[0]["is_lock"].ToString());
                    }
                    return feedback;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("--replayDal-->Add" + ex.Message, ex);
            }
        }

        public bool Update(reply model)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("update dt_reply set ");
                builder.AppendFormat("title='{0}',", model.title);
                builder.AppendFormat("content='{0}',", model.content);
                builder.AppendFormat("user_name='{0}',", model.user_name);
                builder.AppendFormat("user_tel='{0}',", model.user_tel);
                builder.AppendFormat("user_qq='{0}',", model.user_qq);
                builder.AppendFormat("user_email='{0}',", model.user_email);
                builder.AppendFormat("add_time='{0}',", model.add_time);
                builder.AppendFormat("reply_content='{0}',", model.reply_content);
                builder.AppendFormat("reply_time='{0}',", model.reply_time);
                builder.AppendFormat("is_lock={0}", model.is_lock);
                builder.AppendFormat(" where id={0}", model.id);
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
                throw new Exception("--replayDal-->Add" + ex.Message, ex);
            }
        }
    }
}
