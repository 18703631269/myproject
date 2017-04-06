using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Collections;
using Common;
using DBAccess;

namespace DAL
{
    /// <summary>
    /// 数据访问类:频道
    /// </summary>
    public partial class channelDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public channelDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        public string GetChannelName(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 title from  dt_channel");
                strSql.Append(" where id=" + id);
                DataTable dtobj = _access.getDataTable(strSql.ToString());
                if (dtobj.Rows.Count > 0)
                {
                    return Convert.ToString(dtobj.Rows[0][0]);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("--channelDal-->GetChannelName" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * FROM  dt_channel ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }

                recordCount = Convert.ToInt32(_access.getDataTable(PagingHelper.CreateCountingSql(strSql.ToString())).Rows[0][0]);


                return _access.getDataSet(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));

            }
            catch (Exception ex)
            {
                throw new Exception("--channelDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.channel GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select top 1 id,name,title,sort_id,is_video,is_albums,is_attach,is_details,is_fylx,is_city,is_hospl,is_zyxlzt,is_paymoney,is_zslx,is_mdcity,is_szjc,is_yybzj from  dt_channel where id={0};", id);
                return _access.getSinggleObj<Model.channel>(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--channelDal-->GetModel" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                //删除频道表
                StringBuilder strSql3 = new StringBuilder();
                strSql3.AppendFormat("delete from dt_channel where id={0};", id);

                _access.execCommand(strSql3.ToString());
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("--channelDal-->Delete" + ex.Message, ex);
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
                strSql.Append("update dt_channel set " + strValue);
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
                throw new Exception("--channelDal-->UpdateField" + ex.Message, ex);
            }
        }
        public int Add(Model.channel model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("insert into  dt_channel(name,title,sort_id,is_video,is_albums,is_attach,is_details,[is_fylx],[is_city],[is_hospl],[is_zyxlzt],[is_paymoney],[is_zslx],[is_mdcity],[is_szjc],[is_yybzj])values ('{0}','{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15});", model.name, model.title, model.sort_id, model.is_video, model.is_albums, model.is_attach, model.is_details, model.is_fylx, model.is_city, model.is_hospl, model.is_zyxlzt, model.is_paymoney, model.is_zslx, model.is_mdcity, model.is_szjc, model.is_yybzj);
                _access.execCommand(strSql.ToString());
                //取得新插入的ID
                return model.id;
            }
            catch (Exception ex)
            {
                throw new Exception("--channelDal-->Add" + ex.Message, ex);
            }

        }

        public int Edit(Model.channel model)
        {

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("update dt_channel set title='{0}',sort_id={1},is_video={2},is_albums={3},is_attach={4},is_details={5},is_fylx={6},is_city={7},is_hospl={8},is_zyxlzt={9},is_paymoney={10},is_zslx={11},is_mdcity={12},is_szjc={13},is_yybzj={14},name='{15}' where id={16};", model.title, model.sort_id, model.is_video, model.is_albums, model.is_attach, model.is_details, model.is_fylx, model.is_city, model.is_hospl, model.is_zyxlzt, model.is_paymoney, model.is_zslx, model.is_mdcity, model.is_szjc, model.is_yybzj,model.name, model.id);
                _access.execCommand(strSql.ToString());
                //取得新插入的ID
                return model.id;
            }
            catch (Exception ex)
            {
                throw new Exception("--channelDal-->Edit" + ex.Message, ex);
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
                strSql.AppendFormat("select id from dt_navigation where name='{0}' ", name);
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
                throw new Exception("--channelDal-->Exists" + ex.Message, ex);
            }
        }

    }
}

