using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Collections;
using Common;
using DBAccess;

namespace DAL
{
    /// <summary>
    /// 数据访问类:后台导航菜单
    /// </summary>
    public partial class navigationDal
    {
        //数据库表名前缀   //数据库表名前缀
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public navigationDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region 基本方法===============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId()
        {
            try
            {
                string strSql = "select top 1 id from dt_navigation order by id desc";
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
                throw new Exception("--navigationDal-->GetMaxId" + ex.Message, ex);
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
                strSql.AppendFormat("select id from dt_navigation where id={0}", id);
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
                throw new Exception("--navigationDal-->Exists" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 查询是否存在该记录
        /// </summary>
        public bool Exists(string name)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select id from dt_navigation where name='{0}';", name);
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
                throw new Exception("--navigationDal-->Exists" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int GetIdByPage(string pagename)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select id from dt_navigation where name='{0}';", pagename);
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
                throw new Exception("--navigationDal-->GetIdByPage" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 增加一条数据--已验证
        /// </summary>
        public int Add(Model.navigation model)
        {

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into dt_navigation(");
                strSql.Append("parent_id,title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys,class_layer,name)");
                strSql.Append(" values (");
                strSql.AppendFormat("{0},'{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}',{9},{10},'{11}')", model.parent_id, model.title, model.sub_title, model.icon_url, model.link_url, model.sort_id, model.is_lock, model.remark, model.action_type, model.is_sys, model.class_layer, model.name);
                _access.execCommand(strSql.ToString());
                //取得新插入的ID
                model.id = GetMaxId();
                return model.id;
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->Add" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 更新一条数据--已验证
        /// </summary>
        public bool Update(Model.navigation model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("update dt_navigation set ");
                strSql.AppendFormat("parent_id={0},", model.parent_id);
                //strSql.AppendFormat("nav_type='{0}',", model.nav_type);
                strSql.AppendFormat("[name]='{0}',", model.name);
                strSql.AppendFormat("title='{0}',", model.title);
                strSql.AppendFormat("sub_title='{0}',", model.sub_title);
                strSql.AppendFormat("icon_url='{0}',", model.icon_url);
                strSql.AppendFormat("link_url='{0}',", model.link_url);
                strSql.AppendFormat("sort_id={0},", model.sort_id);
                strSql.AppendFormat("is_lock={0},", model.is_lock);
                strSql.AppendFormat("[remark]='{0}',", model.remark);
                strSql.AppendFormat("action_type='{0}',", model.action_type);
                strSql.AppendFormat("is_sys={0}", model.is_sys);
                strSql.AppendFormat(" where id={0}", model.id);
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
                throw new Exception("--navigationDal-->Update" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除一条数据--已验证
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from dt_navigation");
                strSql.Append(" where id in(" + GetIds(id) + ")");
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
                throw new Exception("--navigationDal-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.navigation GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 id,parent_id,title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys,class_layer,channel_id,name");
                strSql.Append(" from dt_navigation ");
                strSql.AppendFormat(" where id={0}", id);

                Model.navigation model = new Model.navigation();
                DataSet ds = _access.getDataSet(strSql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToModel(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->GetModel" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 获取类别列表--已验证
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,parent_id,title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys,channel_id,name");
                strSql.Append(" FROM dt_navigation");
                strSql.Append(" order by sort_id asc,id desc");
                DataSet ds = _access.getDataSet(strSql.ToString());
                //重组列表
                DataTable oldData = ds.Tables[0] as DataTable;
                if (oldData == null)
                {
                    return null;
                }
                //创建一个新的DataTable增加一个深度字段
                DataTable newData = new DataTable();
                newData.Columns.Add("id", typeof(int));
                newData.Columns.Add("parent_id", typeof(int));
                newData.Columns.Add("class_layer", typeof(int));
                newData.Columns.Add("title", typeof(string));
                newData.Columns.Add("sub_title", typeof(string));
                newData.Columns.Add("icon_url", typeof(string));
                newData.Columns.Add("link_url", typeof(string));
                newData.Columns.Add("sort_id", typeof(int));
                newData.Columns.Add("is_lock", typeof(int));
                newData.Columns.Add("remark", typeof(string));
                newData.Columns.Add("action_type", typeof(string));
                newData.Columns.Add("channel_id", typeof(int));
                newData.Columns.Add("is_sys", typeof(int));
                newData.Columns.Add("name", typeof(string));
                //调用迭代组合成DAGATABLE
                GetChilds(oldData, newData, parent_id, 0);
                return newData;
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->GetList" + ex.Message, ex);
            }
        }
        #endregion

        #region 扩展方法===============================
        /// <summary>
        /// 获取父类下的所有子类ID(含自己本身)--如果此不包含子类则返回自己
        /// </summary>
        public string GetIds(int parent_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,parent_id,channel_id");
                strSql.Append(" FROM dt_navigation");
                DataSet ds = _access.getDataSet(strSql.ToString());
                string ids = parent_id.ToString() + ",";
                GetChildIds(ds.Tables[0], parent_id, ref ids);
                return ids.TrimEnd(',');
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->GetIds" + ex.Message, ex);
            }
        }



        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update dt_navigation set " + strValue);
                strSql.Append(" where id=" + id);
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
                throw new Exception("--navigationDal-->UpdateField" + ex.Message, ex);
            }
        }

        #region 私有方法===============================
        /// <summary>
        /// 组合成对象实体
        /// </summary>
        private Model.navigation DataRowToModel(DataRow row)
        {
            try
            {
                Model.navigation model = new Model.navigation();
                if (row != null)
                {
                    if (row["id"] != null && row["id"].ToString() != "")
                    {
                        model.id = int.Parse(row["id"].ToString());
                    }
                    if (row["parent_id"] != null && row["parent_id"].ToString() != "")
                    {
                        model.parent_id = int.Parse(row["parent_id"].ToString());
                    }
                    if (row["title"] != null)
                    {
                        model.title = row["title"].ToString();
                    }
                    if (row["sub_title"] != null)
                    {
                        model.sub_title = row["sub_title"].ToString();
                    }
                    if (row["icon_url"] != null)
                    {
                        model.icon_url = row["icon_url"].ToString();
                    }
                    if (row["link_url"] != null)
                    {
                        model.link_url = row["link_url"].ToString();
                    }
                    if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                    {
                        model.sort_id = int.Parse(row["sort_id"].ToString());
                    }
                    if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                    {
                        model.is_lock = int.Parse(row["is_lock"].ToString());
                    }
                    if (row["remark"] != null)
                    {
                        model.remark = row["remark"].ToString();
                    }
                    if (row["action_type"] != null)
                    {
                        model.action_type = row["action_type"].ToString();
                    }
                    if (row["is_sys"] != null && row["is_sys"].ToString() != "")
                    {
                        model.is_sys = int.Parse(row["is_sys"].ToString());
                    }
                    if (row["class_layer"] != null && row["class_layer"] != "")
                    {
                        model.class_layer = int.Parse(row["class_layer"].ToString());
                    }
                    if (row["name"] != null)
                    {
                        model.name = row["name"].ToString();
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->DataRowToModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int class_layer)
        {
            try
            {
                class_layer++;
                DataRow[] dr = oldData.Select("parent_id=" + parent_id);
                for (int i = 0; i < dr.Length; i++)
                {
                    //添加一行数据
                    DataRow row = newData.NewRow();
                    row["id"] = int.Parse(dr[i]["id"].ToString());
                    row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                    row["class_layer"] = class_layer;
                    row["title"] = dr[i]["title"].ToString();
                    row["sub_title"] = dr[i]["sub_title"].ToString();
                    row["icon_url"] = dr[i]["icon_url"].ToString();
                    row["link_url"] = dr[i]["link_url"].ToString();
                    row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                    row["is_lock"] = int.Parse(dr[i]["is_lock"].ToString());
                    row["remark"] = dr[i]["remark"].ToString();
                    row["action_type"] = dr[i]["action_type"].ToString();
                    row["is_sys"] = int.Parse(dr[i]["is_sys"].ToString());
                    row["channel_id"] = int.Parse(dr[i]["channel_id"].ToString());
                    row["name"] = dr[i]["name"].ToString();
                    newData.Rows.Add(row);
                    //调用自身迭代
                    this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()), class_layer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->GetChilds" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取父类下的所有子类ID
        /// </summary>
        private void GetChildIds(DataTable dt, int parent_id, ref string ids)
        {
            try
            {
                DataRow[] dr = dt.Select("parent_id=" + parent_id);
                for (int i = 0; i < dr.Length; i++)
                {
                    ids += dr[i]["id"].ToString() + ",";
                    //调用自身迭代
                    this.GetChildIds(dt, int.Parse(dr[i]["id"].ToString()), ref ids);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->GetChildIds" + ex.Message, ex);
            }
        }
        #endregion

        #region 根据频道获取到改频道下的信息
        public DataTable GetListByChannelId(int channel_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select id,parent_id,title,sub_title,icon_url,link_url,sort_id,is_lock,[remark],action_type,is_sys,channel_id,name");
                strSql.AppendFormat(" FROM dt_navigation where channel_id={0}", channel_id);
                strSql.AppendFormat(" order by sort_id asc,id desc");
                DataTable _dt= _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--navigationDal-->GetListByChannelId" + ex.Message, ex);
            }
        }
        #endregion
        #endregion
    }
}

