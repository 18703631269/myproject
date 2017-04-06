using System;
using System.Data;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    /// <summary>
    /// ����Ա��Ϣ��
    /// </summary>
    public partial class managerBll
    {
        //private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ

        private readonly DAL.managerDal dal;
        public managerBll()
        {
            dal = new DAL.managerDal();
        }

        #region ��������=============================
        /// <summary>
        /// �Ƿ���ڸü�¼
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
        /// ��ѯ�û����Ƿ����
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
        /// �����û���ȡ��Salt
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
        /// ����һ������
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
        /// ����һ������
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
        /// ɾ��һ������
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
        /// �õ�һ������ʵ��
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
        /// �����û������뷵��һ��ʵ��
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
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        public Model.manager GetModel(string user_name, string password, bool is_encrypt)
        {
            try
            {
                //���һ���Ƿ���Ҫ����
                if (is_encrypt)
                {
                    //��ȡ�ø��û��������Կ
                    string salt = dal.GetSalt(user_name);
                    if (string.IsNullOrEmpty(salt))
                    {
                        return null;
                    }
                    //�����Ľ��м������¸�ֵ
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
        /// ���ǰ��������
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
        /// ��ò�ѯ��ҳ����
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

        #region ========����========
        /// <summary> 
        /// �������� 
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

        #region ========����========
        /// <summary> 
        /// �������� 
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