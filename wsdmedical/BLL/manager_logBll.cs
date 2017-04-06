using System;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace BLL
{
    /// <summary>
    /// 管理日志
    /// </summary>
    public partial class manager_logBll
    {
        // private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息

        private readonly DAL.manager_logDal dal;
        public manager_logBll()
        {
            dal = new DAL.manager_logDal();
        }

        #region 基本方法==============================
        /// <summary>
        /// 增加管理日志
        /// </summary>
        /// <param name="用户id"></param>
        /// <param name="用户名"></param>
        /// <param name="操作类型"></param>
        /// <param name="备注"></param>
        /// <returns></returns>
        public int Add(int user_id, string user_name, string action_type, string remark)
        {
            try
            {
                Model.manager_log manager_log_model = new Model.manager_log();
                manager_log_model.user_id = user_id;
                manager_log_model.user_name = user_name;
                manager_log_model.action_type = action_type;
                manager_log_model.remark = remark;
                manager_log_model.user_ip = GetIPAddress();
                return dal.Add(manager_log_model);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logBll-->Add" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager_log model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager_log GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除7天前的日志数据
        /// </summary>
        public int Delete(int dayCount)
        {
            try
            {
                return dal.Delete(dayCount);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logBll-->Delete" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            try
            {
                return dal.GetList(Top, strWhere, filedOrder);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logBll-->Edit" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            try
            {
                return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
            }
            catch (Exception ex)
            {
                throw new Exception("--manager_logBll-->Edit" + ex.Message, ex);
            }
        }

        #endregion


        public  string GetIPAddress()
        {
            try
            {
                string IP4Address = String.Empty;
                foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }
                if (IP4Address != String.Empty)
                {
                    return IP4Address;
                }

                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }
                return IP4Address;
            }
            catch (Exception ex)//获取失败
            {
                return "0.0.0.0";
            }
        }
    }
}
