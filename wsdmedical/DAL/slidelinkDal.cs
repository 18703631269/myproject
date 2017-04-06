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
    public class slidelinkDal
    {
          private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public slidelinkDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        public int Add(slidelink model)
        {
            int maxId;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("insert into dt_slidelink(");
                builder.AppendFormat("title,link_url,img_url,sort_id,add_user,is_lock,add_time)");
                builder.AppendFormat(" values (");
                builder.AppendFormat("'{0}','{1}','{2}','{3}',{4},{5},'{6}')",
                                     model.title,
                                     model.link_url,
                                     model.img_url,
                                     model.sort_id,
                                     model.add_user,
                                     model.is_lock,
                                     model.add_time);
                int results = _access.execCommand(builder.ToString());
                DataTable _dt = _access.getDataTable("SELECT max(id) from dt_slidelink;");
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
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from dt_slidelink ");
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
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }

        public bool Exists(int id)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select id from dt_slidelink");
            builder.AppendFormat(" where id={0} ", id);
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
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }

        public DataTable GetList(string strWhere)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,title,link_url,img_url,sort_id,is_lock,add_time ");
            builder.Append(" FROM dt_slidelink ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return _access.getDataTable(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
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
            builder.Append(" id,title,link_url,img_url,sort_id,is_lock,add_time ");
            builder.Append(" FROM dt_slidelink ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by sort_id asc,add_time desc");
            return _access.getDataSet(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }

        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM dt_slidelink");
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
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }


        public slidelink GetModel(int id)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.Append("select top 1 id,title,link_url,img_url,sort_id,is_lock,add_time,add_user from dt_slidelink ");
            builder.AppendFormat(" where id={0}", id);
            slidelink link = new slidelink();
            DataSet set = _access.getDataSet(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                if ((set.Tables[0].Rows[0]["id"] != null) && (set.Tables[0].Rows[0]["id"].ToString() != ""))
                {
                    link.id = int.Parse(set.Tables[0].Rows[0]["id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["title"] != null) && (set.Tables[0].Rows[0]["title"].ToString() != ""))
                {
                    link.title = set.Tables[0].Rows[0]["title"].ToString();
                }
                if ((set.Tables[0].Rows[0]["link_url"] != null) && (set.Tables[0].Rows[0]["link_url"].ToString() != ""))
                {
                    link.link_url = set.Tables[0].Rows[0]["link_url"].ToString();
                }
                if ((set.Tables[0].Rows[0]["img_url"] != null) && (set.Tables[0].Rows[0]["img_url"].ToString() != ""))
                {
                    link.img_url = set.Tables[0].Rows[0]["img_url"].ToString();
                }
                if ((set.Tables[0].Rows[0]["sort_id"] != null) && (set.Tables[0].Rows[0]["sort_id"].ToString() != ""))
                {
                    link.sort_id = int.Parse(set.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if ((set.Tables[0].Rows[0]["add_user"] != null) && (set.Tables[0].Rows[0]["add_user"].ToString() != ""))
                {
                    link.add_user = int.Parse(set.Tables[0].Rows[0]["add_user"].ToString());
                }
                if ((set.Tables[0].Rows[0]["is_lock"] != null) && (set.Tables[0].Rows[0]["is_lock"].ToString() != ""))
                {
                    link.is_lock = int.Parse(set.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if ((set.Tables[0].Rows[0]["add_time"] != null) && (set.Tables[0].Rows[0]["add_time"].ToString() != ""))
                {
                    link.add_time = DateTime.Parse(set.Tables[0].Rows[0]["add_time"].ToString());
                }
                return link;
            }
            return null;
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }



        public bool Update(slidelink model)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update dt_slidelink set ");
            builder.AppendFormat("title='{0}',", model.title);
            builder.AppendFormat("link_url='{0}',", model.link_url);
            builder.AppendFormat("img_url='{0}',", model.img_url);
            builder.AppendFormat("sort_id={0},", model.sort_id);
            builder.AppendFormat("add_user={0},", model.add_user);
            builder.AppendFormat("is_lock={0},", model.is_lock);
            builder.AppendFormat("add_time='{0}'", model.add_time);
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
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }

        public void UpdateField(int id, string strValue)
        {
            try
            { 
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("update dt_slidelink set " + strValue);
            builder.AppendFormat(" where id=" + id);
            _access.execCommand(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkDal-->Add" + ex.Message, ex);
            }
        }
    }
}
