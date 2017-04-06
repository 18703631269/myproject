using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class article_albumsBll
    {
        private readonly DAL.article_albumsDal dal;
        public article_albumsBll()
        {
            try
            {

                dal = new DAL.article_albumsDal();
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
        public List<article_albums> GetList(int id)
        {
            try
            {
                return dal.GetList(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public int add(List<article_albums> ls, int id)
        {
            try
            {
                return dal.add(ls, id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public int edit(List<article_albums> ls, int id)
        {
            try
            {
                return dal.edit(ls, id);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }
    }
}
