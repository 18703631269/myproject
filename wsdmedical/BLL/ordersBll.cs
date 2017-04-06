using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ordersBll
    {
        private readonly DAL.ordersDal dal;

        public ordersBll()
        {
            try
            {
                dal = new DAL.ordersDal();
            }
            catch (Exception ex)
            {
                throw new Exception("--ordersBll-->ordersBll" + ex.Message, ex);
            }
        }



       

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            try
            {
                return dal.Exists(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--ordersBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            try
            {
                return dal.Exists(order_no);
            }
            catch (Exception ex) 
            {
                throw new Exception("--ordersBll-->Exists" + ex.Message, ex);
            }
           
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.orders GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string getStatue(string str) 
        {
            return dal.getStatue(str);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataTable GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
    }
}
