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
        /// 增加一条数据,及其子表数据
        /// </summary>
        //public int Add(Model.orders model)
        //{
        //            try
        //            {
        //                StringBuilder strSql = new StringBuilder();
        //                strSql.Append("insert into " + databaseprefix + "orders(");
        //                strSql.Append("order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time)");
        //                strSql.Append(" values (");
        //                strSql.Append("@order_no,@trade_no,@user_id,@user_name,@payment_id,@payment_fee,@payment_status,@payment_time,@express_id,@express_no,@express_fee,@express_status,@express_time,@accept_name,@post_code,@telphone,@mobile,@email,@area,@address,@message,@remark,@is_invoice,@invoice_title,@invoice_taxes,@payable_amount,@real_amount,@order_amount,@point,@status,@add_time,@confirm_time,@complete_time)")

        //            }
        //            catch
        //            {
        //                trans.Rollback();
        //                return -1;
        //            }
        //        }
        //    }
        //    return model.id;
        //}

        /// <summary>
        /// 更新一条数据
        /// </summary>
        //public bool Update(Model.orders model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update " + databaseprefix + "orders set ");
        //    strSql.Append("order_no=@order_no,");
        //    strSql.Append("trade_no=@trade_no,");
        //    strSql.Append("user_id=@user_id,");
        //    strSql.Append("user_name=@user_name,");
        //    strSql.Append("payment_id=@payment_id,");
        //    strSql.Append("payment_fee=@payment_fee,");
        //    strSql.Append("payment_status=@payment_status,");
        //    strSql.Append("payment_time=@payment_time,");
        //    strSql.Append("express_id=@express_id,");
        //    strSql.Append("express_no=@express_no,");
        //    strSql.Append("express_fee=@express_fee,");
        //    strSql.Append("express_status=@express_status,");
        //    strSql.Append("express_time=@express_time,");
        //    strSql.Append("accept_name=@accept_name,");
        //    strSql.Append("post_code=@post_code,");
        //    strSql.Append("telphone=@telphone,");
        //    strSql.Append("mobile=@mobile,");
        //    strSql.Append("email=@email,");
        //    strSql.Append("area=@area,");
        //    strSql.Append("address=@address,");
        //    strSql.Append("message=@message,");
        //    strSql.Append("remark=@remark,");
        //    strSql.Append("is_invoice=@is_invoice,");
        //    strSql.Append("invoice_title=@invoice_title,");
        //    strSql.Append("invoice_taxes=@invoice_taxes,");
        //    strSql.Append("payable_amount=@payable_amount,");
        //    strSql.Append("real_amount=@real_amount,");
        //    strSql.Append("order_amount=@order_amount,");
        //    strSql.Append("point=@point,");
        //    strSql.Append("status=@status,");
        //    strSql.Append("add_time=@add_time,");
        //    strSql.Append("confirm_time=@confirm_time,");
        //    strSql.Append("complete_time=@complete_time");
        //    strSql.Append(" where id=@id");
        //    int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        //public Model.orders GetModel(int id)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time");
        //    strSql.Append(" from " + databaseprefix + "orders");
        //    strSql.Append(" where id=@id");
        //    OleDbParameter[] parameters = {
        //            new OleDbParameter("@id", OleDbType.Integer,4)};
        //    parameters[0].Value = id;

        //    DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return DataRowToModel(ds.Tables[0].Rows[0]);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

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
