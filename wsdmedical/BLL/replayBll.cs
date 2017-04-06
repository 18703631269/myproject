using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class replayBll
    {
        private readonly replayDal dal;

        public replayBll()
        {
            try
            {
                dal = new DAL.replayDal();
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
                return dal.Delete(id);
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
                dal.UpdateField(id, strValue);
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
                return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
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
                return dal.Exists(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public reply GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public bool Update(reply model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
    }
}
