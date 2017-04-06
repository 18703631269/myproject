using DAL;
using Model;
using System;
using System.Data;

namespace BLL
{
    public class t_userBll
    {
        private readonly t_userDal dal;

        public t_userBll()
        {
            try
            {
                dal = new DAL.t_userDal();
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->t_userBll" + ex.Message, ex);
            }
        }

        public bool Delete(string id)
        {
            try
            {
                return dal.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->Delete" + ex.Message, ex);
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
                throw new Exception("--t_userBll-->UpdateField" + ex.Message, ex);
            }
        }
        public bool UpdateFieldByUserId(string usersid, string strValue)
        {
            try
            {
                return dal.UpdateFieldByUserId(usersid, strValue);
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->UpdateFieldByUserId" + ex.Message, ex);
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
                throw new Exception("--t_userBll-->GetList" + ex.Message, ex);
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
                throw new Exception("--t_userBll-->Exists" + ex.Message, ex);
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
                throw new Exception("--t_userBll-->Exists" + ex.Message, ex);
            }
        }

        public t_user GetModel(string id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->GetModel" + ex.Message, ex);
            }
        }
        public t_user GetModelByUserId(string userid)
        {
            try
            {
                return dal.GetModelByUserId(userid);
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->GetModel" + ex.Message, ex);
            }
        }
        public bool Update(t_user model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->Update" + ex.Message, ex);
            }
        }
        public bool Add(t_user model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->Add" + ex.Message, ex);
            }
        }

        public DataTable IsUser(string strUlog, string strPwd) 
        {
            try
            {
                return dal.IsUser(strUlog,strPwd);
            }
            catch (Exception ex)
            {
                throw new Exception("--t_userBll-->IsUser" + ex.Message, ex);
            }
        }
    }
}
