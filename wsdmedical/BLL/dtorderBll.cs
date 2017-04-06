using DAL;
using Model;
using System;
using System.Data;

namespace BLL
{
    public class dtorderBll
    {
        private readonly dtorderDal dal;

        public dtorderBll()
        {
            try
            {
                dal = new DAL.dtorderDal();
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->dtorderBll" + ex.Message, ex);
            }
        }

        public bool Delete(string id,string strT="0")
        {
            try
            {
                return dal.Delete(id,strT);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->Delete" + ex.Message, ex);
            }
        }
        public void UpdateField(string id, string strValue)
        {
            try
            {
                dal.UpdateField(id, strValue);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->UpdateField" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取接受人
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public int GetJsrCount(string strId)
        {
            try
            {
                return dal.GetJsrCount(strId);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->GetJsrCount" + ex.Message, ex);
            }
        }
        
        /// <summary>
        /// 修改接受人的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="strUser"></param>
        /// <param name="strPay"></param>
        public bool UpdateField(string obh, string strUser, string strPay,string stT="0")
        {
            try
            {
                return dal.UpdateField(obh, strUser, strPay, stT);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->UpdateField" + ex.Message, ex);
            }
        }
       


        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetByTypeList(string strWhere,string strF)
        {
            try
            {
                return dal.GetByTypeList(strWhere, strF);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetList(string strWhere, string strF = "", string strFv = "")
        {
            try
            {
                return dal.GetList(strWhere,strF,strFv);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取我的订单列表信息
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strF"></param>
        /// <param name="strFv"></param>
        /// <returns></returns>
        public DataTable GetOrderUP(string strUser, string strF = "", string strFv = "")
        {
            try
            {
                return dal.GetOrderUP(strUser, strF, strFv);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->GetOrderUP" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取定单的opy
        /// </summary>
        /// <param name="strObh"></param>
        /// <returns></returns>
        public string getPay(string strObh) 
        {
            try
            {
                return dal.getPay(strObh);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->getPay" + ex.Message, ex);
            }
        }

        


        /// <summary>
        /// 获取订单的状态
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="strSession"></param>
        /// <returns></returns>
         public string GetOrderState(string strId,string strSession) 
        {
            try
            {
                return dal.GetOrderState(strId, strSession);
            }
            catch (Exception ex) 
            {
                throw new Exception("--dtorderBll-->GetOrderState" + ex.Message, ex);
            }
        }


         /// <summary>
        /// 获取订单的状态
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="strSession"></param>
        /// <returns></returns>
         public string Getjsr(string strId, string strBh, string strUser)
        {
            try
            {
                return dal.Getjsr(strId, strBh, strUser);
            }
            catch (Exception ex) 
            {
                throw new Exception("--dtorderBll-->Getjsr" + ex.Message, ex);
            }
        }

       
        /// <summary>
        /// 获取支付状态
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
         public string GetOrderPay(string strId)
         {
             try
             {
                 return dal.GetOrderPay(strId);
             }
             catch (Exception ex)
             {
                 throw new Exception("--dtorderDal-->GetOrderPay" + ex.Message, ex);
             }
         }




        public bool Exists(string id)
        {
            try
            {
                return dal.Exists(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据字段判断是否存在
        /// </summary>
        /// <param name="strFileds"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(string strFileds,string id)
        {
            try
            {
                return dal.Exists(strFileds,id);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->Exists" + ex.Message, ex);
            }
        }

        public dt_order GetModel(string id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->GetModel" + ex.Message, ex);
            }
        }

        public DataTable GetUnionArt(string strId) 
        {
            try
            {
                return dal.GetUnionArt(strId);
            }
            catch (Exception ex) 
            {
                throw new Exception("--dtorderBll-->GetUnionArt" + ex.Message, ex);
            }
        }


        public DataTable GetUniArt(string strId)
        {
            try
            {
                return dal.GetUniArt(strId);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->GetUniArt" + ex.Message, ex);
            }
        }



        public bool Update(dt_order model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->Update" + ex.Message, ex);
            }
        }
        public bool Add(dt_order model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderBll-->Add" + ex.Message, ex);
            }
        }
    }
}
