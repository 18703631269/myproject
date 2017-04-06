using System;
using System.Data;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 管理员信息表
    /// </summary>
    public partial class managerBll
    {
        //private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息

        private readonly DAL.managerDal dal;
        public managerBll()
        {
            dal = new DAL.managerDal();
        }

        #region 基本方法=============================
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
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            try
            {
                return dal.Exists(user_name);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            try
            {
                return dal.GetSalt(user_name);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager model)
        {
            try
            {
                return dal.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            try
            {
                return dal.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager GetModel(int id)
        {
            try
            {
                return dal.GetModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password)
        {
            try
            {
                return dal.GetModel(user_name, password);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password, bool is_encrypt)
        {
            try
            {
                //检查一下是否需要加密
                if (is_encrypt)
                {
                    //先取得该用户的随机密钥
                    string salt = dal.GetSalt(user_name);
                    if (string.IsNullOrEmpty(salt))
                    {
                        return null;
                    }
                    //把明文进行加密重新赋值
                    password = Encrypt(password, salt);
                }
                return dal.GetModel(user_name, password);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
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
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
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
                throw new Exception("--managerBll-->Exists" + ex.Message, ex);
            }
        }


        public DataTable GetGroup(int group_id)
        {
            try
            {
                return dal.GetGroup(group_id);
            }
            catch (Exception ex)
            {
                throw new Exception("--managerBll-->GetGroup" + ex.Message, ex);
            }
        }
        #endregion

        #region ========加密========
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion
    }
}