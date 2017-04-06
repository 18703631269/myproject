using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Collections.Generic;
using Common;
using DBAccess;

namespace DAL
{
    /// <summary>
    /// 数据访问类:内容类别
    /// </summary>
    public partial class article_categoryDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public article_categoryDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region 基本方法================================
        /// <summary>
        /// 得到最大ID dt_article
        /// </summary>
        private int GetMaxId()
        {
            try
            {
                string strSql = "select top 1 id from  dt_article_category order by id desc";
                DataTable _dtobj = _access.getDataTable(strSql.ToString());
                if (_dtobj.Rows.Count > 0)
                {
                    return Convert.ToInt32(_dtobj.Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetMaxId" + ex.Message, ex);
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
                strSql.AppendFormat("select id from  dt_article_category where id={0};", id);

                return _access.getDataTable(strSql.ToString()).Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_category model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("insert into  dt_article_category(");
                strSql.AppendFormat("channel_id,call_index,title,sort_id,parent_id)");
                strSql.AppendFormat(" values (");
                strSql.AppendFormat("{0},'{1}','{2}',{3},{4})", model.channel_id,model.call_index, model.title, model.sort_id,model.parent_id);
                _access.execCommand(strSql.ToString());
                //取得新插入的ID
                model.id = GetMaxId();
                return model.id;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->Add" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select top 1 title from  dt_article_category where id={0};", id);
                DataTable _dtobj = _access.getDataTable(strSql.ToString());
                if (_dtobj.Rows.Count > 0)
                {
                    return Convert.ToString(_dtobj.Rows[0][0]);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetTitle" + ex.Message, ex);
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
                strSql.Append("update  dt_article_category set " + strValue);
                strSql.Append(" where id=" + id);
                _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->UpdateField" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_category model)
        {
            try
            {
                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("update  dt_article_category set call_index='{0}',title='{1}',sort_id={2},parent_id={3} where id={4}",model.call_index,model.title,model.sort_id,model.parent_id,model.id);
                int result = _access.execCommand(strsql.ToString());
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->Update" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("delete from  dt_article_category where id={0};",id);
                _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_category GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select  top 1 id,channel_id,call_index,title,sort_id,parent_id");
                strSql.AppendFormat(" from  dt_article_category ");
                strSql.AppendFormat(" where id={0}", id);
                return _access.getSinggleObj<Model.article_category>(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetList(int parent_id,int channel_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,parent_id,channel_id,title,sort_id,class_layer");
                strSql.Append(" from  dt_article_category");
                strSql.Append(" where channel_id=" + channel_id + " order by sort_id asc,id desc");
                DataTable oldData = _access.getDataTable(strSql.ToString());
                if (oldData == null)
                {
                    return null;
                }
                //复制结构
                DataTable newData = oldData.Clone();
                //调用迭代组合成DAGATABLE
                GetChilds(oldData, newData, parent_id, channel_id,0);
                return newData;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetList" + ex.Message, ex);
            }
        }
        #endregion

        /// <summary>
        /// 取得所有父类别
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetTopList(int parent_id, int channel_id,int? top=0)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (top == 0 || top == null)
                {
                    strSql.Append("select id,parent_id,channel_id,title,sort_id,class_layer");
                }
                else
                {
                    strSql.AppendFormat("select top {0} id,parent_id,channel_id,title,sort_id,class_layer",top);
                }
                strSql.Append(" from  dt_article_category");
                strSql.Append(" where parent_id=" + parent_id + " and channel_id=" + channel_id + " order by sort_id asc");//,id desc
                DataTable oldData = _access.getDataTable(strSql.ToString());
                if (oldData == null)
                {
                    return null;
                }
                return oldData;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetTopList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取该频道的栏目信息
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetChanList(int channel_id, int? top = 0)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (top == 0 || top == null)
                {
                    strSql.Append("select id,parent_id,channel_id,title,sort_id,class_layer");
                }
                else
                {
                    strSql.AppendFormat("select top {0} id,parent_id,channel_id,title,sort_id,class_layer", top);
                }
                strSql.Append(" from  dt_article_category");
                strSql.Append(" where  channel_id=" + channel_id + " order by sort_id asc");//,id desc
                DataTable oldData = _access.getDataTable(strSql.ToString());
                if (oldData == null)
                {
                    return null;
                }
                return oldData;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetChanList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取该频道的栏目信息
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetCList(int channel_id,string strTitle ,int? top = 0)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (top == 0 || top == null)
                {
                    strSql.Append("select id,parent_id,channel_id,title,sort_id,class_layer");
                }
                else
                {
                    strSql.AppendFormat("select top {0} id,parent_id,channel_id,title,sort_id,class_layer", top);
                }
                strSql.Append(" from  dt_article_category");
                strSql.Append(" where  channel_id=" + channel_id + " and title<>'" + strTitle + "' order by sort_id asc");//,id desc
                DataTable oldData = _access.getDataTable(strSql.ToString());
                if (oldData == null)
                {
                    return null;
                }
                return oldData;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetChanList" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 获取父类的title
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetParentName(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select title from dt_article_category where id=(select parent_id from dt_article_category where id={0})",id);
                DataTable dtobj = _access.getDataTable(strSql.ToString());
                if (dtobj.Rows.Count > 0)
                {
                    return Convert.ToString(dtobj.Rows[0][0]);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("--article_categoryDal-->GetParentName" + ex.Message, ex);
            }
        }

        #region 私有方法================================
        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int channel_id,int class_layer)
        {
            class_layer++;
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["channel_id"] = int.Parse(dr[i]["channel_id"].ToString());
                row["title"] = dr[i]["title"].ToString();
                row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                row["class_layer"] = class_layer;
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()), channel_id,class_layer);
            }
        }
        #endregion
    }
}