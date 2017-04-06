using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class sys_configDal
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public sys_config loadConfig(string configFilePath)
        {
            return (sys_config)SerializationHelper.Load(typeof(sys_config), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public sys_config saveConifg(sys_config model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

        /// <summary>
        ///  读取配置国家信息
        /// </summary>
        public string ReadConfig()
        {
            string model = Utils.GetWebConfig("City");
            return model;
        }

    }
}
