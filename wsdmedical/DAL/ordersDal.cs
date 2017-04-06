using Common;
using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ordersDal
    {
           private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public ordersDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }

        #region 基本方法================================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId()
        {
            string strSql = "select top 1 id from dt_order order by id desc";
            DataTable _dt = _access.getDataTable(strSql.ToString());
            if (_dt.Rows.Count>0)
            {
                return Convert.ToInt32(_dt.Rows[0]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_order");
            strSql.AppendFormat(" where id={0} ",id);
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

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_order");
            strSql.Append(" where order_no='{0}' ");
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

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string getStatue(string str) 
        {
            string strR = "";
            if (str == "0") 
            {
                strR = "等待回复";
            }
            else if (str == "1")
            {
                strR = "正在进行";
            }
            else 
            {
                strR = "已经结束";
            }
            return strR;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.orders GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,order_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,status ");
            strSql.Append(" from dt_order");
            strSql.AppendFormat(" where id={0}",id);
            Model.orders ls=_access.getSinggleObj<Model.orders>(strSql.ToString());
            return ls;
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,order_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time ");
            strSql.Append(" FROM dt_order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return _access.getDataTable(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataTable GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_order");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable _dt = _access.getDataTable(strSql.ToString());
            recordCount = _dt.Rows.Count;
            return _access.getDataTable(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from dt_order ");
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
        #endregion
    }
}
