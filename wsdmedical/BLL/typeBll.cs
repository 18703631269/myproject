using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class typeBll
    {
        private readonly typeDal dal;

        public typeBll()
        {
            try
            {
                dal = new DAL.typeDal();
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public int Add(type model)
        {
            try
            {
                return this.dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return this.dal.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public bool Exists(int id)
        {
            try
            {
                return this.dal.Exists(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

       
        public DataSet GetList(string strWhere)
        {
            try
            {
                return this.dal.GetList(strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 筛选条件
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetByGroup(string strWhere)
        {
            try
            {
                return this.dal.GetByGroup(strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetByGroup" + ex.Message, ex);
            }
        }

        public DataSet GetList(int Top, string strWhere)
        {
            try
            {
                return this.dal.GetList(Top, strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }


        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                return this.dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }


        public type GetModel(int id)
        {
            try
            {
                return this.dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
        public bool Update(type model)
        {
            try
            {
                return this.dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
        public void UpdateField(int id, string strValue)
        {
            try
            {
                this.dal.UpdateField(id, strValue);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public string GetName(string id) 
        {
            try
            {
                return dal.GetName(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--typeBll-->GetName" + ex.Message, ex);
            }
        }

        public string GetCityName(string id)
        {
            try
            {
                return dal.GetCityName(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--typeBll-->GetCityName" + ex.Message, ex);
            }
        }

    }
}
