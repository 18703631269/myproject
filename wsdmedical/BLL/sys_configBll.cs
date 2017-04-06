using Common;
using DAL;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BLL
{
    public class sys_configBll
    {
        private readonly sys_configDal dal = new sys_configDal();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public sys_config loadConfig()
        {
            sys_config model = new sys_config();
            model = dal.loadConfig(Utils.GetXmlMapPath("Configpath"));
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public sys_config saveConifg(sys_config model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath("Configpath"));
        }

        /// <summary>
        ///  读取配置国家信息
        /// </summary>
        public string ReadConfig()
        {
            string model = dal.ReadConfig();
            return model;
        }
    }
}
