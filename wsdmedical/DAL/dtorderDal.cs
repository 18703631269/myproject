using Common;
using DBAccess;
using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace DAL
{
    public class dtorderDal
    {
        private IDBAccess _access;
        
        string connectionstring = Common.CommomFunction.SqlServerString();
        public dtorderDal()
        {
            _access = DBFactory.getDBAccess(DBType.SqlServer, connectionstring);
        }
        public bool Delete(string id,string strT="0")
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("delete from dt_orders ");
                if (strT == "0")
                {
                    builder.AppendFormat(" where oids='{0}'", id);
                }
                else 
                {
                    builder.AppendFormat(" where obh='{0}'", id);
                }
                int results = _access.execCommand(builder.ToString());
                if (results > 0)
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
                throw new Exception("--dtorderDal-->Delete" + ex.Message, ex);
            }
        }
        public void UpdateField(string id, string strValue)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update dt_orders set " + strValue);
                builder.Append(" where obh='" + id + "'");
                _access.execCommand(builder.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->UpdateField" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 获取订单当前是否有接受人
        /// </summary>
        /// <param name="strId">文章的id</param>
        /// <param name="strSession">用户名</param>
        /// <returns></returns>
        public int GetJsrCount(string strId)
        {
            int rs = 0;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select ojsr from dt_orders where obh=@obh");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@obh", strId)};
                DataTable dt = ExecuteDataTable(sb.ToString(), para);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["ojsr"]) == "1") 
                        {
                            rs++;
                        }
                    }
                }
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetJsrCount" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 修改接受人状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="strValue"></param>
        public bool UpdateField(string obh, string strUser,string strPay,string strT="0")
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                if (strT == "0")
                {
                    builder.Append("update dt_orders set ojsj='" + DateTime.Now.ToString() + "',ojsr='1',ouzt=2 ");
                    builder.Append(" where obh='" + obh + "' and ouser='" + strUser + "' and opay='" + strPay + "'");
                }
                else 
                {
                    string strSql = "select * from dt_orders where obh=@obh";
                    SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@obh", obh) };
                    DataTable dt = ExecuteDataTable(strSql,paras);
                    if (dt.Rows.Count > 0) 
                    {
                        for (int i = 0; i < dt.Rows.Count; i++) 
                        {
                            if (Convert.ToString(dt.Rows[i]["opay"]) == strPay)
                            {
                                builder.Append("update dt_orders set ojsj='" + DateTime.Now.ToString() + "',ojsr='1',ouzt=2");
                                builder.Append(" where obh='" + obh + "' and ouser='" + strUser + "' and opay='" + strPay + "'");
                            }
                            else 
                            {
                                builder.Append("update dt_orders set ojsj='" +SqlDateTime.MinValue + "',ojsr='0',ouzt=1");
                                builder.Append(" where obh='" + obh + "' and ouser='" + strUser + "' and opay='" + Convert.ToString(dt.Rows[i]["opay"]) + "'");
                            }
                        }
                    }
                }
                int rows = _access.execCommand(builder.ToString());
                if (rows > 0)
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
                throw new Exception("--dtorderDal-->UpdateField" + ex.Message, ex);
            }
        }


        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
             //   builder.Append("select * FROM dt_orders");
                builder.Append("select [oids],[obh],[opay],[otype],[oyysj],[oyyjz],[oyydw],[oyybzj],[ojjlx],[omdcouty],[omdcity],[ohkgs],[ohbh],[ouser],[ouyy],[ouzt],[ojsr],[ojsj],[obz],[oal],[obl] from(select [oids],[obh],[opay],[otype],[oyysj],[oyyjz],[oyydw],[oyybzj],[ojjlx],[omdcouty],[omdcity],[ohkgs],[ohbh],[ouser],[ouyy],[ouzt],[ojsr],[ojsj],[obz],[oal],[obl],ROW_NUMBER() over(Partition by obh order by ouyy desc) as num from [wsdmedical].[dbo].[dt_orders]");
                if (strWhere.Trim() != "")
                {
                    builder.Append(" where 1=1 " + strWhere);
                }
                builder.Append(") t where t.num=1");
                DataTable _dt = _access.getDataTable(builder.ToString());
                recordCount = _dt.Rows.Count;
                return _access.getDataSet(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, builder.ToString(), filedOrder));
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetList" + ex.Message, ex);
            }
        }
        public bool Exists(string id)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select oids from dt_orders");
                builder.AppendFormat(" where oids='{0}'", id);
                DataTable _dt = _access.getDataTable(builder.ToString());
                if (_dt.Rows.Count > 0)
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
                throw new Exception("--dtorderDal-->Exists" + ex.Message, ex);
            }
        }
        /// <summary>
        /// 根据字段判断是否存在
        /// </summary>
        /// <param name="strFiled"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(string strFiled,string id)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select oids from dt_orders");
                builder.AppendFormat(" where {0}='{1}'", strFiled,id);
                DataTable _dt = _access.getDataTable(builder.ToString());
                if (_dt.Rows.Count > 0)
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
                throw new Exception("--dtorderDal-->Exists" + ex.Message, ex);
            }
        }


        public dt_order GetModel(string id)
        {
            try
            {
                dt_order feedback = new dt_order();
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("select top 1 [oids],[obh],[opay],[otype],[oyysj],[oyyjz],[oyydw],[oyybzj],[ojjlx],[omdcouty],[omdcity],[ohkgs],[ohbh],[ouser],[ouyy],[ouzt],[ojsr],[ojsj],[obz],[oal],[obl]");
                builder.AppendFormat(" from dt_orders ");
                builder.AppendFormat(" where oids='{0}'", id);

                DataSet set = _access.getDataSet(builder.ToString());
                if (set.Tables[0].Rows.Count > 0)
                {
                    if ((set.Tables[0].Rows[0]["oids"] != null) && (set.Tables[0].Rows[0]["oids"].ToString() != ""))
                    {
                        feedback.oids = new Guid(Convert.ToString(set.Tables[0].Rows[0]["oids"]));
                    }
                    if ((set.Tables[0].Rows[0]["obh"] != null) && (set.Tables[0].Rows[0]["obh"].ToString() != ""))
                    {
                        feedback.obh = Convert.ToString(set.Tables[0].Rows[0]["obh"]);
                    }
                    if ((set.Tables[0].Rows[0]["opay"] != null) && (set.Tables[0].Rows[0]["opay"].ToString() != ""))
                    {
                        feedback.opay = Convert.ToString(set.Tables[0].Rows[0]["opay"]);
                    }
                    if ((set.Tables[0].Rows[0]["otype"] != null) && (set.Tables[0].Rows[0]["otype"].ToString() != ""))
                    {
                        feedback.otype = Convert.ToString(set.Tables[0].Rows[0]["otype"]);
                    }
                    if ((set.Tables[0].Rows[0]["oyysj"] != null) && (set.Tables[0].Rows[0]["oyysj"].ToString() != ""))
                    {
                        feedback.oyysj =Convert.ToDateTime(set.Tables[0].Rows[0]["oyysj"]);
                    }
                    if ((set.Tables[0].Rows[0]["oyyjz"] != null) && (set.Tables[0].Rows[0]["oyyjz"].ToString() != ""))
                    {
                        feedback.oyyjz = Convert.ToDateTime(set.Tables[0].Rows[0]["oyyjz"]);
                    }
                    if ((set.Tables[0].Rows[0]["oyydw"] != null) && (set.Tables[0].Rows[0]["oyydw"].ToString() != ""))
                    {
                        feedback.oyydw = set.Tables[0].Rows[0]["oyydw"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["oyybzj"] != null) && (set.Tables[0].Rows[0]["oyybzj"].ToString() != ""))
                    {
                        feedback.oyybzj = set.Tables[0].Rows[0]["oyybzj"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ohkgs"] != null) && (set.Tables[0].Rows[0]["ohkgs"].ToString() != ""))
                    {
                        feedback.ohkgs = set.Tables[0].Rows[0]["ohkgs"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ojjlx"] != null) && (set.Tables[0].Rows[0]["ojjlx"].ToString() != ""))
                    {
                        feedback.ojjlx = set.Tables[0].Rows[0]["ojjlx"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["omdcouty"] != null) && (set.Tables[0].Rows[0]["omdcouty"].ToString() != ""))
                    {
                        feedback.omdcouty =Convert.ToString(set.Tables[0].Rows[0]["omdcouty"]);
                    }
                    if ((set.Tables[0].Rows[0]["omdcity"] != null) && (set.Tables[0].Rows[0]["omdcity"].ToString() != ""))
                    {
                        feedback.omdcity = set.Tables[0].Rows[0]["omdcity"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ohbh"] != null) && (set.Tables[0].Rows[0]["ohbh"].ToString() != ""))
                    {
                        feedback.ohbh = set.Tables[0].Rows[0]["ohbh"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ouser"] != null) && (set.Tables[0].Rows[0]["ouser"].ToString() != ""))
                    {
                        feedback.ouser = set.Tables[0].Rows[0]["ouser"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["ouyy"] != null) && (set.Tables[0].Rows[0]["ouyy"].ToString() != ""))
                    {
                        feedback.ouyy = DateTime.Parse(set.Tables[0].Rows[0]["ouyy"].ToString());
                    }
                    if ((set.Tables[0].Rows[0]["ouzt"] != null) && (set.Tables[0].Rows[0]["ouzt"].ToString() != ""))
                    {
                        feedback.ouzt =int.Parse(set.Tables[0].Rows[0]["ouzt"].ToString());
                    }

                    if ((set.Tables[0].Rows[0]["ojsr"] != null) && (set.Tables[0].Rows[0]["ojsr"].ToString() != ""))
                    {
                        feedback.ojsr = set.Tables[0].Rows[0]["ojsr"].ToString();
                    }

                    if ((set.Tables[0].Rows[0]["ojsj"] != null) && (set.Tables[0].Rows[0]["ojsj"].ToString() != ""))
                    {
                        feedback.ojsj =Convert.ToDateTime(set.Tables[0].Rows[0]["ojsj"]);
                    }

                    if ((set.Tables[0].Rows[0]["obz"] != null) && (set.Tables[0].Rows[0]["obz"].ToString() != ""))
                    {
                        feedback.obz = set.Tables[0].Rows[0]["obz"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["oal"] != null) && (set.Tables[0].Rows[0]["oal"].ToString() != ""))
                    {
                        feedback.oal = set.Tables[0].Rows[0]["oal"].ToString();
                    }
                    if ((set.Tables[0].Rows[0]["obl"] != null) && (set.Tables[0].Rows[0]["obl"].ToString() != ""))
                    {
                        feedback.obl = set.Tables[0].Rows[0]["obl"].ToString();
                    }

                    
                    return feedback;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetModel" + ex.Message, ex);
            }
        }

        public bool Update(dt_order model)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("update dt_orders set ");
                builder.AppendFormat("ojsr=@ojsr,");
                builder.AppendFormat("ouzt=@ouzt,");

                builder.AppendFormat(" where oids=@oids");
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@ojsr",model.ojsr),
                new SqlParameter("@ouzt",model.ouzt),
                new SqlParameter("@oids",model.oids)
                };
                bool results = ExecuteNonQuey(builder.ToString(), paras);

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->Update" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取订单的状态
        /// </summary>
        /// <param name="strId">文章的id</param>
        /// <param name="strSession">用户名</param>
        /// <returns></returns>
        public string GetOrderState(string strId, string strSession)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select ouzt from dt_orders where ouser=@ouser and opay=@opay");
                SqlParameter[] para = new SqlParameter[] {new SqlParameter("@ouser",strSession),new SqlParameter("@opay",strId) };
                DataTable dt = ExecuteDataTable(sb.ToString(), para);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["ouzt"]); 
                }
                else 
                {
                    return "";
                }
            }
            catch (Exception ex) 
            {
                throw new Exception("--dtorderDal-->GetOrderState" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取订单的状态
        /// </summary>
        /// <param name="strId">文章的id</param>
        /// <param name="strSession">用户名</param>
        /// <returns></returns>
        public string Getjsr(string strId, string strBh,string strUser)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select ojsr from dt_orders where  ouser=@ouser and opay=@opay and obh=@obh");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@ouser", strUser), new SqlParameter("@opay", strId), new SqlParameter("@obh", strBh) };
                DataTable dt = ExecuteDataTable(sb.ToString(), para);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["ojsr"]);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->Getjsr" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取订单的状态
        /// </summary>
        /// <param name="strId">文章的id</param>
        /// <param name="strSession">用户名</param>
        /// <returns></returns>
        public string GetOrderPay(string strId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select oal from dt_orders where obh=@oids");
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@oids", strId)};
                DataTable dt = ExecuteDataTable(sb.ToString(), para);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToString(dt.Rows[0]["oal"]);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetOrderPay" + ex.Message, ex);
            }
        }


        public bool Add(dt_order model)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("insert into dt_orders([oids],[obh],[opay],[otype],[ohkgs],[oyysj],[oyyjz],[oyydw],[oyybzj],[ojjlx],[omdcouty],[omdcity],[ohbh],[ouser],[ouyy],[ouzt],[ojsr],[ojsj],[obz],[oal],[obl],[ohbgs])");
                builder.Append("values(@oids,@obh,@opay,@otype,@ohkgs,@oyysj,@oyyjz,@oyydw,@oyybzj,@ojjlx,@omdcouty,@omdcity,@ohbh,@ouser,@ouyy,@ouzt,@ojsr,@ojsj,@obz,@oal,@obl,@ohbgs);");
                SqlParameter[] paras = new SqlParameter[] {
                new SqlParameter("@oids",model.oids),
                new SqlParameter("@obh",model.obh),
                new SqlParameter("@opay",model.opay),
                new SqlParameter("@otype",model.otype),
                new SqlParameter("@ohkgs",model.ohkgs),
                new SqlParameter("@oyysj",model.oyysj),
                new SqlParameter("@oyyjz",model.oyyjz),
                new SqlParameter("@oyydw",model.oyydw),
                new SqlParameter("@oyybzj",model.oyybzj),
                new SqlParameter("@ojjlx",model.ojjlx),
                new SqlParameter("@omdcouty",model.omdcouty),
                new SqlParameter("@omdcity", model.omdcity),
                new SqlParameter("@ohbh", model.ohbh),
                new SqlParameter("@ouser", model.ouser),
                new SqlParameter("@ouyy", model.ouyy),
                new SqlParameter("@ouzt", model.ouzt),
                new SqlParameter("@ojsr", model.ojsr),
                new SqlParameter("@ojsj", model.ojsj),
                new SqlParameter("@obz", model.obz),
                new SqlParameter("@oal", model.oal),
                new SqlParameter("@obl", model.obl),
                  new SqlParameter("@ohbgs", model.ohbgs)
                };
                bool results = ExecuteNonQuey(builder.ToString(), paras);

                return results;

            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->Add" + ex.Message, ex);
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
                        foreach (SqlParameter pas in paras)
                        {
                            cmd.Parameters.Add(pas);
                        }
                    }
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        cmd.Parameters.Clear();
                        falg = true;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("--dtorderDal-->ExecuteNonQuey" + ex.Message, ex);
                }
            }
            return falg;
        }
        /// <summary>
        /// 用于查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private DataTable ExecuteDataTable(string strSql, params SqlParameter[] paras)
        {
            DataTable dt=null;
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
                        foreach (SqlParameter pas in paras)
                        {
                            cmd.Parameters.Add(pas);
                        }
                    }
                    SqlDataAdapter sdata = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sdata.Fill(ds);
                    dt = ds.Tables[0];
                }
                catch (SqlException ex)
                {
                    throw new Exception("--dtorderDal-->ExecuteDataTable" + ex.Message, ex);
                }
            }
            return dt;
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetByTypeList(string strWhere,string strF)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("select distinct(" + strF + ") FROM dt_orders");
                if (strWhere.Trim() != "")
                {
                    builder.Append(" where " + strWhere);
                }
                DataTable _dt = _access.getDataTable(builder.ToString());
                return _dt;
            }
            catch (Exception ex) 
            {
                throw new Exception("--dtorderDal-->GetList" + ex.Message, ex);
            }
        }

        public DataTable GetList(string strUser,string strF="",string strFv="")
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  * FROM dt_orders where 1=1 ");
                if (!string.IsNullOrWhiteSpace(strUser))
                {
                    strSql.Append(" and ouser= '"+strUser+"'");
                }
                if (!string.IsNullOrWhiteSpace(strF) && !string.IsNullOrWhiteSpace(strFv)) 
                {
                    strSql.AppendFormat(" and {0}= '{1}'",strF,strFv);
                }
                strSql.Append(" order by ouyy desc");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetList" + ex.Message, ex);
            }
        }



        public DataTable GetOrderUP(string strUser, string strF = "", string strFv = "")
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [oids],[obh],[opay],[otype],[oyysj],[oyyjz],[oyydw],[oyybzj],[ojjlx],[omdcouty],[omdcity],[ohkgs],[ohbh],[ouser],[ouyy],[ouzt],[ojsr],[ojsj],[obz],[oal],[obl] from(select [oids],[obh],[opay],[otype],[oyysj],[oyyjz],[oyydw],[oyybzj],[ojjlx],[omdcouty],[omdcity],[ohkgs],[ohbh],[ouser],[ouyy],[ouzt],[ojsr],[ojsj],[obz],[oal],[obl],ROW_NUMBER() over(Partition by obh order by ouyy desc) as num from [wsdmedical].[dbo].[dt_orders] where 1=1 ");
                if (!string.IsNullOrWhiteSpace(strUser))
                {
                    strSql.Append(" and ouser= '" + strUser + "'");
                }
                if (!string.IsNullOrWhiteSpace(strF) && !string.IsNullOrWhiteSpace(strFv))
                {
                    strSql.AppendFormat(" and {0}= '{1}'", strF, strFv);
                }
                strSql.Append(") t where t.num=1 order by ouyy desc");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetOrderUP" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据编号或用户的id
        /// </summary>
        /// <param name="strObh"></param>
        /// <returns></returns>
        public string getPay(string strObh)
        {
            string strR="";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select opay FROM dt_orders  where obh='" + strObh + "' ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                if (_dt.Rows.Count > 0) 
                {
                    for (int i = 0; i < _dt.Rows.Count; i++) 
                    {
                        if (Convert.ToString(_dt.Rows[i]["opay"]) != "")
                        {
                            if (i == _dt.Rows.Count - 1)
                            {
                                strR += Convert.ToString(_dt.Rows[i]["opay"]);
                            }
                            else
                            {
                                strR += Convert.ToString(_dt.Rows[i]["opay"]) + ",";
                            }
                        }
                    }
                }
                return strR;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->getPay" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 联合查询订单和art之间的数据
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public DataTable GetUnionArt(string strId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select a.obh,a.otype,a.oyybzj,b.title FROM dt_orders a,dt_article b where a.opay=b.id and a.obh='" + strId + "' ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetUnionArt" + ex.Message, ex);
            }
        }


        /// <summary>
        /// 联合查询订单和art之间的数据
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public DataTable GetUniArt(string strId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * FROM dt_article  where id in(" + strId + ") ");
                DataTable _dt = _access.getDataTable(strSql.ToString());
                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception("--dtorderDal-->GetUniArt" + ex.Message, ex);
            }
        }
    }
}