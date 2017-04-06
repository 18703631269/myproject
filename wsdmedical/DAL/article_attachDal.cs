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
    public class article_attachDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public article_attachDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region Method


        public int add(List<article_attach> ls, int id)
        {
            try
            {
                int counts = 0;
                StringBuilder strSql4;
                foreach (Model.article_attach modelt in ls)
                {
                    strSql4 = new StringBuilder();
                    strSql4.AppendFormat("insert into dt_article_attach(");
                    strSql4.AppendFormat("article_id,file_name,file_path,file_size,file_ext,down_num,point)");
                    strSql4.AppendFormat(" values (");
                    strSql4.AppendFormat("{0},'{1}','{2}',{3},'{4}',{5},{6})", id, modelt.file_name, modelt.file_path, modelt.file_size, modelt.file_ext, modelt.down_num, modelt.point);

                    int results = _access.execCommand(strSql4.ToString()); //带事务
                    counts += results;
                }
                return counts;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        public int edit(List<article_attach> ls, int id)
        {
            try
            {
                int counts = 0;
                StringBuilder strSql4;
                foreach (Model.article_attach modelt in ls)
                {
                    strSql4 = new StringBuilder();
                    if (modelt.id > 0)
                    {
                        strSql4.AppendFormat("update dt_article_attach set ");
                        strSql4.AppendFormat("article_id={0},", modelt.article_id);
                        strSql4.AppendFormat("file_name='{0}',", modelt.file_name);
                        strSql4.AppendFormat("file_path='{0}',", modelt.file_path);
                        strSql4.AppendFormat("file_size={0},", modelt.file_size);
                        strSql4.AppendFormat("file_ext='{0}',", modelt.file_ext);
                        strSql4.AppendFormat("point={0}", modelt.point);
                        strSql4.AppendFormat(" where id={0}", modelt.id);
                        _access.execCommand(strSql4.ToString());
                    }
                    else
                    {
                        strSql4 = new StringBuilder();
                        strSql4.AppendFormat("insert into dt_article_attach(");
                        strSql4.AppendFormat("article_id,file_name,file_path,file_size,file_ext,down_num,point)");
                        strSql4.AppendFormat(" values (");
                        strSql4.AppendFormat("{0},'{1}','{2}',{3},'{4}',{5},{6})", id, modelt.file_name, modelt.file_path, modelt.file_size, modelt.file_ext, modelt.down_num, modelt.point);

                        int results = _access.execCommand(strSql4.ToString()); //带事务
                        counts += results;
                    }
                }
                return counts;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId()
        {
            try
            {
                string strSql = "select top 1 id from dt_article_attach order by id desc";
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
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
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
                strSql.AppendFormat("select count(1) from dt_article_attach where id={0}", id);
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
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取单个附件下载次数
        /// </summary>
        public int GetDownNum(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 down_num from dt_article_attach");
                strSql.Append(" where id=" + id);
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
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取总下载次数
        /// </summary>
        public int GetCountNum(int article_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select sum(down_num) from dt_article_attach");
                strSql.Append(" where article_id=" + article_id);
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
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
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
                strSql.Append("update dt_article_attach set " + strValue);
                strSql.Append(" where id=" + id);
                _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 查找不存在的文件并删除已删除的附件及数据
        /// </summary>
        public void DeleteList(List<Model.article_attach> models, int article_id)
        {
            try
            {
                StringBuilder idList = new StringBuilder();
                if (models != null)
                {
                    foreach (Model.article_attach modelt in models)
                    {
                        if (modelt.id > 0)
                        {
                            idList.Append(modelt.id + ",");
                        }
                    }
                }
                string id_list = Utils.DelLastChar(idList.ToString(), ",");
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,file_path from dt_article_attach where article_id=" + article_id);
                if (!string.IsNullOrEmpty(id_list))
                {
                    strSql.Append(" and id not in(" + id_list + ")");
                }
                DataTable _dt = _access.getDataTable(strSql.ToString());
                foreach (DataRow dr in _dt.Rows)
                {
                    int rows = _access.execCommand("delete from dt_article_attach where id=" + dr["id"].ToString()); //删除数据库
                    if (rows > 0)
                    {
                        Utils.DeleteFile(dr["file_path"].ToString()); //删除文件
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除附件文件
        /// </summary>
        public void DeleteFile(List<Model.article_attach> models)
        {
            try
            {
                if (models != null)
                {
                    foreach (Model.article_attach modelt in models)
                    {
                        Utils.DeleteFile(modelt.file_path);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_attach GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select top 1 id,article_id,file_name,file_path,file_size,file_ext,down_num,point,add_time from dt_article_attach  where id={0}", id);

                Model.article_attach model = new Model.article_attach();
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    if (_dt.Rows[0]["id"].ToString() != "")
                    {
                        model.id = int.Parse(_dt.Rows[0]["id"].ToString());
                    }
                    if (_dt.Rows[0]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(_dt.Rows[0]["article_id"].ToString());
                    }
                    model.file_name = _dt.Rows[0]["file_name"].ToString();
                    model.file_path = _dt.Rows[0]["file_path"].ToString();
                    if (_dt.Rows[0]["file_size"].ToString() != "")
                    {
                        model.file_size = int.Parse(_dt.Rows[0]["file_size"].ToString());
                    }
                    model.file_ext = _dt.Rows[0]["file_ext"].ToString();
                    if (_dt.Rows[0]["down_num"].ToString() != "")
                    {
                        model.down_num = int.Parse(_dt.Rows[0]["down_num"].ToString());
                    }
                    if (_dt.Rows[0]["point"].ToString() != "")
                    {
                        model.point = int.Parse(_dt.Rows[0]["point"].ToString());
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
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_attach> GetList(int article_id)
        {
            try
            {
                List<Model.article_attach> modelList = new List<Model.article_attach>();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,article_id,file_name,file_path,file_size,file_ext,down_num,point,add_time ");
                strSql.Append(" FROM dt_article_attach ");
                strSql.Append(" where article_id=" + article_id);
                DataTable dt = _access.getDataTable(strSql.ToString());

                int rowsCount = dt.Rows.Count;
                if (rowsCount > 0)
                {
                    Model.article_attach model;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        model = new Model.article_attach();
                        if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                        {
                            model.id = int.Parse(dt.Rows[n]["id"].ToString());
                        }
                        if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                        {
                            model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                        }
                        if (dt.Rows[n]["file_name"] != null && dt.Rows[n]["file_name"].ToString() != "")
                        {
                            model.file_name = dt.Rows[n]["file_name"].ToString();
                        }
                        if (dt.Rows[n]["file_path"] != null && dt.Rows[n]["file_path"].ToString() != "")
                        {
                            model.file_path = dt.Rows[n]["file_path"].ToString();
                        }
                        if (dt.Rows[n]["file_ext"] != null && dt.Rows[n]["file_ext"].ToString() != "")
                        {
                            model.file_ext = dt.Rows[n]["file_ext"].ToString();
                        }
                        if (dt.Rows[n]["file_size"] != null && dt.Rows[n]["file_size"].ToString() != "")
                        {
                            model.file_size = int.Parse(dt.Rows[n]["file_size"].ToString());
                        }
                        if (dt.Rows[n]["down_num"] != null && dt.Rows[n]["down_num"].ToString() != "")
                        {
                            model.down_num = int.Parse(dt.Rows[n]["down_num"].ToString());
                        }
                        if (dt.Rows[n]["point"] != null && dt.Rows[n]["point"].ToString() != "")
                        {
                            model.point = int.Parse(dt.Rows[n]["point"].ToString());
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
                throw new Exception("--article_attachDal-->GetList" + ex.Message, ex);
            }
        }

        #endregion
    }
}
