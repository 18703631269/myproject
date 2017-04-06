using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Collections;
using Common;
using DBAccess;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 数据访问类:文章内容
    /// </summary>
    public partial class articleDal
    {
        private IDBAccess _access;
        //string connectionstring = Common.CommomFunction.MyAccessSqlString().Replace("sdy147258369", "sdy123456");
        string connectionstring = Common.CommomFunction.SqlServerString();
        public articleDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }
        public DataTable GetTopList(int channel_id,int top=1)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select  top {0} * FROM dt_article where category_id=0 ",top);
                if (channel_id > 0)
                {
                    strSql.Append(" and channel_id= " + channel_id);
                }
                strSql.Append(" order by sort_id asc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetTopList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据channel_id和category_id查询出文章信息
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog">多个id用逗号拼接的</param>
        /// <returns></returns>
        public DataTable GetAll(int channel_id, string strCatalog)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select a.*,b.parent_id FROM dt_article a,dt_article_category b where a.category_id in ({0}) ", strCatalog);
                if (channel_id > 0)
                {
                    strSql.Append(" and a.channel_id= " + channel_id);
                }
                strSql.Append(" and a.category_id=b.id");
                strSql.Append(" order by a.sort_id desc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetAll" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据channel_id和category_id查询出文章信息
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog">多个id用逗号拼接的</param>
        /// <returns></returns>
        public DataTable GetAll(int channel_id, string strCatalog,string strLx,string strCs,string strYy="")
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select a.*,b.parent_id FROM dt_article a,dt_article_category b where a.category_id in ({0}) ", strCatalog);
                if (channel_id > 0)
                {
                    strSql.Append(" and a.channel_id= " + channel_id);
                }
                strSql.Append(" and a.category_id=b.id");
                if (!string.IsNullOrWhiteSpace(strLx)) 
                {
                    strSql.AppendFormat(" and ','+a.fylx+',' like '%,{0},%'", strLx);
                }
                if (!string.IsNullOrWhiteSpace(strCs))
                {
                    strSql.AppendFormat(" and ','+a.city+',' like '%,{0},%'", strCs);
                }
                if (!string.IsNullOrWhiteSpace(strYy))
                {
                    strSql.AppendFormat(" and a.hospl like '%{0}%'", strYy);
                }
                strSql.Append(" order by a.sort_id desc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetAll" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 接机送机根据channel_id和category_id查询出文章信息
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog">多个id用逗号拼接的</param>
        /// <returns></returns>
        public DataTable GetJJAll(int channel_id, string strCatalog, string strCs,string strYy = "")
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select a.*,b.parent_id FROM dt_article a,dt_article_category b where a.category_id in ({0}) ", strCatalog);
                if (channel_id > 0)
                {
                    strSql.Append(" and a.channel_id= " + channel_id);
                }
                strSql.Append(" and a.category_id=b.id");
                if (!string.IsNullOrWhiteSpace(strCs))
                {
                    strSql.AppendFormat(" and a.szjc='{0}'", strCs);
                    //strSql.AppendFormat(" and ','+a.city+',' like '%,{0},%'", strCs);
                }
                if (!string.IsNullOrWhiteSpace(strYy))
                {
                   // strSql.AppendFormat(" and a.szjc like '%{0}%'", strYy);
                }
                strSql.Append(" order by a.sort_id desc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetJJAll" + ex.Message, ex);
            }
        }



        /// <summary>
        /// 住宿类型和目的城市
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog">多个id用逗号拼接的</param>
        /// <returns></returns>
        public DataTable GetZsAll(int channel_id, string strCatalog, string strLx, string strCs)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select a.*,b.parent_id FROM dt_article a,dt_article_category b where a.category_id in ({0}) ", strCatalog);
                if (channel_id > 0)
                {
                    strSql.Append(" and a.channel_id= " + channel_id);
                }
                strSql.Append(" and a.category_id=b.id");
                if (!string.IsNullOrWhiteSpace(strLx))
                {
                    strSql.AppendFormat(" and a.zslx='{0}'", strLx);
                }
                if (!string.IsNullOrWhiteSpace(strCs))
                {
                    strSql.AppendFormat(" and ','+a.city+',' like '%,{0},%'", strCs);
                }
                strSql.Append(" order by a.sort_id desc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetZsAll" + ex.Message, ex);
            }
        }




        /// <summary>
        /// 根据keyword中信息进行筛选
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog">进行筛选的字符串</param>
        /// <returns></returns>
        public DataTable GetLike(int channel_id, string strCatalog,string strKey)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select * FROM dt_article  where category_id in ({0}) and keyword like '%{1}%' ", strCatalog, strKey);
                if (channel_id > 0)
                {
                    strSql.Append(" and channel_id= " + channel_id);
                }
                strSql.Append(" order by sort_id desc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetAll" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据id查询出文章标题
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog">进行筛选的字符串</param>
        /// <returns></returns>
        public DataTable GetTitleById(string id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(id))
                {
                    strSql.AppendFormat("select id,title FROM dt_article  where id in ({0})", id);
                    strSql.Append(" order by sort_id desc ");
                    DataTable _dt = _access.getDataTable(strSql.ToString());
                    return _dt;
                }
                else 
                {
                    return null;
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetTitleById" + ex.Message, ex);
            }
        }




        /// <summary>
        /// 根据目的城市中信息进行筛选
        /// </summary>
        /// <param name="channel_id"></param>
        /// <param name="strCatalog">进行筛选的字符串</param>
        /// <returns></returns>
        public DataTable GetLikeByK(int channel_id, string strCatalog, string strKey,string strValue)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select * FROM dt_article  where category_id in ({0})", strCatalog);
                if (!string.IsNullOrWhiteSpace(strKey) && !string.IsNullOrWhiteSpace(strValue)) 
                {
                    if (string.Equals(strKey, "city"))
                    {
                        strSql.AppendFormat("and ','+{0}+',' like '%,{1},%'", strKey, strValue);
                    }
                    else 
                    {
                        strSql.AppendFormat("and {0} like '%{1}%'", strKey, strValue);
                    }
                }
                if (channel_id > 0)
                {
                    strSql.Append(" and channel_id= " + channel_id);
                }
                strSql.Append(" order by sort_id desc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetLikeByK" + ex.Message, ex);
            }
        }


        public DataTable GetTop5List(int channel_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 5 * FROM dt_article where 1=1 ");
                if (channel_id > 0)
                {
                    strSql.Append(" and channel_id= " + channel_id);
                }
                strSql.Append(" order by add_time desc,sort_id desc ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetTop5List" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int channel_id, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * FROM dt_article where 1=1 ");
                if (channel_id > 0)
                {
                    strSql.Append(" and channel_id= " + channel_id);
                }
                strSql.Append(" and " + strWhere);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                recordCount = _dt.Rows.Count;
                return _access.getDataSet(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得查询数据--不分页
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * FROM dt_article where 1=1 ");
                if (!string.IsNullOrEmpty(strWhere))
                {
                    strSql.Append(" and "+strWhere);
                }
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetListByChannel(int channel_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select * FROM dt_article where channel_id={0} order by add_time desc,sort_id desc ", channel_id);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetListByChannel" + ex.Message, ex);
            }
        }

        public DataTable GetTop50List(int channel_id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 50 A.update_time,B.real_name,A.title,A.content FROM dt_article as A left join dt_manager as B on A.user_id=B.id where A.status=1 ");
                if (channel_id > 0)
                {
                    strSql.Append(" and A.channel_id= " + channel_id);
                }
                strSql.AppendFormat(" order by A.id desc;");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetTop50List" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体--编辑时使用
        /// </summary>
        public Model.article GetModel(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 id,channel_id,category_id,title,img_url,content,sort_id,click,status,is_hot,user_id,add_time,update_time,video_url,keyword,details_one,details_two,details_three,details_four,detail_title_one,detail_title_two,detail_title_three,detail_title_four,fylx,city,hospl,zyxlzt,paymoney,zslx,mdcity,szjc,chjj");
                strSql.Append(" from dt_article");
                strSql.AppendFormat(" where id={0}", id);
                Model.article model = new Model.article();
                return _access.getSinggleObj<Model.article>(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetModel" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            try
            {
                //删除主表
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from dt_article ");
                strSql.AppendFormat(" where id={0}", id);
                int result = _access.execCommand(strSql.ToString());
                if (result > 0)
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
                throw new Exception("--articleDal-->Delete" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 返回信息标题
        /// </summary>
        public string GetTitle(int id)
        {
            try
            {
                string title = string.Empty;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 title from dt_article");
                strSql.Append(" where id=" + id);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    title = Convert.ToString(_dt.Rows[0][0]);
                }
                if (string.IsNullOrEmpty(title))
                {
                    return string.Empty;
                }
                return title;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetTitle" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回信息内容
        /// </summary>
        public string GetContent(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 content from dt_article");
                strSql.Append(" where id=" + id);
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0)
                {
                    return Convert.ToString(_dt.Rows[0][0]);
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetContent" + ex.Message, ex);
            }
        }

        public void UpdateField(int id, string strValue)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update dt_article set " + strValue);
                strSql.Append(" where id=" + id);
                _access.execCommand(strSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->UpdateField" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id as H ");
                strSql.Append(" from dt_article");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                return _access.getDataTable(strSql.ToString()).Rows.Count;
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->GetCount" + ex.Message, ex);
            }
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into dt_article(");
                strSql.Append(@"channel_id,category_id,title,img_url,[content],sort_id,click,status,is_hot,user_id,add_time,update_time,video_url,keyword,details_one,details_two,
                details_three,details_four,detail_title_one,detail_title_two,detail_title_three,detail_title_four,fylx,city,hospl,zyxlzt,paymoney,zslx,mdcity,szjc,chjj)");
                strSql.Append(" values (");
                strSql.AppendFormat("@channel_id,@category_id,@title,@img_url,@content,@sort_id,@click,@status,@is_hot,@user_id,@add_time,@update_time,@video_url,@keyword,@details_one,@details_two,@details_three,@details_four,@detail_title_one,@detail_title_two,@detail_title_three,@detail_title_four,@fylx,@city,@hospl,@zyxlzt,@paymoney,@zslx,@mdcity,@szjc,@chjj);");
                //, model.channel_id, model.category_id, model.title, model.img_url, model.content, model.sort_id, model.click, model.status, model.is_hot, model.user_id, model.add_time, model.update_time, model.video_url,model.keyword, model.detail_title_one, model.detail_title_two, model.detail_title_three, model.details_one, model.details_two, model.details_three, model.fylx, model.city, model.hospl, model.zyxlzt, model.paymoney, model.zslx, model.mdcity, model.szjc
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@channel_id",model.channel_id),
                new SqlParameter("@category_id",model.category_id),
                new SqlParameter("@title",model.title),
                new SqlParameter("@img_url",model.img_url),
                new SqlParameter("@content",model.content),
                new SqlParameter("@sort_id",model.sort_id),
                new SqlParameter("@click",model.click),
                new SqlParameter("@status",model.status),
                new SqlParameter("@is_hot",model.is_hot),
                new SqlParameter("@user_id", model.user_id),
                new SqlParameter("@add_time", model.add_time),
                new SqlParameter("@update_time", model.update_time),
                new SqlParameter("@video_url",model.video_url),
                new SqlParameter("@keyword",model.keyword),
                new SqlParameter("@detail_title_one",model.detail_title_one),
                new SqlParameter("@detail_title_two", model.detail_title_two),
                new SqlParameter("@detail_title_three",model.detail_title_three),
                 new SqlParameter("@detail_title_four",model.detail_title_four),
                new SqlParameter("@details_one",model.details_one),
                new SqlParameter("@details_two", model.details_two),
                new SqlParameter("@details_three",model.details_three),
                new SqlParameter("@details_four",model.details_four),
                new SqlParameter("@fylx", model.fylx),
                new SqlParameter("@city",model.city),
                new SqlParameter("@hospl",model.hospl),
                new SqlParameter("@zyxlzt",model.zyxlzt),
                new SqlParameter("@paymoney", model.paymoney),
                new SqlParameter("@zslx", model.zslx),
                new SqlParameter("@mdcity",model.mdcity),
                new SqlParameter("@szjc",model.szjc),
                new SqlParameter("@chjj",model.chjj)
                };

                //添加主表数据
               // int result = _access.execCommand(strSql.ToString(),paras);
               bool falg= ExecuteNonQuey(strSql.ToString(), paras);
                DataTable _dt = _access.getDataTable("SELECT max(id) from dt_article;");
                if (falg)
                {
                    return Convert.ToInt32(_dt.Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->Add" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article model)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update dt_article set ");
                strSql.Append("category_id=@category_id,");
                strSql.Append("title=@title,");
                strSql.Append("img_url=@img_url,");
                strSql.Append("[content]=@content,");
                strSql.Append("sort_id=@sort_id,");
                strSql.Append("click=@click,");
                strSql.Append("status=@status,");
                strSql.Append("is_hot=@is_hot,");
                strSql.Append("user_id=@user_id,");
                strSql.Append("update_time=@update_time,");
                strSql.Append("video_url=@video_url,");

                strSql.Append("keyword=@keyword,");
                strSql.Append("details_one=@details_one,");
                strSql.Append("details_two=@details_two,");
                strSql.Append("details_three=@details_three,");
                strSql.Append("details_four=@details_four,");

                strSql.Append("detail_title_one=@detail_title_one,");
                strSql.Append("detail_title_two=@detail_title_two,");
                strSql.Append("detail_title_three=@detail_title_three,");
                strSql.Append("detail_title_four=@detail_title_four,");


                strSql.Append("fylx=@fylx,");
                strSql.Append("city=@city,");
                strSql.Append("hospl=@hospl,");
                strSql.Append("zyxlzt=@zyxlzt,");
                strSql.Append("paymoney=@paymoney,");
                strSql.Append("zslx=@zslx,");
                strSql.Append("mdcity=@mdcity,");
                strSql.Append("szjc=@szjc,");
                strSql.Append("chjj=@chjj");
                strSql.Append(" where id=@id");

                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@category_id",model.category_id),
                new SqlParameter("@title",model.title),
                new SqlParameter("@img_url",model.img_url),
                new SqlParameter("@content",model.content),
                new SqlParameter("@sort_id",model.sort_id),
                new SqlParameter("@click",model.click),
                new SqlParameter("@status",model.status),
                new SqlParameter("@is_hot",model.is_hot),
                new SqlParameter("@user_id", model.user_id),
                new SqlParameter("@add_time", model.add_time),
                new SqlParameter("@update_time", model.update_time),
                new SqlParameter("@video_url",model.video_url),
                new SqlParameter("@keyword",model.keyword),
                new SqlParameter("@detail_title_one",model.detail_title_one),
                new SqlParameter("@detail_title_two", model.detail_title_two),
                new SqlParameter("@detail_title_three",model.detail_title_three),
                new SqlParameter("@detail_title_four",model.detail_title_four),

                new SqlParameter("@details_one",model.details_one),
                new SqlParameter("@details_two", model.details_two),
                new SqlParameter("@details_three",model.details_three),
                new SqlParameter("@details_four",model.details_four),

                new SqlParameter("@fylx", model.fylx),
                new SqlParameter("@city",model.city),
                new SqlParameter("@hospl",model.hospl),
                new SqlParameter("@zyxlzt",model.zyxlzt),
                new SqlParameter("@paymoney", model.paymoney),
                new SqlParameter("@zslx", model.zslx),
                new SqlParameter("@mdcity",model.mdcity),
                new SqlParameter("@szjc",model.szjc),
                 new SqlParameter("@chjj",model.chjj),
                new SqlParameter("@id",model.id)
                };
              //  _access.execCommand(strSql.ToString(),paras);
                return ExecuteNonQuey(strSql.ToString(), paras);
            }
            catch (Exception ex)
            {
                throw new Exception("--articleDal-->Update" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 用于执行添加和修改等方法
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private bool ExecuteNonQuey(string strSql, params SqlParameter[] paras) 
        {
            bool falg = false;
            using (SqlConnection conn = new SqlConnection(connectionstring)) 
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.Text;
                    if (paras != null) 
                    {
                        foreach(SqlParameter pas in paras)
                        {
                            cmd.Parameters.Add(pas);
                        }
                    }
                    int rows=cmd.ExecuteNonQuery();
                    if (rows > 0) 
                    {
                        cmd.Parameters.Clear();
                        falg = true;
                    }
                }
                catch (SqlException ex) 
                {
                    throw new Exception("--articleDal-->ExecuteNonQuey"+ex.Message, ex);
                }
            }
            return falg;
        }

    }
}