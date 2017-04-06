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
    public class article_albumsDal
    {
         private IDBAccess _access;
         //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
         string connectionstring = Common.CommomFunction.SqlServerString();
        public article_albumsDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_albums> GetList(int article_id)
        {
            try
            {
            List<Model.article_albums> modelList = new List<Model.article_albums>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,thumb_path,original_path,remark,add_time ");
            strSql.Append(" FROM dt_article_albums ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = _access.getDataTable(strSql.ToString());

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.article_albums model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.article_albums();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["thumb_path"] != null && dt.Rows[n]["thumb_path"].ToString() != "")
                    {
                        model.thumb_path = dt.Rows[n]["thumb_path"].ToString();
                    }
                    if (dt.Rows[n]["original_path"] != null && dt.Rows[n]["original_path"].ToString() != "")
                    {
                        model.original_path = dt.Rows[n]["original_path"].ToString();
                    }
                    if (dt.Rows[n]["remark"] != null && dt.Rows[n]["remark"].ToString() != "")
                    {
                        model.remark = dt.Rows[n]["remark"].ToString();
                    }
                    if (dt.Rows[0]["add_time"].ToString() != "")
                    {
                        model.add_time = DateTime.Parse(dt.Rows[0]["add_time"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
             }
            catch (Exception ex)
            {
                throw new Exception("--article_albumsDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(List<Model.article_albums> models, int article_id)
        {
            try
            {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = Utils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,thumb_path,original_path from dt_article_albums where article_id=" + article_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataTable _dt = _access.getDataTable(strSql.ToString());
            foreach (DataRow dr in _dt.Rows)
            {
                int rows = _access.execCommand("delete from dt_article_albums where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    Utils.DeleteFile(dr["thumb_path"].ToString()); //删除缩略图
                    Utils.DeleteFile(dr["original_path"].ToString()); //删除原图
                }
            }
                }
            catch (Exception ex)
            {
                throw new Exception("--article_albumsDal-->GetList" + ex.Message, ex);
            }
        }


        public int add(List<article_albums> ls,int id)
        {
            try
            {
            int counts = 0;
            StringBuilder strSql3;
            foreach (Model.article_albums modelt in ls)
            {
                strSql3 = new StringBuilder();
                strSql3.AppendFormat("insert into dt_article_albums(");
                strSql3.AppendFormat("article_id,thumb_path,original_path,remark)");
                strSql3.AppendFormat(" values (");
                strSql3.AppendFormat("{0},'{1}','{2}','{3}')",id,modelt.thumb_path,modelt.original_path,modelt.remark);
               int results=  _access.execCommand(strSql3.ToString());
               counts += results;
            }
            return counts;
                }
            catch (Exception ex)
            {
                throw new Exception("--article_albumsDal-->GetList" + ex.Message, ex);
            }
        }

        public int edit(List<article_albums> ls, int id)
        {
            try
            {
            int counts = 0;
            StringBuilder strSql3;
            foreach (Model.article_albums modelt in ls)
            {
                strSql3 = new StringBuilder();
                if (modelt.id > 0)
                {
                    strSql3.AppendFormat("update dt_article_albums set ");
                    strSql3.AppendFormat("article_id={0},",modelt.article_id);
                    strSql3.AppendFormat("thumb_path='{0}',",modelt.thumb_path);
                    strSql3.AppendFormat("original_path='{0}',",modelt.original_path);
                    strSql3.AppendFormat("remark='{0}'",modelt.remark);
                    strSql3.AppendFormat(" where id={0}",modelt.id);
                }
                else
                {
                    strSql3 = new StringBuilder();
                    strSql3.AppendFormat("insert into dt_article_albums(");
                    strSql3.AppendFormat("article_id,thumb_path,original_path,remark)");
                    strSql3.AppendFormat(" values (");
                    strSql3.AppendFormat("{0},'{1}','{2}','{3}')", id, modelt.thumb_path, modelt.original_path, modelt.remark);
                    int results = _access.execCommand(strSql3.ToString());
                    counts += results;
                }
            }
            return counts;    }
            catch (Exception ex)
            {
                throw new Exception("--article_albumsDal-->GetList" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 删除相册图片
        /// </summary>
        public void DeleteFile(List<Model.article_albums> models)
        {
            try
            { 
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    Utils.DeleteFile(modelt.thumb_path);
                    Utils.DeleteFile(modelt.original_path);
                }
            }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_albumsDal-->GetList" + ex.Message, ex);
            }
        }
    }
}
