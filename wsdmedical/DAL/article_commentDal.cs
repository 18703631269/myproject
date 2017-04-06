using Common;
using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class article_commentDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public article_commentDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region 基本方法===========================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId()
        {
            try
            {
                string strSql = "select top 1 id from dt_article_comment order by id desc";
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
                throw new Exception("--article_commentDal-->GetMaxId" + ex.Message, ex);
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
                strSql.Append("select id from dt_article_comment");
                strSql.AppendFormat(" where id={0} ", id);
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
                throw new Exception("--article_commentDal-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回数据总数(AJAX分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id ");
                strSql.Append(" from dt_article_comment ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
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
                throw new Exception("--article_commentDal-->GetCount" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_comment model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("insert into dt_article_comment(");
                strSql.AppendFormat("channel_id,article_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time)");
                strSql.AppendFormat(" values (");
                strSql.AppendFormat("{0},{1},{2},'{3}','{4}','{5}','{6}',{7},'{8}',{9},'{10}')", model.channel_id, model.article_id, model.user_id, model.user_name, model.user_ip, model.content, model.is_lock, model.add_time, model.is_reply, model.reply_content, model.reply_time);
                return _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentDal-->Add" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update dt_article_comment set " + strValue);
                strSql.Append(" where id=" + id);
                _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentDal-->UpdateField" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_comment model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("update dt_article_comment set ");
                strSql.AppendFormat("channel_id={0},",model.channel_id);
                strSql.AppendFormat("article_id={0},",model.article_id);
                strSql.AppendFormat("user_id={0},",model.user_id);
                strSql.AppendFormat("user_name='{0}',",model.user_name);
                strSql.AppendFormat("user_ip='{0}',",model.user_ip);
                strSql.AppendFormat("content='{0}',",model.content);
                strSql.AppendFormat("is_lock={0},",model.is_lock);
                strSql.AppendFormat("add_time='{0}',",model.add_time);
                strSql.AppendFormat("is_reply={0},",model.is_reply);
                strSql.AppendFormat("reply_content='{0}',",model.reply_content);
                strSql.AppendFormat("reply_time='{0}'",model.reply_time);
                strSql.AppendFormat(" where id={0}",model.id);
                int results = _access.execCommand(strSql.ToString());
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
                throw new Exception("--article_commentDal-->Update" + ex.Message, ex);
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
            strSql.AppendFormat("delete from dt_article_comment ");
            strSql.AppendFormat(" where id={0}",id);

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
                throw new Exception("--article_commentDal-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_comment GetModel(int id)
        {
            try
            { 
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select  top 1 id,channel_id,article_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time");
            strSql.AppendFormat(" from dt_article_comment ");
            strSql.AppendFormat(" where id={0}",id);
            return _access.getSinggleObj<Model.article_comment>(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentDal-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,channel_id,article_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time ");
            strSql.Append(" FROM dt_article_comment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return _access.getDataTable(strSql.ToString());
             }
            catch (Exception ex)
            {
                throw new Exception("--article_commentDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataTable GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_article_comment");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable _dt = _access.getDataTable(strSql.ToString());
            recordCount = _dt.Rows.Count;
            DataTable _dts= _access.getDataTable(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            return _dts;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_commentDal-->GetList" + ex.Message, ex);
            }
        }

        #endregion
    }
}
