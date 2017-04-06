using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class slidelinkBll
    {
           private readonly slidelinkDal dal;

           public slidelinkBll()
        {
            try
            {
                dal = new DAL.slidelinkDal();
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
            }
        }

        public int Add(slidelink model)
        {
            try
            {
                return this.dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
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
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
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
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetList(string strWhere)
        {
            try
            {
                return this.dal.GetList(strWhere);
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
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
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
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
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
            }
        }


        public slidelink GetModel(int id)
        {
            try
            {
                return this.dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
            }
        }
        public bool Update(slidelink model)
        {
            try
            {
                return this.dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
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
                throw new Exception("--slidelinkBll-->GetList" + ex.Message, ex);
            }
        }

    }
}
